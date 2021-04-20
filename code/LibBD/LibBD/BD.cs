using System;

namespace LibBD
{
    public abstract class BD
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            abstract bool create(string table, List<DataCollection>)
        }
    }
}
