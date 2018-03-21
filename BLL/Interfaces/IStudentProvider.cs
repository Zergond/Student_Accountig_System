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
        Task<List<StudentTableViewModel>> GetStudentsAsync();
        Task<List<StudentTableViewModel>> GetStudentsAsyncByFilter(string pageIndex, string pageSize);
        Task<Student> GetByIdAsync(string id);
        Task<Status> CreateAsync(Student student);
        Task<Status> EditStudentAsync(StudentTableViewModel student,string Id);
        int GetStudentsCount();

    }
    
}
