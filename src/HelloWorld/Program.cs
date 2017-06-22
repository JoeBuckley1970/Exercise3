/*--------------------------------------------------------------------------------------+
|
|  $Copyright: (c) 2017 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AssetWise.Interns;
using AssetWise.Interns.Models;

namespace HelloWorld
    {
    class Program
        {
        const int MaxIterations = 100000;

        static void Main (string[] args)
            {
            do
                {
                // Initialize
                var listOfGreetings = CreateListOfGreetings();

                // Json Serialization
                var json = SerializeJson(listOfGreetings);
                listOfGreetings = DeserializeJson(json);
                json = SerializeAndCompressJson(listOfGreetings);
                listOfGreetings = DecompressAndDeserializeJson(json);

                Console.WriteLine("Done. (any key to go again)\n");
                } while ( Console.ReadKey().Key != ConsoleKey.Escape );
            }

        // Creates MaxIterations worth of Greetings
        private static IEnumerable<Greeting> CreateListOfGreetings ()
            {
            var returnValue = new List<Greeting>();

            var serviceProvider = new Interns2017.DataService.ServiceProvider();
            var persons = serviceProvider.GetPersons();
            var messages = serviceProvider.GetMessages();

            var me = persons.FirstOrDefault(p => p.Name == "Joe");
            var everyoneButMe = persons.Where(p => p != me).ToList();

            var stopwatch = new Stopwatch();

            do
                {
                stopwatch.Start();

                var greeting = new Greeting()
                    {
                    From = me,
                    To = everyoneButMe.FindRandom(),
                    Message = messages.FindRandom(),
                    };

                returnValue.Add(greeting);

                stopwatch.Stop();

                } while ( returnValue.Count < MaxIterations );

            Console.WriteLine("Total Messages Created {0}, Total Time {1} (ms), Average Time (per item) {2} (ms)",
                returnValue.Count, stopwatch.ElapsedMilliseconds, stopwatch.ElapsedMilliseconds / (float) returnValue.Count);

            return returnValue;
            }

        private static string SerializeJson (IEnumerable<Greeting> queue)
            {
            string returnValue;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            returnValue = queue.ToJson();

            stopwatch.Stop();

            Console.WriteLine("\tSerialize\t\t\t{0} (ms)", stopwatch.ElapsedMilliseconds);

            return returnValue;
            }

        private static IEnumerable<Greeting> DeserializeJson (string json)
            {
            IEnumerable<Greeting> returnValue;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            returnValue = json.FromJson<IEnumerable<Greeting>>();

            stopwatch.Stop();

            Console.WriteLine("\tDeserialize\t\t\t{0} (ms)", stopwatch.ElapsedMilliseconds);

            return returnValue;
            }

        private static string SerializeAndCompressJson (IEnumerable<Greeting> queue)
            {
            string returnValue;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            returnValue = queue.ToJsonCompressed();

            stopwatch.Stop();

            Console.WriteLine("\tSerialize and Compress\t\t{0} (ms)", stopwatch.ElapsedMilliseconds);

            return returnValue;
            }

        private static IEnumerable<Greeting> DecompressAndDeserializeJson (string json)
            {
            IEnumerable<Greeting> returnValue;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            returnValue = json.FromJsonCompressed<IEnumerable<Greeting>>();

            stopwatch.Stop();

            Console.WriteLine("\tDecompress and Deserialize\t{0} (ms)", stopwatch.ElapsedMilliseconds);

            return returnValue;
            }
        }
    }
