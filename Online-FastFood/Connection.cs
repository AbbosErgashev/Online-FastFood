using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Online_FastFood
{
    public class Connection
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["FoodieDBConnectionString"].ConnectionString;
        }

        public class Utils
        {
            SqlConnection con;
            SqlCommand cmd;
            SqlDataAdapter sda;
            DataTable dt;

            public static bool IsValidExtension(string fileName)
            {
                bool isValid = false;
                string[] fileExtension = { ".jpg", ".png", ".jpeg" };
                for (int i = 0; i < fileExtension.Length - 1; i++)
                {
                    if (fileName.Contains(fileExtension[i]))
                    {
                        isValid = true;
                        break;
                    }
                }

                return isValid;
            }

            public static string GetImageUrl(Object url)
            {
                string url1 = "";
                if(string.IsNullOrEmpty(url.ToString()) || url == DBNull.Value)
                {
                    url1 = "../Images/No_Image.png";
                }
                else
                {
                    url1 = string.Format("../{0}", url);
                }

                return url1;
            }

            public bool updateCartQuantity(int quantity, int productId, int userId)
            {

                bool isUpdated = false;
                con = new SqlConnection(Connection.GetConnectionString());
                cmd = new SqlCommand("Cart_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "update");
                cmd.Parameters.AddWithValue("@ProductId", productId);
                cmd.Parameters.AddWithValue("@Quantity", 1);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    isUpdated = true;
                }
                catch (Exception ex)
                {
                    isUpdated = false;
                    System.Web.HttpContext.Current.Response.Write("<script>alert('Error - " + ex.Message + " ');</script>");
                }
                finally
                {
                    con.Close();
                }

                return isUpdated;
            }
        }
    }
}