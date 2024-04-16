using System.ComponentModel.DataAnnotations;
namespace APBD_RestAPI_SQL.Animals;

public class CreateAnimalDTO
{
    [Required]
    [EmailAddress]
    public string Name { get; set; }
}