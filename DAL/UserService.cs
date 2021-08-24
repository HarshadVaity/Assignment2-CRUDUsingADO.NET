using DataModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class UserService
    {
        private string ConnectionString { get; set; }
        public UserService(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public List<User> GetAllUsers()
        {
            List<User> userList = new List<User>();
            using  (SqlConnection connection=new SqlConnection(this.ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("USPGetAllUsers", connection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                using(SqlDataReader dataReader= sqlCommand.ExecuteReader())
                {
                    while(dataReader.Read())
                    {
                        User user = new User();
                        user.Id = Convert.ToInt32(dataReader["Id"]);
                        user.FirstName = Convert.ToString(dataReader["First Name"]);
                        user.LastName = Convert.ToString(dataReader["Last Name"]);

                        State state = new State();

                        state.Id = Convert.ToInt32(dataReader["StateId"]);
                        state.Name = Convert.ToString(dataReader["State Name"]);

                        user.State = state;

                        userList.Add(user);

                    }
                }
            }
            return userList;
        }
        public List<State> GetAllStates()
        {
            List<State> StateList = new List<State>();
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM UDFGetAllStates()", connection);
                sqlCommand.CommandType = System.Data.CommandType.Text;
                connection.Open();
                using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        State state = new State();
                        state.Id = Convert.ToInt32(dataReader["Id"]);
                        state.Name = Convert.ToString(dataReader["Name"]);
                        StateList.Add(state);

                    }
                }
            }
            return StateList;
        }

        public bool CreateUser(User model)
        {
            var returnVal=0;
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("USPCreateUser", connection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@FirstName", model.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", model.LastName);
                sqlCommand.Parameters.AddWithValue("@StateId", model.State.Id);


                connection.Open();
                returnVal = sqlCommand.ExecuteNonQuery();
            }

            return returnVal > 0 ? true : false;
        }

        public User GetUser(int id)
        {
            User  user = new User();
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("USPGetUser", connection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserId",id);

                connection.Open();
                using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                       
                        user.Id = Convert.ToInt32(dataReader["Id"]);
                        user.FirstName = Convert.ToString(dataReader["First Name"]);
                        user.LastName = Convert.ToString(dataReader["Last Name"]);

                        State state = new State();

                        state.Id = Convert.ToInt32(dataReader["StateId"]);
                        state.Name = Convert.ToString(dataReader["State Name"]);

                        user.State = state;

                      

                    }
                }
            }
            return user;
        }

        public bool UpdateUser(User model)
        {
            var returnVal = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("USPUpdateUser", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", model.Id);
                    sqlCommand.Parameters.AddWithValue("@FirstName", model.FirstName);
                    sqlCommand.Parameters.AddWithValue("@LastName", model.LastName);
                    sqlCommand.Parameters.AddWithValue("@StateId", model.State.Id);


                    connection.Open();
                    returnVal = sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            

            return returnVal > 0 ? true : false;
        }

        public bool DeleteUser(int id)
        {
            var returnVal = 0;
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("USPDeleteUser", connection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@UserId", id);
                connection.Open();
                returnVal = sqlCommand.ExecuteNonQuery();
            }

            return returnVal > 0 ? true : false;
        }
    }
}
