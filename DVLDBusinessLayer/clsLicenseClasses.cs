using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsLicenseClasses
    {

        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; } 
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }
        public float ClassFees { get; set; }


        public clsLicenseClasses()
        {
            LicenseClassID = 0;
            ClassName = "";
            ClassDescription = "";
            MinimumAllowedAge = 0;
            DefaultValidityLength = 0;
            ClassFees = 0;

        }


        public clsLicenseClasses(int licenseClassID, string className, 
            string classDescription, byte minimumAllowedAge,
            byte defaultValidityLength, float classFees)
        {
            LicenseClassID = licenseClassID;
            ClassName = className;
            ClassDescription = classDescription;
            MinimumAllowedAge = minimumAllowedAge;
            DefaultValidityLength = defaultValidityLength;
            ClassFees = classFees;
        }





        // ارجاع كل انواع الرخص
        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassesDataAccess.GetAllLicenseClasses();
        }



        // البحث عن شخص على حسب الرقم
        public static clsLicenseClasses Find(int LicenseClassID)
        {
            string ClassName = "";
            string ClassDescription = "";
            byte MinimumAllowedAge = 0;
            byte DefaultValidityLength = 0;
            float ClassFees = 0;

            if (clsLicenseClassesDataAccess.GetLicenseClassByID( LicenseClassID,
                 ref ClassName, ref ClassDescription, ref MinimumAllowedAge,
            ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClasses(LicenseClassID, ClassName, ClassDescription,
                    MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
            {

                return null;
            }


        }


        // البحث عن نوع الرخصة على حسب الاسم
        public static clsLicenseClasses FindClassName(string ClassName)
        {
            int LicenseClassID = 0;
                string ClassDescription = "";
                     byte MinimumAllowedAge = 0;
            byte DefaultValidityLength = 0;
                        float ClassFees = 0;
                        
            if (clsLicenseClassesDataAccess.GetLicenseClassByClassName(ref LicenseClassID, 
                 ClassName, ref ClassDescription, ref MinimumAllowedAge,
            ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClasses( LicenseClassID, ClassName, ClassDescription, 
                    MinimumAllowedAge, DefaultValidityLength,  ClassFees);
            }
            else
            {

                return null;
            }


        }



    }
}
