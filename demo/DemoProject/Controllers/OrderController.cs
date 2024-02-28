using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Controllers;

[Route("api/product/{productId:int}/orders")]
public class OrderController : ControllerBase
{

    [Route("{id:int}")]
    [HttpGet]
    public void GetById(int productId, int id)
    {
        // _context.Orders.Single(x => x.Id == id && x.ProductId == productId);
        

    }
}