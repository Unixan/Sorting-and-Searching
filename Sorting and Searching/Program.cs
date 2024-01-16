using System.Diagnostics;

namespace Sorting_and_Searching
{
    internal class Program
    {
        private static Stopwatch st = new Stopwatch();
        private static Random random = new Random();
        static void Main(string[] args)
        {
            const int n = 10000000;
            var testArray = GenerateIntArray(n, false);
            var target = testArray[random.Next(1, testArray.Length)];
            var sortedArray = BucketSort(testArray);
            Console.WriteLine(sortedArray.Length);
            Console.WriteLine(IsSorted(sortedArray));
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

        private static IList<int> ForLoopSort(IList<int> listToSort)
        {
            for (var i = 0; i < listToSort.Count - 1; i++)
            {
                for (var j = i; j < listToSort.Count; j++)
                {
                    if (listToSort[i] > listToSort[j])
                    {
                        (listToSort[i], listToSort[j]) = (listToSort[j], listToSort[i]);
                    }
                }
            }
            var sortedArray = listToSort;
            return sortedArray;
        }

        public static int[] BucketSort(int[] arrayToSort, int numberOfBuckets = 10000)
        {
            if (arrayToSort.Length < numberOfBuckets) return null;
            var sortedArray = new int[arrayToSort.Length];

            // Creating buckets
            var buckets = new List<int>[numberOfBuckets];
            for (var i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new List<int>();
            }

            AssignToBuckets(arrayToSort, buckets, numberOfBuckets);
            // Sorting of each individual bucket:
            st.Start();
            foreach (var bucket in buckets)
            {
                InsertionSort(bucket);
            }
            // Combine all Lists into common array:
            sortedArray = AssignToCommonArray(buckets, arrayToSort.Length);
            st.Stop();
            Console.WriteLine(st.Elapsed);
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
            st.Start();
            Array.Sort(arrayToSort);
            st.Stop();
            Console.WriteLine(st.Elapsed);
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
                    arr[i] = random.Next(1, int.MaxValue);
            else
                for (var i = 0; i < length; i++)
                    arr[i] = random.Next(0, maxVal + 1);

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