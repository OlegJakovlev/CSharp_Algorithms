using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsAssignment.Exceptions
{
    class StoredDataIsInvalidException : Exception
    {
        public StoredDataIsInvalidException()
        {
        }

        public StoredDataIsInvalidException(string message) : base(message)
        {
            Console.WriteLine(message);
            Console.WriteLine("");
        }
    }
}
