namespace APBD_RestAPI_SQL.Animals;

public interface IAnimalService
{
    public IEnumerable<Animal> GetAllAnimals(string orderBy);
    public bool AddAnimal(CreateAnimalDTO dto);
    public bool UpdateAnimal(int idAnimal, UpdateAnimalDTO dto);
    public bool DeleteAnimal(int idAnimal);
}

public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;
    public AnimalService(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }
    
    public IEnumerable<Animal> GetAllAnimals(string orderBy)
    {
        return _animalRepository.FetchAllAnimals(orderBy);
    }
    
    
    public bool AddAnimal(CreateAnimalDTO dto)
    {
        return _animalRepository.CreateAnimal(dto.Name);
    }
    public bool UpdateAnimal(int idAnimal, UpdateAnimalDTO dto)
    {
        var animalToUpdate = _animalRepository.GetAnimalById(idAnimal);
        if (animalToUpdate == null)
            return false;
        
        if (!string.IsNullOrEmpty(dto.Name))
            animalToUpdate.Name = dto.Name;

        if (!string.IsNullOrEmpty(dto.Description))
            animalToUpdate.Description = dto.Description;

        if (!string.IsNullOrEmpty(dto.CATEGORY))
            animalToUpdate.CATEGORY = dto.CATEGORY;

        if (!string.IsNullOrEmpty(dto.AREA))
            animalToUpdate.AREA = dto.AREA;
        
        return _animalRepository.UpdateAnimal(idAnimal, animalToUpdate);
    }

    public bool DeleteAnimal(int idAnimal)
    {
        var animalToDelete = _animalRepository.GetAnimalById(idAnimal);
        if (animalToDelete == null)
            return false;
        
        return _animalRepository.DeleteAnimal(idAnimal);
    }
}