using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities.Repositories;
using DAL.Entities.Models;
using DAL.Interfaces;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IStudentProvider
    {
        Task<Status> DeleteStudentAsync(Student student);
        Task<object> GetStudentsAsync();
        Task<object> GetStudentsAsyncByFilter(string Id, string Name, string LastName, string Age, string StudyDate, string RegisteredDate);
        Task<Student> GetByIdAsync(string id);
        Task<Status> CreateAsync(Student student);
        Task<Status> EditStudentAsync(StudentTableViewModel student,string id);

    }
    
}
