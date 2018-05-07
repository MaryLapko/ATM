using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection
{
    class StackVsQueueComparison
    {
        public static void Compare()
        {
            int numElem = 10000;
            int elemToAdd = 10000;
            Stack<int> stack = createAndInitStack(numElem);
            Queue<int> queue = createAndInitQueue(numElem);
            Action<int> pushElementToStack = (value) => {
                for (int i = numElem; i < numElem + value; i++)
                {
                    stack.Push(i);
                }
            };
            long stackPushOperationTime = measureOperation(pushElementToStack, elemToAdd);
            Action<int> popElement = (value) => {
                for (int i = numElem + value - 1; i > numElem; i--)
                {
                    stack.Pop();
                }
            };
            long stackPopOperationTime = measureOperation(popElement, elemToAdd);
            Action<int> containsValueAtStack = (value) => stack.Contains(value); 
            long stackFindOperationTime = measureOperation(containsValueAtStack, numElem/2);
            Action<int> addElementToQueue = (value) => {
                for (int i = numElem; i < numElem + value; i++)
                {
                    queue.Enqueue(i);
                }

            };
            long queueAddElementOperationTime = measureOperation(addElementToQueue, elemToAdd);
            Action<int> removeElementFromQueue = (value) => {
                for (int i = numElem + value - 1; i > numElem; i--)
                {
                    queue.Dequeue();
                }
            };
            long queueRemoveOperationTime = measureOperation(popElement, elemToAdd);
            Action<int> containsValueAtQueue = (value) => queue.Contains(value);
            long queueFindOperationTime = measureOperation(containsValueAtQueue, numElem/2);
            Console.Out.WriteLine("Stack Push Value Operation Time: " + stackPushOperationTime + "ms");
            Console.Out.WriteLine("Pop Element from Stack: " + stackPopOperationTime + "ms");
            Console.Out.WriteLine("Contains Value at Stack: " + stackPopOperationTime + "ms");
            Console.Out.WriteLine("Queue Add Value Operation Time: " + queueAddElementOperationTime + "ms");
            Console.Out.WriteLine("Queue Delete Value Operation Time: " + queueRemoveOperationTime + "ms");
            Console.Out.WriteLine("Queue Contains Value Operation Time: " + queueFindOperationTime + "ms");
            Console.ReadKey();
        }

        private static Stack<int> createAndInitStack(int numElement)
        {
            Stack<int> stack = new Stack<int>();

            initStack(numElement, stack);

            return stack;
        }

        private static Queue<int> createAndInitQueue(int numElement)
        {
            Queue<int> queue = new Queue<int>();

            initQueue(numElement, queue);

            return queue;
        }

        private static void initStack(int numElement, Stack<int> stack)
        {
            for (int i = 0; i < numElement; i++)
            {
                stack.Push(i);
            }
        }
        private static void initQueue(int numElement, Queue<int> queue)
        {
            for (int i = 0; i < numElement; i++)
            {
                queue.Enqueue(i);
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
    } }
