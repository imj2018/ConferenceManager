using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceManager
{
    class ScheduleConference
    {

        public List<string> FileContent;
        public SortedList<string, int> ParsedFileContent;
        private readonly ManageDataFromFile _manageDataFromFile;
        private readonly ScheduleTracks _scheduleTracks;

        public ScheduleConference()
        {
            _manageDataFromFile = new ManageDataFromFile(this);
            _scheduleTracks = new ScheduleTracks(this);
            //
        }

        /**
         * Method for handling the text file
         *
         */
        public void ParseTextFile(string fileName)
        {

            FileStream fileStream = null;

            _manageDataFromFile.ReaadFromFile(fileName, fileStream);
            _manageDataFromFile.ParseDataForUse();
            _scheduleTracks.ScheduleDataIntoTracks();

            Console.ReadLine();

        }
    }
}
