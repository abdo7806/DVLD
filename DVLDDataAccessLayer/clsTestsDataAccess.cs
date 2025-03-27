using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDDataAccessLayer
{
    public class clsTestsDataAccess
    {

        // اضافة اختبار
        public static int AddNewTest(int TestAppointmentID,
            bool TestResult, string Notes, int CreatedByUserID)
        {

            int _TestID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Tests
       (TestAppointmentID, TestResult,Notes, CreatedByUserID)
        VALUES
	      (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID)
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


            if (Notes != "" && Notes != null)
                command.Parameters.AddWithValue("@Notes", Notes);

            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);





            try
            {
                connection.Open();

                object result = command.ExecuteScalar();




                if (result != null && int.TryParse(result.ToString(), out int insTestID))
                {
                    _TestID = insTestID;
                }


            }

            catch (Exception ex)
            {


            }

            finally
            {
                connection.Close();
            }



            return _TestID;
        }


        //هل هناك موعد ناجح

        public static int DidHeFailTheTest(int LocalDLA_ID, int TestTypeID)
        {
            int CountRows = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"SELECT count(*) 
         FROM     LocalDrivingLicenseApplications INNER JOIN
                  TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                  Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
				  where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDLA_ID
                   and TestResult = 1 and TestAppointments.TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDLA_ID", LocalDLA_ID);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);



            try
            {
                connection.Open();
                object result = command.ExecuteScalar();




                if (result != null && int.TryParse(result.ToString(), out int insTestAppointmentID))
                {
                    CountRows = insTestAppointmentID;
                }


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
               
            }

            finally
            {
                connection.Close();
            }

            return CountRows;
        }



        // البحث عن  اختبار 
        public static bool GetTestByID(ref int TestID, int TestAppointmentID,
            ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from Tests
                      where TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;

                    TestID = (int)reader["TestID"];
                    TestResult = (bool)reader["TestResult"];
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


        // عدد الاختبارات التي رسب فيها
        public static int TheNumberOfTestsHeFailed(int LocalDLA_ID, int TestTypeID)
        {
            int CountRows = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"SELECT count(*) 
         FROM     LocalDrivingLicenseApplications INNER JOIN
                  TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                  Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
				  where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDLA_ID
                   and TestResult = 0 and TestAppointments.TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDLA_ID", LocalDLA_ID);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);



            try
            {
                connection.Open();
                object result = command.ExecuteScalar();




                if (result != null && int.TryParse(result.ToString(), out int insTestAppointmentID))
                {
                    CountRows = insTestAppointmentID;
                }


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }

            return CountRows;
        }


    }
}
