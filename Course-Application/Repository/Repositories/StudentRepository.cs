using Domain.Models;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public void DeleteAll(int id)
        {
            AppDbContext<Student>.datas.RemoveAll(m => m.Group.Id == id);

        }

        public List<Student> GetByAge(int age)
        {
            return AppDbContext<Student>.datas.Where(m=>m.Age==age).ToList();
        }

        public List<Student> GetByGroupId(int id)
        {
           return AppDbContext<Student>.datas.Where(m => m.Group.Id ==id).ToList();
         
        }

        public List<Student> GetByNameOrSurname(string searchText)
        {
            return AppDbContext<Student>.datas.Where(m=>m.Name==searchText||m.Surname==searchText).ToList();    
        }
    }
}
