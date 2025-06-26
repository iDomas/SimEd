using SimEd.Models.Languages.CurlyBasedLanguages;
using Shouldly;

namespace SimEd.Test.Lexer;

public class LexerShould
{
    [Theory]
    [InlineData("\"\\\\s+\");", 6)]
    [InlineData("\"o object\\n\"));", 12)]
    public void MatchStrings(string input, int expectedLength)
    {
        var actualMatchLength = CurlyLexerRules.StringMatch(input.ToCharArray());
        actualMatchLength.ShouldBe(expectedLength);
    }
}