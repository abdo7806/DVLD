using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsPeople
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int PeopleID { get; set; }
        public string NationalNo { get; set; }// الرقم الوطني
        public string FirstName { get; set; }// الاسم الاول
        public string SecondName { get; set; }// الاسم الثاني
        public string ThirdName { get; set; }// الاسم الثالث
        public string LastName { get; set; }// الاسم الاخير
        public DateTime DateOfBirth { get; set; }// تاريخ الميلاد
        public int Gendor { get; set; }// الجنس

        public string Address { get; set; }// العنوان
        public string Phone { get; set; }// رقم الهاتف
        public string Email { get; set; }// الايمايل
        public int NationalityCountryID { get; set; }// معرف البلد
        public string ImagePath { get; set; }// مسار الصورة



        public clsPeople()
        {
            this.PeopleID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";

            this.DateOfBirth = DateTime.Now.AddYears(-18);
            this.Gendor = 0;
            this.Address = "";
            this.Phone = "";
            this.Email = "";

            this.NationalityCountryID = -1;
            this.ImagePath = "";

            Mode = enMode.AddNew;


        }

        public clsPeople(int peopleID, string nationalNo, string firstName,
            string secondName, string thirdName, string lastName, DateTime dateOfBirth, int gendor, string address, string phone, string email, int nationalityCountryID, string imagePath)
        {

            PeopleID = peopleID;
            NationalNo = nationalNo;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gendor = gendor;
            Address = address;
            Phone = phone;
            Email = email;
            NationalityCountryID = nationalityCountryID;
            ImagePath = imagePath;
            Mode = enMode.Update;

        }





        // ارجاع كل الاشخاص في النظام

        public static DataTable GetAllPeople()
        {
            return clsPeopleDataAccess.GetAllPeople();

        }






        // ترجع ترو لو كان الرقم الوطني موجود
        public static bool TestNationalNo(string NationalNo)
        {

            return clsPeopleDataAccess.TestNationalNo(NationalNo);

        }


        // البحث عن شخص
        public static clsPeople Find(int PeopleID)
        {
            string NationalNo = "", FirstName = "",
             SecondName = "", ThirdName = "", LastName = "";
            DateTime DateOfBirth = new DateTime();
            int Gendor = -1;
            string Address = "", Phone = "", Email = "";
            int NationalityCountryID = 190;
            string ImagePath = "";

            if (clsPeopleDataAccess.GetPersonByID(PeopleID, ref NationalNo, ref FirstName,
            ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth,
            ref Gendor, ref Address, ref Phone, ref Email,
            ref NationalityCountryID, ref ImagePath))
            {
                return new clsPeople(PeopleID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                    DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            }
            else
            {

                return null;
            }


        }


        //اضافة شخص
        private bool _AddNewPerson()
        {
            //call DataAccess Layer 

            this.PeopleID = clsPeopleDataAccess.AddNewPerson(this.NationalNo, this.FirstName,
            this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth,
            this.Gendor, this.Address, this.Phone, this.Email,
            this.NationalityCountryID, this.ImagePath);

            return (this.PeopleID != -1);
        }


        // التعديل على بيانات الشخص
        private bool _UpdatePerson()
        {
            return clsPeopleDataAccess.UpdatePerson(this.PeopleID, this.NationalNo, this.FirstName,
            this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth,
            this.Gendor, this.Address, this.Phone, this.Email,
            this.NationalityCountryID, this.ImagePath);
        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePerson();


            }

            return false;
        }


        // نفحص لو كان مرطبط بابيانات في النظام
        public static bool IsThisPersonConnectedToTheSystem(int PeopleID)
        {
            return clsPeopleDataAccess.IsThisPersonConnectedToTheSystem(PeopleID);
        }

        //حذف سجل
        public static bool DeletePeople(int PeopleID)
        {
            return clsPeopleDataAccess.DeletePeople(PeopleID);
        }


        // البحث عن شخص من خلال رقمة الوطني
        public static int FindNationalNo(string NationalNo)
        {
            //int PeopleID = -1;

            return clsPeopleDataAccess.GetNationalNo(NationalNo);

        }


        // ارجاع الاسم كامل
        public string GetFullName()
        {
            return $"{this.FirstName} {this.SecondName} {this.ThirdName} {this.LastName}";
        }

    }
}
