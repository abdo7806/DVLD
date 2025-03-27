using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Data.SqlClient;
using System.Security.Policy;

namespace DVLDDataAccessLayer
{
    public class clsPeopleDataAccess
    {
        // ارجاع كل الاشخاص في النظام
        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName,
                         CASE 
                               WHEN People.Gendor = 0 THEN 'Male' 
                               WHEN People.Gendor = 1 THEN 'Female' 
                               ELSE 'Unknown' 
                           END AS Gendor,
                       People.DateOfBirth, Countries.CountryName as 'Nationality', People.Phone, People.Email
                       FROM     People INNER JOIN
                           Countries ON People.NationalityCountryID = Countries.CountryID";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

            }
            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }

            return dt;
        }



        // ترجع ترو لو كان الرقم الوطني موجود

        public static bool TestNationalNo(string NationalNo)
        {

            int _BookID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @" select * from People
						   where NationalNo =@NationalNo;
                           SELECT SCOPE_IDENTITY();";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);


            try
            {

                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    _BookID = insertedID;
                }

                connection.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return (_BookID > 0);

        }




        //اضافة شخص
        public static int AddNewPerson(string NationalNo, string FirstName,
            string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
            int Gendor, string Address, string Phone, string Email, 
            int NationalityCountryID, string ImagePath)
        {

            int _PeopleID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO People (NationalNo, FirstName,SecondName,ThirdName,LastName,
                             DateOfBirth, Gendor,Address,Phone,Email, NationalityCountryID, ImagePath)
                             VALUES (@NationalNo, @FirstName,@SecondName,@ThirdName,@LastName,
                             @DateOfBirth, @Gendor,@Address,@Phone,@Email,
                             @NationalityCountryID, @ImagePath);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ThirdName != "" && ThirdName != null)
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);


            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);



            try
            {
                connection.Open();

                object result = command.ExecuteScalar();




                if (result != null && int.TryParse(result.ToString(), out int insPeopleID))
                {
                    _PeopleID = insPeopleID;
                }


            }

            catch (Exception ex)
            {


            }

            finally
            {
                connection.Close();
            }



            return _PeopleID;
        }


        // البحث عن شخص
        public static bool GetPersonByID(int PeopleID, ref string NationalNo, ref string FirstName,
            ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
            ref int Gendor, ref string Address, ref string Phone, ref string Email,
            ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from People where PersonID = @PeopleID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PeopleID", PeopleID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;


                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    byte tinyIntValue = reader.GetByte(7);
                    Gendor =  (int)tinyIntValue;
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    Email = (string)reader["Email"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    if(reader["ImagePath"] != System.DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }

                }
                else
                {
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }


        // التعديل على بيانات الشخص
        public static bool UpdatePerson(int PeopleID, string NationalNo, string FirstName,
            string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
            int Gendor, string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            
             string query = @"UPDATE People
                                SET NationalNo = @NationalNo
                                   ,FirstName = @FirstName
                                   ,SecondName = @SecondName
                                   ,ThirdName = @ThirdName
                                   ,LastName = @LastName
                                   ,DateOfBirth = @DateOfBirth
                                   ,Gendor = @Gendor
                                   ,Address = @Address
                                   ,Phone = @Phone
                                   ,Email = @Email
                                   ,NationalityCountryID = @NationalityCountryID
                                   ,ImagePath = @ImagePath
                                where PersonID = @PeopleID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PeopleID", PeopleID);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ThirdName != "" && ThirdName != null)
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);


            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }



        // نفحص لو كان مرطبط بابيانات في النظام
        public static bool IsThisPersonConnectedToTheSystem(int PeopleID)
        {
            int countDrivers = 0;
            int countUsers = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT count(Drivers.PersonID) as 'countDrivers',count(Users.PersonID) as 'countUsers'
FROM     People INNER JOIN
                  Drivers ON People.PersonID = Drivers.PersonID INNER JOIN
                  Users ON People.PersonID = Users.PersonID 

				  where People.PersonID =@PeopleID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PeopleID", PeopleID);
        


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {



                    countDrivers = (int)reader["countDrivers"];
                    countUsers = (int)reader["countUsers"];



                }


            }

            catch (Exception ex)
            {


            }

            finally
            {
                connection.Close();
            }

            return ((countDrivers + countUsers) > 0);
        }



        // حذف شخص
        public static bool DeletePeople(int PeopleID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @" Delete FROM People
                         where PersonID =@PeopleID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PeopleID", PeopleID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {

                connection.Close();

            }

            return (rowsAffected > 0);

        }



        // البحث عن شخص من خلال رقمة الوطني
        public static int GetNationalNo(string NationalNo)
        {
            int _PersonID = -1;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select PersonID from People where NationalNo = @NationalNo;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
              //  SqlDataReader reader = command.ExecuteReader();


                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insPeopleID))
                {
                    _PersonID = insPeopleID;
                }



            }
            catch (Exception ex)
            {
                connection.Close();
            }
            finally
            {
                connection.Close();
            }
            return _PersonID;
        }


    }
}
