using Grammr.Text;

namespace Grammr;

[DebuggerTypeProxy(typeof(CollectionDebugView))]
[DebuggerDisplay("Count = {Count}")]
public sealed class ResultQueue : IReadOnlyCollection<Result>
{
    private readonly List<Result> Queue = [];

    public Result Failure { get; private set; } = Result.NoMatch(TokenStream.None, null!);

    public int Count => Queue.Count;

    public Result Outcome => Count == 0 || Queue[^1].Stream.Length < Failure.Stream.Length
        ? Failure
        : Queue[^1];

    public Result Dequeue()
    {
        var index = Queue.Count - 1;
        var result = Queue[index];
        Queue.RemoveAt(index);
        return result;
    }

    public Dequeuer DequeueAll() => new(this);

    public ResultQueue Enqueue(Result result)
        => result.Success
        ? Match(result.Stream, result.Node)
        : NoMatch(result.Stream, result.Message!);

    public ResultQueue NoMatch(TokenStream stream, string? message)
    {
        if (stream.Length > Failure.Stream.Length || Failure.Message is null)
        {
            Failure = Result.NoMatch(stream, message!);
        }
        return this;
    }

    public ResultQueue Match(TokenStream stream, Syntax.TreeNode? node)
    {
        var result = Result.Successful(node, stream);

        if (Count == 0 || Queue[^1].Stream.Length < result.Stream.Length)
        {
            Queue.Add(result);
        }
        else
        {
            var search = Queue.BinarySearch(result);
            if (search < 0)
            {
                search = ~search;
            }
            if (search == Count)
            {
                Queue.Add(result);
            }
            else
            {
                Queue.Insert(search, result);
            }
        }
        return this;
    }

    public ResultQueue Clear()
    {
        Queue.Clear();
        Failure = Result.NoMatch(TokenStream.None, null);
        return this;
    }

    /// <inheritdoc />
    [Pure]
    public IEnumerator<Result> GetEnumerator() => Queue.AsEnumerable().Reverse().GetEnumerator();

    /// <inheritdoc />
    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct Dequeuer : IEnumerator<Result>, IEnumerable<Result>
    {
        private readonly ResultQueue Queue;

        internal Dequeuer(ResultQueue queue) => Queue = queue;

        public Result Current { get; private set; }

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (Queue.Count == 0) return false;
            Current = Queue.Dequeue();
            return true;
        }

        public readonly void Dispose() { /* Nothing to dispose. */ }

        public IEnumerator<Result> GetEnumerator() => this;

        IEnumerator IEnumerable.GetEnumerator() => this;

        public void Reset() => throw new NotSupportedException();
    }
}
