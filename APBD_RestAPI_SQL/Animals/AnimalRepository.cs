using System.Data.SqlClient;
namespace APBD_RestAPI_SQL.Animals;

public interface IAnimalRepository
{
    public IEnumerable<Animal> FetchAllAnimals(string orderBy);
    public bool CreateAnimal(string name);
    public bool UpdateAnimal(int idAnimal, Animal animal);
    public bool DeleteAnimal(int idAnimal);
    public Animal GetAnimalById(int idAnimal);
}

public class AnimalRepository : IAnimalRepository
{
    private readonly IConfiguration _configuration;
    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IEnumerable<Animal> FetchAllAnimals(string orderBy)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        var safeOrderBy = new string[] { "IdAnimal", "Name" }.Contains(orderBy) ? orderBy : "IdAnimal";
        using var command = new SqlCommand($"SELECT IdAnimal, Name FROM Animal ORDER BY {safeOrderBy}", connection);
        using var reader = command.ExecuteReader();

        var animals = new List<Animal>();
        while (reader.Read())
        {
            var animal = new Animal()
            {
                IdAnimal = (int)reader["IdAnimal"],
                Name = reader["Name"].ToString()!
            };
            animals.Add(animal);
        }

        return animals;
    }
    public Animal GetAnimalById(int idAnimal)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand("SELECT IdAnimal, Name FROM Animal WHERE IdAnimal = @idAnimal", connection);
        command.Parameters.AddWithValue("@idAnimal", idAnimal);
        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Animal
            {
                IdAnimal = (int)reader["IdAnimal"],
                Name = reader["Name"].ToString()
            };
        }
        else
        {
            return null;
        }
    }

    public bool CreateAnimal(string name)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        
        using var command = new SqlCommand("INSERT INTO Students (Name) VALUES (@name)", connection);
        command.Parameters.AddWithValue("@name", name);
        var affectedRows = command.ExecuteNonQuery();
        return affectedRows == 1;
    }
    public bool UpdateAnimal(int idAnimal, Animal animal)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand("UPDATE Animal SET Name = @name WHERE IdAnimal = @idAnimal", connection);
        command.Parameters.AddWithValue("@name", animal.Name);
        command.Parameters.AddWithValue("@idAnimal", idAnimal);
        var affectedRows = command.ExecuteNonQuery();
        return affectedRows == 1;
    }

    public bool DeleteAnimal(int idAnimal)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand("DELETE FROM Animal WHERE IdAnimal = @idAnimal", connection);
        command.Parameters.AddWithValue("@idAnimal", idAnimal);
        var affectedRows = command.ExecuteNonQuery();
        return affectedRows == 1;
    }
}