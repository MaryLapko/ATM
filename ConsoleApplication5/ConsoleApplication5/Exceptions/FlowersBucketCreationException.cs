using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowersGirl.Exceptions
{
    class FlowersBucketCreationException : Exception
    {
        public FlowersBucketCreationException() : base()
        {
        }

        public FlowersBucketCreationException(string message) 
            : base(message) {
        }
    }
}
