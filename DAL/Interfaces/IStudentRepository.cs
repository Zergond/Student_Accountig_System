using DAL.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IStudentRepository : ISqlRepository
    {
       Task <Student> Add(Student student);

    }
}
