using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsLicensesDataAccess
    {
        //اضافة رخصة
        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClass,
            DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees,
            bool IsActive, int IssueReason, int CreatedByUserID)
        {

            int _LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Licenses
        (ApplicationID
           ,DriverID
           ,LicenseClass
           ,IssueDate
           ,ExpirationDate
           ,Notes
           ,PaidFees
           ,IsActive
           ,IssueReason
           ,CreatedByUserID)
     VALUES
           (@ApplicationID
           , @DriverID
           , @LicenseClass
           , @IssueDate
           , @ExpirationDate
           , @Notes
           , @PaidFees
           , @IsActive
           , @IssueReason
           , @CreatedByUserID)
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            if (Notes != "" && Notes != null)
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);



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


            }

            finally
            {
                connection.Close();
            }



            return _LicenseID;
        }




        // ارجاع بيانات الرحصة حسب رقم الطلب ApplicationID
        public static bool GetLicenseByApplicationID(ref int LicenseID, int ApplicationID, ref int DriverID, ref int LicenseClass,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref float PaidFees,
            ref bool IsActive, ref int IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from Licenses
                            where ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;


                    LicenseID = (int)reader["LicenseID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    PaidFees = (float)(decimal)reader["PaidFees"];
                    IsActive = (bool)reader["IsActive"];
                    byte tinyIntValue = reader.GetByte(9);
                   
                    IssueReason = (int)tinyIntValue;
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    if (reader["Notes"] != System.DBNull.Value)
                    {
                        Notes = (string)reader["Notes"];

                    }
                    else
                    {
                        Notes = "";
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





        // ارجاع كل الرخص في لشخص معين
        public static DataTable GetAllLicenseByID(int DriverID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            /* string query = @"SELECT Licenses.LicenseID as 'Lic.ID', Licenses.ApplicationID as 'App.ID', LicenseClasses.ClassName as 'Class Name',
               Licenses.IssueDate as 'Issue Date', Licenses.ExpirationDate as 'Expiration Date', Licenses.IsActive as 'Is Active'
               FROM     Licenses INNER JOIN
                   LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
                   where Licenses.LicenseID NoT in(
                  SELECT IssuedUsingLocalLicenseID FROM InternationalLicenses
                  )
                  and Licenses.DriverID = @DriverID";*/

            string query = @"SELECT Licenses.LicenseID as 'Lic.ID', Licenses.ApplicationID as 'App.ID', LicenseClasses.ClassName as 'Class Name',
              Licenses.IssueDate as 'Issue Date', Licenses.ExpirationDate as 'Expiration Date', Licenses.IsActive as 'Is Active'
              FROM     Licenses INNER JOIN
                  LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
				  where Licenses.DriverID = @DriverID";


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


        // ارجاع رخصة حسب رقمة
        public static bool GetLicenseByLicenseID(int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClass,
           ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref float PaidFees,
           ref bool IsActive, ref int IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from Licenses
                            where LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;


                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    PaidFees = (float)(decimal)reader["PaidFees"];
                    IsActive = (bool)reader["IsActive"];
                    byte tinyIntValue = reader.GetByte(9);

                    IssueReason = (int)tinyIntValue;
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    if (reader["Notes"] != System.DBNull.Value)
                    {
                        Notes = (string)reader["Notes"];

                    }
                    else
                    {
                        Notes = "";
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



        //هل الرخصة منتهية الصلاحية؟
        public static bool IsTheLicenseExpired(int LicenseID)
        {
           

            int _LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @" select count(*) from Licenses
                        where LicenseID = @LicenseID and ExpirationDate < GETDATE();";

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



        //جعل الترخيص غير نشط
        public static bool MakeTheLicenseNotActive(int LicenseID)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"update Licenses 
                              set  
                           IsActive = 0
		                   where LicenseID = @LicenseID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;


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


        //هل الترخيص فعال؟

        public static bool IsTheLicenseIsActive(int LicenseID)
        {


            int _LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @" select count(*) from Licenses
                          where IsActive = 1 and LicenseID = @LicenseID";

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


    }
}
