using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsDrivers
    {
       /* public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;*/
        public int DriverID {  get; set; }
        public int PersonID {  get; set; }
        public int CreatedByUserID {  get; set; }
        public DateTime CreatedDate {  get; set; }

        public clsDrivers()
        {
            this.DriverID = 0;
            this.PersonID = 0;
            this.CreatedByUserID = 0;
            this.CreatedDate = DateTime.Now;

        }

        public clsDrivers(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
        }

        //اضافة سائق
        public bool AddNewDriver()
        {
            //call DataAccess Layer 

            this.DriverID = clsDriversDataAccess.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);

            return (this.DriverID != -1);
        }



        // البحث عن شخص
        public static clsDrivers Find(int PersonID)
        {
            int DriverID = 0;
            int CreatedByUserID = 0;
            DateTime CreatedDate = DateTime.Now;

            if (clsDriversDataAccess.GetDriverByID(ref DriverID, PersonID, ref CreatedByUserID,
            ref CreatedDate))
            {
                return new clsDrivers(DriverID, PersonID, CreatedByUserID,
             CreatedDate);
            }
            else
            {

                return null;
            }


        }


        //ارجاع كل السائقين

        public static DataTable GetAllDriver()
        {
            return clsDriversDataAccess.GetAllDriver();
        }

    }
}
