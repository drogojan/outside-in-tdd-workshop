using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OpenChat.API.Controllers;
using OpenChat.Application.Common;
using OpenChat.Application.Posts;
using Xunit;

namespace OpenChat.Presentation.UnitTests
{
    public class PostsControllerTests
    {
        private static readonly Guid USER_ID = Guid.Parse("04cec3f7-87fa-49b2-80a5-a08f0c7e02e7");
        private static readonly Guid POST_ID = Guid.Parse("86822512-9a65-44cd-b59b-37dafdb34c1d");
        private static readonly string DATE_TIME = "2020-02-25T09:30:15Z";
        private static readonly string POST_TEXT = "Feeling good today!";
        private readonly NewPost NEW_POST = new NewPost { Text = POST_TEXT };
        private static readonly PostApiModel POST = new PostApiModel { UserId = USER_ID, PostId = POST_ID, Text = POST_TEXT, DateTime = DATE_TIME };
        private readonly IEnumerable<PostApiModel> POSTS = new List<PostApiModel> { POST };

        [Fact]
        public void Create_a_post()
        {
            Mock<IPostService> postServiceMock = new Mock<IPostService>();
            var sut = new PostsController(postServiceMock.Object);

            sut.Create(USER_ID, NEW_POST);

            postServiceMock.Verify(m => m.CreatePost(USER_ID, POST_TEXT), Times.Once);
        }

        [Fact]
        public void Return_the_newly_created_post()
        {
            Mock<IPostService> postServiceStub = new Mock<IPostService>();
            postServiceStub.Setup(m => m.CreatePost(USER_ID, POST_TEXT)).Returns(POST);

            var sut = new PostsController(postServiceStub.Object);
            var actionResult = sut.Create(USER_ID, NEW_POST);

            var createdResult = actionResult as CreatedResult;
            createdResult.Should().NotBeNull();

            var createdPost = createdResult.Value as PostApiModel;
            createdPost.Should().NotBeNull();

            createdPost.UserId.Should().Be(USER_ID);
            createdPost.PostId.Should().Be(POST_ID);
            createdPost.Text.Should().Be(POST_TEXT);
            createdPost.DateTime.Should().Be(DATE_TIME);
        }

        [Fact]
        public void Return_an_error_when_creating_a_post_with_inappropriate_language()
        {
            Mock<IPostService> postServiceStub = new Mock<IPostService>();
            postServiceStub.Setup(m => m.CreatePost(USER_ID, POST_TEXT)).Throws<InappropriateLanguageException>();

            var sut = new PostsController(postServiceStub.Object);
            var actionResult = sut.Create(USER_ID, NEW_POST);

            var badRequestResult = actionResult as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();

            var apiError = badRequestResult.Value as ApiError;
            apiError.Should().NotBeNull();
            apiError.Message.Should().Be("Post contains inappropriate language");
        }

        [Fact]
        public void Return_the_posts_for_a_given_user()
        {
            Mock<IPostService> postServiceStub = new Mock<IPostService>();
            postServiceStub.Setup(m => m.PostsBy(USER_ID)).Returns(POSTS);

            var sut = new PostsController(postServiceStub.Object);
            var actionResult = sut.PostsBy(USER_ID);

            actionResult.Should().BeOfType<ActionResult<IEnumerable<PostApiModel>>>();
            var postByUser = actionResult.Value;

            postByUser.Should().BeEquivalentTo(POSTS);
        }
    }
}