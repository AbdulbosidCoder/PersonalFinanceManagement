using PersonalFinanceManagement.Data.IRepositories;
using PersonalFinanceManagement.Domain.Entities;
using PersonalFinanceManagement.Service.DTOs.User;
using PersonalFinanceManagement.Service.Exceptions;
using PersonalFinanceManagement.Service.Interfaces;

namespace PersonalFinanceManagement.Service.Services;

public class UserService : IUserService
{

    IRepository<User> userRepository = new Repository<User>();

    public async Task<bool> DeleteByIdAsync(int id)
    {
        var users = await this.userRepository.SelectAllAsync();
        foreach (var user in users)
        {
            if (user.Id == id)
            {
                bool isDeleted = await this.userRepository.DeleteByIdAsync(id);
                return isDeleted;
            }
        }
        throw new PersonalFinanceException(404, "User not found");
    }
    public async Task<bool> CreateAsync(UserForCreationDto dto)
    {
        var users = await this.userRepository.SelectAllAsync();
        foreach (var user in users)
        {
            if (user.UserName == dto.Username)
                throw new PersonalFinanceException(409, "User already exists");
        }

        var mappedUser = new User()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            UserName = dto.Username,
            Password = dto.Password,
            Email = dto.Email,
            DateOfBirth = dto.DateOfBirth,
        };
        bool isCreated = await this.userRepository.InsertAsync(mappedUser);
        return isCreated;
    }
    public async Task<UserForResultDto> GetByIdAsync(int id)
    {
        var user = await this.userRepository.SelectByIdAsync(id);
        if (user is null)
            throw new PersonalFinanceException(404, "User not found");

        var mappedUser = new UserForResultDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.UserName,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };
        return mappedUser;
    }
    public async Task<bool> UpdateAsync(int id, UserForUpdateDto dto)
    {
        var person = await this.userRepository.SelectByIdAsync(id);
        if (person is null)
            throw new PersonalFinanceException(404, "User not found");

        var mappedUser = new User()
        {
            Id = id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            UserName = dto.Username,
            Email = dto.Email,
            DateOfBirth = dto.DateOfBirth,
            CreatedAt = person.CreatedAt
        };

        await this.userRepository.UpdateAsync(mappedUser);
        return true;
    }
    public async Task<IEnumerable<UserForResultDto>> GetAllAsync()
    {
        var mappedUsers = new List<UserForResultDto>();
        var users = await this.userRepository.SelectAllAsync();
        foreach (var user in users)
        {
            var mappedUser = new UserForResultDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                CreatedAt = user.CreatedAt
            };
            mappedUsers.Add(mappedUser);
        }
        return mappedUsers;
    }

    public async Task<UserForResultDto> GetByUsernameAsync(string username)
    {
        var users = await userRepository.SelectAllAsync();
        var user = users.Where(e=>e.UserName == username).FirstOrDefault();
        if (user == null)
            throw new PersonalFinanceException(404, "User not found");

        var userForResult = await GetByIdAsync(user.Id);
        return userForResult;
    }




}
