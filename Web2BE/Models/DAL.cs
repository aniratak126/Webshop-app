using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

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
                user.UserName = Convert.ToString(dt.Rows[0]["UserName"]);
                user.BirthDate = Convert.ToDateTime(dt.Rows[0]["BirthDate"]);
                user.Adress = Convert.ToString(dt.Rows[0]["Adress"]);
                user.ImageUrl = Convert.ToString(dt.Rows[0]["ImageUrl"]);

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

        public Response UpdateProfile(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand sqlCommand = new SqlCommand("sp_updateProfile", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@FirstName", users.FirstName);
            sqlCommand.Parameters.AddWithValue("@LastName", users.LastName);
            sqlCommand.Parameters.AddWithValue("@Password", users.Password);
            sqlCommand.Parameters.AddWithValue("@Email", users.Email);
            sqlCommand.Parameters.AddWithValue("@UserName", users.UserName);
            sqlCommand.Parameters.AddWithValue("@Adress", users.Adress);
            sqlCommand.Parameters.AddWithValue("@BirthDate", users.BirthDate);
            sqlCommand.Parameters.AddWithValue("@ImageUrl", users.ImageUrl);

            connection.Open();
            int i = sqlCommand.ExecuteNonQuery();
            connection.Close();

            if(i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Update execute successfully";
            } else
            {
                response.StatusCode = 100;
                response.StatusMessage = "ERROR: Try again.";
            }

            return response;
        }

        public Response AddToCart(Cart cart, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand sqlCommand = new SqlCommand("sp_AddToCart", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@UserId", cart.UserId);
            sqlCommand.Parameters.AddWithValue("@UnitPrice", cart.UnitPrice);
            sqlCommand.Parameters.AddWithValue("@Discount", cart.Discount);
            sqlCommand.Parameters.AddWithValue("@Quantity", cart.Quantity);
            sqlCommand.Parameters.AddWithValue("@TotalPrice", cart.TotalPrice);
            sqlCommand.Parameters.AddWithValue("@MedicineId", cart.MedicineId);

            connection.Open();
            int i = sqlCommand.ExecuteNonQuery();
            connection.Close();

            if(i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Item added successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Item can't be added.";
            }

            return response;
        }

        public Response PlaceOrder(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand sqlCommand = new SqlCommand("sp_PlaceOrder", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@ID", users.Id);
            connection.Open();
            int i = sqlCommand.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Order has been placed successfully.";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order could not be placed.";
            }

            return response;
        }

        public Response OrderList(Users users, SqlConnection connection)
        {
            Response response = new Response();
            List<Orders> orders = new List<Orders>();
            SqlDataAdapter da = new SqlDataAdapter("sp_OrderList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Type", users.Type);
            da.SelectCommand.Parameters.AddWithValue("@ID", users.Id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count>0)
            {
                for(int i=0; i<dt.Rows.Count; i++)
                {
                    Orders order = new Orders();
                    order.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    order.OrderNumber = Convert.ToString(dt.Rows[i]["OrderNo"]);
                    order.OrderTotal = Convert.ToDecimal(dt.Rows[i]["OrderTotal"]);
                    order.OrderStatus = Convert.ToString(dt.Rows[i]["OrderStatus"]);
                    orders.Add(order);
                }
                if(orders.Count>0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Order details fetched.";
                    response.listOrders = orders;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Order details are not available.";
                    response.listOrders = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order details are not available.";
                response.listOrders = null;
            }

            return response;
        }

        public Response AddUpdateMedicine(Medicines medicine, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand sqlCommand = new SqlCommand("sp_AddUpdateMedicine", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Name", medicine.Name);
            sqlCommand.Parameters.AddWithValue("@Manufacturer", medicine.Manufacturer);
            sqlCommand.Parameters.AddWithValue("@Quantity", medicine.Quantity);
            sqlCommand.Parameters.AddWithValue("@UnitPrice", medicine.UnitPrice);
            sqlCommand.Parameters.AddWithValue("@Discount", medicine.Discount);
            sqlCommand.Parameters.AddWithValue("@Description", medicine.Description);
            sqlCommand.Parameters.AddWithValue("@ExpDate", medicine.ExpDate);
            sqlCommand.Parameters.AddWithValue("@ImageUrl", medicine.ImgUrl);
            sqlCommand.Parameters.AddWithValue("@Status", medicine.Status);
            sqlCommand.Parameters.AddWithValue("@Type", medicine.Type);
            connection.Open();
            int i = sqlCommand.ExecuteNonQuery();
            connection.Close();

            if(i>0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Medicine inserted successfully.";
            }else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Medicine did't save. Try again.";
            }

            return response;
        }

        public Response UserList(SqlConnection connection)
        {
            Response response = new Response();
            List<Users> usrs = new List<Users>();
            SqlDataAdapter da = new SqlDataAdapter("sp_UserList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Users user = new Users();
                    user.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    user.UserName = Convert.ToString(dt.Rows[i]["UserName"]);
                    user.FirstName = Convert.ToString(dt.Rows[i]["FirstName"]);
                    user.LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                    user.BirthDate = Convert.ToDateTime(dt.Rows[i]["BirthDate"]);
                    user.Adress = Convert.ToString(dt.Rows[i]["Adress"]);
                    user.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    user.Password = Convert.ToString(dt.Rows[i]["Password"]);
                    user.Status = Convert.ToInt32(dt.Rows[i]["Status"]);
                    user.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);

                    usrs.Add(user);
                }
                if (usrs.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Users details fetched.";
                    response.listUsers = usrs;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Users details are not available.";
                    response.listUsers = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Users details are not available.";
                response.listUsers = null;
            }

            return response;
        }
    }
}
