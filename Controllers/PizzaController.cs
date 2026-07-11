using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[Controller]")]

public class PizzaController : ControllerBase
{
    public PizzaController()
    {
        
    }

    //Get All Action
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();

    // Get by id Action
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        try
        {
            var pizza = PizzaService.Get(id);

            if(pizza == null)
                return NotFound("Doesn't Have the Product Yet");
        
            return pizza;    
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
        
    }

    // POST Action
    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        // this code will save the pizza and return a result
        PizzaService.Add(pizza);
        return CreatedAtAction(nameof(Get), new {id = pizza.Id}, pizza);
    }

    // PUT Action
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        // This code will update the pizza and return a result
        if(id != pizza.Id)
            return BadRequest();

        var existingPizza = PizzaService.Get(id);
        if (existingPizza is null)
            return NotFound();

        PizzaService.Update(pizza);

        return NoContent();
    }

    // DELETE Action   
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        // This code will delete the pizza and return a result
        var pizza = PizzaService.Get(id);

        if(pizza is null)
            return NotFound();

        PizzaService.Delete(id);

        return NoContent();
    }
}