using StudentManagementAPI.Models;
namespace StudentManagementAPI.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetAsync(string id);
        Task<Student> CreateAsync(Student student);
        Task UpdateAsync(string id, Student student);
        Task DeleteAsync(string id);
    }
}
