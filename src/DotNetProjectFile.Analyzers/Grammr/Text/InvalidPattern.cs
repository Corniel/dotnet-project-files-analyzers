namespace Grammr.Text;

/// <summary>The exception that is thrown when invalid patterns are specified.</summary>
public class InvalidPattern : FormatException
{
    /// <summary>Initializes a new instance of the <see cref="InvalidPattern"/> class.</summary>
    public InvalidPattern() { }

    /// <summary>Initializes a new instance of the <see cref="InvalidPattern"/> class.</summary>
    /// <param name="message">
    /// error message and a reference to the inner exception that is the cause of this
    /// exception.
    /// </param>
    public InvalidPattern(string message) : base(message) { }

    /// <summary>Initializes a new instance of the <see cref="InvalidPattern"/> class.</summary>
    /// <param name="message">
    /// error message and a reference to the inner exception that is the cause of this
    /// exception.
    /// </param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception.
    /// </param>
    public InvalidPattern(string message, Exception? innerException) : base(message, innerException) { }
}
