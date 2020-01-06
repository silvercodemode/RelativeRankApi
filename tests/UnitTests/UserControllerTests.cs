using Microsoft.AspNetCore.Mvc;
using Moq;
using RelativeRank.Controllers;
using RelativeRank.DataTransferObjects;
using RelativeRank.Entities;
using RelativeRank.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Xunit;

namespace RelativeRankTests.UnitTests
{
    public class UserControllerTests
    {
        private Mock<IUserService> _userServiceMock;

        public UserControllerTests() => _userServiceMock = new Mock<IUserService>();

        [Fact]
        public async Task UpdateUsersShowList_WhenPassedNullBody_ReturnsStatus400Response()
        {
            // Arrange
            var userController = new UserController(_userServiceMock.Object);

            // Act
            var response = await userController.UpdateUsersShowList(null);

            // Assert
            var badResponse = Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, badResponse.StatusCode);
        }

        [Fact]
        public async Task UpdateUsersShowList_WhenPassedShowListWith_MultipleShowsOfTheSameRank_ReturnsStatus400Response()
        {
            // Arrange
            var userController = new UserController(_userServiceMock.Object);
            var rankedList = new List<RankedShow>();
            rankedList.Add(new RankedShow
            {
                Name = "show1",
                Rank = 1,
            });
            rankedList.Add(new RankedShow
            {
                Name = "show2",
                Rank = 1,
            });

            // Act
            var response = await userController.UpdateUsersShowList(new UpdateUserShowListModel
            {
                Username = "user",
                ShowList = rankedList
            });

            // Assert
            var badResponse = Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, badResponse.StatusCode);
        }
    }
}
