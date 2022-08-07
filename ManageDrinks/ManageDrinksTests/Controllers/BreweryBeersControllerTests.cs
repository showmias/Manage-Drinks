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
    public class BreweryBeersControllerTests
    {
        private readonly Mock<IBreweryBeersRepository> _breweryBeersRepositoryMock;
        private readonly Mock<ILogger<BreweryBeersController>> _logServiceMock;

        private BreweryBeersController _breweryBeersController;

        public BreweryBeersControllerTests()
        {
            _breweryBeersRepositoryMock = new Mock<IBreweryBeersRepository>();
            _logServiceMock = new Mock<ILogger<BreweryBeersController>>();
            _breweryBeersController = new BreweryBeersController(_logServiceMock.Object, _breweryBeersRepositoryMock.Object);
        }

        [TestMethod]
        public async Task GetAllBreweryBeers_ShouldReturn_Valid_Test()
        {
            _breweryBeersRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(GetBreweryBeersObject());

            var result = await _breweryBeersController.GetAllBreweryBeers();

            Assert.IsNotNull(result);
            Assert.AreEqual(((ObjectResult)result.Result).StatusCode, 200);
        }

        [TestMethod]
        public async Task GetBreweryBeersById_ShouldReturn_Valid_Test()
        {
            var BreweryId = 1;
            _breweryBeersRepositoryMock.Setup(repo => repo.GetBreweryBeersById(BreweryId)).Returns(Task.FromResult(GetBreweryBeersObject()));

            var result = await _breweryBeersController.GetBreweryBeers(BreweryId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetBreweryBeersById_ShouldReturn_NotFound_Test()
        {
            var beerId = 1;
            _breweryBeersRepositoryMock.Setup(repo => repo.GetBreweryBeersById(beerId));

            var result = await _breweryBeersController.GetBreweryBeers(beerId);

            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, 404);
        }

        [TestMethod]
        public async Task InsertBreweryBeers_ShouldReturn_Valid_Test()
        {
            BreweryBeers BreweryBeers = GetBreweryBeers();
            _breweryBeersRepositoryMock.Setup(repo => repo.InsertRecord(BreweryBeers));

            var result = await _breweryBeersController.PostBreweryBeers(BreweryBeers);

            Assert.IsNotNull(result);
        }

        private static BreweryBeers GetBreweryBeers()
        {
            return new BreweryBeers { BreweryBeerId = 1, BreweryId = 1, BeerId = 1 };
        }

        private static IEnumerable<BreweryBeers> GetBreweryBeersObject()
        {
            var BreweryBeers = new List<BreweryBeers>();
            BreweryBeers.Add(new BreweryBeers { BreweryBeerId = 1, BreweryId = 1, BeerId = 1 });
            return BreweryBeers;
        }
    }
}
