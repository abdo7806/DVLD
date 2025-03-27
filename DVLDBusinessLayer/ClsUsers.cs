using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class ClsUsers
    {
        public static ClsUsers _User = new ClsUsers();
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public ClsUsers()
        {
            this.UserID = 0;
            this.PersonID = 0;
            this.UserName = "";
            this.Password = "";
            this.IsActive = true;
            Mode = enMode.AddNew;
        }

        public ClsUsers(int UserID, int PersonID,
            string UserName, string Password, bool IsActive)
        {

            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            Mode = enMode.Update;

        }


        // دالة تشفير او تجزيء كلمة السر
        static string ComputeHash(string input)
        {
            //SHA is Secutred Hash Algorithm.
            // Create an instance of the SHA-256 algorithm
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash value from the UTF-8 encoded input string
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert the byte array to a lowercase hexadecimal string
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }


        //التحقق لو كان المستخدم موجود
        public static bool Find(string UserName, string Password)
        {
            int UserID = -1;
            int PersonID = -1;
            bool IsActive = false;


            Password = ComputeHash(Password);
            //
            if (ClsUserDataAccess.GetUserByID(ref UserID, ref PersonID, UserName, Password, ref IsActive))
            {

                _User = new ClsUsers(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {

                _User = null;
            }

            return (_User != null);

        }

        //التحقق لو كان المستخدم موجود
        public static bool UserIsActive()
        {

            return _User.IsActive;
        }

        //ارجاع كل المستخدمين
        public static DataTable GetAllUsers()
        {
            return ClsUserDataAccess.GetAllUsers();
        }


        //هل الشخص موجود
        public static bool DoesPersonExist(int PersonID)
        {
            return ClsUserDataAccess.DoesPersonExist(PersonID);
        }

        private bool _AddNewUser()
        {
            this.Password = ComputeHash(this.Password);

            this.UserID = ClsUserDataAccess.AddNewUser(this.PersonID, this.UserName,
                this.Password, this.IsActive);

            return (this.UserID != -1);
        }

        // التعديل على بيانات المستخدم
        private bool _UpdateUser()
        {
            this.Password = ComputeHash(this.Password);

            return ClsUserDataAccess.UpdatePerson(this.UserID, this.PersonID, this.UserName,
                this.Password, this.IsActive);
        }


        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUser();
            }

            return false;
        }




        // البحث عن مستخدم من خلال PersonID
        public static ClsUsers Find(int UserID)
        {
            int PersonID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;


            if (ClsUserDataAccess.GetUserByPersonID(UserID, ref PersonID,
            ref UserName, ref Password, ref IsActive))
            {
                return new ClsUsers(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }



        //حذف مستخدم
        public static bool DeleteUsers(int UserID)
        {
            return ClsUserDataAccess.DeleteUsers(UserID);
        }


    }
}
