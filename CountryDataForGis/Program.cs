using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountryDataForGis.CodeGenerators;
using CountryDataForGis.ReaderDataFromCsv;

namespace CountryDataForGis
{
    class Program
    {

        static void Main(string[] args)
        {
            //This is a basic example on how to initialize the 2 main classes:
            //  -DataServiceForCsvFile (used the read data from csv files)
            //  -CodeGenerator (used to autogenrate some code (interfaces, classes,...) based on csv files key-values and headers)
            //**** for more specific and detailed examples CHECK THE TEST PROJECT ****



            string folderPath_CountryData = @"D:\MyData\Repos\CountryDataForGis\CountryDataForGis\CountryData\";

            //*************************************
            //Get CSV file reader service
            //*************************************
            DataServiceForCsvFile dataServiceForCsvFile = new DataServiceForCsvFile(folderPath_CountryData);



            //*************************************
            //Start file code Generator
            //*************************************
            CodeGenerator CodeGenerator = new CodeGenerator(dataServiceForCsvFile);

            string fullDirectoryPath01 = @"D:\MyData\Repos\CountryDataForGis\CountryDataForGis\CodeGenerators\GeneratedCode\";
            CodeGenerator.GenerateModelInterfaceFromCsvFile(
                "DatasetsCountryCode", 
                "DatasetsCountryCode.cs", 
                $@"{folderPath_CountryData}\FromGitHub\datasets\country-codes\country-codes.csv", 
                fullDirectoryPath01);

            string localeFolderName = "en_GB";
            string fullDirectoryPath02 = @"D:\MyData\Repos\CountryDataForGis\CountryDataForGis\CodeGenerators\GeneratedCode\";
            CodeGenerator.GenerateModelInterfaceFromCsvFile(
                "UmpirskyCountryList", 
                "UmpirskyCountryList.cs", 
                $@"{folderPath_CountryData}\FromGitHub\umpirsky\country-list\{localeFolderName}\country.csv", 
                fullDirectoryPath02);

        }
    }





}
