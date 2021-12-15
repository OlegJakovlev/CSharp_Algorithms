using System;

namespace AlgorithmsAssignment
{
    static class Sorting
    {
        private static int operationCounter = 0;

        public static void MergeSort(int mode)
        {
            foreach (var dataFullEntry in ReadAndStoreData.dataStorage)
            {
                ReadAndStoreData.Data item = dataFullEntry.Value;

                // Work only with choosen and unsorted arrays.
                if (item.active == true)
                {
                    operationCounter = 0;

                    // Ignore already sorted arrays in selected mode.
                    if (item.sortedStatus != mode)
                    {
                        // Reset operation counter
                        operationCounter = 4;

                        // Pointer to current array to work with.
                        int[] currentArray = item.data; 

                        // Length of array
                        int n = currentArray.Length; 

                        // Temporar array to store result.
                        int[] temp = new int[n]; 

                        MergeSortRecursive(currentArray, temp, 0, n - 1, mode);

                        item.sortedStatus = mode; 
                    }

                    Console.WriteLine($"Number of steps done:{operationCounter}");
                    ReadAndStoreData.OutputArray(dataFullEntry.Key);
                }
            }
        }

        private static void Merge(int[] data, int[] temp, int l, int m, int r, int mode)
        {
            int currentIndex = l; 
            int LIndex = l; 
            int RIndex = m + 1; 

            // Add previous assigning steps.
            operationCounter += 3;

            while (LIndex <= m && RIndex <= r)
            {
                // There are 3 operations, assignment + 2 increments.
                operationCounter += 3;

                // Ascending mode.
                if (mode == 1)
                {
                    if (data[LIndex] < data[RIndex])
                    {
                        temp[currentIndex++] = data[LIndex++];
                    }
                    else
                    {
                        temp[currentIndex++] = data[RIndex++];
                    }
                }
                // Descending mode.
                else
                {
                    if (data[LIndex] >= data[RIndex])
                    {
                        temp[currentIndex++] = data[LIndex++];
                    }
                    else
                    {
                        temp[currentIndex++] = data[RIndex++];
                    }
                }
            }

            // Append left part.
            while (LIndex <= m)
            {
                operationCounter += 3;
                temp[currentIndex++] = data[LIndex++];
            }

            // Save the modificated array.
            operationCounter += currentIndex - l;
            for (LIndex = l; LIndex < currentIndex; LIndex++)
            {
                data[LIndex] = temp[LIndex];
            }
        }

        private static void MergeSortRecursive(int[] data, int[] temp, int l, int r, int mode)
        {
            if (l >= r) return;

            // Calculate middle point.
            int m = (l + r) / 2; 
            operationCounter++;

            MergeSortRecursive(data, temp, l, m, mode);
            MergeSortRecursive(data, temp, m + 1, r, mode);

            Merge(data, temp, l, m, r, mode);
        }

        public static void HeapSort(int mode)
        {
            foreach (var dataFullEntry in ReadAndStoreData.dataStorage)
            {
                ReadAndStoreData.Data item = dataFullEntry.Value;

                // Work only with choosen and unsorted arrays.
                if (item.active == true)
                {
                    operationCounter = 0;

                    if (item.sortedStatus != mode)
                    {
                        operationCounter = 3;

                        // Pointer to current array to work with.
                        int[] currentArray = item.data; 

                        // Length of array.
                        int n = currentArray.Length; 

                        // Create heap.
                        for (int i = n / 2 - 1; i >= 0; i--)
                        {
                            operationCounter++;

                            if (mode == 1)
                            {
                                MaxHeapify(currentArray, n, i);
                            }
                            else if (mode == 2)
                            {
                                MinHeapify(currentArray, n, i);
                            }
                        }

                        // Heapsort.
                        for (int i = n - 1; i >= 0; i--)
                        {
                            operationCounter++;

                            // Swap.
                            Swap(currentArray, 0, i);

                            if (mode == 1)
                            {
                                MaxHeapify(currentArray, i, 0);
                            }
                            else if (mode == 2)
                            {
                                MinHeapify(currentArray, i, 0);
                            }
                        }

                        item.sortedStatus = mode;
                    }

                    Console.WriteLine($"Number of steps done:{operationCounter}");
                    ReadAndStoreData.OutputArray(dataFullEntry.Key);
                }
            }
        }

        private static void MaxHeapify(int[] data, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            operationCounter += 3;

            if (left < n && data[left] > data[largest])
            {
                largest = left;
                operationCounter++;
            }
            if (right < n && data[right] > data[largest])
            {
                largest = right;
                operationCounter++;
            }

            if (largest != i)
            {
                Swap(data, i, largest);
                MaxHeapify(data, n, largest);
            }
        }

