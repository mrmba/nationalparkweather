using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;


namespace Capstone.Web.Models
{
    public class NPSClass
    {
        public Park Park { get; set; }
        public List<Park> AllParks { get; set; } = new List<Park>();
        public Weather Weather { get; set; }
        public List<Weather> AllWeather { get; set; } = new List<Weather>();
        public SurveyResult SurveyResult { get; set; }
        public List<DateTime> DateTimes { get; set; } = new List<DateTime>();


    }
}