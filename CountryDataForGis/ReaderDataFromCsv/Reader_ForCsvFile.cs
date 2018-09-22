using CountryDataForGis.Helper;
using System;
using System.Collections.Generic;
using System.IO;

namespace CountryDataForGis.ReaderDataFromCsv
{
    public class Reader_GitHubUser_Datasets
    {
        public string FilePath_csv_countryCodes { get; private set; }

        //**********************
        //CONSTRUCTOR
        //**********************
        public Reader_GitHubUser_Datasets(string filePath_csv_countryCodes)
        {
            this.FilePath_csv_countryCodes = filePath_csv_countryCodes;
        }


        //public string[] GetCsvHeaderNames()
        //{
        //    string[] arrHeaders = new string[] { };

        //    using (var reader = new StreamReader(this._FilePath_csv_countryCodes))
        //    {
        //        //while (!reader.EndOfStream)
        //        //{
        //            var lineHeader = reader.ReadLine();
        //            arrHeaders = BasicHelpers._SplitCSV(lineHeader);
        //        //}
        //    }

        //    return arrHeaders;
        //}

        public Dictionary<string, string> GetDataBy_ISO3166_1_Alpha_3(string ISO3166_1_Alpha_3)
        {
            Dictionary<string, string> resultDictionary = new Dictionary<string, string>();

            using (var reader = new StreamReader(this.FilePath_csv_countryCodes))
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
                        arrHeaders = BasicHelpers._SplitCSV(lineHeader); //lineHeader.Split(',');

                        for (int i = 0; i < arrHeaders.Length; i++)
                        {
                            if (arrHeaders[i] == "ISO3166-1-Alpha-3")
                            {
                                indexISO3166_1_Alpha_3 = i;
                                break;
                            }
                            if (i == arrHeaders.Length - 1)
                            {
                                throw new Exception($@"ZR in GetDataBy_ISO3166_1_Alpha_3(string ISO3166_1_Alpha_3), there is no header with name ISO3166_1_Alpha_3");
                            }
                        }

                        isThisFirstHeaderRow = false;
                        continue;
                    }

                    //Read rows below header
                    var line = reader.ReadLine();
                    string[] values = BasicHelpers._SplitCSV(line);//line.Split(',');

