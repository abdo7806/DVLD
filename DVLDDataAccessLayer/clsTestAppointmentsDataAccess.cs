using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsTestAppointmentsDataAccess
    {

    
            public static DataTable GetAllAppointmentsByIDLocalDLA_ID(int LocalDLA_ID, int TestTypeID)
            {
                DataTable dt = new DataTable();
                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            /*  string query = @"SELECT TestAppointmentID, AppointmentDate, PaidFees, IsLocked
             FROM     TestAppointments_View
                  where LocalDrivingLicenseApplicationID = @LocalDLA_ID and TestTypeTitle = @TestTypeTitle;";*/

            string query = @"select TestAppointmentID as 'AppointmentID', AppointmentDate as 'Appointment Date', 
            PaidFees as 'Paid Fees', IsLocked as 'Is Locked' from TestAppointments
            where LocalDrivingLicenseApplicationID = @LocalDLA_ID and TestTypeID = @TestTypeID;";
                SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDLA_ID", LocalDLA_ID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


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



            //اضافة اختبرات
            public static int AddNewTestAppointments(int LocalDLA_ID, int TestTypeID,
            DateTime AppointmentDate, float PaidFees, int CreatedByUserID)
        {

            int TestAppointmentID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"insert into TestAppointments (TestTypeID
           ,LocalDrivingLicenseApplicationID
           ,AppointmentDate
           ,PaidFees
           ,CreatedByUserID
           ,IsLocked)
      VALUES 
            (@TestTypeID,@LocalDLA_ID,@AppointmentDate,
            @PaidFees,
            @CreatedByUserID,0);
               SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDLA_ID", LocalDLA_ID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);



            try
            {



                connection.Open();

                object result = command.ExecuteScalar();




                if (result != null && int.TryParse(result.ToString(), out int insTestAppointmentID))
                {
                    TestAppointmentID = insTestAppointmentID;
                }

            }

            catch (Exception ex)
            {


            }

            finally
            {
                connection.Close();
            }



            return TestAppointmentID;
        }


        //اغالق موعد الاختبار 
        public static bool ClosingTheTestDate(int TestAppointmentID)
        {

            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"update TestAppointments 
              set  
           IsLocked = 1 
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

        // التعديل على تاريخ الاختبار 
        public static bool UpdateTestAppointments(int TestAppointmentID, DateTime AppointmentDate)
        {

            int _TestAppointmentID = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"update TestAppointments 
              set  
           AppointmentDate = @AppointmentDate
                  where TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
           


            try
            {
                connection.Open();
                _TestAppointmentID = command.ExecuteNonQuery();

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

            return (_TestAppointmentID > 0);
        }



        //هل هناك موعد فعال؟
        public static bool IsThereAnActiveAppointment(int LocalDLA_ID, int TestTypeID)
        {
            int CountRows = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"select count(*) from TestAppointments 
                   where LocalDrivingLicenseApplicationID = @LocalDLA_ID 
                       and TestTypeID = @TestTypeID and IsLocked = 0;";

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
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (CountRows > 0);
        }



        // البحث عن موعد اختبار 
        public static bool GetTestApplicationByID(int TestAppointmentID, int TestTypeID, ref int LocalDLA_ID,
            ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID, ref bool IsLocked)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from TestAppointments 
                     where TestAppointmentID = @TestAppointmentID 
                and TestTypeID = @TestTypeID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;

                    LocalDLA_ID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = (float)(decimal)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];


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






        // هل لدية اي موعد سابق
        public static bool CountTestAppointmentsByLocalDLA_ID(int LocalDLA_ID)
        {
            int CountRows = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"select count(*) from TestAppointments
                      where LocalDrivingLicenseApplicationID = @LocalDLA_ID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDLA_ID", LocalDLA_ID);




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
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (CountRows > 0);
        }

    }
}
