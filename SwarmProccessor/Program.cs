using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwarmCL;

namespace SwarmProccessor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start console");
            new SwarmCL.SwarmEngine(TargetTypes.FileList, @"C:\Resources\Paths to Files 2.txt").Start();
            Console.Read();
        }
    }
}
