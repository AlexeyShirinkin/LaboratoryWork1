using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LaboratoryWork1
{
    public static class RSA
    {
        public static List<string> Encode(string value, BigInt publicExponent, BigInt modulo)
        {
            var encodedParts = new List<string>();

            foreach (var symbol in value)
            {
                var result = FastPowOnModulo(new BigInt(((int)symbol).ToString()), publicExponent, modulo);
                encodedParts.Add(result.Value.ToString());
            }

            return encodedParts;
        }

        public static string Decode(List<string> input, BigInt secretExponent, BigInt modulo)
        {
            var decodedValue = new StringBuilder();

            foreach (var item in input)
            {
                var result = FastPowOnModulo(new BigInt(item), secretExponent, modulo);
                decodedValue.Append((char)result.Value);
            }

            return decodedValue.ToString();
        }

        public static BigInt CalculateSecretExponent(BigInt publicExponent, BigInt modulo)
            => InverseElementOnModulo(publicExponent, modulo);

        public static BigInt CalculatePublicExponent(BigInt modulo)
        {
            var exponent = new BigInt("3");
            var one = new BigInt("1");

            for (var i = new BigInt("0"); i < modulo; i += one)
            {
                if (EuclideanAlgorithm.GreatestCommonDivisor(exponent, modulo, out var x, out var y) == one)
                    return exponent;
                exponent += one;
            }

            return exponent;
        }

        public static bool IsSimpleNumber(BigInt number)
        {
            var zero = new BigInt("0");
            var one = new BigInt("1");
            var two = new BigInt("2");

            if (number < two)
                return false;

            if (number == two)
                return true;

            for (var i = two; i < number; i += one)
                if (number % i == zero)
                    return false;

            return true;
        }

        private static BigInt InverseElementOnModulo(BigInt number, BigInt modulo)
        {
            var divisor = EuclideanAlgorithm.GreatestCommonDivisor(number, modulo, out var result, out var y);
            if (divisor != new BigInt("1"))
                throw new InvalidOperationException();
            result = (result % modulo + modulo) % modulo;
            return result;
        }

        private static BigInt FastPowOnModulo(BigInt value, BigInt exponent, BigInt modulo)
        {
            var binaryValue = ConvertToBinary(exponent);

            var arr = new BigInt[binaryValue.Count];
            arr[0] = value;
            for (var i = 1; i < binaryValue.Count; i++)
                arr[i] = arr[i - 1] * arr[i - 1] % modulo;

            var multiplication = new BigInt("1");
            var zero = new BigInt("0");
            for (var j = 0; j < binaryValue.Count; j++)
                if (binaryValue[j] > zero)
                    multiplication *= binaryValue[j] * arr[j];

            return multiplication % modulo;
        }

        private static List<BigInt> ConvertToBinary(BigInt value)
        {
            var temp = value;
            var binaryValue = new List<BigInt>();
            var two = new BigInt("2");

            while (temp.Value >= 1)
            {
                binaryValue.Add(temp % two);
                temp /= two;
            }

            return binaryValue;
        }
    }
}
