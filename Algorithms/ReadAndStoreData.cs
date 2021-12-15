using System;
using System.Collections.Generic;
using System.IO;

namespace AlgorithmsAssignment
{
    static class ReadAndStoreData
    {
        public class Data
        {
            public enum SortedStatus
            {
                None,
                Ascending,
                Descending
            }

            public bool active = false;
            public int sortedStatus;
            public int[] data;

            public Data(bool status, int[] newData)
            {
                active = status;
                sortedStatus = (int)SortedStatus.None;
                data = newData;
            }
        }

        public static Dictionary<string, Data> dataStorage = new Dictionary<string, Data>();

        public static string[] nameOfFilesToReadFrom = new string[] {
            "Road_1_256.txt",
            "Road_2_256.txt",
            "Road_3_256.txt",
            "Road_1_2048.txt",
            "Road_2_2048.txt",
            "Road_3_2048.txt",
        };

        public static List<string> dataStorageKeys = new List<string>();

        public static void ReadFiles()
        {
            foreach (string name in nameOfFilesToReadFrom)
            {
                // Path of file to read from.
                string path = Path.Combine(Directory.GetCurrentDirectory(), name);

                // In case file doesn't exist.
                if (!File.Exists(path))
                {
                    continue;
                }

                // Read all content of file by line.
                string[] lines = System.IO.File.ReadAllLines(path);

                // Create new array.
                int[] data = new int[lines.Length];

                // Add values to array.
                for (int i = 0; i < lines.Length; i++)
                {
                    data[i] = Int32.Parse(lines[i]);
                }

                // Add array to data storage.
                try
                {
                    AddArray(name, data);
                }
                catch (ArgumentException)
                {
                    UpdateArray(name, data);
                }
            }

            if (DataExists())
            {
                Console.WriteLine("Data successfully saved!");
            }
            else
            {
                Console.WriteLine("Something went wrong! Please check file existance!");
            }
            Console.WriteLine("");
        }

        private static int[] GetDataFromFile(string name)
        {
            // Path of file to read from.
            string path = Path.Combine(Directory.GetCurrentDirectory(), name);

            // In case file doesn't exist.
            if (!File.Exists(path))
            {
                return new int[0];
            }

            // Read all content of file by line.
            string[] lines = System.IO.File.ReadAllLines(path);

            // Create new array.
            int[] data = new int[lines.Length];

            // Add values to array.
            for (int i = 0; i < lines.Length; i++)
            {
                data[i] = Int32.Parse(lines[i]);
            }

            return data;
        }

        public static bool AnyArraySelected()
        {
            bool flag = false;
            foreach (Data item in dataStorage.Values)
            {
                if (item.active == true)
                {
                    flag = true;
                    break;
                }
            }

            return flag;
        }

        public static bool SelectedArraysAreSorted()
        {
            bool flag = true;
            foreach (Data item in dataStorage.Values)
            {
                if (item.active == true && item.sortedStatus == (int)Data.SortedStatus.None)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

        public static void OutputArrays()
        {
            foreach (Data item in dataStorage.Values)
            {
                if (item.active == true)
                {
                    int step = (item.data.Length >= 2048) ? 50 : 10;

                    // Display values with defined step
                    for (int index = 0; index < item.data.Length; index += step)
                    {
                        Console.Write(item.data[index] + " ");
                    }
                    Console.WriteLine("");
                    Console.WriteLine("------------------------");
                }
            }
        }

        public static void OutputArray(string nameOfArray)
        {
            Data item = dataStorage[nameOfArray];

            if (item.active == true)
            {
                int step = (item.data.Length > 512) ? 50 : 10;

                // Display values with defined step
                for (int index = 0; index < item.data.Length; index += step)
                {
                    Console.Write(item.data[index] + " ");
                }
                Console.WriteLine("");
                Console.WriteLine("------------------------");
                Console.WriteLine("");
            }
        }

        private static void AddArray(string nameOfNewArray, int[] data)
        {
            dataStorageKeys.Add(nameOfNewArray);
            dataStorage.Add(nameOfNewArray, new Data(false, data));
        }

        private static void UpdateArray(string nameOfNewArray, int[] data)
        {
            dataStorage[nameOfNewArray].data = data;
        }

        public static bool DataExists()
        {
            return (dataStorage.Count == 0) ? false : true;
        }

        public static void MergeArrays(string nameOfFirstArray, string nameOfSecondArray)
        {
            // Get references to selected arrays.
            int[] first = GetDataFromFile(nameOfFirstArray);
            int[] second = GetDataFromFile(nameOfSecondArray);

            // Create new array with length of sum selected arrays.
            int[] data = new int[first.Length + second.Length];

            // Merge the arrays.
            Array.Copy(first, data, first.Length);
            Array.Copy(second, 0, data, first.Length, second.Length);

            // Give array the name.
            string nameOfNewArray = "Merged_Array"+DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss_ff");

            // Add array to data storage.
            AddArray(nameOfNewArray, data);
        }
    }
}
