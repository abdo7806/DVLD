using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsInternationalLicensesDataAccess
    {
        // ارجاع كل الرخص الدولية في لشخص معين
        public static DataTable GetAllInternationalLicenseByID(int DriverID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT InternationalLicenseID as 'Int License ID', ApplicationID as 'Application ID', 
                     IssuedUsingLocalLicenseID as 'L.License ID', IssueDate as 'Issue Date', ExpirationDate as 'Expiration Date', 
                     IsActive as 'Is Active'
                     FROM     InternationalLicenses
                     where DriverID = @DriverID";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

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




        // ارجاع رجاع رخصة دولية حسب رقمها
        public static bool GetInternationalLicenseByID(ref int InternationalLicenseID, ref int ApplicationID, ref int DriverID, int LicenseID,
            ref DateTime IssueDate, ref DateTime ExpirationDate,
            ref bool IsActive,  ref int CreatedByUserID)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from InternationalLicenses	
                             where IssuedUsingLocalLicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;


                    InternationalLicenseID = (int)reader["InternationalLicenseID"];
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];


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




        //اضافة رخصة دولية 
        public static int AddNewInternationalLicense(int ApplicationID, int DriverID, int LicenseID,
            DateTime IssueDate, DateTime ExpirationDate,
            bool IsActive, int CreatedByUserID)
        {

            int _InternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO InternationalLicenses
        (ApplicationID
           ,DriverID
           ,IssuedUsingLocalLicenseID
           ,IssueDate
           ,ExpirationDate
           ,IsActive
           ,CreatedByUserID)
     VALUES
           (@ApplicationID
           , @DriverID
           , @LicenseID
           , @IssueDate
           , @ExpirationDate
           , @IsActive
           , @CreatedByUserID)
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();




                if (result != null && int.TryParse(result.ToString(), out int insLicenseID))
                {
                    _InternationalLicenseID = insLicenseID;
                }


            }

            catch (Exception ex)
            {


            }

            finally
            {
                connection.Close();
            }



            return _InternationalLicenseID;
        }



        // ارجاع كل الرخص الدولية 
        public static DataTable GetAllInternationalLicense()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT InternationalLicenseID as 'Int_LicenseID', ApplicationID as 'Application_ID', DriverID,
                     IssuedUsingLocalLicenseID as 'L_LicenseID', IssueDate as 'Issue Date', ExpirationDate as 'Expiration Date', 
                     IsActive as 'Is Active'
                     FROM     InternationalLicenses;";


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



        //تعديل رقم الرخصة في الرخصة الدولية
        public static bool EditingTheLicenseNumberInTheInternationalLicense(int NewLicenseID, int OldLicenseID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @" update InternationalLicenses
                           set IssuedUsingLocalLicenseID = @NewLicenseID
                           where IssuedUsingLocalLicenseID = @OldLicenseID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NewLicenseID", NewLicenseID);
            command.Parameters.AddWithValue("@OldLicenseID", OldLicenseID);

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


    }
}
