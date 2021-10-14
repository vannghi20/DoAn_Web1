using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using ProjectWeb1.Controllers;
using ProjectWeb1.Interface;
using ProjectWeb1.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace FoodItemControllerTest
{
    public class Tests
    {
        //gia lap moi truong IFoodItemLogic
        private Mock<IFoodItemLogic> simulationDataOfFoodController = new Mock<IFoodItemLogic>();
        internal FoodItemController foodItemController;

        [SetUp]
        public void Setup()
        {
            simulationDataOfFoodController = new Mock<IFoodItemLogic>();
            this.foodItemController = new FoodItemController(simulationDataOfFoodController.Object);
        }
        [Test]
        public async Task CreateFood_returnActionResult_success()
        {
            try
            {
                FoodItem food = new FoodItem() { Id = 1, ImgSource = "test", Title = "test", Descr = "test" };
                //set simulation Data 
                this.simulationDataOfFoodController.Setup(x => x.CreateNewFood(food)).ReturnsAsync(true);
                var response = await this.foodItemController.CreateNewFood(food);
                var data = JObject.FromObject(response);
                var statusCode = (int)data["StatusCode"];
                Assert.IsNotNull(response);
                Assert.IsInstanceOf<OkObjectResult>(response);
                Assert.AreEqual((int)HttpStatusCode.OK, statusCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        //Trâm
        [Test]
        public async Task CreateFood_returnActionResult_failed_smallerParameterNull()
        {
            try
            {
                FoodItem food = new FoodItem() { Id = 1, ImgSource = "test" };
                //set simulation Data 
                this.simulationDataOfFoodController.Setup(x => x.CreateNewFood(food)).ReturnsAsync(false);
                var response = await this.foodItemController.CreateNewFood(food);
                var data = JObject.FromObject(response);
                var statusCode = (int)data["StatusCode"];
                Assert.IsNotNull(response);
                Assert.IsInstanceOf<BadRequestObjectResult>(response);
                Assert.AreEqual((int)HttpStatusCode.BadRequest, statusCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}