using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTransactionApp.Models;
using SalesTransactionApp.Services.Interface;

namespace SalesTransactionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesTransact _salesTransact;
        public SalesController(ISalesTransact salesTransact)
        {
            _salesTransact = salesTransact;
        }
       

        [HttpGet("GetAllTransactions")]
        public IActionResult Get()
        {
            var salesDetails = _salesTransact.Get();
            return Ok(salesDetails);

        }

        [HttpPost("AddTransactDetails")]
        public IActionResult Create(SalesTransact salesTransact)
        {
            var response = _salesTransact.Add(salesTransact);
            if (response != null)
            {
                return Ok();
            }

            return BadRequest(response);

        }
        [HttpPut("EditTransactDetails/{id}")]
        public IActionResult Edit(Guid id, SalesTransact salesTransact)
        {
            var response = _salesTransact.Update(id, salesTransact);
            if (response != null)
            {
                return Ok();
            }

            return BadRequest(response);
        }
        [HttpPost("DeleteTransactDetails/{id}")]
        public IActionResult Delete(Guid id)
        {

            var response = _salesTransact.Delete(id);
            if (response != null)
            {
                return Ok();
            }

            return BadRequest(response);

        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string itemName, [FromQuery] DateTime? salesDate, [FromQuery] string paymentType)
        {
            try
            {
                var results = await _salesTransact.Search(itemName, salesDate, paymentType);

                if (results == null)
                {
                    return NotFound("No transactions found matching the search criteria.");
                }

                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
