using System;
using System.IO;

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

            var modulo = p * q;
            var phi = (p - new BigInt("1")) * (q - new BigInt("1"));
            var publicExponent = RSA.CalculatePublicExponent(phi);
            var secretExponent = RSA.CalculateSecretExponent(publicExponent, phi);


            var writer = new StreamWriter("encoded.txt");
            foreach (var item in RSA.Encode(value, publicExponent, modulo))
                writer.WriteLine(item);
            writer.Close();

            reader = new StreamReader("encoded.txt");
            var data = RSA.Decode(reader.ReadToEnd().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries),
                secretExponent, modulo);
            reader.Close();

            var result = new StreamWriter("result.txt");
            result.Write(data);
            result.Close();
        }
    }
}
