using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection
{
    class HashTableVsDictionaryComparison
    {
        public static void Compare()
        {
            int numElem = 10000;
            int elemToAdd = 10000;
            Hashtable hashTable = createAndInitHashtable(numElem);
            Dictionary<int,int> dictionary = createAndInitDictionary(numElem);
            Action<int> addElementToHashtable = (value) => {
                for (int i = numElem; i < numElem + value; i++)
                {
                    hashTable.Add(i, i);    
                }
            };
            long hashAddOperationTime = measureOperation(addElementToHashtable, elemToAdd);
            Action<int> deleteElementFromHashtable = (value) => {
                for (int i = numElem; i < numElem + value; i++)
                {
                    hashTable.Remove(i);
                }
            };
            long hashDeleteOperationTime = measureOperation(deleteElementFromHashtable, elemToAdd);
            Action<int> findElementsAtHashtable = (value) => hashTable.Contains(value);
            long hashContainsOperationTime = measureOperation(findElementsAtHashtable, elemToAdd);

            Action<int> addElementToDictionary = (value) => {
                for (int i = numElem; i < numElem + value; i++)
                {
                    dictionary.Add(i,i);
                }
            };
            long dictionaryAddOperationTime = measureOperation(addElementToDictionary, elemToAdd);
            Action<int> deleteElementFromDictionary = (value) => {
                for (int i = numElem; i < numElem + value; i++)
                {
                    dictionary.Remove(i);
                }
            };
            long dictionaryDeleteOperationTime = measureOperation(deleteElementFromDictionary, elemToAdd);
            Action<int> findElementsAtDictionary = (value) => dictionary.ContainsKey(value);
            long dictionaryContainsKeyOperationTime = measureOperation(findElementsAtDictionary, elemToAdd);

            Console.Out.WriteLine("Hashtable Add Operation Time: " + hashAddOperationTime + "ms");
            Console.Out.WriteLine("List Add Operation Time: " + hashDeleteOperationTime + "ms");
            Console.Out.WriteLine("List Add Operation Time: " + hashContainsOperationTime + "ms");
            Console.Out.WriteLine("Linked List Add Operation Time: " + dictionaryAddOperationTime + "ms");
            Console.Out.WriteLine("List Delete Operation Time: " + dictionaryDeleteOperationTime + "ms");
            Console.Out.WriteLine("List Find Value Operation Time: " + dictionaryContainsKeyOperationTime + "ms");
            Console.ReadKey();
        }

        private static Hashtable createAndInitHashtable(int numElem)
        {
            Hashtable hashTable = new Hashtable();

            initHashtable(numElem, hashTable);

            return hashTable;
        }

        private static Dictionary<int,int> createAndInitDictionary(int numElem)
        {
            Dictionary <int,int> dictionary = new Dictionary<int, int>();

            initDictionary(numElem, dictionary);

            return dictionary;
        }

        private static void initHashtable(int numElement, Hashtable hashTable)
        {
            for (int i = 0; i < numElement; i++)
            {
                hashTable.Add(i, i);
            }     
        }

        private static void initDictionary(int numElement, Dictionary <int,int> dictionary)
        {
            for (int i = 0; i < numElement; i++)
            {
                dictionary.Add(i, i);
            }
        }
        private static long measureOperation(Action<int> action, int val)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            action(val);

            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds;
        }
    }

   

}

