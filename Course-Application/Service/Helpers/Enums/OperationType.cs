using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.Enums
{
    public enum OperationType
    {
        GroupCreate=1,
        GroupDelete,
        GroupUpdate,
        GroupGetAll,
        GroupGetAllByRoom,
        GroupGetAllByTeacher,
        GroupGetById,
        GroupGetByName,
        StudentCreate,
        StudentDelete,
        StudentUpdate,
        StudentGetAll,
        StudentGetByAge,
        StudentGetById,
        StudentGetByGroupId,
        StudentGetByNameorSurname

    }
}
