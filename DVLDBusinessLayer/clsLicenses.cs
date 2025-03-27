using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsLicenses
    {
        public int LicenseID { get; set; }// رقم الرخصة
        public int ApplicationID { get; set; }// معرف رقم الطلب
        public int DriverID { get; set; }// معرف السائق
        public int LicenseClass { get; set; }// معرف فئه الترخيص
        public DateTime IssueDate { get; set; }// تاريخ الاصدار
        public DateTime ExpirationDate { get; set; }// تاريخ الانتهاء
        public string Notes { get; set; }// ملاحظة
        public float PaidFees { get; set; }// الرسوم المدفوعة
        public bool IsActive { get; set; }// هل هوا ناشط
        public int IssueReason { get; set; }// سسبب المشكلة
        public int CreatedByUserID { get; set; }// معرف المستخدم الذي قام بانشأ الرخصة

        public clsLicenses()
        {
            this.LicenseID = 0;
            this.ApplicationID = 0;
            this.DriverID = 0;
            this.LicenseClass = 0;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = true;
            this.IssueReason = 1;
            this.CreatedByUserID = 0;


        }

        public clsLicenses(int LicenseID, int ApplicationID, int DriverID, int LicenseClass,
            DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees,
            bool IsActive, int IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
        }


        //اضافة رخصة
        public bool AddNewLicense()
        {
            //call DataAccess Layer 
           
            this.LicenseID = clsLicensesDataAccess.AddNewLicense(this.ApplicationID, this.DriverID, 
                this.LicenseClass,  this.IssueDate, this.ExpirationDate,  this.Notes, this.PaidFees,
                this.IsActive, this.IssueReason, this.CreatedByUserID);

            return (this.LicenseID != -1);
        }


        // ارجاع بيانات الرحصة حسب رقم الطلب ApplicationID
        public static clsLicenses FindApplicationID(int ApplicationID)
        {
            int LicenseID = 0;
            int DriverID = 0;
            int LicenseClass = 0;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = true;
            int IssueReason = 0;
            int CreatedByUserID = 0;

            if (clsLicensesDataAccess.GetLicenseByApplicationID(ref LicenseID, ApplicationID, ref DriverID, ref LicenseClass,
            ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees,
            ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicenses( LicenseID, ApplicationID, DriverID, LicenseClass,
            IssueDate, ExpirationDate, Notes, PaidFees,
             IsActive, IssueReason, CreatedByUserID);
            }
            else
            {

                return null;
            }

        }



        // ارجاع كل الرخص في لشخص معين
        public static DataTable GetAllLicenseByID(int DriverID)
        {
            return clsLicensesDataAccess.GetAllLicenseByID(DriverID);
        }




        // ارجاع رخصة حسب رقمة
        public static clsLicenses Find(int LicenseID)
        {
            int ApplicationID = 0;
            int DriverID = 0;
            int LicenseClass = 0;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = true;
            int IssueReason = 0;
            int CreatedByUserID = 0;

            if (clsLicensesDataAccess.GetLicenseByLicenseID( LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass,
            ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees,
            ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicenses(LicenseID, ApplicationID, DriverID, LicenseClass,
            IssueDate, ExpirationDate, Notes, PaidFees,
             IsActive, IssueReason, CreatedByUserID);
            }
            else
            {

                return null;
            }

        }



        //هل الرخصة منتهية الصلاحية؟

        public static bool IsTheLicenseExpired(int LicenseID)
        {
            return clsLicensesDataAccess.IsTheLicenseExpired(LicenseID);
        }



        //جعل الترخيص غير نشط

       public static bool MakeTheLicenseNotActive(int LicenseID)
        {
            return clsLicensesDataAccess.MakeTheLicenseNotActive(LicenseID);
        }


        //هل الترخيص فعال؟

        public static bool IsTheLicenseIsActive(int LicenseID)
        {
            return clsLicensesDataAccess.IsTheLicenseIsActive(LicenseID);
        }

    }
}
