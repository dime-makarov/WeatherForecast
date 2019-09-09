using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nelibur.ObjectMapper;
using Dm.WeatherForecast.Client.Viewer.ViewerApp.ViewModel;
using System.Configuration;

namespace Dm.WeatherForecast.Client.Viewer.ViewerApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // Binding configuration
            TinyMapper.Bind<Dm.WeatherForecast.Service.Wcf.Contract.City, ViewModel.CityViewModel>();
            TinyMapper.Bind<Dm.WeatherForecast.Service.Wcf.Contract.Forecast, ViewModel.ForecastViewModel>();
        }

        protected ForecastServiceClient ForecastClient;

        protected List<CityViewModel> Cities;

        protected List<ForecastViewModel> Forecasts;
        
        /// <summary>
        /// Form load handler
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            string hostName = ConfigurationManager.AppSettings["ForecastServiceHostName"];
            ForecastClient = new ForecastServiceClient(hostName);

            // Implementation is in MainForm.Designer.cs
            SetupDataGrid();

            Cities = LoadCities();
            cmbCities.DataSource = Cities;
            cmbCities.SelectedItem = null;
        }

        /// <summary>
        /// Load cities
        /// </summary>
        protected List<CityViewModel> LoadCities()
        {
            var cities = ForecastClient.GetCities();

            return TinyMapper.Map<List<CityViewModel>>(cities);
        }

        /// <summary>
        /// Load forecasts
        /// </summary>
        protected List<ForecastViewModel> LoadForecasts(int cityId)
        {
            var forecasts = ForecastClient.GetForecast(cityId);

            return TinyMapper.Map<List<ForecastViewModel>>(forecasts);
        }

        /// <summary>
        /// Change city handler
        /// </summary>
        protected void cmbCities_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int cityId = ((CityViewModel)cmbCities.SelectedItem).Id;

            Forecasts = LoadForecasts(cityId);

            DataGrid.DataSource = Forecasts;
        }
    }
}
