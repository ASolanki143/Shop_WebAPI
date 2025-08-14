using MyWebApiApp.Models;
using Microsoft.Data.SqlClient;
using MyWebApiApp.Models.DTOs;
using MyWebApiApp.Utilities;
using System.Data;
namespace MyWebApiApp.Data
{
    public class UserRepository
    {
        private readonly DBHelper _dBHelper;
        public UserRepository(DBHelper dBHelper)
        {
            _dBHelper = dBHelper;
        }

        #region UserList
        public IEnumerable<UserModel> SelectAll()
        {
            var users = new List<UserModel>();
            var dt = _dBHelper.ExecuteDataTable("PR_User_UserList");

            foreach (DataRow row in dt.Rows)
            {
                users.Add(new UserModel()
                {
                    UserID = (int)row["UserID"],
                    UserName = row["UserName"].ToString(),
                    Email = row["Email"].ToString(),
                    Password = row["Password"].ToString()
                });
            }

            return users;
        }
        #endregion

        #region Login
        public LoginResponseDto? Login(String UserName, String Password)
        {
            Console.WriteLine("Login Repository");
            Console.WriteLine(UserName);
            Console.WriteLine(Password);
            LoginResponseDto? user = null;
            var dt = _dBHelper.ExecuteDataTable(
                "PR_User_Login",
                new SqlParameter("@UserName",UserName),
                new SqlParameter("@Password",Password)
            );

            if (dt.Rows.Count == 0) return null;
            var row = dt.Rows[0];
            
            return new LoginResponseDto()
            {
                UserID = (int)row["UserID"],
                UserName = row["UserName"].ToString(),
                Role = row["Role"].ToString()
            };
        }
        #endregion
    }
}