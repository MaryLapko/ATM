using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowersGirl.Exceptions
{
    class FlowerCostException : Exception
    {
        public FlowerCostException() : base()
        {
        }

        public FlowerCostException(string message) : base(message)
        {
        }
    }
}
