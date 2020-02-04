using System;
using FluentAssertions;
using Moq;
using OpenChat.Application.Posts;
using OpenChat.Common;
using OpenChat.Domain.Entities;
using Xunit;

namespace OpenChat.Application.UnitTests
{
    public class PostServiceTests
    {
        private readonly Guid POST_ID = Guid.Parse("86822512-9a65-44cd-b59b-37dafdb34c1d");
        private readonly Guid USER_ID = Guid.Parse("04cec3f7-87fa-49b2-80a5-a08f0c7e02e7");
        private readonly string POST_TEXT = "New post text";
        private readonly DateTime DATE_TIME = new DateTime(2020, 2, 25, 9, 30, 15);
        private readonly string FORMATTED_DATE_TIME = "2020-02-25T09:30:15Z";

        [Fact]
        public void Create_a_post()
        {
            ILanguageService languageServiceStub = Mock.Of<ILanguageService>(m => m.IsInappropriate(POST_TEXT) == false);
            IGuidGenerator guidGeneratorStub = Mock.Of<IGuidGenerator>(g => g.Next() == POST_ID);
            var dateTimeStub = Mock.Of<IDateTime>(m => m.Now == DATE_TIME);
            Mock<IPostRepository> postRepositoryMock = new Mock<IPostRepository>();

            var sut = new PostService(languageServiceStub, guidGeneratorStub, dateTimeStub, postRepositoryMock.Object);
            PostApiModel createdPost = sut.CreatePost(USER_ID, POST_TEXT);

            postRepositoryMock.Verify(m => m.Add(
                It.Is<Post>(
                    p =>
                        p.Id == POST_ID
                        && p.UserId == USER_ID
                        && p.Text == POST_TEXT
                        && p.DateTime == DATE_TIME
                )
            ));

            createdPost.PostId.Should().Be(POST_ID);
            createdPost.UserId.Should().Be(USER_ID);
            createdPost.Text.Should().Be(POST_TEXT);
            createdPost.DateTime.Should().Be(FORMATTED_DATE_TIME);
        }

        [Fact]
        public void Throws_InappropriateLanguageException_when_creating_a_post_with_inappropriate_language()
        {
            ILanguageService languageServiceStub = Mock.Of<ILanguageService>(m => m.IsInappropriate(POST_TEXT) == true);
            IGuidGenerator guidGeneratorDummy = Mock.Of<IGuidGenerator>();
            IDateTime dateTimeDummy = Mock.Of<IDateTime>();
            IPostRepository postRepositoryDummy = Mock.Of<IPostRepository>();

            var sut = new PostService(languageServiceStub, guidGeneratorDummy, dateTimeDummy, postRepositoryDummy);

            Action action = () => sut.CreatePost(USER_ID, POST_TEXT);

            action.Should().Throw<InappropriateLanguageException>().WithMessage("Post contains inappropriate language");
        }
    }
}