using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Web.Mvc;

namespace CRUD_application_2.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        private UserController controller;

        [TestInitialize]
        public void Setup()
        {
            // Initialize UserController and any other necessary setup before each test
            controller = new UserController();
            // Assuming UserController.userlist is public for test purposes
            UserController.userlist.Clear(); // Ensure a clean state before each test
        }

        [TestMethod]
        public void Index_ReturnsViewResult_WithUserList()
        {
            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var model = result.Model as System.Collections.Generic.List<User>;
            Assert.IsNotNull(model); // Ensure model is not null
        }

        [TestMethod]
        public void Details_WithValidId_ReturnsUser()
        {
            // Arrange
            UserController.userlist.Add(new User { Id = 1, Name = "Test User", Email = "test@example.com" });

            // Act
            var result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var user = result.Model as User;
            Assert.IsNotNull(user);
            Assert.AreEqual("Test User", user.Name);
        }

        [TestMethod]
        public void Create_Post_ValidUser_AddsUserAndRedirects()
        {
            // Arrange
            var user = new User { Id = 1, Name = "New User", Email = "newuser@example.com" };

            // Act
            var result = controller.Create(user) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(UserController.userlist.Contains(user));
        }

        // Additional tests for Edit and Delete methods follow a similar pattern

        [TestCleanup]
        public void Cleanup()
        {
            // Cleanup resources if needed after each test
        }
    }
}
