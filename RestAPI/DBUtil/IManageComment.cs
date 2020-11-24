using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.models;

namespace RestAPI.DBUtil
{
    interface IManageComment
    {
        List<Comment> getAllComments();

        Comment GetCommentFromId(int id);

        bool CreateComment(Comment item);

        bool UpdateComment(Comment item, int id);

        bool DeleteComment(int id);
    }
}
