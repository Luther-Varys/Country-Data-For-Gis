using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CountryDataForGis.Helper
{
    public class BasicHelpers
    {
        public static void CheckIfFolderPathExits(string folderPath)
        {
            bool exits = Directory.Exists(folderPath);

            if (!exits)
                throw new Exception($"ZR the folder path {folderPath} does not exist");
        }

        public static string[] _SplitCSV(string input)
        {
            //Check this stackoverflow post on how to parse csv file when escaping commas in a quotation:
            //--->   dat,"ok,hello,yes",end, end2
            //https://stackoverflow.com/questions/3776458/split-a-comma-separated-string-with-both-quoted-and-unquoted-strings

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

        public static string _TrimQuotesFromString(string stringToTrim)
        {
            //will remove " quotes sigle character from the beginning and end of the string
            //Check stackoverflow post
            //https://stackoverflow.com/questions/2639918/how-to-remove-char-from-the-begin-and-the-end-of-a-string
            stringToTrim = Regex.Replace(stringToTrim, "^\"|\"$", "");

            return stringToTrim;
        }


        public static string _ReplcaeNonAlphanumeric(string strToFilter)
        {
            string strRes = Regex.Replace(strToFilter, "[^a-zA-Z0-9+]", "_");

            return strRes;
        }

        public static string FirstCharToUpper(string input)
        {
            //https://stackoverflow.com/questions/4135317/make-first-letter-of-a-string-upper-case-with-maximum-performance
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }


        public static string FirstCharToLower(string input)
        {
            //https://stackoverflow.com/questions/4135317/make-first-letter-of-a-string-upper-case-with-maximum-performance
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToLower() + input.Substring(1);
            }
        }




        public static string[] GetCsvHeaderNames(string filePathToCsv)
        {
            string[] arrHeaders = new string[] { };

            using (var reader = new StreamReader(filePathToCsv))
            {
                var lineHeader = reader.ReadLine();
                arrHeaders = BasicHelpers._SplitCSV(lineHeader);
            }

            return arrHeaders;
        }

    }





}
