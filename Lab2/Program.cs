using System.Text.RegularExpressions;

namespace Lab2;

internal class Program
{
    private static void Main(string[] args)
    {
        var input = 
            """
                -2 + (245 div 3);  // note
            2 mod 3 * hello
            """;

        input = input.Trim();
        input += '\n';
        while (input.Length > 0)
        {
            var index = 1;
            while (true)
            {
                var token = input[..index];
                if (TryMatchToken(token))
                {
                    input = input[index..].TrimStart();
                    break;
                }
                index++;
                
                if (index > input.Length)
                    return;
            }
        }
    }

    private static bool TryMatchToken(string token)
    {
        Dictionary<TokenType, string> regexes = new()
        {
            { TokenType.Keyword, @"(div|mod) " },
            { TokenType.Number, @"\d+ " },
            { TokenType.Operator, @"^[+*\/] |-" },
            { TokenType.Delimiter, @"[();]" },
            { TokenType.Identifier, @"[a-zA-Z][a-zA-Z\d]*( |\n)" },
            { TokenType.Comment, @"\/\/.*\n" },
        };

        foreach (var kvp in regexes)
        {
            var regex = new Regex(kvp.Value);
            var match = regex.Match(token);
            if (!match.Success)
                continue;

            if (kvp.Key != TokenType.Comment)
                Console.WriteLine($"{kvp.Key}: {match.Value}");
            return true;
        }

        return false;
    }
}

internal enum TokenType
{
    Identifier,
    Number,
    Operator,
    Delimiter,
    Keyword,
    Comment,
}