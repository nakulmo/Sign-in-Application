using Sign_in_Application.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Sign_in_Application.Repository
{
    public class UserRepository
    {
        private SqlConnection sqlconnection;


        ///To Handle connection related activities
        private void Connection()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["GetConnection"].ToString();
            sqlconnection = new SqlConnection(connectionstring);
        }

        /// <summary>
        /// This class is used for sign in connection with database
        /// </summary>
        /// <param name="username"></param>
        /// <returns>connection for getting username and password from database</returns>



        public List<Signin> SigninUser(string username)
        {
            try
            {
                Connection();
                List<Signin> signinlist = new List<Signin>();
                SqlCommand sqlcommand = new SqlCommand("UserSignin", sqlconnection);
                sqlcommand.CommandType = CommandType.StoredProcedure;

                sqlcommand.Parameters.AddWithValue("@Username", username);

                SqlDataAdapter sqldataadapter = new SqlDataAdapter(sqlcommand);
                DataTable datatable = new DataTable();
                sqlconnection.Open();
                sqldataadapter.Fill(datatable);
                sqlconnection.Close();

                // Bind Sign in generic list using dataRow
                foreach (DataRow datarow in datatable.Rows)
                {
                    signinlist.Add(
                        new Signin
                        {
                            Id = Convert.ToInt32(datarow["Id"]),
                            Username = Convert.ToString(datarow["Username"]),
                            Password = (Convert.ToString(datarow["Password"])),
                        }
                    );
                }
                return signinlist;
            }
            catch (Exception ex)
            {
                // handle the exception
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

    }
}