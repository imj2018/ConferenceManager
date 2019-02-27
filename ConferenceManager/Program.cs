using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceManager
{
    class Program
    {
        static void Main(string[] args)
        {
            ScheduleConference conference = new ScheduleConference();

            /**
             * Note that the Copy Output via the Properties (file right click properties)
             * needs to be set to Copy Always/ Copy if newer for test purposes
             *
             */
            conference.ParseTextFile(@"file.txt");
        }
    }
}
