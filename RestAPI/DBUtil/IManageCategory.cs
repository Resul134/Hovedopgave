using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.models;

namespace RestAPI.DBUtil
{
    interface IManageCategory
    {
        List<Category> getAllCategories();

        Category GetCategoryFromId(int id);

        bool CreateCategory(Category item);

        bool UpdateCategory(Category item, int id);

        bool deleteCategory(int id);
    }
}
