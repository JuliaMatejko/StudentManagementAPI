using StudentManagementAPI.Models;
using MongoDB.Driver;
namespace StudentManagementAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMongoCollection<Student> _students;

        public StudentService(IStudentStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _students = database.GetCollection<Student>(settings.StudentCoursesCollectionName);
        }

        public async Task<Student> CreateStudentAsync(Student student)
        {
            await _students.InsertOneAsync(student);
            return student; 
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _students.Find(student => true).ToListAsync();
        }

        public async Task<Student?> GetStudentAsync(string id)
        {
            return await _students.Find(student => student.Id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteStudentAsync(string id)
        {
            await _students.DeleteOneAsync(student => student.Id == id);
        }

        public async Task UpdateStudentAsync(string id, Student student)
        {
            await _students.ReplaceOneAsync(student => student.Id == id, student);
        }
    }
}
