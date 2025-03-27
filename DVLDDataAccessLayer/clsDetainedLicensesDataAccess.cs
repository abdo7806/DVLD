using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsDetainedLicensesDataAccess
    {
        // ارجاع كل الرخص المحجوزة
        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT DetainedLicenses.DetainID, DetainedLicenses.LicenseID, DetainedLicenses.DetainDate,
DetainedLicenses.IsReleased, DetainedLicenses.FineFees, DetainedLicenses.ReleaseDate, People.NationalNo,
(People.FirstName+ ' '+People.SecondName+ ' '+ People.ThirdName+ ' '+ People.LastName) as 'Full Name',
                  Applications.ApplicationID as 'ReleaseApplicationID' 
FROM     DetainedLicenses left JOIN
                  Applications ON DetainedLicenses.ReleaseApplicationID = Applications.ApplicationID
				  INNER JOIN Licenses on Licenses.LicenseID = DetainedLicenses.LicenseID
				  INNER JOIN Drivers on Drivers.DriverID = Licenses.DriverID
				  INNER JOIN People on People.PersonID = Drivers.PersonID;";

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


        // اضافة رخصة محجوزة
        public static int AddNewDetainedLicense(int LicenseID, DateTime DetainDate,
            float FineFees, int CreatedByUserID, bool IsReleased)
        {

            int _DetainID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO DetainedLicenses
           (LicenseID
           ,DetainDate
           ,FineFees
           ,CreatedByUserID
           ,IsReleased
           ,ReleaseDate
           ,ReleasedByUserID
           ,ReleaseApplicationID)
     VALUES
           (@LicenseID
           ,@DetainDate
           ,@FineFees
           ,@CreatedByUserID 
           ,@IsReleased
           ,null
           ,null
           ,null);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);




            try
            {
                connection.Open();

                object result = command.ExecuteScalar();




                if (result != null && int.TryParse(result.ToString(), out int insDetainID))
                {
                    _DetainID = insDetainID;
                }


            }

            catch (Exception ex)
            {


            }

            finally
            {
                connection.Close();
            }



            return _DetainID;
        }

        // البحث عن رخصة محجوزة

        public static bool GetDetainedID(ref int DetainID, int LicenseID, ref DateTime DetainDate,
            ref float FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate,
            ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from DetainedLicenses
                     where LicenseID = @LicenseID and IsReleased = 0;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;




                    DetainID = (int)reader["DetainID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = (float)(decimal)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];


                  
                    if (reader["ReleaseDate"] != System.DBNull.Value)
                    {
                        ReleaseDate = (DateTime)reader["ReleaseDate"];
                    }

                    if (reader["ReleasedByUserID"] != System.DBNull.Value)
                    {
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];
                    }

                    if (reader["ReleaseApplicationID"] != System.DBNull.Value)
                    {
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];

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



        //هل تم الافراج عن الاعتقال
        public static bool IsTheDetainIsReleased(int LicenseID)
        {


            int _LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @" select count(*) from DetainedLicenses
                      where LicenseID = @LicenseID and IsReleased = 0";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();


                object result = command.ExecuteScalar();




                if (result != null && int.TryParse(result.ToString(), out int insLicenseID))
                {
                    _LicenseID = insLicenseID;
                }

            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {

                connection.Close();

            }

            return (_LicenseID > 0);
        }



        // التعديل على الرخصة المحجوزة
        public static bool UpdateDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate,
            float FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate,
            int ReleasedByUserID, int ReleaseApplicationID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"UPDATE DetainedLicenses
                    SET LicenseID = @LicenseID
                       ,DetainDate = @DetainDate
                       ,FineFees = @FineFees 
                       ,CreatedByUserID = @CreatedByUserID
                       ,IsReleased = @IsReleased
                       ,ReleaseDate = @ReleaseDate
                       ,ReleasedByUserID = @ReleasedByUserID
                       ,ReleaseApplicationID = @ReleaseApplicationID
                  WHERE DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
            command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);



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



    }
}