                    //Save Header as key and row value as value in dictionary
                    if (values[indexISO3166_1_Alpha_3] == ISO3166_1_Alpha_3)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            string valueCleanedStr = BasicHelpers._TrimQuotesFromString(values[i]);
                            resultDictionary.Add(arrHeaders[i], valueCleanedStr);
                        }
                    }


                }
            }

            return resultDictionary;

        }

        public List<Dictionary<string, string>> GetAllData()
        {
            List<Dictionary<string, string>> listResultDictionary = new List<Dictionary<string, string>>();

            using (var reader = new StreamReader(this.FilePath_csv_countryCodes))
            {

                bool isThisFirstHeaderRow = true;
                string[] arrHeaders = new string[] { };

                while (!reader.EndOfStream)
                {
                    //Read header
                    if (isThisFirstHeaderRow)
                    {
                        var lineHeader = reader.ReadLine();
                        arrHeaders = BasicHelpers._SplitCSV(lineHeader);//lineHeader.Split(',');
                        isThisFirstHeaderRow = false;
                        continue;
                    }

                    //Read rows below header
                    var line = reader.ReadLine();
                    string[] values = BasicHelpers._SplitCSV(line); //line.Split(',');

                    //Save Header as key and row value as value in dictionary
                    Dictionary<string, string> dictRowKeyValue = new Dictionary<string, string>();
                    for (int i = 0; i < values.Length; i++)
                    {
                        string valueCleanedStr = BasicHelpers._TrimQuotesFromString(values[i]);
                        dictRowKeyValue.Add(arrHeaders[i], valueCleanedStr);
                    }
                    listResultDictionary.Add(dictRowKeyValue);
                }
            }

            return listResultDictionary;

        }















    }

    public class Reader_GitHubUser_Umpirsky_Country
    {
        private string _FilePath_dataFolder { get; set; }
        private string _LocaleFolder { get; set; }

        public string GetFileDataFrom_csv { get { return $@"{this._FilePath_dataFolder}{this._LocaleFolder}\country.csv"; } }

        //**********************
        //CONSTRUCTOR
        //**********************
        public Reader_GitHubUser_Umpirsky_Country(string filePath_dataFolder, string localeFolder)
        {
            this._FilePath_dataFolder = filePath_dataFolder;
            this._LocaleFolder = localeFolder;
        }

        public void SetLocalFolder(string localFolderName)
        {
            this._LocaleFolder = localFolderName;
        }

        public Dictionary<string, string> GetCountryDataAll()
        {
            Dictionary<string, string> keyValuePairData = new Dictionary<string, string>();

            using (var reader = new StreamReader(this.GetFileDataFrom_csv))
            {

                bool isThisFirstHeaderRow = true;

                while (!reader.EndOfStream)
                {
                    //Read header
                    if (isThisFirstHeaderRow)
                    {
                        isThisFirstHeaderRow = false;
                        continue;
                    }

                    //Read rows below header
                    var line = reader.ReadLine();
                    string[] key_value = BasicHelpers._SplitCSV(line);//line.Split(',');

                    //Save Header as key and row value as value in dictionary
                    keyValuePairData.Add(BasicHelpers._TrimQuotesFromString(key_value[0]), BasicHelpers._TrimQuotesFromString(key_value[1]));

                }
            }

            return keyValuePairData;
        }

        public KeyValuePair<string, string> GetValueBy_ISO_3166_1_alpha_2(string iso_3166_1_alpha_2)
        {
            using (var reader = new StreamReader(this.GetFileDataFrom_csv))
            {

                bool isThisFirstHeaderRow = true;

                while (!reader.EndOfStream)
                {
                    //Read header
                    if (isThisFirstHeaderRow)
                    {
                        var line_header = reader.ReadLine();
                        isThisFirstHeaderRow = false;
                        continue;
                    }

                    //Read rows below header
                    var line = reader.ReadLine();
                    string[] key_value = BasicHelpers._SplitCSV(line);//line.Split(',');

                    //Check if iso3166 1 alpha 2 matches
                    if (key_value[0] == iso_3166_1_alpha_2)
                    {
                        var kvp = new KeyValuePair<string, string>(BasicHelpers._TrimQuotesFromString(key_value[0]), BasicHelpers._TrimQuotesFromString(key_value[1]));
                        return kvp;
                    }

                }
            }

            throw new Exception($@"ZR in GetValueBy_ISO_3166_1_alpha_2(string iso_3166_1_alpha_2): could not find iso_3166_1_alpha_2 with key of {iso_3166_1_alpha_2}");

        }

    }

    public class Reader_GitHubUser_Umpirsky_Language
    {
        private string _FilePath_dataFolder { get; set; }
        private string _LocaleFolder { get; set; }

        public string GetFileDataFrom_csv { get { return $@"{this._FilePath_dataFolder}{this._LocaleFolder}\language.csv"; } }

        //**********************
        //CONSTRUCTOR
        //**********************
        public Reader_GitHubUser_Umpirsky_Language(string filePath_dataFolder, string localeFolder)
        {
            this._FilePath_dataFolder = filePath_dataFolder;
            this._LocaleFolder = localeFolder;
        }

        public void SetLocalFolder(string localFolderName)
        {
            this._LocaleFolder = localFolderName;
        }

        public Dictionary<string, string> GetLanguageDataAll()
        {
            Dictionary<string, string> keyValuePairData = new Dictionary<string, string>();

            using (var reader = new StreamReader(this.GetFileDataFrom_csv))
            {

                bool isThisFirstHeaderRow = true;

                while (!reader.EndOfStream)
                {
                    //Read header
                    if (isThisFirstHeaderRow)
                    {
                        var line_header = reader.ReadLine();
                        isThisFirstHeaderRow = false;
                        continue;
                    }

                    //Read rows below header
                    var line = reader.ReadLine();
                    string[] key_value = BasicHelpers._SplitCSV(line);//line.Split(',');

                    //Save Header as key and row value as value in dictionary
                    keyValuePairData.Add(BasicHelpers._TrimQuotesFromString(key_value[0]), BasicHelpers._TrimQuotesFromString(key_value[1]));

                }
            }

            return keyValuePairData;
        }

        public KeyValuePair<string, string> GetValueBy_LanguageCode(string languageCode)
        {
            using (var reader = new StreamReader(this.GetFileDataFrom_csv))
            {

                bool isThisFirstHeaderRow = true;

                while (!reader.EndOfStream)
                {
                    //Read header
                    if (isThisFirstHeaderRow)
                    {
                        isThisFirstHeaderRow = false;
                        continue;
                    }

                    //Read rows below header
                    var line = reader.ReadLine();
                    string[] key_value = BasicHelpers._SplitCSV(line);//line.Split(',');

                    //Check if iso3166 1 alpha 2 matches
                    if (key_value[0] == languageCode)
                    {
                        var kvp = new KeyValuePair<string, string>(BasicHelpers._TrimQuotesFromString(key_value[0]), BasicHelpers._TrimQuotesFromString(key_value[1]));
                        return kvp;
                    }

                }
            }

            throw new Exception($@"ZR in GetValueBy_LanguageCode(string languageCode): could not find languageCode with key of {languageCode}");

        }

    }

    public class Reader_GitHubUser_Umpirsky_Locale
    {
        private string _FilePath_dataFolder { get; set; }
        private string _LocaleFolder { get; set; }

        public string GetFileDataFrom_csv { get { return $@"{this._FilePath_dataFolder}{this._LocaleFolder}\locales.csv"; } }

        //**********************
        //CONSTRUCTOR
        //**********************
        public Reader_GitHubUser_Umpirsky_Locale(string filePath_dataFolder, string localeFolder)
        {
            this._FilePath_dataFolder = filePath_dataFolder;
            this._LocaleFolder = localeFolder;
        }

        public void SetLocalFolder(string localFolderName)
        {
            this._LocaleFolder = localFolderName;
        }

        public Dictionary<string, string> GetLocaleDataAll()
        {
            Dictionary<string, string> keyValuePairData = new Dictionary<string, string>();

            using (var reader = new StreamReader(this.GetFileDataFrom_csv))
            {

                bool isThisFirstHeaderRow = true;

                while (!reader.EndOfStream)
                {
                    //Read header
                    if (isThisFirstHeaderRow)
                    {
                        var line_header = reader.ReadLine();
                        isThisFirstHeaderRow = false;
                        continue;
                    }

                    //Read rows below header
                    var line = reader.ReadLine();
                    string[] key_value = BasicHelpers._SplitCSV(line);//line.Split(',');

                    //Save Header as key and row value as value in dictionary
                    keyValuePairData.Add(BasicHelpers._TrimQuotesFromString(key_value[0]), BasicHelpers._TrimQuotesFromString(key_value[1]));

                }
            }

            return keyValuePairData;
        }

        public KeyValuePair<string, string> GetValueBy_LocaleCode(string languageCode)
        {
            using (var reader = new StreamReader(this.GetFileDataFrom_csv))
            {

                bool isThisFirstHeaderRow = true;

                while (!reader.EndOfStream)
                {
                    //Read header
                    if (isThisFirstHeaderRow)
                    {
                        isThisFirstHeaderRow = false;
                        continue;
                    }

                    //Read rows below header
                    var line = reader.ReadLine();
                    string[] key_value = BasicHelpers._SplitCSV(line);//line.Split(',');

                    //Check if iso3166 1 alpha 2 matches
                    if (key_value[0] == languageCode)
                    {
                        var kvp = new KeyValuePair<string, string>(BasicHelpers._TrimQuotesFromString(key_value[0]), BasicHelpers._TrimQuotesFromString(key_value[1]));
                        return kvp;
                    }

                }
            }

            throw new Exception($@"ZR in GetValueBy_LocaleCode(string languageCode): could not find languageCode with key of {languageCode}");

        }

    }

}
