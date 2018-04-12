using Capstone.Web.Models;
using Capstone.Web.NPSDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {


        NPSClass multiClass = new NPSClass();
        NpsDALSql sqlDAL = new NpsDALSql();

        private string connectionString;

        public HomeController(string connectionString)
        {

            this.connectionString = connectionString;
        }

        // Dependency Injection
        private INpsDAL _dal;
        public HomeController(INpsDAL dal)
        {
            _dal = dal;
        }

        // GET: Home
        public ActionResult Index()
        {
            if (Session["C or F"]==null)
            {
                Session["C or F"] = 1;
            }
            

            IList<Park> parks = _dal.GetAllParks();

            Session["parks"] = parks;

            return View(parks);
        }


        public ActionResult ParkDetails(string parkCode="CVNP")
        {
            if (Session["C or F"] == null)
            {
                Session["C or F"] = 1;
            }
            DateTime dt = DateTime.Now;
            List<DateTime> alldates = new List<DateTime>();

            for (int i = 0; i < 5; i++)
            {
                alldates.Add(DateTime.Now.AddDays(i));
            }
            multiClass.DateTimes = alldates;

            multiClass.AllParks = sqlDAL.GetAllParks();
            multiClass.Park = multiClass.AllParks.Find(p => p.ParkCode.ToUpper() == parkCode.ToUpper());

            multiClass.AllWeather = sqlDAL.GetAllWeather();

            multiClass.AllWeather = sqlDAL.GetOneParkWeather(parkCode);

            return View(multiClass);
        }




        
        public ActionResult CelFer(int oneORtwo, string parkcode)
        {

            if (oneORtwo==1)
            {
                Session["C or F"] = 1;
            }
            if (oneORtwo==2)
            {
                Session["C or F"] = 2;
            }

            return Redirect(Request.UrlReferrer.ToString());
        }



        //view survey page
        public ActionResult Survey()
        {
            if (Session["C or F"] == null)
            {
                Session["C or F"] = 1;
            }


            return View();
        }

        //add new survey to database
        [HttpPost]
        public ActionResult Survey(string parkName, string Email, string State, string ActivityLevel)
        {           
            SurveyResult sr = new SurveyResult(parkName);
            sr.EmailAddress = Email;
            sr.State = State;
            sr.ActivityLevel = ActivityLevel;

            _dal.SaveSurvey(sr);

            return RedirectToAction("FavoriteParks");
        }

        public ActionResult FavoriteParks()
        {
            if (Session["C or F"] == null)
            {
                Session["C or F"] = 1;
            }
            IList<SurveyRank> ranks = _dal.GetParkRanks();
            return View(ranks);
        }

    }
}