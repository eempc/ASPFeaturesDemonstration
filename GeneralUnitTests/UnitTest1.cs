using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASPFeaturesDemonstration.Data;
using System;

namespace GeneralUnitTests {
    [TestClass]
    public class CountriesTest {
        public Countries countries = new Countries();
        
        [TestMethod]
        public void SpotCheckAU() {            
            string expected = "Australia";
            string test = countries.CountryList["AU"];

            Assert.AreEqual(expected, test);
        }

        [TestMethod]
        public void SpotCheckNL() {
            string expected = "Netherlands";
            string test = countries.CountryList["NL"];

            Assert.AreEqual(expected, test);
        }

        [TestMethod]
        public void SpotCheckES() {
            string expected = "Spain";
            string test = countries.CountryList["ES"];

            Assert.AreEqual(expected, test);
        }

    }
}
