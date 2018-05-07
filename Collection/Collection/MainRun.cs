using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection
{
    class MainRun
    {
        public static void Main(string[] args)
        {
            ArrayVsLinkedListComparison.Compare();
            StackVsQueueComparison.Compare();
            HashTableVsDictionaryComparison.Compare();
        }
    }
}
