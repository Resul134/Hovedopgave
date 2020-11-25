using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.models;

namespace RestAPI.DBUtil
{
    interface IManageAssingedUsers
    {
        List<AssignedUser> getAllAssignedUsers();

        AssignedUser GetAssignedUsersFromId(int id);

        bool CreateAssignedUsers(AssignedUser item);

        bool UpdateAssignedUsers(AssignedUser item, int id);

        bool deleteAssignedUsers(int id);
    }
}
