# Grammr


## Token
There are several ways to construct single tokens:

``` C#
public sealed class CustomGrammar : Grammar
{
    // End of line.
    readonly Token EndOfLine = eol();

    // Single char.
    readonly Token C = ch('C');

    // Collection of chars.
    readonly Token ABC = str("ABC");

    // Based on Predicate<char>.
    readonly Token WhiteSpace = match(char.IsWhiteSpace);

    // Based on a regular expression.
    readonly Token Digits = regex("[0-9]+");
}
```

## Tokens
Tokens can be combined:

``` C#
public sealed class CustomGrammar : Grammar
{
    // Sequence
    readonly Tokens Sequence = ch('A') & ch('B');

    // Switch
    readonly Switch = ch('A') & ch('B');

    // Zero or one.
    readonly Token Options = ch('A')[..1]; // ch('A').Option;

    // Zero or more.
    readonly Token Options = ch('A')[0..]; // ch('A').Star;

    // One or more.
    readonly Token Options = ch('A')[1..]; // ch('A').Star;

     // Repeat
    readonly Token Options = ch('A')[a..b]; // ch('A').Repeat(a, b);
}
```

## Parsers

``` C#
public sealed class CustomGrammar : Grammar
{
    readonly Parser<SomeResult> Some = 

}
```
