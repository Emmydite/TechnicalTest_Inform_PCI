using Inform.BLL.Models;
using Inform.BLL.Services;
using Inform.DAL.Entities;
using Inform.DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechnicalTest_Inform.Controllers;

namespace Inform.Test
{
    public class ContactsUnitTest
    {
        private readonly Mock<IRepository<Contact>> _mockRepo;

        private readonly ContactService _contactService;
        private readonly HomeController _controller;

        public ContactsUnitTest()
        {

            _mockRepo = new Mock<IRepository<Contact>>();

            _contactService = new ContactService(_mockRepo.Object);

            _controller = new HomeController(_contactService);
        }

        [Fact]
        public async Task Index_ActionExecutes_ReturnsViewForIndex()
        {
            var result = await _controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Test_Index_ReturnsViewResult_WithContactList()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetAllAsync())
                 .ReturnsAsync(GetTestContacts().AsQueryable());

            //Act
            var result = await _controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<ContactDTO>>(viewResult.ViewData.Model);

            Assert.Equal(3, model.Count());
        }

        [Fact]
        public async Task AddContact_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(GetTestContacts().AsQueryable());

            _controller.ModelState.AddModelError("SessionName", "Required");

            var contactRec = new ContactDTO();

            // Act
            var result = await _controller.AddContact(contactRec);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task AddContact_ReturnsARedirect_WhenModelStateIsValid()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Contact>()))
                      .ReturnsAsync(true)
                      .Verifiable();

            var contactRec = new ContactDTO()
            {
                FirstName = "Janet",
                LastName = "Smith",
                Email = "emmy@gmail.com"
            };

            // Act
            var result = await _controller.AddContact(contactRec);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockRepo.Verify();
        }


        //dummy test records
        private List<Contact> GetTestContacts()
        {
            var contacts = new List<Contact>();

            contacts.Add(new Contact()
            {
                Id = 1,
                FirstName = "Janet",
                LastName = "Smith",
                Email = "emmy@gmail.com"
            });
            contacts.Add(new Contact()
            {
                Id = 2,
                FirstName = "Frank",
                LastName = "Bloswick",
                Email = "emmy2@gmail.com"
            });
            contacts.Add(new Contact()
            {
                Id = 3,
                FirstName = "Tonya",
                LastName = "Bazinaw",
                Email = "emmy3@gmail.com"
            });

            return contacts;
        }
    }
}