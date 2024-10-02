using Bogus;
using ConsoleApp8.Model;
using System.Text.Json;

void generateUsers(int count)
{
    for (int i = 0; i < count; i++)
    {
        Faker<User> faker = new();

        var users = faker.RuleFor(u => u.FirstName, f => f.Person.FirstName)
            .RuleFor(u => u.LastName, f => f.Person.LastName)
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.DateOfBirth, f => f.Person.DateOfBirth)
            .Generate(50);


        var json = JsonSerializer.Serialize(users);
        File.WriteAllText($"users{i + 1}.json", json);
    }
}


generateUsers(5);
//static List<User> ReadJsonFile(string filePath)
//{
//    var json = File.ReadAllText(filePath);
//    return JsonSerializer.Deserialize<List<User>>(json);
//}
List<User> ReadUsers(int count)
{
    List<User> allUsers = new List<User>();

    for (int i = 1; i <= count; i++)
    {
        
        string fileCount = $"users{i}.json";

        if (File.Exists(fileCount))
        {
            
            string json = File.ReadAllText(fileCount);
            List<User> users = JsonSerializer.Deserialize<List<User>>(json);

            if (users != null)
            {
                allUsers.AddRange(users);
            }
        }
        
    }

    return allUsers;
}
List<User> allUsers = ReadUsers(5);
foreach (var user in allUsers)
{
    Console.WriteLine($"{user.FirstName} {user.LastName} ----> {user.Email}");
}



