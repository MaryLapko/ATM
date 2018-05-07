using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowersGirl.Exceptions
{
    class FlowersBucketCurrencyException : Exception
    {
        public FlowersBucketCurrencyException() : base()
        { }

        public FlowersBucketCurrencyException(string message) : base(message)
        { }
    }
}
