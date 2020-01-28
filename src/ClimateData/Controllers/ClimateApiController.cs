using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace ClimateData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClimateApiController : ControllerBase
    {
        readonly RestClient client = new RestClient("http://climatedataapi.worldbank.org/climateweb/rest/v1/country/annualavg/pr/");

        public double getAveAnnualRainfall(int fromCCYY, int toCCYY, string[] countryISOs)
        {
            double total = new double();
            foreach (string countryISO in countryISOs)
            {
                RestRequest request = new RestRequest("{fromCCYY}/{toCCYY}/{countryISOs}", Method.GET);
                request.AddUrlSegment("fromCCYY", fromCCYY);
                request.AddUrlSegment("toCCYY", toCCYY);
                request.AddUrlSegment("countryISOs", countryISO);
                var response = client.Execute(request);
                if (response.Content == "Invalid country code. Three letters are required")
                {
                    throw new BadCountryCodeException(string.Format("{0} not recognized by climateweb", countryISO));
                }
                var rainfallData = JsonSerializer.Deserialize<List<RainfallDataModel>>(response.Content);
                if (!rainfallData.Any())
                {
                    throw new BadDateRangeException("date range " + fromCCYY + "-" + toCCYY + " not supported");
                }
                var sum = rainfallData.SelectMany(x => x.annualData).Sum();
                var avg = sum / rainfallData.Count;
                total += avg;
            }
            return total / countryISOs.Length;
         }

    }
    public class BadDateRangeException : NotSupportedException
    {
        public BadDateRangeException(string message) 
            : base(message)
        { 
        }
    }
    public class BadCountryCodeException : NotSupportedException   
    {
        public BadCountryCodeException(string message) 
            : base(message)
        {
        }
    }
}
