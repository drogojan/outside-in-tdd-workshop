using FluentAssertions;
using OpenChat.Application.Posts;
using Xunit;

namespace OpenChat.Application.UnitTests
{
    public class LanguageServiceTests
    {
        [Theory]
        [InlineData("orange", true)]
        [InlineData("ice cream", true)]
        [InlineData("good", false)]
        public void Informs_when_the_text_contains_inappropriate_language(string text, bool expected)
        {
            var sut = new LanguageService();
            sut.IsInappropriate(text).Should().Be(expected);
        }
    }
}