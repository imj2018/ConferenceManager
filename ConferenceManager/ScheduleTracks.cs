using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceManager
{
    class ScheduleTracks
    {
        private ScheduleConference _scheduleConference;

        public ScheduleTracks(ScheduleConference scheduleConference)
        {
            _scheduleConference = scheduleConference;
        }


        /**
         * Method will sort sessions to Tracks one or two depending on time
         * available between morning and afternoon, also considering lunch and
         * the network evening event
         *
         */
        public void ScheduleDataIntoTracks()
        {
            SortedList<int, string> trackOne = new SortedList<int, string>();
            SortedList<int, string> trackTwo = new SortedList<int, string>();
            int track1Index = 1;
            int track2Index = 1;

            /**
             * Prepare morning session for both Track 1 and 2
             * Track 1 has three 1 hour (60min) sessions 
             * Track 2 has six 30 minute sessions
             *
             */
            track1Index = addTrack(trackOne, track1Index, 60, 3);          
            track2Index = addTrack(trackTwo, track2Index, 30, 6);

            /**
            * Prepare afternoon sessions
            * Track 1 has four, 45 minute sessions
            * Track 1 has 2, 30 minute sessions
            * Track 2 has 4, 45minute sessions and a remianing 1 hour session
            * 
            */
            track1Index = addTrack(trackOne, track1Index, 45, 4);
            track1Index = addTrack(trackOne, track1Index, 30, 2);
            track2Index = addTrack(trackTwo, track2Index, 45, 4);
            track2Index = addTrack(trackTwo, track2Index, 60, 1);


            /**
             *  Create TimeSpan objects beginning at 9 and consideration
             *  for lunch beginning at 12 to allocate time
             *
             */
            TimeSpan timeTrackOne = new TimeSpan(9, 0, 0);
            TimeSpan timeTrackTwo = new TimeSpan(9, 0, 0);
            TimeSpan lunch = new TimeSpan(12, 0, 0);

            timeTrackOne = scheduleTime(trackOne, timeTrackOne, lunch, "Track 1");
            timeTrackTwo = scheduleTime(trackTwo, timeTrackTwo, lunch, "Track 2");
            
        }

        /**
         * Method will allocate time for tracks also Lunch and the afternoon Networking event
         *
         */
        private TimeSpan scheduleTime(SortedList<int, string> trackNumber, TimeSpan timeTrackOne, 
            TimeSpan lunch, string trackName)
        {
            Console.WriteLine("\n");
            Console.WriteLine(trackName + "\n");

            foreach (var track in trackNumber)
            {
                if (timeTrackOne.Equals(lunch))
                {
                    timeTrackOne += TimeSpan.FromMinutes(60);
                    Console.WriteLine(lunch + " -  Lunch");
                }

                Console.WriteLine(track.Key + " - " + timeTrackOne + " - " + track.Value);
                var tmin = _scheduleConference.ParsedFileContent.Where(a => a.Key == track.Value).FirstOrDefault();
                timeTrackOne += TimeSpan.FromMinutes(tmin.Value);
            }

            Console.WriteLine(timeTrackOne + " -  Networking Event");
            return timeTrackOne;
        }

        /**
         * Method will add to the List either trackOne or trackTwo as desired, collections paired
         * with lambdas shortens the code by a significant amount and is more readable
         *
         */
        private int addTrack(SortedList<int, string> track, int index, int value, int take)
        {
            var trackNameAndTime = _scheduleConference.ParsedFileContent.Where(a => a.Value == value).Take(take).ToList();
            foreach (var t in trackNameAndTime)
            {
                track.Add(index, t.Key);
                index++;
            }

            return index;
        }
    }
}