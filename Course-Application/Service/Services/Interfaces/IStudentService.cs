using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IStudentService
    {
        List<Student> GetByAge(int age);
        List<Student> GetByGroupId(int id);
        List<Student> GetByNameOrSurname(string searchText);
    }
}
