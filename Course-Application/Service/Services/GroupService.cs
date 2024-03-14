using Domain.Models;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Helpers.Constants;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class GroupService : IGroupService
    {

        private readonly IGroupRepository _GroupRepo;
        private int count = 1;
        public GroupService()
        {
            _GroupRepo=new GroupRepository();
        }
        public void Create(Group data)
        {
            if (data is null) throw new ArgumentNullException();
            data.Id = count;
            _GroupRepo.Create(data);
            count++;
        }

        public void Delete(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            Group group = _GroupRepo.GetById((int)id);

            if (group is null) throw new NotFoundException(ResponseMessages.DataNotFound);

            _GroupRepo.Delete(group);
        }
        public void Update(Group data)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetAll()
        {
            return _GroupRepo.GetAll();
        }

        public List<Group> GetAllByRoom(string roomName)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetAllByTeacher(string teacherName)
        {
            throw new NotImplementedException();
        }

        public Group GetById(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            Group group = _GroupRepo.GetById((int)id);

            if (group is null) throw new NotFoundException(ResponseMessages.DataNotFound);

            return group;
        }

        public Group GetByName(string groupName)
        {
            throw new NotImplementedException();
        }

        
    }
}
