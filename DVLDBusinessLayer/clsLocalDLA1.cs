using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsLocalDLA1
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LocalDLA_ID { get; set; }
        public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }

     


        public clsLocalDLA1()
        {
            this.LocalDLA_ID = 0;
            this.ApplicationID = 0;
            this.LicenseClassID = 0;

            Mode = enMode.AddNew;

        }

        public clsLocalDLA1(int LocalDLA_ID, int ApplicationID, int LicenseClassID)
        {
            this.LocalDLA_ID = LocalDLA_ID;
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;

            Mode = enMode.Update;

        }






        //اضافة طلب'
        private bool _AddNewLocalDLA()
        {
            //call DataAccess Layer 

            this.LocalDLA_ID = clsLocalDLA1DataAccess.AddNewLocalDLA(this.ApplicationID, this.LicenseClassID);

            return (this.LocalDLA_ID != -1);
        }

        // التعديب على كلب رخية جديدة
        private bool _UpdateLocalDLA()
        {
            return clsLocalDLA1DataAccess.UpdateLocalDLA(this.LocalDLA_ID, this.ApplicationID, 
                                               this.LicenseClassID);
        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDLA())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLocalDLA();


            }

            return false;
        }


        //هل لديك رخصة قيادة من هذا النوع؟
        public static bool DoYouHaveDrivingLicenseOfThisType(int PersonID, int LicenseClassID, int ApplicationStatus)
        {
            return clsLocalDLA1DataAccess.DoYouHaveDrivingLicenseOfThisType(PersonID, LicenseClassID, ApplicationStatus);
        }

        // البحث عن طلب الرخصة الجديدة من خلال الرقم
        public static clsLocalDLA1 Find(int LocalDLA_ID)
        {

     
            int ApplicationID = -1;
            int LicenseClassID = -1;

            if (clsLocalDLA1DataAccess.GetLocalDLAByLocalDLA_ID(LocalDLA_ID, ref ApplicationID,
            ref LicenseClassID))
            {
                return new clsLocalDLA1(LocalDLA_ID, ApplicationID, LicenseClassID);
            }
            else
            {

                return null;
            }
        }



        // البحث عن طلب الرخصة الجديدة من خلال الرقم الطلب
        public static clsLocalDLA1 FindByApplicationID(int ApplicationID)
        {


            int LocalDLA_ID = -1;
            int LicenseClassID = -1;

            if (clsLocalDLA1DataAccess.GetLocalDLAByApplicationID(ref LocalDLA_ID, ApplicationID,
            ref LicenseClassID))
            {
                return new clsLocalDLA1(LocalDLA_ID, ApplicationID, LicenseClassID);
            }
            else
            {

                return null;
            }
        }



        // حذف طلب رخصة جديدة
        public static bool DeleteLocalDLA1(int LocalDLA_ID)
        {
            return clsLocalDLA1DataAccess.DeleteLocalDLA1(LocalDLA_ID);
        }


    }
}
