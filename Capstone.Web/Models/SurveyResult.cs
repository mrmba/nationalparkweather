using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class SurveyResult
    {
        public int SurveyID { get; set; }
        public string ParkCode { get; set; }
        public string EmailAddress { get; set; }
        public string State { get; set; }
        public string ActivityLevel { get; set; }

        Dictionary<string, string> parkcodes = new Dictionary<string, string>()
        {
            { "Cuyahoga Valley National Park", "CVNP" },
            { "Everglades National Park", "ENP" },
            { "Grand Canyon National Park", "GCNP" },
            { "Glacier National Park", "GNP" },
            { "Great Smoky Mountains National Park", "GSMNP" },
            { "Grand Teton National Park", "GTNP" },
            { "Mount Rainier National Park", "MRNP" },
            { "Rocky Mountain National Park", "RMNP" },
            { "Yellowstone National Park", "YNP" },
            { "Yosemite National Park", "YNP2" },
        };

        public SurveyResult(string parkName )
        {            
            string code = parkcodes[parkName];
            ParkCode = code;
        }

        public SurveyResult()
        {
        }

    }
}