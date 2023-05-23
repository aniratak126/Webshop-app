using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace Web2BE.Models
{
    public class DAL
    {
        public Response Register(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand sqlCommand = new SqlCommand("sp_register", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@FisrtName", users.FirstName);
            sqlCommand.Parameters.AddWithValue("@LastName", users.LastName);
            sqlCommand.Parameters.AddWithValue("@Password", users.Password);
            sqlCommand.Parameters.AddWithValue("@Email", users.Email);
            sqlCommand.Parameters.AddWithValue("@Euro", 0);
            sqlCommand.Parameters.AddWithValue("@Type", "Users");
            sqlCommand.Parameters.AddWithValue("@Status", "Pending");

            connection.Open();
            int i = sqlCommand.ExecuteNonQuery();
            connection.Close();
            if(i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User registered successfully";
            } else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User registration failed";
            }

            return response;
        }

        public Response Login(Users users, SqlConnection connection)
        {
            SqlDataAdapter sqlDA = new SqlDataAdapter("sp_login", connection);
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDA.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
            sqlDA.SelectCommand.Parameters.AddWithValue("@Password", users.Password);
            DataTable dt = new DataTable();
            sqlDA.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if(dt.Rows.Count>0)
            {
                user.Id = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                response.StatusCode = 200;
                response.StatusMessage = "User is valid";
                response.user = user;
            } else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User is invalid";
                response.user = null;
            }

            return response;
        }

        public Response ViewUser(Users users, SqlConnection connection)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("p_viewUser", connection);
            sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ID", users.Id);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                user.Id = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                user.Euro = Convert.ToDecimal(dt.Rows[0]["Euro"]);
                user.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]);
                user.Password = Convert.ToString(dt.Rows[0]["Password"]);

                response.StatusCode = 200;
                response.StatusMessage = "User exists.";
                response.user = user;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User does not exists.";
                response.user = null;
            }

            return response;
        }
    }
}
