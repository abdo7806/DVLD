using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsLocalDLA_ViewDataAccuss
    {
        // ارجاع كل طلبات رخص القيادة الجديدة
        public static DataTable GetAllLocalDLA()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select LocalDrivingLicenseApplicationID as 'L.D.LAPPID', ClassName as 'Driving Class', 
                         NationalNo, FullName, ApplicationDate as 'Application Date', 
                         PassedTestCount as 'Passed Test', 
                         Status  from LocalDrivingLicenseApplications_View;";

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

        // البحث عن طلب
        public static bool GetApplicationByID(int LocalDLA_ID, ref string ClassName, ref string NationalNo,
            ref string FullName, ref DateTime ApplicationDate, ref int PassedTestCount, ref string Status)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from  LocalDrivingLicenseApplications_View
where LocalDrivingLicenseApplicationID = @LocalDLA_ID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDLA_ID", LocalDLA_ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;

                    ClassName = (string)reader["ClassName"];
                    NationalNo = (string)reader["NationalNo"];
                    FullName = (string)reader["FullName"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    PassedTestCount = (int)reader["PassedTestCount"];
                    Status = (string)reader["Status"];


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
    }
}
