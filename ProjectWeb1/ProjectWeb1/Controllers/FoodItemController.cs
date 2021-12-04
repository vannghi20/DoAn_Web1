using Microsoft.AspNetCore.Mvc;
using ProjectWeb1.Interface;
using ProjectWeb1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWeb1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodItemController: ControllerBase
    {
        private readonly IFoodItemLogic _foodItemLogic;
        public FoodItemController(IFoodItemLogic foodItemLogic)
        {
            _foodItemLogic = foodItemLogic;
        }
        // GET: api/<FoodItemController>
   
        [HttpGet("{id}")]
        public async Task<ActionResult> GetFoodById(string id)
        {
            try
            {
                var response = await _foodItemLogic.GetFoodById(id);
                if (response != null && response.Count > 0)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
        // GET: api/<FoodItemController>
        [HttpGet]
        public async Task<ActionResult> GetAllFood()
        {
            try
            {
                var response = await _foodItemLogic.GetAllFood();
                if (response != null && response.Count > 0)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        // POST api/<UserController>---code của trâm
        [HttpPost("add")]
        public async Task<ActionResult> CreateNewFood(FoodItem food)
        {
            try
            {
                var response = await _foodItemLogic.CreateNewFood(food);
                if (food.Title != null && food.Id > 0)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
        // POST api/<UserController>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFood(FoodItem food)
        {
            try
            {
                var response = await _foodItemLogic.UpdateFood(food);
                if (food.ImgSource != null && food.Title != null && food.Descr != null)
                {
                    return Ok(response);

                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        // Delete api/<UserController>-----
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFood(int id)
        {
            try
            {
                var response = await _foodItemLogic.DeleteFood(id);
                if (id > 0)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
    }
}
