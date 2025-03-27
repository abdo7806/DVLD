using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsTestAppointments
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int TestAppointmentID { get; set; }// رقم اموعد لاختبار
        public int TestTypeID { get; set; }// رقم الاختبار
        public int LocalDLA_ID { get; set; }// رقم طلب رخصة جديدة
        public DateTime AppointmentDate { get; set; }// تاريخ موعد الاختبار 
        public float PaidFees { get; set; }// سعر الاختبار الاختبار
        public int CreatedByUserID { get; set; }// رقم المستخدم الذي قام بانشأ الاختبار
        public bool IsLocked { get; set; }// هل هوا موقفل او مستخدم 

        public clsTestAppointments()
        {
            TestAppointmentID = 0;
            TestTypeID = 0;
            LocalDLA_ID = 0;
            AppointmentDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = 0;
            IsLocked = false;
            Mode = enMode.AddNew;
        }


        public clsTestAppointments(int TestAppointmentID, int TestTypeID, int LocalDLA_ID,
            DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDLA_ID = LocalDLA_ID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            Mode = enMode.Update;
        }


        public void GetModeUpdate()
        {
            Mode = enMode.Update;
        }


        // ارجاع كل مواعيد الحجز
        public static DataTable GetAllAppointmentsByIDLocalDLA_ID(int LocalDLA_ID, int TestTypeID)
        {
            return clsTestAppointmentsDataAccess.GetAllAppointmentsByIDLocalDLA_ID(LocalDLA_ID, TestTypeID);
        }


        //اغالق موعد الاختبار 
        public static bool ClosingTheTestDate(int TestAppointmentID)
        {
            return clsTestAppointmentsDataAccess.ClosingTheTestDate(TestAppointmentID);
        }


        //اضافة اختبرات
        private  bool _AddNewTestAppointments()
        {
            this.TestAppointmentID =  clsTestAppointmentsDataAccess.AddNewTestAppointments(this.LocalDLA_ID, this.TestTypeID,
                this.AppointmentDate,this.PaidFees, this.CreatedByUserID);
            return (this.TestAppointmentID > 0);
        }


        // التعديل على تاريخ الاختبار 
        private bool _UpdateTestAppointments()
        {
            return clsTestAppointmentsDataAccess.UpdateTestAppointments(this.TestAppointmentID,
                this.AppointmentDate);
        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointments())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestAppointments();


            }
            return false;
        }


        //هل هناك موعد فعال؟
        public static bool IsThereAnActiveAppointment(int LocalDLA_ID, int TestTypeID)
        {
            return clsTestAppointmentsDataAccess.IsThereAnActiveAppointment(LocalDLA_ID, TestTypeID);
        }


        // البحث عن موعد اختبار
        public static clsTestAppointments Fine(int TestAppointmentID, int TestTypeID)
        {

            int LocalDLA_ID = 0;
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0;
            int CreatedByUserID = 0;
            bool IsLocked = false;


            // البحث عن طلب
            if (clsTestAppointmentsDataAccess.GetTestApplicationByID(TestAppointmentID, TestTypeID, ref LocalDLA_ID,
            ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked))
            {
                return new clsTestAppointments(TestAppointmentID, TestTypeID, LocalDLA_ID,
                AppointmentDate, PaidFees, CreatedByUserID, IsLocked);
            }
            else
            {

                return null;
            }
        }



        // هل لدية اي موعد سابق
        public static bool CountTestAppointmentsByLocalDLA_ID(int LocalDLA_ID)
        {
            return clsTestAppointmentsDataAccess.CountTestAppointmentsByLocalDLA_ID(LocalDLA_ID);
        }




    }
}
    