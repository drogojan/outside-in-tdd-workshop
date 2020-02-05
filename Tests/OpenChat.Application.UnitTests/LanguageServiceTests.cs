using FluentAssertions;
using OpenChat.Application.Posts;
using Xunit;

namespace OpenChat.Application.UnitTests
{
    public class LanguageServiceTests
    {
        [Theory]
        [InlineData("", false)]
        [InlineData("text", false)]
        [InlineData("ice cream", true)]
        [InlineData("orange", true)]
        [InlineData("elephant", true)]
        [InlineData("Ice cream", true)]
        [InlineData("Ice CrEaM", true)]
        [InlineData("ELEPHANT", true)]
        [InlineData("oranGE", true)]
        [InlineData("orange ice cream for an elephant", true)]
        public void Informs_when_the_text_contains_inappropriate_language(string text, bool expected)
        {
            var sut = new LanguageService();
            sut.IsInappropriate(text).Should().Be(expected);
        }
    }
}