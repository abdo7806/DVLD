using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsLocalDLA_View
    {
        public int LocalDLA_ID { get; set; }
        public string ClassName { get; set; }
        public string NationalNo { get; set; }
        public string FullName { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int PassedTestCount { get; set; }
        public string Status { get; set; }

        public clsLocalDLA_View()
        {
            this.LocalDLA_ID = 0;
            this.ClassName = "";
            this.NationalNo = "";
            this.FullName = "";
            this.ApplicationDate = DateTime.Now;
            this.PassedTestCount = 0;
            this.Status = "";

        }
        public clsLocalDLA_View(int LocalDLA_ID, string ClassName, string NationalNo,
            string FullName, DateTime ApplicationDate, int PassedTestCount, string Status)
        {
            this.LocalDLA_ID = LocalDLA_ID;
            this.ClassName = ClassName;
            this.NationalNo = NationalNo;
            this.FullName = FullName;
            this.ApplicationDate = ApplicationDate;
            this.PassedTestCount = PassedTestCount;
            this.Status = Status;
        }


        // ارجاع كل طلبات رخص القيادة الجديدة
        public static DataTable GetAllLocalDLA()
        {
            return clsLocalDLA_ViewDataAccuss.GetAllLocalDLA();
        }

        public static clsLocalDLA_View Fine(int LocalDLA_ID)
        {

            int PassedTestCount = 0;

            string ClassName = "", NationalNo = "", FullName = "", Status = "";
            DateTime ApplicationDate = DateTime.Now;


            // البحث عن طلب
            if (clsLocalDLA_ViewDataAccuss.GetApplicationByID(LocalDLA_ID, ref ClassName, ref NationalNo,
            ref FullName, ref ApplicationDate, ref PassedTestCount, ref Status))
            {
                return new clsLocalDLA_View(LocalDLA_ID, ClassName, NationalNo,
             FullName, ApplicationDate, PassedTestCount, Status);
            }
            else
            {

                return null;
            }
        }
    }
}
