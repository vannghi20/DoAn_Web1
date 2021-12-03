using Microsoft.AspNetCore.Mvc;
using ProjectWeb1.Interface;
using ProjectWeb1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWeb1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        
        private readonly ICustomerLogic _customerLogic;
        public CustomerController(ICustomerLogic customerLogic)
        {
            _customerLogic = customerLogic;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllCustomer()
        {
            try
            {
                var response = await _customerLogic.GetAllCustomer();
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
        public async Task<ActionResult> Register(Customer user)
        {
                if (ModelState.IsValid)
                {
                    
                    var response = await _customerLogic.Register(user);
                    return Ok(response);
                }
                else
                {
                    return BadRequest("Đăng kí thất bại");
                }

        }

       
    }
}
