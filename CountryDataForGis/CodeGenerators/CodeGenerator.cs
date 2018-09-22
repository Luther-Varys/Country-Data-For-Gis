using CountryDataForGis.Helper;
using CountryDataForGis.ReaderDataFromCsv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryDataForGis.CodeGenerators
{
    public class CodeGenerator
    {
        //private string _FolderPath_CountryData { get; set; }
        private DataServiceForCsvFile _DataServiceForCsvFile { get; set; }

        //private string _FilePathCsv_FromGitHub_datasets_country_codes { get { return $@"{this._FolderPath_CountryData}FromGitHub\datasets\country-codes\country-codes.csv"; } }




        //***********************
        //Constructor
        //***********************
        public CodeGenerator(DataServiceForCsvFile dataServiceForCsvFile)
        {
            //BasicHelpers.CheckIfFolderPathExits(folderPath_CountryData);
            //this._DataServiceForCsvFile = new DataServiceForCsvFile(folderPath_CountryData);
            //this._FolderPath_CountryData = folderPath_CountryData;

            this._DataServiceForCsvFile = dataServiceForCsvFile;
        }






        public void GenerateModelInterfaceFromCsvFile(string genInterfaceName, string fileGeneratedNameWithExt, string csvFilePath, string destinationfullDirectoryPath)
        {
            //string pathCSVFile = _DataServiceForCsvFile.Reader_GitHubUser_Datasets.FilePath_csv_countryCodes;
            string[] headers = BasicHelpers.GetCsvHeaderNames(csvFilePath); 

            //Create fields
            string interfaceFields = string.Empty;
            for (int i = 0; i < headers.Length; i++)
            {
                //Filter non alphanumeric characters
                string filteredString_toAlphanumeric = BasicHelpers._ReplcaeNonAlphanumeric(headers[i]);
                //Lowercase all the string
                filteredString_toAlphanumeric = filteredString_toAlphanumeric.ToLowerInvariant();
                //Uppercase only first letter of the string
                filteredString_toAlphanumeric = BasicHelpers.FirstCharToUpper(filteredString_toAlphanumeric);

                interfaceFields = interfaceFields + $@"
    string {filteredString_toAlphanumeric} {{ get; set; }}";
            }



            string modelAutoGenTest = $@"
public interface {genInterfaceName}
{{
{interfaceFields}
}}
";
            //this.PathProject_directory + 
            System.IO.Directory.CreateDirectory(destinationfullDirectoryPath);
            string combinedPath = Path.Combine(destinationfullDirectoryPath, $@"{fileGeneratedNameWithExt}");
            System.IO.File.WriteAllText(combinedPath, modelAutoGenTest);



        }


    }
}
