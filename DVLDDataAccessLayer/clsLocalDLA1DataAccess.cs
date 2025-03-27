using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLDDataAccessLayer
{
    public class clsLocalDLA1DataAccess
    {
       


        //اضافة طلب
        public static int AddNewLocalDLA(int ApplicationID, int LicenseClassID)
        {

            int LocalDLA_ID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO LocalDrivingLicenseApplications
           (ApplicationID
           ,LicenseClassID)
     VALUES
           (@ApplicationID
           ,@LicenseClassID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);



            try
            {
                connection.Open();

                object result = command.ExecuteScalar();




                if (result != null && int.TryParse(result.ToString(), out int insLocalDLA_ID))
                {
                    LocalDLA_ID = insLocalDLA_ID;
                }


            }

            catch (Exception ex)
            {


            }

            finally
            {
                connection.Close();
            }



            return LocalDLA_ID;
        }





        //هل لديك رخصة قيادة من هذا النوع؟
        public static bool DoYouHaveDrivingLicenseOfThisType(int PersonID, int LicenseClassID, int ApplicationStatus)
        {
            //int rowsAffected = -1;

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT *
             FROM LocalDrivingLicenseApplications INNER JOIN
                  Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
				  where Applications.ApplicantPersonID = @PersonID and
                              LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
				  and Applications.ApplicationStatus != @ApplicationStatus";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);

            try
            {
                connection.Open();

                // rowsAffected = command.ExecuteNonQuery();

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
                isFound = true;

            }
            finally
            {
                connection.Close();
            }
            return isFound;
            
        }



        // البحث عن طلب الرخصة الجديدة من خلال الرقم
        public static bool GetLocalDLAByLocalDLA_ID(int LocalDLA_ID, ref int ApplicationID, ref int LicenseClassID)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @" select * from  LocalDrivingLicenseApplications
                where LocalDrivingLicenseApplicationID= @LocalDLA_ID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDLA_ID", LocalDLA_ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;


                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
              


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




        // البحث عن طلب الرخصة الجديدة من خلال الرقم الطلب
        public static bool GetLocalDLAByApplicationID(ref int LocalDLA_ID, int ApplicationID, ref int LicenseClassID)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @" select * from  LocalDrivingLicenseApplications
                where ApplicationID= @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;


                    LocalDLA_ID = (int)reader["LocalDrivingLicenseApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];



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


        // التعديب على طلب رخية جديدة
        public static bool UpdateLocalDLA(int LocalDLA_ID, int ApplicationID, int LicenseClassID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"UPDATE LocalDrivingLicenseApplications
                                SET ApplicationID = @ApplicationID
                                   ,LicenseClassID = @LicenseClassID
          
                                where LocalDrivingLicenseApplicationID = @LocalDLA_ID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDLA_ID", LocalDLA_ID);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);



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



        // حذف طلب رخصة جديدة
        public static bool DeleteLocalDLA1(int LocalDLA_ID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @" Delete from LocalDrivingLicenseApplications 
                              where LocalDrivingLicenseApplicationID = @LocalDLA_ID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDLA_ID", LocalDLA_ID);

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
