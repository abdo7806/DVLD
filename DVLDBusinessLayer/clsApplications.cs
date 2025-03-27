using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsApplications
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int ApplicationID { get; set; }// رقم الطلب
        public int ApplicantPersonID { get; set; }// معرف الشخص
        public DateTime ApplicationDate { get; set; }//تاريخ التقديم
        public int ApplicationTypeID { get; set; }// معرف نوع الطلب
        public int ApplicationStatus { get; set; }// حالة الطلب
        public DateTime LastStatusDate { get; set; }// تريخ الحالة الاخيرة
        public float PaidFees { get; set; }//  الرسوم المدفعة
        public int CreatedByUserID { get; set; }// معرف المستخدم الذي قام بانشأة

        public clsApplications()
        {
            ApplicationID = 0;
            ApplicantPersonID = 0;
            ApplicationDate = DateTime.Now;
            ApplicationTypeID = 0;
            ApplicationStatus = 0;
            LastStatusDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = 0;
            Mode = enMode.AddNew;
        }

        public clsApplications(int ApplicationID,int ApplicantPersonID, 
            DateTime ApplicationDate,int ApplicationTypeID, int ApplicationStatus,
            DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            Mode = enMode.Update;
        }

        //اضافة طلب'
        private bool _AddNewApplication()
        {
            //call DataAccess Layer 

            this.ApplicationID = clsApplicationsAccess.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate,
            this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate, this.PaidFees,
            this.CreatedByUserID);



            return (this.ApplicationID != -1);
        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return true;// _UpdatePerson();


            }

            return false;
        }



        // الاغاء الطلب
        public static bool CancelApplication(int LocalDLA_ID)
        {
            int ApplicationID = clsLocalDLA1.Find(LocalDLA_ID).ApplicationID;

            return clsApplicationsAccess.CancelApplication(ApplicationID);
        }


        // البحث عن طلب
        public static clsApplications Fine(int ApplicationID)
        {
            int ApplicantPersonID = 0;
            int ApplicationTypeID = 0;
            DateTime ApplicationDate = DateTime.Now;
            byte ApplicationStatus = 0;

            DateTime LastStatusDate = DateTime.Now;
            float PaidFees = 0;
            int CreatedByUserID = 0;

            // البحث عن طلب
            if (clsApplicationsAccess.GetApplicationByID(ApplicationID, ref ApplicantPersonID,
            ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus,
            ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
            {
                return new clsApplications(ApplicationID, ApplicantPersonID,
             ApplicationDate, ApplicationTypeID, ApplicationStatus,
             LastStatusDate, PaidFees, CreatedByUserID);
            }
            else
            {

                return null;
            }
        }



        // جعل الطلب مكتمل
        public static bool CompletedApplication(int ApplicationID)
        {
            return clsApplicationsAccess.CompletedApplication(ApplicationID);
        }



        //هل تم الانتهاء من التطبيق
        public static bool IsApplicationCompleted(int ApplicationID)
        {
            return clsApplicationsAccess.IsApplicationCompleted(ApplicationID);
        }



        // حذف طالب  
        public static bool DeleteApplication(int ApplicationID)
        {
            return clsApplicationsAccess.DeleteApplication(ApplicationID);
        }


    }
}
