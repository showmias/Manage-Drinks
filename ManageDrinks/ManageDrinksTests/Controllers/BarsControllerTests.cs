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
    public class BarsControllerTest
    {
        private readonly Mock<IBarRepository> _barRepositoryMock;
        private readonly Mock<ILogger<BarsController>> _logServiceMock;

        private BarsController _barsController;

        public BarsControllerTest()
        {
            _barRepositoryMock = new Mock<IBarRepository>();
            _logServiceMock = new Mock<ILogger<BarsController>>();
            _barsController = new BarsController(_logServiceMock.Object, _barRepositoryMock.Object);
        }

        [TestMethod]
        public async Task GetAllBar_ShouldReturn_Valid_Test()
        {
            _barRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(It.IsAny<List<Bar>>());

            var result = await _barsController.GetBars();

            Assert.IsNotNull(result);
            Assert.AreEqual(((ObjectResult)result.Result).StatusCode, 200);
        }

        [TestMethod]
        public async Task GetBarById_ShouldReturn_Valid_Test()
        {
            var barId = 1;
            _barRepositoryMock.Setup(repo => repo.GetById(barId)).Returns(Task.FromResult(GetBarObject()));

            var result = await _barsController.GetBar(barId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetBarById_ShouldReturn_NotFound_Test()
        {
            var barId = 1;
            _barRepositoryMock.Setup(repo => repo.GetById(barId));

            var result = await _barsController.GetBar(barId);

            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, 404);
        }

        [TestMethod]
        public async Task GetBarById_ShouldReturn_BadRequest_Test()
        {
            var barId = 0;
            _barRepositoryMock.Setup(repo => repo.GetById(barId));

            var result = await _barsController.GetBar(barId);

            Assert.AreEqual(((BadRequestResult)result.Result).StatusCode, 400);
        }

        [TestMethod]
        public async Task InsertBar_ShouldReturn_Valid_Test()
        {
            Bar bar = GetBarObject();
            _barRepositoryMock.Setup(repo => repo.InsertRecord(bar));

            var result = await _barsController.PostBar(bar);

            Assert.IsNotNull(result);
        }


        [TestMethod]
        public async Task UpdateBar_ShouldReturn_Valid_Test()
        {
            Bar bar = GetBarObject();
            _barRepositoryMock.Setup(repo => repo.UpdateRecord(bar));

            var result = await _barsController.PutBar(1, bar);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UpdateBar_ShouldReturn_BadRequest_Test()
        {
            Bar bar = GetBarObject();
            _barRepositoryMock.Setup(repo => repo.UpdateRecord(bar));

            var result = await _barsController.PutBar(10, bar);

            Assert.IsNotNull(result);
            Assert.AreEqual(((BadRequestResult)result).StatusCode, 400);
        }

        private static Bar GetBarObject()
        {
            return new Bar { BarId = 1, Address = "AddressString", Name = "Bar1" };
        }
    }
}