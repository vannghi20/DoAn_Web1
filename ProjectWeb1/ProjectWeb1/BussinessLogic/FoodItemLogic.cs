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
                food.Version = (int)dt.Rows[i]["Version"];
                foodList.Add(food);
            }
            return foodList;
        }
        public async Task<List<FoodItem>> GetFoodById(string id)
        {
            List<FoodItem> foodList = new List<FoodItem>();
            int idReturn = 0;

            bool kiemTra = int.TryParse(id, out idReturn);

            if (kiemTra == true)
            {
                DataTable dt = await _sqlServer.GetData($"select * from FoodItem where Id ={id}");

                FoodItem food = new FoodItem();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    food = new FoodItem();
                    food.Id = (int)dt.Rows[i]["Id"];
                    food.ImgSource = dt.Rows[i]["ImgSource"].ToString();
                    food.Title = dt.Rows[i]["Title"].ToString();
                    food.Descr = dt.Rows[i]["Descr"].ToString();
                    food.Version = (int)dt.Rows[i]["Version"];
                    foodList.Add(food);
                }
            }
            return foodList;
        }
        public async Task<bool> CreateNewFood(FoodItem food)
        {
            string query = "insert into FoodItem (ImgSource,Title,Descr,Version) values (@ImgSource,@Title,@Descr,@Version);";
            var parameters = new IDataParameter[]
            {
                new SqlParameter("@ImgSource", food.ImgSource),
                new SqlParameter("@Title",food.Title),
                new SqlParameter("@Descr",food.Descr),
                new SqlParameter("@Version",0),
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

            string query = "update FoodItem set ImgSource = @ImgSource,Title = @Title, Descr = @Descr,Version = @Version where Id = @Id";
            int version = food.Version + 1;
            var parameters = new IDataParameter[]
            {
                new SqlParameter("@Id", food.Id),
                new SqlParameter("@ImgSource", food.ImgSource),
                new SqlParameter("@Title",food.Title),
                new SqlParameter("@Descr",food.Descr),
                new SqlParameter("@Version",version),
           };
            if (await _sqlServer.ExcuteDate(query, parameters) > 0)
            {

                return "Update Successfully";
            }
            else
            {
                throw new Exception();

            }
        }
    }
}
