using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.models;

namespace RestAPI.DBUtil
{
    interface IManageQualifications
    {
        List<Qualification> getAllQualifications();

        Qualification GetQualificationsFromId(int id);

        bool RemoveQualifications(int userid);

        bool CreateQualifications(Qualification item);

        bool UpdateQualifications(Qualification item, int id);

        bool deleteQualifications(int id);
    }
}
