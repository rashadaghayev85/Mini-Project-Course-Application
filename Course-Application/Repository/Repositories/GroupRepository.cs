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
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        public List<Group> GetAllByRoom(string roomName)
        {
            return AppDbContext<Group>.datas.Where(m => m.Room == roomName).ToList();
        }

        public List<Group> GetAllByTeacher(string teacherName)
        {
            return AppDbContext<Group>.datas.Where(m => m.Teacher == teacherName).ToList();
        }

        public Group GetByName(string groupName)
        {
            return AppDbContext<Group>.datas.FirstOrDefault(m => m.Name == groupName);
        }
    }
}
