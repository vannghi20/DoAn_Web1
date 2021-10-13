using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using ProjectWeb1.Controllers;
using ProjectWeb1.Interface;
using ProjectWeb1.Models;
using System;
using System.Collections.Generic;
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
        public async Task GetAllFood_returnListOfFood_success()
        {
            //set simulation Data 
            this.simulationDataOfFoodController.Setup(x => x.GetAllFood()).ReturnsAsync(new List<FoodItem>()
            {
                new FoodItem{Id= 1,ImgSource="test",Title="test",Descr="test"}
            });
            var response = await this.foodItemController.GetAllFood();
            var data = JObject.FromObject(response);
            var statusCode = (int)data["StatusCode"];
            var responseModel = JsonConvert.DeserializeObject<List<FoodItem>>(data["Value"].ToString());
            Assert.IsNotNull(response);
            Assert.IsInstanceOf<OkObjectResult>(response);
            Assert.AreEqual((int)HttpStatusCode.OK, statusCode);
            Assert.IsInstanceOf<List<FoodItem>>(responseModel);
            Assert.That(responseModel[0].Id == 1 && responseModel[0].ImgSource == "test" && responseModel[0].Title == "test" && responseModel[0].Descr == "test");

        }
        [Test]
        public async Task GetAllFood_returnListOfFood_failed()
        {
            //set simulation Data 
            this.simulationDataOfFoodController.Setup(x => x.GetAllFood()).Returns(Task.FromResult<List<FoodItem>>(null));
            var response = await this.foodItemController.GetAllFood();
            var data = JObject.FromObject(response);
            var statusCode = (int)data["StatusCode"];
            var responseModel = JsonConvert.DeserializeObject<List<FoodItem>>(data["Value"].ToString());
            Assert.IsNotNull(response);
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, statusCode);
            Assert.IsNull(responseModel);
        }
        [Test]
        public async Task UpdateFood_returnActionResult_success()
        {
            try
            {
                FoodItem food = new FoodItem() { Id = 1, ImgSource = "test", Title = "test", Descr = "test" };
                //set simulation Data 
                this.simulationDataOfFoodController.Setup(x => x.UpdateFood(food)).ReturnsAsync("success");
                var response = await this.foodItemController.UpdateFood(food);
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
        [Test]
        public async Task UpdateFood_returnActionResult_failedFoodNull()
        {
            try
            {
                FoodItem food = new FoodItem() { Id = 1 };
                //set simulation Data 
                this.simulationDataOfFoodController.Setup(x => x.UpdateFood(food)).ReturnsAsync("something went wrong");
                var response = await this.foodItemController.UpdateFood(food);
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