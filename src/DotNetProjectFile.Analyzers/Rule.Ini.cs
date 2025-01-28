#pragma warning disable SA1118 // Parameter should not span multiple lines: readability for descriptions.

namespace DotNetProjectFile;

public static partial class Rule
{
    public static class Ini
    {
        public static DiagnosticDescriptor SyntaxError => New(
           id: 4000,
           title: "Invalid INI file",
           message: "{0}.",
           description: "Syntax Error",
           tags: ["INI", "syntax error"],
           category: Category.SyntaxError,
           isEnabled: false);

        public static DiagnosticDescriptor InvalidHeader => New(
           id: 4001,
           title: "Invalid INI Header",
           message: "{0}",
           description: "A INI header should have the format [<Header>].",
           tags: ["INI", "syntax error"],
           category: Category.SyntaxError,
           isEnabled: false);

        public static DiagnosticDescriptor InvalidKeyValuePair => New(
           id: 4002,
           title: "Invalid INI key-value pair",
           message: "{0}",
           description: "A INI key-value pair should have the format <Key> ( : | = ) <Value>.",
           tags: ["INI", "syntax error"],
           category: Category.SyntaxError,
           isEnabled: false);

        public static DiagnosticDescriptor EmptySection => New(
            id: 4010,
            title: "Sections should contain at least one key-value pair",
            message: "Section [{0}] is empty.",
            description:
                "A Section in INI file groups key-value pairs. Having an empty " +
                "section has no added value.",
            tags: ["INI", "noise"],
            category: Category.Noise,
            isEnabled: false);

        public static DiagnosticDescriptor HeaderMustBeGlob => New(
            id: 4050,
            title: "Header must be a GLOB",
            message: "Header [{0}] is not a valid GLOB.",
            description:
                ".editorconfig files work on the premise that header texts are " +
                "GLOB's matching files the key-value pairs of the section apply " +
                "to. Therefor, they must be valid GLOBs.",
            tags: [".editorconfig", "GLOB"],
            category: Category.SyntaxError,
            isEnabled: false);

        public static DiagnosticDescriptor UseEqualsAssign => New(
            id: 4051,
            title: "Use equals sign for key-value assignments",
            message: "Use = instead.",
            description: "In .editorconfig files instead of : use = as assignment sign.",
            tags: ["INI", ".editorconfig"],
            category: Category.Clarity,
            isEnabled: false);
    }
}
