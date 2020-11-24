using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.models;

namespace RestAPI.DBUtil
{
    interface IManageRating
    {
        List<Rating> getAllRatings();

        Rating GetRatingFromId(int id);

        bool CreateRating(Rating item);

        bool UpdateRating(Rating item, int id);

        bool deleteRating(int id);
    }
}
