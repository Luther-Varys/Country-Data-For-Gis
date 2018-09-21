using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CountryDataForGis
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }






    public class DataService
    {
        private string _FolderPath_CountryData { get; set; }

        //Data readers
        public Reader_GitHubUser_Datasets Reader_GitHubUser_Datasets { get; set; }



        public DataService(string folderPath_CountryData)
        {
            BasicHelpers.CheckIfFolderPathExits(folderPath_CountryData);

            this._FolderPath_CountryData = folderPath_CountryData;


            //Data readers
            this.Reader_GitHubUser_Datasets = new Reader_GitHubUser_Datasets( $@"{this._FolderPath_CountryData}FromGitHub\datasets\country-codes\country-codes.csv" );
        }
    }





    public  class Reader_GitHubUser_Datasets
    {
        private string _FilePath_csv_countryCodes { get; set; }

        public Reader_GitHubUser_Datasets(string filePath_csv_countryCodes)
        {
            this._FilePath_csv_countryCodes = filePath_csv_countryCodes;
        }


        public Dictionary<string, string> GetDataBy_ISO3166_1_Alpha_3(string ISO3166_1_Alpha_3)
        {
            Dictionary<string, string> resultDictionary = new Dictionary<string, string>();

            using (var reader = new StreamReader(this._FilePath_csv_countryCodes))
            {

                bool isThisFirstHeaderRow = true;
                string[] arrHeaders = new string[] { };

                int indexISO3166_1_Alpha_3 = 0;

                while (!reader.EndOfStream)
                {
                    //Read header
                    if (isThisFirstHeaderRow)
                    {
                        string lineHeader = reader.ReadLine();
                        arrHeaders = this._SplitCSV(lineHeader); //lineHeader.Split(',');

                        for (int i = 0; i < arrHeaders.Length; i++)
                        {
                            if (arrHeaders[i] == "ISO3166-1-Alpha-3")
                            {
                                indexISO3166_1_Alpha_3 = i;
                                break;
                            }
                            if (i == arrHeaders.Length-1)
                            {
                                throw new Exception($@"ZR in GetDataBy_ISO3166_1_Alpha_3(string ISO3166_1_Alpha_3), there is no header with name ISO3166_1_Alpha_3");
                            }
                        }

                        isThisFirstHeaderRow = false;
                        continue;
                    }

                    //Read rows below header
                    var line = reader.ReadLine();
                    string[] values = this._SplitCSV(line);//line.Split(',');

                    //Save Header as key and row value as value in dictionary
                    if(values[indexISO3166_1_Alpha_3]== ISO3166_1_Alpha_3)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            resultDictionary.Add(arrHeaders[i], values[i]);
                        }
                    }


                }
            }

            return resultDictionary;

        }



        private string[] _SplitCSV(string input)
        {
            Regex csvSplit = new Regex("(?:^|,)(\"(?:[^\"])*\"|[^,]*)", RegexOptions.Compiled);
            List<string> list = new List<string>();
            string curr = null;
            foreach (Match match in csvSplit.Matches(input))
            {
                curr = match.Value;
                if (0 == curr.Length)
                {
                    list.Add("");
                }

                list.Add(curr.TrimStart(','));
            }

            return list.ToArray();
        }




        //public Dictionary<string, string>[][] GetAllData()
        //{
        //    //List<Dictionary<string, string>> listResultDictionary = new List<Dictionary<string, string>>();

        //    using (var reader = new StreamReader(this._FilePath_csv_countryCodes))
        //    {

        //        bool isThisFirstHeaderRow = true;
        //        string[] arrHeaders = new string[] { };

        //        while (!reader.EndOfStream)
        //        {
        //            //Read header
        //            if (isThisFirstHeaderRow)
        //            {
        //                var lineHeader = reader.ReadLine();
        //                arrHeaders = lineHeader.Split(',');
        //                isThisFirstHeaderRow = false;
        //                continue;
        //            }

        //            //Read rows below header
        //            var line = reader.ReadLine();
        //            string[] values = line.Split(',');

        //            //Save Header as key and row value as value in dictionary
        //            for (int i = 0; i < values.Length; i++)
        //            {
        //                listResultDictionary.Add();
        //            }
        //        }
        //    }

        //    return listResultDictionary;

        //}



    }





    internal class BasicHelpers
    {
        internal static void CheckIfFolderPathExits(string folderPath)
        {
            bool exits = Directory.Exists(folderPath);

            if (!exits)
                throw new Exception($"ZR the folder path {folderPath} does not exist");
        }


     

    }





}
