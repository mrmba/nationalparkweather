using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.NPSDatabase
{
    public interface INpsDAL
    {
        List<Park> GetAllParks();
        List<Weather> GetAllWeather();
        bool SaveSurvey(SurveyResult newSurvey);
        List<SurveyRank> GetParkRanks();

    }
}
