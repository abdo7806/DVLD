using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsApplicationTypes
    {

        public int ApplicationTypeID { get; set; }// رقم الخدمة
        public string ApplicationTypeTitle { get; set; }// اسم الخدمة
        public float ApplicationFees { get; set; }// رسوم الو سعر الخدمة

        public clsApplicationTypes()
        {
            this.ApplicationTypeID = -1;
            this.ApplicationTypeTitle = "";
            this.ApplicationFees = -1;
        }

        public clsApplicationTypes(int ApplicationTypeID,
            string ApplicationTypeTitle, float ApplicationFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
        }


     // ارجاع كل الخدمات
        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypesDataAccess.GetAllApplicationTypes();
        }


        // البحث عن عن خدمة
        public static clsApplicationTypes Find(int ApplicationTypeID)
        {

             string ApplicationTypeTitle = "";
            float ApplicationFees = -1;

       
            if (clsApplicationTypesDataAccess.GetApplicationTypeByID(ApplicationTypeID,
                ref ApplicationTypeTitle, ref ApplicationFees))
            {
                return new clsApplicationTypes(ApplicationTypeID,
                 ApplicationTypeTitle,  ApplicationFees);
            }
            else
            {

                return null;
            }

        }


        // التعديل على بيانات الخدمة
        public bool UpdateApplicationType()
        {
            return clsApplicationTypesDataAccess.UpdateApplicationType(this.ApplicationTypeID, 
                                                          this.ApplicationTypeTitle, this.ApplicationFees);
        }

    

    }
}
