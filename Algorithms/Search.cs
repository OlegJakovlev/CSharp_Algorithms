using AlgorithmsAssignment.Exceptions;
using System;
using System.Collections.Generic;

namespace AlgorithmsAssignment
{
    static class Search
    {
        public static int operationCounter = 0;

        public static List<int> indexesOfNumber = new List<int>();

        public static void BinarySearch(int mode, int valueToSearch)
        {
            // Foreach array which is active try to find value.
            foreach (var dataFullEntry in ReadAndStoreData.dataStorage)
            {
                ReadAndStoreData.Data dataEntry = dataFullEntry.Value;
                if (dataEntry.active)
                {
                    try
                    {
                        operationCounter = 0;
                        Console.WriteLine("------------------------");
                        Console.WriteLine($"Currently selected array: {dataFullEntry.Key}");

                        int minIndex;
                        if (dataEntry.sortedStatus == (int)ReadAndStoreData.Data.SortedStatus.Ascending)
                        {
                            minIndex = AscendingBinarySearch(valueToSearch, dataEntry.data, mode);
                        }
                        else
                        {
                            minIndex = DescendingBinarySearch(valueToSearch, dataEntry.data, mode);
                        }

                        GetAllIndexesOfNumber(minIndex, dataEntry.data);
                        PrintStepsAndIndexesOfNumber();
                    }
                    catch (NoSuchValueException)
                    {
                        Console.WriteLine("------------------------");
                        continue;
                    }
                }
            }

            Console.WriteLine("");
        }

        private static int AscendingBinarySearch(int valueToSearch, int[] whereToSearch, int mode)
        {
            int minIndex = -1;
            int l = 0;
            int r = whereToSearch.Length - 1;
            int m;

            operationCounter += 4;

            while (l <= r)
            {
                // Calculate center.
                m = (l + r) / 2;
                operationCounter++;

                if (whereToSearch[m] == valueToSearch)
                {
                    // Save the value where value has been found.
                    minIndex = m;
                    operationCounter++;
                    r = m - 1;
                }
                else if (whereToSearch[m] > valueToSearch)
                {
                    r = m - 1;
                    operationCounter++;
                }
                else if (whereToSearch[m] < valueToSearch)
                {
                    l = m + 1;
                    operationCounter++;
                }
            }

            // If no value is found.
            if (minIndex == -1)
            {
                switch (mode)
                {
                    case 1:
                        throw new NoSuchValueException("No such value found in the array!");

                    case 2:
                        if (l >= whereToSearch.Length) l = whereToSearch.Length - 1;
                        if (r < 0) r = 0;

                        // Find closest value.
                        int newValueToSearch = (valueToSearch - whereToSearch[l] < whereToSearch[r] - valueToSearch) ? whereToSearch[l] : whereToSearch[r];
                        PrintClosestValueText(newValueToSearch);

                        // Find recursive index as current might be not the first appearance.
                        minIndex = AscendingBinarySearch(newValueToSearch, whereToSearch, 1);

                        operationCounter += 6;
                        break;
                }
            }

            operationCounter++;
            return minIndex;
        }

        private static int DescendingBinarySearch(int valueToSearch, int[] whereToSearch, int mode)
        {
            int minIndex = -1;
            int l = 0;
            int r = whereToSearch.Length - 1;
            int m;

            operationCounter += 4;

            while (r >= l)
            {
                // Calculate center.
                m = (l + r) / 2;
                operationCounter++;

                if (whereToSearch[m] == valueToSearch)
                {
                    // Save the value where value has been found.
                    operationCounter += 2;
                    minIndex = m;
                    r = m - 1;
                }
                else if (whereToSearch[m] < valueToSearch)
                {
                    r = m - 1;
                    operationCounter++;
                }
                else if (whereToSearch[m] > valueToSearch)
                {
                    l = m + 1;
                    operationCounter++;
                }
            }

            // If no value is found.
            if (minIndex == -1)
            {
                switch (mode)
                {
                    case 1:
                        throw new NoSuchValueException("No such value found in the array!");

                    case 2:
                        if (l >= whereToSearch.Length) l = whereToSearch.Length - 1;
                        if (r < 0) r = 0;

                        // Find closest value.
                        int newValueToSearch = (valueToSearch - whereToSearch[l] < whereToSearch[r] - valueToSearch) ? whereToSearch[l] : whereToSearch[r];
                        PrintClosestValueText(newValueToSearch);

                        // Find recursive index as current might be not the first appearance.
                        minIndex = DescendingBinarySearch(newValueToSearch, whereToSearch, 1);
                        operationCounter += 6;
                        
                        break;
                }
            }

            operationCounter++;
            return minIndex;
        }

        public static void InterpolationSearch(int mode, int valueToSearch)
        {
            // Foreach array which is active try to find value.
            foreach (var dataFullEntry in ReadAndStoreData.dataStorage)
            {
                ReadAndStoreData.Data dataEntry = dataFullEntry.Value;
                if (dataEntry.active)
                {
                    try
                    {
                        operationCounter = 0;
                        Console.WriteLine("------------------------");
                        Console.WriteLine($"Currently selected array: {dataFullEntry.Key}");

                        int minIndex;
                        if (dataEntry.sortedStatus == (int)ReadAndStoreData.Data.SortedStatus.Ascending)
                        {
                            minIndex = AscendingInterpolationSearch(valueToSearch, dataEntry.data, mode);
                        }
                        else
                        {
                            minIndex = DescendingInterpolationSearch(valueToSearch, dataEntry.data, mode);
                        }

                        GetAllIndexesOfNumber(minIndex, dataEntry.data);
                        PrintStepsAndIndexesOfNumber();
                    }
                    catch (NoSuchValueException)
                    {
                        continue;
                    }
                }
            }
        }

