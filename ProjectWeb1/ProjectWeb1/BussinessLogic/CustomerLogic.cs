using ProjectWeb1.Interface;
using ProjectWeb1.Models;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace ProjectWeb1.BussinessLogic
{
    public class CustomerLogic:ICustomerLogic
    {
        private readonly ISqlServer _sqlServer;

        public CustomerLogic(ISqlServer sqlServer)
        {
            _sqlServer = sqlServer;
        }

        public static string GetHash(string plainText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            // Compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(plainText));
            // Get hash result after compute it
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
        public async Task<bool> Register(Customer customer)
        {
            string password = GetHash(customer.Password);
            string query = "insert into Customer (CustomerName,CustomerPhone,Password) values (@CustomerName,@CustomerPhone,@Password);";
            var parameters = new IDataParameter[]
            {
                new SqlParameter("@CustomerName", customer.CustomerName),
                new SqlParameter("@CustomerPhone",customer.CustomerPhone),
                new SqlParameter("@Password",password)
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

        public async Task<List<Customer>> GetAllCustomer()
        {
            DataTable dt = await _sqlServer.GetData("select * from Customer");
            List<Customer> UerLisst = new List<Customer>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Customer ure = new Customer();
                ure.Id = (int)dt.Rows[i]["Id"];
                ure.CustomerName = dt.Rows[i]["CustomerName"].ToString();
                ure.CustomerPhone = dt.Rows[i]["CustomerPhone"].ToString();
                UerLisst.Add(ure);
            }
            return UerLisst;
        }
        public async Task<List<Customer>> GetFoodById(string id)
        {
            List<Customer> UerLisst = new List<Customer>();
            int idReturn = 0;

            bool kiemTra = int.TryParse(id, out idReturn);

            if (kiemTra == true)
            {
                DataTable dt = await _sqlServer.GetData($"select * from FoodItem where Id ={id}");

                Customer ure = new Customer();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ure = new Customer();
                    ure.Id = (int)dt.Rows[i]["Id"];
                    ure.CustomerName = dt.Rows[i]["CustomerName"].ToString();
                    ure.CustomerPhone = dt.Rows[i]["CustomerPhone"].ToString();
                    UerLisst.Add(ure);
                }
            }
            return UerLisst;
        }
        // DeleteFood
        public async Task<bool> DeleteCustomer(int id)
        {

            string query = "delete from Customer where Id = @Id;";
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
    }
}
