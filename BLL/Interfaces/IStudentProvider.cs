using System.Linq;
using System.Threading.Tasks;
using DAL.Entities.Repositories;
using DAL.Entities.Models;
using DAL.Interfaces;

namespace BLL.Interfaces
{
    public interface IStudentProvider
    {
        Task<Status> DeleteAsync(Student student);
        Task<IQueryable<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
        Task<Student> GetByIdAsync(string id);
        Task<Status> CreateAsync(Student student);
    }
    
}
