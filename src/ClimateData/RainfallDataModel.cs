using System.Collections.Generic;

namespace ClimateData
{
    public class RainfallDataModel
    {
        public string gcm { get; set; }
        public string variable { get; set; }
        public int fromYear { get; set; }
        public int toYear { get; set; }
        public List<double> annualData { get; set; }
    }
}
