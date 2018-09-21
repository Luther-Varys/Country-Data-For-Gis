using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CountryDataForGis.Test
{


    [TestClass]
    public class UnitTest1
    {
        public string FolderPath_CountryData { get { return @"D:\MyData\Repos\CountryDataForGis\CountryDataForGis\CountryData\"; } }



        [TestMethod]
        public void TestMethod1()
        {
            // arrange 
            DataService dataService = new DataService(this.FolderPath_CountryData);


            // assert 
            Dictionary<string,string> result = dataService.Reader_GitHubUser_Datasets.GetDataBy_ISO3166_1_Alpha_3("ITA");

            KeyValuePair<string, string> kvp = result.Single(x => x.Key == "ITU");
            string key = kvp.Key.ToString();
            string value = kvp.Value.ToString();

            // act



        }
    }
}
