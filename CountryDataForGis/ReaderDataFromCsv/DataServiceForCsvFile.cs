using CountryDataForGis.Helper;

namespace CountryDataForGis.ReaderDataFromCsv
{
    public class DataServiceForCsvFile
    {
        private string _FolderPath_CountryData { get; set; }

        //Data readers
        public Reader_GitHubUser_Datasets Reader_GitHubUser_Datasets { get; set; }
        public Reader_GitHubUser_Umpirsky_Country Reader_GitHubUser_Umpirsky_Country { get; set; }
        public Reader_GitHubUser_Umpirsky_Language Reader_GitHubUser_Umpirsky_Language { get; set; }
        public Reader_GitHubUser_Umpirsky_Locale Reader_GitHubUser_Umpirsky_Locale { get; set; }


        //**********************
        //CONSTRUCTOR
        //**********************
        public DataServiceForCsvFile(string folderPath_CountryData)
        {

            BasicHelpers.CheckIfFolderPathExits(folderPath_CountryData);
            this._FolderPath_CountryData = folderPath_CountryData;


            //Data readers
            this.Reader_GitHubUser_Datasets = new Reader_GitHubUser_Datasets($@"{this._FolderPath_CountryData}FromGitHub\datasets\country-codes\country-codes.csv");
            this.Reader_GitHubUser_Umpirsky_Country = new Reader_GitHubUser_Umpirsky_Country($@"{this._FolderPath_CountryData}FromGitHub\umpirsky\country-list\", "it_IT");
            this.Reader_GitHubUser_Umpirsky_Language = new Reader_GitHubUser_Umpirsky_Language($@"{this._FolderPath_CountryData}FromGitHub\umpirsky\language-list\", "it_IT");
            this.Reader_GitHubUser_Umpirsky_Locale = new Reader_GitHubUser_Umpirsky_Locale($@"{this._FolderPath_CountryData}FromGitHub\umpirsky\locale-list\", "it_IT");
        }
    }





}
