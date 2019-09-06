using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using Dm.WeatherForecast.DataAccess.Contract;

namespace Dm.WeatherForecast.DataAccess.Service.Sqlite
{
    public class SqliteDataAccessService : IForecastDataAccess
    {
        public SqliteDataAccessService()
        {
            // TODO: Move to configuration file
            ConnectionString = @"Data Source=WeatherForecast.db;";
        }

        protected string ConnectionString;

        protected string DateTimeFormat = @"ddMMyyyyHHmmss";

        /// <summary>
        /// Get all cities
        /// </summary>
        public IEnumerable<City> GetCities()
        {
            List<City> result = new List<City>();
            string sql = @"select Id, Name from Cities";

            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            result.Add(new City
                            {
                                Id = (int)(long)reader[0],
                                Name = (string)reader[1]
                            });
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Add new city
        /// </summary>
        /// <returns>Id of new inserted city</returns>
        public int AddCity(City newCity)
        {
            int result = 0;
            string sqlInsert = @"insert into Cities(Name) values (@cityName)";
            string sqlGetInsertedId = @"select seq from sqlite_sequence where name='Cities'";

            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(sqlInsert, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("cityName", newCity.Name);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SQLiteCommand(sqlGetInsertedId, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    var obj = cmd.ExecuteScalar();
                    result = (int)(long)obj;
                }
            }

            return result;
        }

        /// <summary>
        /// Get city by name
        /// </summary>
        /// <returns>City instance or null</returns>
        public City GetCityByName(string cityName)
        {
            City result = null;
            string sql = @"select Id, Name from Cities where Name=@cityName";

            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("cityName", cityName);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            // There must be only one city with given name
                            result = new City
                            {
                                Id = (int)(long)reader[0],
                                Name = (string)reader[1]
                            };
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Get forecast for particular city
        /// </summary>
        public IEnumerable<Forecast> GetForecast(int cityId, DateTime targetDate)
        {
            List<Forecast> result = new List<Forecast>();
            string sql = @"select CityId, TargetDate, Temperature, WindSpeed, WindDirection, Pressure, Humidity from Forecasts where CityId=@cityId and TargetDate like @targetDate";

            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("cityId", cityId);
                    cmd.Parameters.AddWithValue("targetDate", targetDate.ToString("ddMMyyyy") + "%"); // for LIKE clause

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Forecast
                            {
                                CityId = (int)(long)reader[0],
                                TargetDate = DateTime.ParseExact((string)reader[1], DateTimeFormat, CultureInfo.InvariantCulture),
                                Temperature = reader.IsDBNull(2) ? int.MinValue : (int)(long)reader[2],
                                WindSpeed = reader.IsDBNull(3) ? int.MinValue : (int)(long)reader[3],
                                WindDirection = reader.IsDBNull(4) ? string.Empty : (string)reader[4],
                                Pressure = reader.IsDBNull(5) ? int.MinValue : (int)(long)reader[5],
                                Humidity = reader.IsDBNull(6) ? int.MinValue : (int)(long)reader[6]
                            });
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Add new or update existing forecast
        /// </summary>
        /// <returns></returns>
        public void AddOrUpdateForecast(Forecast newForecast)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            // Dispose logic
        }
    }
}
