using ClimateData.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Servitium
{
    [TestClass]
    public class ClimateApiTests
    {
        readonly ClimateApiController controller = new ClimateApiController();
        [TestMethod]
        public void Average_Rainfall_For_GreatBritain_From1980_to_1999_Exists()
        {
            //arrange
            string[] country = new string[] { "gbr" };
            //act
            var apiResult = controller.getAveAnnualRainfall(1980, 1999, country);
            //assert
            Assert.AreEqual(988.8454972331015, apiResult);
        }

        [TestMethod]
        public void Average_Rainfall_For_France_From_1980_to_1999_Exists()
        {
            //arrange
            string[] country = new string[] { "fra" };
            //act
            var apiResult = controller.getAveAnnualRainfall(1980, 1999, country);
            //assert
            Assert.AreEqual(913.7986955122727, apiResult);
        }

        [TestMethod]
        public void Average_Rainfall_For_Egypt_From_1980_to_1999_Exists()
        {
            //arrange
            string[] country = new string[] { "egy" };
            //act
            var apiResult = controller.getAveAnnualRainfall(1980, 1999, country);
            //assert
            Assert.AreEqual(54.58587712129825, apiResult);
        }

        [TestMethod]
        //[Ignore]
        public void Average_Rainfall_For_GreatBritain_From_1985_to_1995_DoesNotExist()
        {
            // wrong date ranges just return an empty list of data -
            try
            {
                //arrange
                string[] country = new string[] { "gbr" };
                //act
                controller.getAveAnnualRainfall(1985, 1995, country);
                Assert.Inconclusive("should have failed in line above");
            }
            catch (BadDateRangeException e)
            {
                //assert
                Assert.AreEqual("date range 1985-1995 not supported", e.Message);
            }
        }

        [TestMethod]
        public void Average_Rainfall_For_MiddleEarth_From_1980_to_1999_DoesNotExist()
        {
            try
            {
                //arrange
                string[] country = new string[] { "mde" };
                //act
                controller.getAveAnnualRainfall(1980, 1999, country);
                Assert.Inconclusive("should have failed in line above");
            }
            catch (BadCountryCodeException e)
            {
                //assert
                Assert.AreEqual("mde not recognized by climateweb", e.Message);
            }
        }

        [TestMethod]
        public void Average_Rainfall_For_GreatBritain_And_France_From_1980_to_1999_Can_Be_Calculated_From_TwoRequests()
        {
            //arrange
            string[] country = new string[] { "gbr", "fra" };
            //act
            var apiResult = controller.getAveAnnualRainfall(1980, 1999, country);
            //assert
            Assert.AreEqual(951.3220963726872, apiResult);
        }
    }
}