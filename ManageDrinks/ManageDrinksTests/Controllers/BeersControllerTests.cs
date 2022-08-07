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
    public class BeersControllerTests
    {
        private readonly Mock<IBeerRepository> _beerRepositoryMock;
        private readonly Mock<ILogger<BeersController>> _logServiceMock;

        private BeersController _beersController;

        public BeersControllerTests()
        {
            _beerRepositoryMock = new Mock<IBeerRepository>();
            _logServiceMock = new Mock<ILogger<BeersController>>();
            _beersController = new BeersController(_logServiceMock.Object, _beerRepositoryMock.Object);
        }

        [TestMethod]
        public async Task GetAllBeer_ShouldReturn_Valid_Test()
        {
            _beerRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(It.IsAny<List<Beer>>());

            var result = await _beersController.GetBeers();

            Assert.IsNotNull(result);
            Assert.AreEqual(((ObjectResult)result.Result).StatusCode, 200);
        }

        [TestMethod]
        public async Task GetBeerById_ShouldReturn_Valid_Test()
        {
            var beerId = 1;
            _beerRepositoryMock.Setup(repo => repo.GetById(beerId)).Returns(Task.FromResult(GetBeerObject()));

            var result = await _beersController.GetBeer(beerId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetBeerById_ShouldReturn_NotFound_Test()
        {
            var beerId = 1;
            _beerRepositoryMock.Setup(repo => repo.GetById(beerId));

            var result = await _beersController.GetBeer(beerId);

            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, 404);
        }

        [TestMethod]
        public async Task InsertBeer_ShouldReturn_Valid_Test()
        {
            Beer beer = GetBeerObject();
            _beerRepositoryMock.Setup(repo => repo.InsertRecord(beer));

            var result = await _beersController.PostBeer(beer);

            Assert.IsNotNull(result);
        }


        [TestMethod]
        public async Task UpdateBeer_ShouldReturn_Valid_Test()
        {
            Beer beer = GetBeerObject();
            _beerRepositoryMock.Setup(repo => repo.UpdateRecord(beer));

            var result = await _beersController.PutBeer(1, beer);

            Assert.IsNotNull(result);
        }

        private static Beer GetBeerObject()
        {
            return new Beer { BeerId = 1, Name = "Beer1", PercentageAlcoholByVolume = 12 };
        }
    }
}
