using Antlr4;

namespace DotNetProjectFile.Ini;

public sealed class KeyValuePairSyntax(
    KeySyntax key,
    ValueSyntax value,
    IniParser.KeyValuePairContext context,
    AbstractSyntaxTree tree) : IniSyntax(context, tree)
{
    public KeySyntax Key { get; } = key;

    public ValueSyntax Value { get; } = value;

    public KeyValuePair<string, string> Pair => new KeyValuePair<string, string>(Key.Text, Value.Text);
}
