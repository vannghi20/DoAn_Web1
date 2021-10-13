using Microsoft.AspNetCore.Mvc;
using ProjectWeb1.Interface;
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
        public async Task<ActionResult> GetFoodById(int id)
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
    }
}
