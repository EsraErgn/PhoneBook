using ContactService.DTOs.Person;

namespace ContactService.Services
{
    public interface IPersonService
    {
        Task<List<PersonResponse>> GetAllAsync();
        Task<PersonResponse> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(CreatePersonRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
