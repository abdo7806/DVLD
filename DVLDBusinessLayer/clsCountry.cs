using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsCountry
    {
        public int CountryID {  get; set; }
        public string CountryName { get; set; }

        //ارجاع جميع البلادان
        public static DataTable GetAllCountries()
        {
            return clsCountryDataAccess.GetAllCountries();

        }


        // ارجاع رقم البلد 
        public static int Find(string CountryName)
        {
            int CountryID = -1;

            clsCountryDataAccess.GetCountryByName(ref CountryID, CountryName);
            return CountryID;
        }

        // ارجاع رقم البلد 
        public static string Find(int CountryID)
        {
            string CountryName = "";
            clsCountryDataAccess.GetCountryByID(CountryID, ref CountryName);
            return CountryName;
        }



    }
}
