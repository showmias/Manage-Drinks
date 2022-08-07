using ManageDrinks.Controllers;
using ManageDrinks.Models;
using ManageDrinks.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManageDrinksTests.Controllers
{
    [TestClass]
    public class BreweriesControllerTests
    {
        private readonly Mock<IBreweryRepository> _breweryRepositoryMock;
        private readonly Mock<ILogger<BreweriesController>> _logServiceMock;

        private BreweriesController _breweriesController;

        public BreweriesControllerTests()
        {
            _breweryRepositoryMock = new Mock<IBreweryRepository>();
            _logServiceMock = new Mock<ILogger<BreweriesController>>();
            _breweriesController = new BreweriesController(_logServiceMock.Object, _breweryRepositoryMock.Object);
        }

        [TestMethod]
        public async Task GetAllBrewery_ShouldReturn_Valid_Test()
        {
            _breweryRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(It.IsAny<List<Brewery>>());

            var result = await _breweriesController.GetBreweries();

            Assert.IsNotNull(result);
            Assert.AreEqual(((ObjectResult)result.Result).StatusCode, 200);
        }

        [TestMethod]
        public async Task GetBreweryById_ShouldReturn_Valid_Test()
        {
            var beerId = 1;
            _breweryRepositoryMock.Setup(repo => repo.GetById(beerId)).Returns(Task.FromResult(GetBreweryObject()));

            var result = await _breweriesController.GetBrewery(beerId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetBreweryById_ShouldReturn_NotFound_Test()
        {
            var beerId = 1;
            _breweryRepositoryMock.Setup(repo => repo.GetById(beerId));

            var result = await _breweriesController.GetBrewery(beerId);

            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, 404);
        }

        [TestMethod]
        public async Task InsertBrewery_ShouldReturn_Valid_Test()
        {
            Brewery brewery = GetBreweryObject();
            _breweryRepositoryMock.Setup(repo => repo.InsertRecord(brewery));

            var result = await _breweriesController.PostBrewery(brewery);

            Assert.IsNotNull(result);
        }


        [TestMethod]
        public async Task UpdateBrewery_ShouldReturn_Valid_Test()
        {
            Brewery brewery = GetBreweryObject();
            _breweryRepositoryMock.Setup(repo => repo.UpdateRecord(brewery));

            var result = await _breweriesController.PutBrewery(1, brewery);

            Assert.IsNotNull(result);
        }

        private static Brewery GetBreweryObject()
        {
            return new Brewery { BreweryId = 1, Name = "Brewery1" };
        }
    }
}

