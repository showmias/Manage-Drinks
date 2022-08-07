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
    public class BarBeersControllerTests
    {
        private readonly Mock<IBarBeersRepository> _barBeersRepositoryMock;
        private readonly Mock<ILogger<BarBeersController>> _logServiceMock;

        private BarBeersController _barBeersController;

        public BarBeersControllerTests()
        {
            _barBeersRepositoryMock = new Mock<IBarBeersRepository>();
            _logServiceMock = new Mock<ILogger<BarBeersController>>();
            _barBeersController = new BarBeersController(_logServiceMock.Object, _barBeersRepositoryMock.Object);
        }

        [TestMethod]
        public async Task GetAllBarBeers_ShouldReturn_Valid_Test()
        {
            _barBeersRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(GetBarBeersObject());

            var result = await _barBeersController.GetAllBarBeers();

            Assert.IsNotNull(result);
            Assert.AreEqual(((ObjectResult)result.Result).StatusCode, 200);
        }

        [TestMethod]
        public async Task GetBarBeersById_ShouldReturn_Valid_Test()
        {
            var barId = 1;
            _barBeersRepositoryMock.Setup(repo => repo.GetBarBeersById(barId)).Returns(Task.FromResult(GetBarBeersObject()));

            var result = await _barBeersController.GetBarBeers(barId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetBarBeersById_ShouldReturn_NotFound_Test()
        {
            var beerId = 1;
            _barBeersRepositoryMock.Setup(repo => repo.GetBarBeersById(beerId));

            var result = await _barBeersController.GetBarBeers(beerId);

            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, 404);
        }

        [TestMethod]
        public async Task GetBarBeersById_ShouldReturn_BadRequest_Test()
        {
            var beerId = 0;
            _barBeersRepositoryMock.Setup(repo => repo.GetBarBeersById(beerId));

            var result = await _barBeersController.GetBarBeers(beerId);

            Assert.AreEqual(((BadRequestResult)result.Result).StatusCode, 400);
        }

        [TestMethod]
        public async Task InsertBarBeers_ShouldReturn_Valid_Test()
        {
            BarBeers barBeers = GetBarBeers();
            _barBeersRepositoryMock.Setup(repo => repo.InsertRecord(barBeers));

            var result = await _barBeersController.PostBarBeers(barBeers);

            Assert.IsNotNull(result);
        }

        private static BarBeers GetBarBeers()
        {
            return new BarBeers { BarBeerId = 1, BarId = 1, BeerId = 1 };
        }

        private static IEnumerable<BarBeers> GetBarBeersObject()
        {
            var barBeers = new List<BarBeers>();
            barBeers.Add(new BarBeers { BarBeerId = 1, BarId = 1, BeerId = 1 });
            return barBeers;
        }
    }
}
