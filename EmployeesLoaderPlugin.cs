using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

[Author("Ivan Petrov")]
public class EmployeesLoaderPlugin : IEmployeesLoaderPlugin
{
    public IEnumerable<DataTransferObject> Run(IEnumerable<DataTransferObject> data)
    {
        var result = new List<EmployeesDTO>();

        foreach (var item in data)
        {
            if (item is EmployeesDTO e)
                result.Add(e);
        }

        Console.WriteLine("[EmployeesLoaderPlugin] Загружаю пользователей из API...");

        try
        {
            var newUsers = ЗагрузитьПользователей();
            result.AddRange(newUsers);

            foreach (var user in newUsers)
            {
                Console.WriteLine($"Добавлен пользователь: {user.Name} ({user.Phone})");
            }

            Console.WriteLine($"[EmployeesLoaderPlugin] Загрузка завершена. Добавлено {newUsers.Count} записей.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EmployeesLoaderPlugin] Ошибка: {ex.Message}");
        }

        return result;
    }

    private List<EmployeesDTO> ЗагрузитьПользователей()
    {
        const string apiUrl = "https://dummyjson.com/users";

        using (var client = new WebClient())
        {
            var json = client.DownloadString(apiUrl);
            var response = JsonConvert.DeserializeObject<ApiОтвет>(json);

            var users = new List<EmployeesDTO>();

            foreach (var user in response.Users)
            {
                var newUser = new EmployeesDTO
                {
                    Name = $"{user.FirstName} {user.LastName}"
                };
                newUser.AddPhone(user.Phone);
                users.Add(newUser);
            }

            return users;
        }
    }

    private class ApiОтвет
    {
        public List<User> Users { get; set; }
    }

    private class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
    }
}
