using ProjectWeb1.Interface;
using ProjectWeb1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWeb1.BussinessLogic
{
    public class FoodItemLogic: IFoodItemLogic
    {
        private readonly ISqlServer _sqlServer;

        public FoodItemLogic(ISqlServer sqlServer)
        {
            _sqlServer = sqlServer;
        }

        public async Task<List<FoodItem>> GetAllFood()
        {
            DataTable dt = await _sqlServer.GetData("select * from FoodItem");
            List<FoodItem> foodList = new List<FoodItem>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FoodItem food = new FoodItem();
                food.Id = (int)dt.Rows[i]["Id"];
                food.ImgSource = dt.Rows[i]["ImgSource"].ToString();
                food.Title = dt.Rows[i]["Title"].ToString();
                food.Descr = dt.Rows[i]["Descr"].ToString();
                foodList.Add(food);
            }
            return foodList;
        }
    }
}
