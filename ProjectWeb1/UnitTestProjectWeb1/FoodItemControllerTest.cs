using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using ProjectWeb1.Controllers;
using ProjectWeb1.Interface;
using ProjectWeb1.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace UnitTestProjectWeb1
{
    public class FoodItemControllerTest
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
        //GetFoodByidSuccess_Test
        [Test]
        public async Task GetUserById_returnListOfUser_success()
        {
            int id = 1;
            //set simulation Data 
            this.simulationDataOfFoodController.Setup(x => x.GetFoodById(id)).ReturnsAsync(new List<FoodItem>()
            {
                new FoodItem{Id= 1,ImgSource="test",Title="test",Descr="test"}
            });
            var response = await this.foodItemController.GetFoodById(id);
            var data = JObject.FromObject(response);
            var statusCode = (int)data["StatusCode"];
            var responseModel = JsonConvert.DeserializeObject<List<FoodItem>>(data["Value"].ToString());
            Assert.IsNotNull(response);
            Assert.IsInstanceOf<OkObjectResult>(response);
            Assert.AreEqual((int)HttpStatusCode.OK, statusCode);
            Assert.IsInstanceOf<List<FoodItem>>(responseModel);
            Assert.That(responseModel[0].Id == 1 && responseModel[0].ImgSource == "test" && responseModel[0].Title == "test" && responseModel[0].Descr == "test");
        }
        //GetFoodByidFailed_Test
        [Test]
        public async Task GetFoodById_returnListOfFood_failed()
        {
            int id = 1;
            //set simulation Data 
            this.simulationDataOfFoodController.Setup(x => x.GetFoodById(id)).Returns(Task.FromResult<List<FoodItem>>(null));
            var response = await this.foodItemController.GetFoodById(id);
            var data = JObject.FromObject(response);
            var statusCode = (int)data["StatusCode"];
            var responseModel = JsonConvert.DeserializeObject<List<FoodItem>>(data["Value"].ToString());
            Assert.IsNotNull(response);
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, statusCode);
            Assert.IsNull(responseModel);
        }
        //DeleteTest_Failed
        [Test]
        public async Task DeleteFood_returnActionResult_failedIdNull()
        {           
                int id = 0;
                this.simulationDataOfFoodController.Setup(x => x.DeleteFood(id)).ReturnsAsync(false);
                var response = await this.foodItemController.DeleteFood(id);
                var data = JObject.FromObject(response);
                var statusCode = (int)data["StatusCode"];
                Assert.IsNotNull(response);
                Assert.IsInstanceOf<BadRequestObjectResult>(response);
                Assert.AreEqual((int)HttpStatusCode.BadRequest, statusCode);
        }
        //DeleteTest_Success
        [Test]
        public async Task DeleteFood_returnActionResult_success()
        {
                int id = 1;
                this.simulationDataOfFoodController.Setup(x => x.DeleteFood(id)).ReturnsAsync(true);
                var response = await this.foodItemController.DeleteFood(id);
                var data = JObject.FromObject(response);
                var statusCode = (int)data["StatusCode"];
                Assert.IsNotNull(response);
                Assert.IsInstanceOf<OkObjectResult>(response);
                Assert.AreEqual((int)HttpStatusCode.OK, statusCode);
        }
    }
}