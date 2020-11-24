using Library.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.DBUtil
{
    public interface IManageUsers
    {
        List<User> getAllUsers();


        User GetUserFromId(int id);


        bool CreateUser(User item);


        bool UpdateUser(User item, int id);


        bool deleteUser(int id);
    }
}
