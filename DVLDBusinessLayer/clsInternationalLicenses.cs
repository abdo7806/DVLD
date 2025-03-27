using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsInternationalLicenses
    {
        public int InternationalLicenseID { get; set; }// رقم الرخصة
        public int ApplicationID { get; set; }// معرف رقم الطلب
        public int DriverID { get; set; }// معرف السائق
        public int LicenseID { get; set; }// معرف فئه الترخيص
        public DateTime IssueDate { get; set; }// تاريخ الاصدار
        public DateTime ExpirationDate { get; set; }// تاريخ الانتهاء
        public bool IsActive { get; set; }// هل هوا ناشط
        public int CreatedByUserID { get; set; }// معرف المستخدم الذي قام بانشأ الرخصة

        public clsInternationalLicenses()
        {
            this.InternationalLicenseID = 0;
            this.ApplicationID = 0;
            this.DriverID = 0;
            this.LicenseID = 0;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = true;
            this.CreatedByUserID = 0;


        }

        public clsInternationalLicenses(int InternationalLicenseID, int ApplicationID, int DriverID, int LicenseID,
            DateTime IssueDate, DateTime ExpirationDate, 
            bool IsActive, int CreatedByUserID)
        {
            
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseID = LicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
        }


        // ارجاع كل الرخص الدولية في لشخص معين
        public static DataTable GetAllInternationalLicenseByID(int DriverID)
        {
            return clsInternationalLicensesDataAccess.GetAllInternationalLicenseByID(DriverID);
        }




        // ارجاع رجاع رخصة دولية حسب رقمها
        public static clsInternationalLicenses Find(int LicenseID)
        {

            int InternationalLicenseID = 0;
            int ApplicationID = 0;
            int DriverID = 0;
            
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = true;
            int IssueReason = 0;
            int CreatedByUserID = 0;

            if (clsInternationalLicensesDataAccess.GetInternationalLicenseByID(ref InternationalLicenseID, ref ApplicationID, 
                ref DriverID,  LicenseID,
            ref IssueDate, ref ExpirationDate,
            ref IsActive, ref CreatedByUserID))
            {
                return new clsInternationalLicenses(InternationalLicenseID, ApplicationID,
                 DriverID, LicenseID,
             IssueDate,  ExpirationDate,
             IsActive,  CreatedByUserID);
            }
            else
            {

                return null;
            }

        }



        //اضافة رخصة
        public bool AddNewInternationalLicense()
        {
            //call DataAccess Layer 

            this.InternationalLicenseID = clsInternationalLicensesDataAccess.AddNewInternationalLicense(this.ApplicationID,
                this.DriverID, this.LicenseID,
             this.IssueDate, this.ExpirationDate,
             this.IsActive, this.CreatedByUserID);

            return (this.InternationalLicenseID != -1);
        }


        // ارجاع كل الرخص الدولية 
        public static DataTable GetAllInternationalLicense()
        {
            return clsInternationalLicensesDataAccess.GetAllInternationalLicense();
        }


        //تعديل رقم الرخصة في الرخصة الدولية
        public static bool EditingTheLicenseNumberInTheInternationalLicense(int NewLicenseID, int OldLicenseID)
        {
            return clsInternationalLicensesDataAccess.EditingTheLicenseNumberInTheInternationalLicense(NewLicenseID, OldLicenseID);
        }

    }
}
