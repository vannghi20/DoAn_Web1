using ProjectWeb1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWeb1.Interface
{
    public interface IFoodItemLogic
    {
        Task<List<FoodItem>> GetFoodById(string id);
        Task<bool> DeleteFood(int Id);
    }

}
