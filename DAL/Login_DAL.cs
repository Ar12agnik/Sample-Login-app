using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.WebSockets;
using Sample_Login_app.Models;

namespace Sample_Login_app.DAL
{
    public class Login_DAL
    {
        string constring = ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;
        public bool ValidateUser(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                int id = 0;
                SqlCommand cmd = new SqlCommand("check_login", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                con.Open();
                id = Convert.ToInt32(cmd.ExecuteScalar());
                id = Math.Abs(id);
                con.Close();
                if (id > 0)
                    return true;
                else if (id == 0)
                    return false;
                else
                    return false;
            }
        }
        public bool RegisterUser(combinedModels _combined_models)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    if (!user_exist(_combined_models.Username))
                    {
                        int id = 0;
                        SqlCommand cmd = new SqlCommand("createuser", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@username", _combined_models.Username);
                        cmd.Parameters.AddWithValue("@Email", _combined_models._Credencials.Email);
                        cmd.Parameters.AddWithValue("@Password", _combined_models._Credencials.Password);
                        cmd.Parameters.AddWithValue("@phone", _combined_models._Detailes.Phone);
                        cmd.Parameters.AddWithValue("@address", _combined_models._Detailes.Address);
                        cmd.Parameters.AddWithValue("@First_Name", _combined_models._Detailes.First_Name);
                        cmd.Parameters.AddWithValue("@Middle_name", _combined_models._Detailes.Middle_name);
                        cmd.Parameters.AddWithValue("@Last_Name", _combined_models._Detailes.Last_Name);
                        con.Open();
                        id = cmd.ExecuteNonQuery();
                        con.Close();
                        if (id > 0)
                            return true;
                        else if (id == 0)
                            return false;
                        else
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private bool user_exist(string username)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("user_exists", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);
                con.Open();
                id = cmd.ExecuteNonQuery();
                con.Close();
                if (id > 0)
                    return true;
                else if (id == 0)
                    return false;
                else
                    return false;

            }


        }
        public List<user_detailes> get_user_detailes(string username)
        {
            List<user_detailes> _user_detailes = new List<user_detailes>();
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("get_user_detailes", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", username);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    user_detailes _detailes = new user_detailes();
                    _detailes.First_Name = rdr["First_Name"].ToString();
                    _detailes.Middle_name = rdr["Middle_name"].ToString();
                    _detailes.Last_Name = rdr["Last_Name"].ToString();
                    _detailes.Address = rdr["Address"].ToString();
                    _detailes.Phone = rdr["Phone"].ToString();
                    _user_detailes.Add(_detailes);
                }
                con.Close();
            }
            return _user_detailes;
        }
    }
}