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

        List<AssignedUser> getAllAssignedUsersMyTask(int taskID);

        List<AssignedUser> getAcceptedAssignments(int id);

        List<AssignedUser> getDeniedAssignments(int id);

        bool RemoveAssignedUsers_If_Denied(int taskid);

        AssignedUser GetAssignedUsersFromId(int id);

        bool RemoveAssignedUser(int userid);

        bool CreateAssignedUsers(AssignedUser item);

        bool UpdateAssignedUsers(AssignedUser item, int id);

        bool deleteAssignedUsers(int id);
    }
}
