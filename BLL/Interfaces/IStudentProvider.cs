using System.Linq;
using System.Threading.Tasks;
using DAL.Entities.Repositories;
using DAL.Entities.Models;

namespace BLL.Interfaces
{
    public interface IStudentProvider
    {
        Task<Status> DeleteAsync(Student entity);
        Task<IQueryable<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
        Task<Student> GetByIdAsync(string id);
        Task<Status> CreateAsync(Student student);
    }
    public enum Status { Succees=1,Failure,Dublication }
}