        private static void MinHeapify(int[] data, int n, int i)
        {
            int smallest = i; 
            int left = 2 * i + 1; 
            int right = 2 * i + 2; 

            operationCounter += 3;

            if (left < n && data[left] < data[smallest])
            {
                smallest = left; 
                operationCounter++;
            }
            if (right < n && data[right] < data[smallest])
            {
                smallest = right; 
                operationCounter++;
            }

            if (smallest != i)
            {
                Swap(data, i, smallest);
                MinHeapify(data, n, smallest);
            }
        }

        public static void QuickSort(int mode)
        {
            foreach (var dataFullEntry in ReadAndStoreData.dataStorage)
            {
                ReadAndStoreData.Data item = dataFullEntry.Value;

                // Work only with choosen and unsorted arrays.
                if (item.active == true)
                {
                    operationCounter = 0;

                    if (item.sortedStatus != mode)
                    {
                        operationCounter = 2;

                        // Pointer to current array to work with.
                        int[] currentArray = item.data;  

                        QuickSortRecursive(currentArray, 0, currentArray.Length - 1, mode);

                        item.sortedStatus = mode; 
                    }

                    Console.WriteLine($"Number of steps done:{operationCounter}");
                    ReadAndStoreData.OutputArray(dataFullEntry.Key);
                }
            }
        }

        private static void QuickSortRecursive(int[] data, int low, int high, int mode)
        {
            while (low < high)
            {
                int pivot = QuickSortPartition(data, low, high, mode);
                operationCounter++;

                if (pivot - low < high - pivot)
                {
                    QuickSortRecursive(data, low, pivot - 1, mode);
                    low = pivot + 1; 
                    operationCounter++;
                }
                else
                {
                    QuickSortRecursive(data, pivot + 1, high, mode);
                    high = pivot - 1; 
                    operationCounter++;
                }
            }
        }

        private static int QuickSortPartition(int[] data, int low, int high, int mode)
        {
            int m = (low + high) / 2; 
            Swap(data, low, m);

            int pivot = data[low]; 
            int lo = low + 1; 
            int hi = high; 

            operationCounter += 4;

            while (lo <= hi)
            {
                if (mode == 1)
                {
                    while (data[hi] > pivot)
                    {
                        hi--; 
                        operationCounter++;
                    }
                        
                    while (lo <= hi && data[lo] <= pivot)
                    {
                        lo++; 
                        operationCounter++;
                    }
                }
                else if (mode == 2)
                {
                    while (data[hi] < pivot)
                    {
                        hi--; 
                        operationCounter++;
                    }
                    while (lo <= hi && data[lo] > pivot)
                    {
                        lo++; 
                        operationCounter++;
                    }
                }

                if (lo <= hi)
                {
                    Swap(data, lo, hi);
                    lo++;
                    hi--;
                    operationCounter += 2;
                }
            }

            Swap(data, low, hi);

            return hi;
            
        }

        public static void RadixSort(int mode)
        {
            foreach (var dataFullEntry in ReadAndStoreData.dataStorage)
            {
                ReadAndStoreData.Data item = dataFullEntry.Value;

                // Work only with choosen and unsorted arrays.
                if (item.active == true)
                {
                    operationCounter = 0;

                    if (item.sortedStatus != mode)
                    {
                        operationCounter = 2;

                        // Pointer to current array to work with.
                        int[] currentArray = item.data;

                        Radix(currentArray, mode);

                        item.sortedStatus = mode;
                    }

                    Console.WriteLine($"Number of steps done:{operationCounter}");
                    ReadAndStoreData.OutputArray(dataFullEntry.Key);
                }
            }
        }

        private static void Radix(int[] data, int mode)
        {
            int i, j;
            int[] temp = new int[data.Length];
            operationCounter += 4;

            for (int bitShift = 31; bitShift > -1; --bitShift)
            {
                j = 0; 
                operationCounter++;

                for (i = 0; i < data.Length; ++i)
                {
                    // Move the element or not, by executing bitShift and comparing with 0.
                    bool move = (data[i] << bitShift) >= 0;
                    operationCounter += 2;

                    // Ascending.
                    if (mode == 1)
                    {
                        if (bitShift == 0 ? !move : move)
                        {
                            data[i - j] = data[i]; 
                            operationCounter++;
                        }
                        else
                        {
                            temp[j++] = data[i]; 
                            operationCounter++;
                        }
                    }
                    // Descending.
                    else if (mode == 2)
                    {
                        if (bitShift == 0 ? move : !move)
                        {
                            data[i - j] = data[i]; 
                            operationCounter++;
                        }
                        else
                        {
                            temp[j++] = data[i]; 
                            operationCounter++;
                        }
                    }
                }

                // Copy temp data to array.
                Array.Copy(temp, 0, data, data.Length - j, j);
                operationCounter += j + 1 - (data.Length - j);
            }
        }

        private static void Swap(int[] data, int firstIndex, int secondIndex)
        {
            operationCounter += 3;

            int tmp = data[firstIndex];
            data[firstIndex] = data[secondIndex];
            data[secondIndex] = tmp;
        }
    }
}
