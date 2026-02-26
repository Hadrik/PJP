using System.Text.RegularExpressions;

namespace Lab2;

internal partial class Program
{
    private static readonly Dictionary<TokenType, Regex> TokenRegexes = new()
    {
        { TokenType.Space, RxSpace() },
        { TokenType.Comment, RxComment() },
        { TokenType.Keyword, RxKeyword() },
        { TokenType.Number , RxNumber() },
        { TokenType.Operator, RxOperator() },
        { TokenType.Delimiter, RxDelimiter() },
        { TokenType.Identifier, RxIdentifier() },
    };
    
    private static void Main()
    {
        var input = 
            """
                -2 + (245 div 3);  // note
            2 mod 3 * hello
            """;

        while (input.Length > 0)
        {
            foreach (var kvp in TokenRegexes)
            {
                var match = kvp.Value.Match(input);
                if (!match.Success)
                    continue;

                if (kvp.Key != TokenType.Comment && kvp.Key != TokenType.Space)
                    Console.WriteLine($"{kvp.Key}: {match.Value}");
                
                input = input[match.Length..];
                break;
            }
        }
    }

    [GeneratedRegex(@"^[a-zA-Z][a-zA-Z\d]*")]
    private static partial Regex RxIdentifier();
    
    [GeneratedRegex(@"^\d+")]
    private static partial Regex RxNumber();
    
    [GeneratedRegex(@"^[+\-*\/]")]
    private static partial Regex RxOperator();
    
    [GeneratedRegex(@"^[();]")]
    private static partial Regex RxDelimiter();
    
    [GeneratedRegex(@"^(div|mod)\b")]
    private static partial Regex RxKeyword();
    
    [GeneratedRegex(@"^\/\/.*")]
    private static partial Regex RxComment();
    
    [GeneratedRegex(@"^\s+")]
    private static partial Regex RxSpace();
}

internal enum TokenType
{
    Identifier,
    Number,
    Operator,
    Delimiter,
    Keyword,
    Comment,
    Space,
}