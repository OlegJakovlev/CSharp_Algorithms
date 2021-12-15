using System;

namespace AlgorithmsAssignment.Exceptions
{
    class NoSuchValueException : Exception
    {
        public NoSuchValueException()
        {
        }

        public NoSuchValueException(string message) : base(message)
        {
            Console.WriteLine(message);
        }
    }
}
