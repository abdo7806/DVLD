using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsTests
    {
        public int TestID { get; set; }// رقم الاختبار
        public int TestAppointmentID { get; set; }// رقم اموعد لاختبار
        public bool TestResult { get; set; }// نتيجة الاختبار
        public string Notes { get; set; }// ملاحظة
        public int CreatedByUserID { get; set; }// رقم المستخدم الذي قام بانشأ الاختبار


        public clsTests()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = true;
            this.Notes = "";
            this.CreatedByUserID = -1;
        }

        public clsTests(int TestID, int TestAppointmentID, 
            bool TestResult, string Notes, int CreatedByUserID)
        {
             this.TestID = TestID;
             this.TestAppointmentID = TestAppointmentID;
             this.TestResult = TestResult;
             this.Notes = Notes;
             this.CreatedByUserID = CreatedByUserID;
        }


        // اضافة اختبار
        public bool AddNewTest()
        {

            this.TestID = clsTestsDataAccess.AddNewTest(this.TestAppointmentID, this.TestResult,
            this.Notes, this.CreatedByUserID);

            return (this.TestID != -1);
        }


  
        //هل هناك موعد ناجح
        public static int DidHeFailTheTest(int LocalDLA_ID, int TestTypeID)
        {
            return clsTestsDataAccess.DidHeFailTheTest(LocalDLA_ID, TestTypeID);
        }



        // البحث عن اختبار
        public static clsTests Fine(int TestAppointmentID)
        {

            int TestID = -1;

            bool TestResult = false;
            string Notes = "";
            int CreatedByUserID = -1;

            // البحث عن طلب
            if (clsTestsDataAccess.GetTestByID(ref TestID, TestAppointmentID, ref TestResult,
            ref Notes, ref CreatedByUserID))
            {
                return new clsTests(TestID, TestAppointmentID, TestResult,
                 Notes, CreatedByUserID);
            }
            else
            {

                return null;
            }
        }



        // عدد الاختبارات التي رسب فيها
        public static int TheNumberOfTestsHeFailed(int LocalDLA_ID, int TestTypeID)
        {
            return clsTestsDataAccess.TheNumberOfTestsHeFailed(LocalDLA_ID, TestTypeID);
        }

    }
}
