using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using OpenChat.Application.Posts;
using OpenChat.Application.Wall;
using OpenChat.Domain.Entities;
using OpenChat.Test.Infrastructure;
using OpenChat.Test.Infrastructure.Builders;
using Xunit;

namespace OpenChat.Application.UnitTests
{
    public class WallServiceTests
    {
        [Fact]
        public void Return_the_wall_posts()
        {
            Mock<IPostRepository> postRepositoryStub = new Mock<IPostRepository>();
            Guid USER_ID = Guid.NewGuid();
            Guid POST_ID = Guid.NewGuid();
            string POST_TEXT = "text";
            DateTime POST_DATE_TIME = new DateTime(2020, 2, 25, 10, 15, 5);
            string FORMATTED_DATE_TIME = "2020-02-25T10:15:05Z";
            Post POST = PostBuilder.APost()
                            .WithId(POST_ID)
                            .WithUserId(USER_ID)
                            .WithText(POST_TEXT)
                            .WithDateTime(POST_DATE_TIME).Build();
            IEnumerable<Post> POSTS = new List<Post> { POST };
            postRepositoryStub.Setup(m => m.WallPostsFor(USER_ID)).Returns(POSTS);

            var sut = new WallService(postRepositoryStub.Object);

            var posts = sut.GetWallPosts(USER_ID);

            posts.Should().HaveCount(1);
            var post = posts.Single();
            post.PostId.Should().Be(POST_ID);
            post.UserId.Should().Be(USER_ID);
            post.Text.Should().Be(POST_TEXT);
            post.DateTime.Should().Be(FORMATTED_DATE_TIME);
        }
    }
}