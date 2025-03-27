using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsTestTypes// انواع الاختبرات
    {
        public int TestTypeID { get; set; }// رقم الاختبار
        public string TestTypeTitle { get; set; }// اسم الاختبار
        public string TestTypeDescription { get; set; }// طريقة الاختبار
        public float TestTypeFees { get; set; }// رسوم سعر الاختبار

        public clsTestTypes()
        {
            this.TestTypeID = -1;
            this.TestTypeTitle = "";
            this.TestTypeDescription = "";
            this.TestTypeFees = -1;

        }

        public clsTestTypes(int TestTypeID,string TestTypeTitle,
            string TestTypeDescription, float TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
        }


        //ارجاع كل الاختبرات
        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesDataAccess.GetAllTestTypes();
        }


        // البحث عن اختبار
        public static clsTestTypes Find(int TestTypeID)
        {

            string TestTypeTitle = "";
            string TestTypeDescription = "";
            float TestTypeFees = -1;


            if (clsTestTypesDataAccess.GetTestTypeByID(TestTypeID, ref TestTypeTitle,
                ref TestTypeDescription, ref TestTypeFees))
            {
                return new clsTestTypes(TestTypeID,  TestTypeTitle,
                 TestTypeDescription,  TestTypeFees);
            }
            else
            {

                return null;
            }

        }

        // التعديل على بيانات اختبار
        public bool UpdateTestType()
        {
            return clsTestTypesDataAccess.UpdateTestType(this.TestTypeID, this.TestTypeTitle,
                this.TestTypeDescription, this.TestTypeFees);
        }

    }
}
