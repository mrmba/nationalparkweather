using Capstone.Web.Models;
using Capstone.Web.NPSDatabase;
using System;
using System.Collections.Generic;

namespace NPSTestDALMethods
{
    class Program
    {
        public string _connectionString;

        static void Main(string[] args)
        {
            string _connectionString;

            NPSClass test = new NPSClass();
            NpsDALSql dALSql = new NpsDALSql();

            List<Park> parks = dALSql.GetAllParks();



        }
    }
}
