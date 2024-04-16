using Microsoft.AspNetCore.Mvc;

namespace APBD_RestAPI_SQL.Animals;

[ApiController]
[Route("/api/animal")]
public class AnimalController
{
    private readonly IAnimalService _animalService;
    public AnimalController(IAnimalService animalService)
    {
        _animalService = animalService;
    }
    
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAllAnimals([FromQuery] string orderBy)
    {
        var animals = _animalService.GetAllAnimals(orderBy);
        return Ok(animals);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetAnimal([FromRoute] int id)
    {
        return Ok(id);
    }
    
    [HttpPost]
    public IActionResult AnimalStudent([FromBody] CreateAnimalDTO dto)
    {
        var success = _animalService.AddAnimal(dto);
        return success ? StatusCode(StatusCodes.Status201Created) : Conflict();
    }
}