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
        public LoginResponse? Login(string userName, string password)
        {
            var dt = _dBHelper.ExecuteDataTable(
                "PR_User_Login",
                new SqlParameter("@UserName",userName),
                new SqlParameter("@Password",password)
            );

            if (dt.Rows.Count == 0) return null;
            var row = dt.Rows[0];
            
            return new LoginResponse()
            {
                UserID = (int)row["UserID"],
                UserName = row["UserName"].ToString(),
                Role = row["Role"].ToString()
            };
        }
        #endregion
    }
}