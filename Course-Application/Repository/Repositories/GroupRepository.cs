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
            return AppDbContext<Group>.datas.Where(m => m.Room.ToLower().Trim() == roomName.ToLower().Trim()).ToList();
        }

        public List<Group> GetAllByTeacher(string teacherName)
        {
            return AppDbContext<Group>.datas.Where(m => m.Teacher.ToLower().Trim() == teacherName.ToLower().Trim()).ToList();
        }

        public Group GetByName(string groupName)
        {
            return AppDbContext<Group>.datas.FirstOrDefault(m => m.Name.ToLower().Trim()==groupName.ToLower().Trim());
        }

        public List<Group> SearchGroupsByName(string groupName)
        {
            return AppDbContext<Group>.datas.Where(m => m.Name.ToLower().Trim().StartsWith(groupName.ToLower().Trim())).ToList();
        }
    }
}
