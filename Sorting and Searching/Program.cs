using System.Diagnostics;

namespace Sorting_and_Searching
{
    internal class Program
    {
        private static Stopwatch _st = new();
        private static Random _random = new();
        static void Main(string[] args)
        {
            const int n = 10000000;
            var testUnsortedArray = GenerateIntArray(n, false);
            var testSortedArray = GenerateIntArray(n, true);
            var target = testUnsortedArray[_random.Next(1, n)];
            var sortedTarget = testSortedArray[_random.Next(1, n)];
            //var sortedArray = BucketSort(testArray);
            ForLoopSearch(target, testUnsortedArray);
            IndexOfSearch(target, testUnsortedArray);
            for (int i = 0; i < n; i++)
            {
                BinarySearch(i, testSortedArray);
            }
            //Console.WriteLine(IsSorted(sortedArray));
        }

        private static void BinarySearch(int target, int[] array)
        {
            // Sorted array (Data must be sorted to use binary search)
            // 0.00005 - 0.0001 sec
            Console.WriteLine("\nBinary Iterative Search");
            var indexToFind = -1;
            var startIndex = 0;
            var stopIndex = array.Length - 1;
            _st.Reset();
            _st.Start();
           
            Console.WriteLine(target);
            while (startIndex <= stopIndex)
            {
                var index = (startIndex + stopIndex) / 2;
               if (array[index] == target)
                {
                    indexToFind = index;
                    break;
                }

                if (array[index] < target)
                {
                    startIndex = index + 1;
                }

                else
                {
                    stopIndex = index - 1;
                }

            }
            _st.Stop();
            if (indexToFind != -1)
            {
                Console.WriteLine(target + " found at index " + indexToFind + " in\n" + _st.Elapsed + " seconds");
            }
            else
            {
                Console.Read();
            }
        }
        private static void IndexOfSearch(int target, int[] array)
        {
            // Unsorted array
            // 0.001 - 0.003 sec
            Console.WriteLine("\nIndexOf Search\n");
            _st.Restart();
            _st.Start();
            var index = Array.IndexOf(array, target, 0);
            _st.Stop();
            Console.WriteLine(target + " found at index " + index + " in\n" + _st.Elapsed + " seconds!");
        }

        private static void ForLoopSearch(int target, int[] array)
        {
            // Unsorted array
            // 0.001 - 0.01 sec
            Console.WriteLine("ForLoop Sort\n");
            _st.Reset();
            _st.Start();
            var index = 0;
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i] != target) continue;
                index = i;
                break;
            }
            _st.Stop();
            Console.WriteLine(target + " found at index " + index + " in\n" + _st.Elapsed + " seconds!");
        }

        public static IList<int> InsertionSort(IList<int> list)
        {
            for (var i = 1; i < list.Count; i++)
            {
                var temp = list[i];
                var j = i - 1;
                while (j >= 0 && list[j] > temp)
                {
                    list[j + 1] = list[j];
                    j--;
                }
                list[j + 1] = temp;
            }
            return list;
        }

        private static IList<int> ForLoopSort(IList<int> arrayToSort)
        {
            for (var i = 0; i < arrayToSort.Count - 1; i++)
            {
                for (var j = i; j < arrayToSort.Count; j++)
                {
                    if (arrayToSort[i] > arrayToSort[j])
                    {
                        (arrayToSort[i], arrayToSort[j]) = (arrayToSort[j], arrayToSort[i]);
                    }
                }
            }
            var sortedArray = arrayToSort;
            return sortedArray;
        }

        public static int[] BucketSort(int[] arrayToSort, int numberOfBuckets = 10000)
        {
            if (arrayToSort.Length < 2) return null;

            // Creating buckets
            var buckets = new List<int>[numberOfBuckets];
            for (var i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new List<int>();
            }
            AssignToBuckets(arrayToSort, buckets, numberOfBuckets);

            // Sorting of each individual bucket:
            foreach (var bucket in buckets)
            {
                InsertionSort(bucket);
            }
            // Combine all buckets into common array:
            var sortedArray = AssignToCommonArray(buckets, arrayToSort.Length);
            return sortedArray;
        }
        private static int GetBucketNumber(int number, int numberOfBuckets)
        {
            var index = Convert.ToInt32((numberOfBuckets / (double)int.MaxValue) * number);
            return index >= numberOfBuckets ? numberOfBuckets - 1 : index;
        }
        private static void AssignToBuckets(int[] arrayToSort, List<int>[] buckets, int numberOfBuckets)
        {
            foreach (var number in arrayToSort)
            {
                var bucketNumber = GetBucketNumber(number, numberOfBuckets);
                buckets[bucketNumber].Add(number);
            }
        }
        private static int[] AssignToCommonArray(IList<int>[] buckets, int arraySize)
        {
            var sortedArray = new int[arraySize];
            var index = 0;
            foreach (var bucket in buckets)
            {
                foreach (var number in bucket)
                {
                    sortedArray[index] = number;
                    index++;
                }
            }
            return sortedArray;
        }
        public static int[] ArraySort(int[] arrayToSort)
        {
            _st.Start();
            Array.Sort(arrayToSort);
            _st.Stop();
            Console.WriteLine(_st.Elapsed);
            return arrayToSort;
        }
        public static int[] GenerateIntArray(int length, bool sorted, int maxVal = 0)
        {
            var arr = new int[length];

            if (sorted)
                for (var i = 0; i < length; i++)
                    arr[i] = i;
            else if (maxVal == 0)
                for (var i = 0; i < length; i++)
                    arr[i] = _random.Next(1, int.MaxValue);
            else
                for (var i = 0; i < length; i++)
                    arr[i] = _random.Next(0, maxVal + 1);

            return arr;
        }
        private static bool IsSorted(IList<int> arr)
        {
            for (int i = 1; i < arr.Count; i++)
                if (arr[i] < arr[i - 1])
                    return false;
            return true;
        }
    }
}