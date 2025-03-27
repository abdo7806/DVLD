using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsDetainedLicenses
    {
        public int DetainID { get; set; }// ID
        public int LicenseID { get; set; }// رقم الرخصة
        public DateTime DetainDate { get; set; }// تاريخ الحجز
        public float FineFees { get; set; }// مبلغ الغرامة
        public int CreatedByUserID { get; set; }// تم الانشائ بواسكة معرف المستخدم
        public bool IsReleased { get; set; }// هل هاي محجوزه او لا
        public DateTime ReleaseDate { get; set; }// تاريخ الافراج
        public int ReleasedByUserID { get; set; }// رقم مستخدم الافراج
        public int ReleaseApplicationID { get; set; }// رقم التطبيق او الطلب 



        public clsDetainedLicenses()
        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = 0;
            this.IsReleased = false;
            this.ReleaseDate = new DateTime();
            this.ReleasedByUserID = 0;
            this.ReleaseApplicationID = 0;
        }

        public clsDetainedLicenses(int DetainID, int LicenseID, DateTime DetainDate,
            float FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate,
            int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
        }



        // ارجاع كل الرخص المحجوزة
        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicensesDataAccess.GetAllDetainedLicenses();
        }



        //اضافة شخص
        public bool AddNewDetainedLicense()
        {
            this.DetainID = clsDetainedLicensesDataAccess.AddNewDetainedLicense(this.LicenseID, this.DetainDate,
            this.FineFees, this.CreatedByUserID, this.IsReleased);

            return (this.DetainID != -1);
        }


        // البحث عن رخصة محجوزة
        public static clsDetainedLicenses Find(int LicenseID)
        {

            int DetainID = 0;
            DateTime DetainDate = DateTime.Now;

            float FineFees = 0;
            int CreatedByUserID = 0;
            bool IsReleased = false;
            DateTime ReleaseDate = new DateTime();
            int ReleasedByUserID = 0;
            int ReleaseApplicationID = 0;

        

            if (clsDetainedLicensesDataAccess.GetDetainedID(ref DetainID, LicenseID, ref DetainDate,
            ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate,
            ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainedLicenses(DetainID, LicenseID, DetainDate,
             FineFees, CreatedByUserID, IsReleased, ReleaseDate,
             ReleasedByUserID, ReleaseApplicationID);
            }
            else
            {

                return null;
            }


        }


        //هل تم الافراج عن الاعتقال

        public static bool IsTheDetainIsReleased(int LicenseID)
        {
            return clsDetainedLicensesDataAccess.IsTheDetainIsReleased(LicenseID);
        }



        // التعديل على الرخصة المحجوزة
        public bool UpdateDetainedLicense()
        {
            return clsDetainedLicensesDataAccess.UpdateDetainedLicense(this.DetainID, this.LicenseID, this.DetainDate,
             this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate,
             this.ReleasedByUserID, this.ReleaseApplicationID);
        }


    }
}
