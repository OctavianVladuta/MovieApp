using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using roweb.API.Controllers;
using roweb.Domain.Dto;
using roweb.Domain.Models;
using RowebInterview.Services.Comments;
using Xunit;

namespace roweb.Tests.Controllers
{
    public class CommentsControllerTests
    {
        [Fact]
        public async Task GetComments_ReturnsOk_WithComments()
        {
            var movieId = 123;
            var mockService = new Mock<ICommentsService>();
            var comments = new List<Comment>
            {
                new Comment { Id = 1, MovieId = movieId, UserName = "User1", Text = "Great movie!", CreatedAt = DateTime.UtcNow }
            };
            mockService.Setup(s => s.GetCommentsByMovieIdAsync(movieId))
                .ReturnsAsync(comments);
            var controller = new CommentsController(mockService.Object);

            var result = await controller.GetComments(movieId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnComments = Assert.IsAssignableFrom<IEnumerable<Comment>>(okResult.Value);
            Assert.Single(returnComments);
        }


        [Fact]
        public async Task PostComment_ReturnsOk_WhenModelIsValid()
        {

            var mockService = new Mock<ICommentsService>();
            var comment = new CreateCommentDto { MovieId = 123, Text = "Great movie!" };

            mockService.Setup(s => s.AddCommentAsync(It.IsAny<Comment>()))
                .Returns(Task.CompletedTask);
            var controller = new CommentsController(mockService.Object);

            var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, "TestUser")
            };
            var identity = new System.Security.Claims.ClaimsIdentity(claims, "TestAuthType");
            var principal = new System.Security.Claims.ClaimsPrincipal(identity);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };


            var result = await controller.PostComment(comment);


            var okResult = Assert.IsType<OkObjectResult>(result);


            var propertyInfo = okResult.Value.GetType().GetProperty("message");
            Assert.NotNull(propertyInfo);
            var messageValue = propertyInfo.GetValue(okResult.Value)?.ToString();
            Assert.Equal("TestUser", messageValue);
        }
    }
}
