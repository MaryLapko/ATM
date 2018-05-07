using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection
{
    class ArrayVsLinkedListComparison
    {
        public static void Compare()
        {
            int numElem = 1000000;
            int elemToAdd = 1000000;
            IList<int> list = createAndInitArrayList(numElem);
            LinkedList<int> linkedList = createAndInitLinkedList(numElem);
            Action<int> addElementToList = (value) => {
                for (int i = numElem; i < numElem + value; i++)
                {
                    list.Add(i);
                }
            };
            long listAddOperationTime = measureOperation(addElementToList, elemToAdd);
            Action<int> deleteElement = (value) => {
                for (int i = numElem + value - 1; i > numElem; i--)
                {
                    list.RemoveAt(i);
                }
             };
            long listDeleteOperationTime = measureOperation(deleteElement, elemToAdd);
            Action<int> containsValueAtList = (value) => list.Contains(value);
            long listContainsValueOperationTime = measureOperation(containsValueAtList, numElem/2);
            Action<int> addElementToLinkedListAction = (value) =>
            {
                for (int i = numElem; i < numElem + value; i++)
                {
                    linkedList.AddLast(i);
                }
            };
            long linkedListAddOperationTime = measureOperation(addElementToLinkedListAction, elemToAdd);
            Action<int> deleteElementFromLinkedListAction = (value) => {
                for (int i = numElem + value - 1; i > numElem; i--)
                {
                    linkedList.RemoveLast();
                }
            };
            long linkedListDeleteOperationTime = measureOperation(deleteElementFromLinkedListAction, elemToAdd);
            Action<int> containsValueAtLinkedList = (value) => linkedList.Contains(value);
            long linkedListContainsValueOperationTime = measureOperation(containsValueAtLinkedList, numElem / 2);
            Console.Out.WriteLine("List Add Operation Time: " + listAddOperationTime + "ms");
            Console.Out.WriteLine("Linked List Add Operation Time: " + linkedListAddOperationTime + "ms");
            Console.Out.WriteLine("List Delete Operation Time: " + listDeleteOperationTime + "ms");
            Console.Out.WriteLine("Linked List Delete Operation Time: " + linkedListDeleteOperationTime + "ms");
            Console.Out.WriteLine("List Find Value Operation Time: " + listContainsValueOperationTime + "ms");
            Console.Out.WriteLine("LinkedList Find Value Operation Time: " + linkedListContainsValueOperationTime + "ms");
            Console.ReadKey();
        }

        private static IList<int> createAndInitArrayList(int numElement)
        {
            IList<int> list = new List<int>();

            initList(numElement, list);

            return list;
        }

        private static LinkedList<int> createAndInitLinkedList(int numElement)
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            initLinkedList(numElement, linkedList);

            return linkedList;
        }

        private static void initList(int numElement, IList<int> list)
        {
            for (int i = 0; i < numElement; i++)
            {
                list.Add(i);
            }
        }

        private static void initLinkedList(int numElement, LinkedList<int> linkedList)
        {
            for (int i = 0; i < numElement; i++)
            {
                linkedList.AddLast(i);
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
