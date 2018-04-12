using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class SurveyRank
    {
        public string ParkCode { get; set; }
        public string ParkName { get; set; }
        public int Votes { get; set; }
    }
}