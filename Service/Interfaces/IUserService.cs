using PersonalFinanceManagement.Domain.Entities;
using PersonalFinanceManagement.Service.DTOs.User;

namespace PersonalFinanceManagement.Service.Interfaces;

public interface IUserService
{
    public Task<bool> CreateAsync(UserForCreationDto user);
    public Task<bool> UpdateAsync(int id , UserForUpdateDto user);
    public Task<bool> DeleteByIdAsync(int id);
    public Task<UserForResultDto> GetByIdAsync(int id);
    public Task<IEnumerable<UserForResultDto>> GetAllAsync();
    public Task<UserForResultDto> GetByUsernameAsync(string username);

}
