using ProjectWeb1.Interface;
using ProjectWeb1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public async Task<List<FoodItem>> GetFoodById(int id)
        {
            DataTable dt = await _sqlServer.GetData($"select * from FoodItem where Id ={id}");
            List<FoodItem> foodList = new List<FoodItem>();
            FoodItem food = new FoodItem();
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                food.Id = (int)dt.Rows[i]["Id"];
                food.ImgSource = dt.Rows[i]["ImgSource"].ToString();
                food.Title = dt.Rows[i]["Title"].ToString();
                food.Descr = dt.Rows[i]["Descr"].ToString();
                foodList.Add(food);
            }

            return foodList;
        }
        // DeleteFood
        public async Task<bool> DeleteFood(int id)
        {

            string query = "delete from FoodItem where Id = @Id;";
            var parameters = new IDataParameter[]
            {
                new SqlParameter("@Id",id)

           };
            if (await _sqlServer.ExcuteDate(query, parameters) > 0)
            {

                return true;
            }
            else
            {
                return false;

            }
        }
        public async Task<string> UpdateFood(FoodItem food)
        {

            string query = "update FoodItem set ImgSource = @ImgSource,Title = @Title, Descr = @Descr where Id = @Id;";
            var parameters = new IDataParameter[]
            {
                new SqlParameter("@Id", food.Id),
                new SqlParameter("@ImgSource", food.ImgSource),
                new SqlParameter("@Title",food.Title),
                new SqlParameter("@Descr",food.Descr),
           };
            if (await _sqlServer.ExcuteDate(query, parameters) > 0)
            {

                return "Update Successfully";
            }
            else
            {
                return "something went wrong";

            }
        }
    }
}
