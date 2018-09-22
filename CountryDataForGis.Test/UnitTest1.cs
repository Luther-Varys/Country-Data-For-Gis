using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CountryDataForGis.ReaderDataFromCsv;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CountryDataForGis.Helper;

namespace CountryDataForGis.Test
{



    [TestClass]
    public class UnitTest1
    {
        public string FolderPath_CountryData { get { return @"D:\MyData\Repos\CountryDataForGis\CountryDataForGis\CountryData\"; } }



        public UnitTest1()
        {

        }



        [TestMethod]
        public void Test_BasicHelpers_GetCsvHeaderNames()
        {
            // arrange 
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            DataServiceForCsvFile dataServiceForCsvFile = new DataServiceForCsvFile(this.FolderPath_CountryData);


            // act
            string[] result = BasicHelpers.GetCsvHeaderNames(Path.Combine(this.FolderPath_CountryData + "/FromGitHub/datasets/country-codes/", "country-codes.csv"));//dataServiceForCsvFile.Reader_GitHubUser_Datasets.GetCsvHeaderNames();
            string headerName_first = result[0];
            string headerName_last = result[result.Length - 1];


            // assert 
            Assert.IsTrue(headerName_first == "FIFA");
            Assert.IsTrue(headerName_last == "EDGAR");



            stopwatch.Stop();
            string timeElpsed = stopwatch.Elapsed.ToString();

        }





        [TestMethod]
        public void Test_Reader_GitHubUser_Datasets_GetDataBy_ISO3166_1_Alpha_3()
        {
            // arrange 
            DataServiceForCsvFile dataServiceForCsvFile = new DataServiceForCsvFile(this.FolderPath_CountryData);
            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();


            // act 
            Dictionary<string,string> result = dataServiceForCsvFile.Reader_GitHubUser_Datasets.GetDataBy_ISO3166_1_Alpha_3("ITA");
            KeyValuePair<string, string> kvp_Dial = result.Single(x => x.Key == "Dial");
            KeyValuePair<string, string> kvp_Languages = result.Single(x => x.Key == "Languages");
           


            // assert
            Assert.IsTrue(kvp_Dial.Value == "39");
            Assert.IsTrue(kvp_Languages.Value == "it-IT,de-IT,fr-IT,sc,ca,co,sl");


            stopwatch.Stop();
            string timeElpsed = stopwatch.Elapsed.ToString();
        }

        [TestMethod]
        public void Test_Reader_GitHubUser_Datasets_GetAllData()
        {
            // arrange 
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            DataServiceForCsvFile dataServiceForCsvFile = new DataServiceForCsvFile(this.FolderPath_CountryData);


            // act 
            List<Dictionary<string, string>> result = dataServiceForCsvFile.Reader_GitHubUser_Datasets.GetAllData();
            Dictionary<string, string> result_filtered = new Dictionary<string, string>();

            foreach (Dictionary<string, string> dict in result)
            {
                string testKey = "ISO3166-1-Alpha-3";
                string testValue = "ITA";

                KeyValuePair<string,string> kvpRes = dict.SingleOrDefault(x => x.Key == testKey && x.Value == testValue);
                if (kvpRes.Key == testKey && kvpRes.Value == testValue)
                {
                    result_filtered = dict;
                    continue;
                }
            }

            KeyValuePair<string, string> kvp_Dial = result_filtered.Single(x => x.Key == "Dial");
            KeyValuePair<string, string> kvp_Languages = result_filtered.Single(x => x.Key == "Languages");



            // assert
            Assert.IsTrue(kvp_Dial.Value == "39");
            Assert.IsTrue(kvp_Languages.Value == "it-IT,de-IT,fr-IT,sc,ca,co,sl");



            stopwatch.Stop();
            string timeElpsed = stopwatch.Elapsed.ToString();

        }





        [TestMethod]
        public void Test_Reader_GitHubUser_Umpirsky_Country_GetCountryDataAll()
        {
            //*******************
            // arrange 
            //*******************
            DataServiceForCsvFile dataServiceForCsvFile = new DataServiceForCsvFile(this.FolderPath_CountryData);
            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //*******************
            // act 
            //*******************
            dataServiceForCsvFile.Reader_GitHubUser_Umpirsky_Country.SetLocalFolder("en_GB");
            Dictionary<string, string> result_en_GB = dataServiceForCsvFile.Reader_GitHubUser_Umpirsky_Country.GetCountryDataAll();

            dataServiceForCsvFile.Reader_GitHubUser_Umpirsky_Country.SetLocalFolder("it_IT");
            Dictionary<string, string> result_it_IT = dataServiceForCsvFile.Reader_GitHubUser_Umpirsky_Country.GetCountryDataAll();

            bool isPresentInenGB = result_en_GB.Any(x => x.Key == "FR" && x.Value == "France");
            bool isPresentInitIT = result_it_IT.Any(x => x.Key == "FR" && x.Value == "Francia");

            //check if a single iso_3166_1_alpha_2 exist and get the value by langauge
            dataServiceForCsvFile.Reader_GitHubUser_Umpirsky_Country.SetLocalFolder("es_ES");
            KeyValuePair<string, string> kvp_esES = dataServiceForCsvFile.Reader_GitHubUser_Umpirsky_Country.GetValueBy_ISO_3166_1_alpha_2("US");



            //*******************
            // assert
            //*******************
            Assert.IsTrue(isPresentInenGB);
            Assert.IsTrue(isPresentInitIT);
            Assert.IsTrue(kvp_esES.Value == "Estados Unidos");

            stopwatch.Stop();
            string timeElpsed = stopwatch.Elapsed.ToString();
        }

        [TestMethod]
        public void Test_Reader_GitHubUser_Umpirsky_Language_GetLanguageDataAll()
        {
            //*******************
            // arrange 
            //*******************
            DataServiceForCsvFile dataServiceForCsvFile = new DataServiceForCsvFile(this.FolderPath_CountryData);
            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //*******************
            // act 
            //*******************
            dataServiceForCsvFile.Reader_GitHubUser_Umpirsky_Language.SetLocalFolder("en_GB");
            Dictionary<string, string> result_en_GB = dataServiceForCsvFile.Reader_GitHubUser_Umpirsky_Language.GetLanguageDataAll();

            dataServiceForCsvFile.Reader_GitHubUser_Umpirsky_Language.SetLocalFolder("it_IT");
            Dictionary<string, string> result_it_IT = dataServiceForCsvFile.Reader_GitHubUser_Umpirsky_Language.GetLanguageDataAll();

            bool isPresentInenGB = result_en_GB.Any(x => x.Key == "fr" && x.Value == "French");
            bool isPresentInitIT = result_it_IT.Any(x => x.Key == "fr" && x.Value == "francese");

            //check if a single iso_3166_1_alpha_2 exist and get the value by langauge
            dataServiceForCsvFile.Reader_GitHubUser_Umpirsky_Language.SetLocalFolder("es_ES");
            KeyValuePair<string, string> kvp_esES = dataServiceForCsvFile.Reader_GitHubUser_Umpirsky_Language.GetValueBy_LanguageCode("en_US");



            //*******************
            // assert
            //*******************
            Assert.IsTrue(isPresentInenGB);
            Assert.IsTrue(isPresentInitIT);
            Assert.IsTrue(kvp_esES.Value == "inglés estadounidense");

            stopwatch.Stop();
            string timeElpsed = stopwatch.Elapsed.ToString();
        }

        [TestMethod]
        public void Test_Reader_GitHubUser_Umpirsky_Locale_GetLocaleDataAll()
        {
            //*******************
            // arrange 
            //*******************
            DataServiceForCsvFile dataServiceForCsvFile = new DataServiceForCsvFile(this.FolderPath_CountryData);
            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //*******************
            // act 
            //*******************
            dataServiceForCsvFile.Reader_GitHubUser_Umpirsky_Locale.SetLocalFolder("en_GB");
            Dictionary<string, string> result_en_GB = dataServiceForCsvFile.Reader_GitHubUser_Umpirsky_Locale.GetLocaleDataAll();

            dataServiceForCsvFile.Reader_GitHubUser_Umpirsky_Locale.SetLocalFolder("it_IT");
            Dictionary<string, string> result_it_IT = dataServiceForCsvFile.Reader_GitHubUser_Umpirsky_Locale.GetLocaleDataAll();

            bool isPresentInenGB = result_en_GB.Any(x => x.Key == "fr_FR" && x.Value == "French (France)");
            bool isPresentInitIT = result_it_IT.Any(x => x.Key == "fr_FR" && x.Value == "francese (Francia)");

            //check if a single iso_3166_1_alpha_2 exist and get the value by langauge
            dataServiceForCsvFile.Reader_GitHubUser_Umpirsky_Locale.SetLocalFolder("es_ES");
            KeyValuePair<string, string> kvp_esES = dataServiceForCsvFile.Reader_GitHubUser_Umpirsky_Locale.GetValueBy_LocaleCode("en_US");



            //*******************
            // assert
            //*******************
            Assert.IsTrue(isPresentInenGB);
            Assert.IsTrue(isPresentInitIT);
            Assert.IsTrue(kvp_esES.Value == "inglés (Estados Unidos)");

            stopwatch.Stop();
            string timeElpsed = stopwatch.Elapsed.ToString();
        }


    }
}
