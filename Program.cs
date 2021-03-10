using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LaboratoryWork1
{
    public class Program
    {
        public static void Main()
        {
            var a = "-1234";
            BigInteger.Parse(a);
            var value1 = BigInteger.Parse(a); ;
            Console.WriteLine(value1);
        }
    }
}
