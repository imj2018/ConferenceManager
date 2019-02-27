using System;
using System.Collections.Generic;
using System.IO;

namespace ConferenceManager
{
    class ManageDataFromFile
    {
        private ScheduleConference _scheduleConference;

        public ManageDataFromFile(ScheduleConference scheduleConference)
        {
            _scheduleConference = scheduleConference;
        }

         /**
         * Read from the file.txt (the file containing conference data)
         *
         */
        public void ReaadFromFile(string fileName, FileStream fileStream)
        {
            try
            {
                fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex);
                Console.Write(ex.StackTrace);
            }

            StreamReader streamReader = new StreamReader(fileStream);
            string line = streamReader.ReadLine();

            Console.WriteLine("Test Input :" + "\n");

            try
            {
                _scheduleConference.FileContent = new List<string>();

                while ((line = streamReader.ReadLine()) != null)
                {
                    if (line.Length == 0)
                    {
                        continue;
                    }

                    _scheduleConference.FileContent.Add(line);
                }

                if (_scheduleConference.FileContent.Count == 0)
                {
                    Console.WriteLine("Unable to read file contents");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
                Console.Write(ex.StackTrace);
            }
            finally
            {
                streamReader.Close();
            }
        }

        /**
         * Method will store data into a SortedList so the data can be
         * worked with
         *
         */
        public void ParseDataForUse()
        {
            _scheduleConference.ParsedFileContent = new SortedList<string, int>();

            int minutes = 0;
            int totalMinutes = 0;

            foreach (var str in _scheduleConference.FileContent)
            {
                Console.WriteLine(str);

                /**
                 * Convert lightning balue to minutes, lighting is 5 minutes
                 * then add to SortedList
                 *
                 */
                string minutesAsString = str.Substring(str.LastIndexOf(" ", StringComparison.Ordinal) + 1);
                if (minutesAsString.Equals("lightning"))
                {
                    minutes = 5;
                    totalMinutes = totalMinutes + minutes;
                }
                else
                {
                    string temp = minutesAsString.Replace("min", "");
                    minutes = int.Parse(temp);
                    totalMinutes = totalMinutes + minutes;
                }

                _scheduleConference.ParsedFileContent.Add(str, minutes);
            }
        }
    }
}