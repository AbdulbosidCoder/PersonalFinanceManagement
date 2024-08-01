using PersonalFinanceManagement.Pressentations;
using PersonalFinanceManagement.Service.DTOs.User;
using PersonalFinanceManagement.Service.Exceptions;
using PersonalFinanceManagement.Service.Interfaces;
using PersonalFinanceManagement.Service.Services;
using System.Runtime.CompilerServices;
namespace PersonalFinanceManagement;

public class Program
{

    static IUserService userService = new UserService();
    static Pressentation pressentation = new Pressentation();
    static async Task Main(string[] args)
    {
        
        
        
        
        while(true)
        {
            await Console.Out.WriteLineAsync("1=>LogIn");
            await Console.Out.WriteLineAsync("2=>SignIn");
            var result = int.Parse(Console.ReadLine());
            switch (result)
            {
                case 1:
                    {
                        await LogInAsync();
                        return;

                    }
                case 2:
                    {
                        await SingInAsync();
                        return;
                    }
            }
        }
        
        
        

        

    }

    static async Task LogInAsync()
    {
        try
        {
            await Console.Out.WriteLineAsync("Enter UserName");
            var userName = Console.ReadLine();
            var user = await userService.GetByUsernameAsync(userName);

            await pressentation.ShowForUserAsync(user.Id);
        }catch(PersonalFinanceException ex )
        {

            Console.WriteLine(ex.Message);
        }

    }
    static async Task SingInAsync()
    {
        UserForCreationDto user = new UserForCreationDto();

        await Console.Out.WriteLineAsync("Enter FirstName  ");
        user.FirstName = Console.ReadLine();
        await Console.Out.WriteLineAsync("Enter LastName");
        user.LastName = Console.ReadLine();
        await Console.Out.WriteLineAsync("Enter Username");
        user.Username = Console.ReadLine();
        await Console.Out.WriteLineAsync("Enter Password");
        user.Password = Console.ReadLine();
        await Console.Out.WriteLineAsync("Enter Email");
        user.Email = Console.ReadLine();
        await Console.Out.WriteLineAsync("Enter Date of birth (format: yyyy-MM-dd):");
        user.DateOfBirth = DateOnly.Parse(Console.ReadLine());
        var isCreated = await userService.CreateAsync(user);
        await Console.Out.WriteLineAsync("Successefuly created");
        var newUser  =  await userService.GetByUsernameAsync(user.Username);
        await pressentation.ShowForUserAsync(newUser.Id);
    }



}
