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
       Student Add(Student student);
        int Count();
    }
}
