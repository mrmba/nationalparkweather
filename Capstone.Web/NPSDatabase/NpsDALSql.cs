using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Capstone.Web.NPSDatabase
{
    public class NpsDALSql : INpsDAL 
    {
        private string _connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=npgeek;Integrated Security=True";
        

        public NpsDALSql()
        {

        }

        public NpsDALSql(string _connectionString)
        {
            this._connectionString = _connectionString;
        }




        public List<Park> GetAllParks()
        {

            const string sql = "SELECT * from park ";

            List<Park> allParks = new List<Park>();


            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    allParks.Add(ParkModel(reader));
                }
            }
            return allParks;
        }

        public List<Weather> GetAllWeather()
        {
            string SQL_GetWeather = "Select *,(high-32)*0.5556 as Hcel,(low-32)*0.5556 as Lcel From weather";

            List<Weather> weather = new List<Weather>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetWeather, conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Weather thisWeather = GetWeatherFromReader(reader);
                        weather.Add(thisWeather);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return weather;
        }



        public List<Weather> GetOneParkWeather(string parkCode)
        {
            string SQL_GetWeather = "SELECT *,(high-32)*0.5556 as Hcel,(low-32)*0.5556 as Lcel FROM weather WHERE parkcode = @parkcode";

            List<Weather> weather = new List<Weather>();


            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(SQL_GetWeather, conn);
                cmd.Parameters.AddWithValue("@parkcode", parkCode);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    weather.Add(GetWeatherFromReader(reader));
                }
            }
            return weather;
        }


        public List<SurveyRank> GetParkRanks()
        {
            const string sql = "select count(survey_result.parkCode) as votes, survey_result.parkCode, parkName from survey_result join park on survey_result.parkCode = park.parkCode group by survey_result.parkCode, parkName order by(votes) desc, parkName asc";

            List<SurveyRank> allRanks = new List<SurveyRank>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    allRanks.Add(GetRanksFromReader(reader));
                }
            }
            return allRanks;
        }



        public bool SaveSurvey(SurveyResult newSurvey)
        {
            SurveyResult s = new SurveyResult() { ParkCode = newSurvey.ParkCode, EmailAddress = newSurvey.EmailAddress, State = newSurvey.State, ActivityLevel = newSurvey.ActivityLevel };

            string SQL_AddSurvey = "Insert into survey_result (parkCode, emailAddress, state, activityLevel) Values(@parkcode, @email, @state, @activitylevel);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(SQL_AddSurvey, conn);
                cmd.Parameters.Add(new SqlParameter("@parkcode", s.ParkCode));
                cmd.Parameters.Add(new SqlParameter("@email", s.EmailAddress));
                cmd.Parameters.Add(new SqlParameter("@state", s.State));
                cmd.Parameters.Add(new SqlParameter("@activitylevel", s.ActivityLevel));
                cmd.ExecuteNonQuery();

            }

            return true;
        }



        private Weather GetWeatherFromReader(SqlDataReader reader)
        {
            Weather convertWeather = new Weather();
            convertWeather.ParkCode = Convert.ToString(reader["parkCode"]);
            convertWeather.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
            convertWeather.Low = Convert.ToInt32(reader["low"]);
            convertWeather.High = Convert.ToInt32(reader["high"]);
            convertWeather.Low_Celsius = Convert.ToInt32(reader["Lcel"]);
            convertWeather.High_Celsius = Convert.ToInt32(reader["Hcel"]);
            convertWeather.Forecast = Convert.ToString(reader["forecast"]);


            return convertWeather;
        } 

        private Park ParkModel(SqlDataReader reader)
        {
            Park park = new Park();
            park.ParkCode = Convert.ToString(reader["parkCode"]);
            park.ParkName = Convert.ToString(reader["parkName"]);
            park.State = Convert.ToString(reader["state"]);
            park.Acreage = Convert.ToInt32(reader["acreage"]);
            park.ElevationInFeet = Convert.ToInt32(reader["ElevationInFeet"]);
            park.MilesOfTrail = Convert.ToInt32(reader["MilesOfTrail"]);
            park.NumberOfCampsites = Convert.ToInt32(reader["NumberOfCampsites"]);
            park.Climate = Convert.ToString(reader["climate"]);
            park.YearFounded = Convert.ToInt32(reader["YearFounded"]);
            park.AnnualVisitorCount = Convert.ToInt32(reader["AnnualVisitorCount"]);
            park.InspirationalQuote = Convert.ToString(reader["InspirationalQuote"]);
            park.InspirationalQuoteSource = Convert.ToString(reader["InspirationalQuoteSource"]);
            park.ParkDescription = Convert.ToString(reader["ParkDescription"]);
            park.EntryFee = Convert.ToInt32(reader["EntryFee"]);
            park.NumberOfAnimalSpecies = Convert.ToInt32(reader["NumberOfAnimalSpecies"]);

            return park;
        }

        private SurveyRank GetRanksFromReader(SqlDataReader reader)
        {
            SurveyRank convertRank = new SurveyRank();
            convertRank.ParkCode = Convert.ToString(reader["parkCode"]);
            convertRank.ParkName = Convert.ToString(reader["parkName"]);
            convertRank.Votes = Convert.ToInt32(reader["votes"]);
            
            return convertRank;
        }
    }
}