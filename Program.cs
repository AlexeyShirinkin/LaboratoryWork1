using System;
using System.IO;
using System.Text;

namespace LaboratoryWork1
{
    public class Program
    {
        public static void Main()
        {
            var reader = new StreamReader("text.txt");
            var p = new BigInt(reader.ReadLine());
            var q = new BigInt(reader.ReadLine());
            var value = reader.ReadLine();
            reader.Close();

            if (!RSA.IsSimpleNumber(p) || !RSA.IsSimpleNumber(q) || p == q)
            {
                Console.WriteLine("Числа не взаимопросты! Повторите попытку");
                return;
            }

            var n = p * q;
            var m = (p - new BigInt("1")) * (q - new BigInt("1"));
            var e = RSA.CalculatePublicExponent(m);
            var d = RSA.CalculateSecretExponent(e, m);

            var encoded = RSA.Encode(value, e, n);
            foreach (var item in encoded)
                Console.WriteLine(item);

            Console.WriteLine(RSA.Decode(encoded, d, n));
        }
    }
}
