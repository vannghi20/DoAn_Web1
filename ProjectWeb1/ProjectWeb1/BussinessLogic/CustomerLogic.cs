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
        
    }
}