        private static int AscendingInterpolationSearch(int valueToSearch, int[] whereToSearch, int mode)
        {
            int minIndex = -1; 
            int l = 0; 
            int r = whereToSearch.Length - 1; 
            int pos; 

            operationCounter += 4;

            while (l <= r && (valueToSearch >= whereToSearch[l]) && (valueToSearch <= whereToSearch[r]))
            {
                // If l and r are the same we need need to replace the value to handle DivisionByZero error.
                int difference = (whereToSearch[r] - whereToSearch[l] == 0) ? 1 : whereToSearch[r] - whereToSearch[l]; 

                // Calculate center.
                pos = l + ((valueToSearch - whereToSearch[l]) * (r - l) / difference);
                operationCounter+=3;

                // Save the value where value has been found.
                if (whereToSearch[pos] == valueToSearch)
                {
                    operationCounter++;

                    // Check if mid possition is not most left, if not check left value.
                    minIndex = pos;
                    if (pos > 0) {
                        r = pos - 1;
                        operationCounter++;
                    }
                    else break;
                }
                else if (whereToSearch[pos] < valueToSearch)
                {
                    l = pos + 1; 
                    operationCounter++;
                }
                else
                {
                    r = pos - 1; 
                    operationCounter++;
                }
            }

            // If no value is found.
            if (minIndex == -1)
            {
                switch (mode)
                {
                    case 1:
                        throw new NoSuchValueException("No such value found in the array!");

                    case 2:
                        // Find closest value.
                        int newValueToSearch = (valueToSearch - whereToSearch[l] < whereToSearch[r] - valueToSearch) ? whereToSearch[l] : whereToSearch[r];
                        PrintClosestValueText(newValueToSearch);

                        // Find recursive index as current might be not the first appearance.
                        minIndex = AscendingInterpolationSearch(newValueToSearch, whereToSearch, 1); 
                        operationCounter += 6;
                        break;
                }
            }

            operationCounter++;
            return minIndex;
        }

        private static int DescendingInterpolationSearch(int valueToSearch, int[] whereToSearch, int mode)
        {
            int minIndex = -1; 
            int l = 0; 
            int r = whereToSearch.Length - 1; 
            int pos; 

            operationCounter += 4;

            while ((l <= r) && (valueToSearch <= whereToSearch[l]) && (valueToSearch >= whereToSearch[r]))
            {
                // If l and r are the same we need need to replace the value to handle DivisionByZero error.
                int difference = (whereToSearch[r] - whereToSearch[l] == 0) ? 1 : whereToSearch[r] - whereToSearch[l];

                // Calculate mid point.
                pos = l + (r - l) / difference * (valueToSearch - whereToSearch[l]); 
                operationCounter+=3;

                // Save the value where value has been found.
                if (whereToSearch[pos] == valueToSearch)
                {
                    operationCounter++;

                    // Check if mid possition is not most left, if not check left value.
                    minIndex = pos;
                    if (pos > 0)
                    {
                        operationCounter++;
                        r = pos - 1;
                    }
                    else break;
                }
                
                if (whereToSearch[pos] < valueToSearch)
                {
                    r = pos - 1; 
                    operationCounter++;
                }
                else if (whereToSearch[pos] > valueToSearch)
                {
                    l = pos + 1; 
                    operationCounter++;
                }
            }

            // No value is found.
            if (minIndex == -1)
            {
                switch (mode)
                {
                    case 1:
                        throw new NoSuchValueException("No such value found in the array!");

                    case 2:
                        // Find closest value.
                        int newValueToSearch = (valueToSearch - whereToSearch[l] > whereToSearch[r] - valueToSearch) ? whereToSearch[l] : whereToSearch[r];
                        PrintClosestValueText(newValueToSearch);

                        // Find recursive index as current might be not the first appearance.
                        minIndex = DescendingInterpolationSearch(newValueToSearch, whereToSearch, 1); 
                        operationCounter += 6;
                        break;
                }
            }

            operationCounter++;
            return minIndex;
        }

        private static void GetAllIndexesOfNumber(int minIndex, int[] data)
        {
            // Clear indexes from previous request
            indexesOfNumber.Clear();

            int currentIndex = minIndex;
            operationCounter++;

            // Go right and check if data is the same
            while (currentIndex < data.Length && data[currentIndex] == data[minIndex])
            {
                // If so, add it to the list;
                indexesOfNumber.Add(currentIndex);
                currentIndex++;
                operationCounter++;
            }
        }

        private static void PrintClosestValueText(int newValueToSearch)
        {
            Console.WriteLine("");
            Console.WriteLine("Initial value was not found!");
            Console.WriteLine($"Closest value is: {newValueToSearch}");
        }

        private static void PrintStepsAndIndexesOfNumber()
        {
            Console.WriteLine("");
            Console.WriteLine($"Number of steps done: {operationCounter}");
            Console.WriteLine("Indexes where value can be found are: ");
            foreach (int index in indexesOfNumber)
            {
                Console.Write(index + " ");
            }
            Console.WriteLine("");
            Console.WriteLine("");
        }
    }
}
