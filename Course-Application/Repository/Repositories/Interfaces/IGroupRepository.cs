﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IGroupRepository:IBaseRepository<Group>
    {
        List<Group> GetAllByTeacher(string teacherName);
        List<Group> GetAllByRoom(string roomName);
        List<Group>SearchGroupsByName(string groupName);
        Group GetByName(string groupName);

    }
}
