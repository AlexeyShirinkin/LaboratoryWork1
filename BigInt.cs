using System;
using System.Numerics;

namespace LaboratoryWork1
{
    public class BigInt
    {
        public BigInteger Value { get; }
        public int Sign => Value.Sign;

        public BigInt()
        {
            Value = new BigInteger(0);
        }
            
        public BigInt(string numbers)
        {
            Value = BigInteger.Parse(numbers);
        }

        public BigInt(byte[] numbers)
        {
            Value = new BigInteger(numbers);
        }

        public static bool operator ==(BigInt number1, BigInt number2) => number1.Value == number2.Value;
        public static bool operator !=(BigInt number1, BigInt number2) => !(number1 == number2);
        public static bool operator >(BigInt number1, BigInt number2) => number1.Value > number2.Value;
        public static bool operator >=(BigInt number1, BigInt number2) => number1.Value >= number2.Value;
        public static bool operator <(BigInt number1, BigInt number2) => number1.Value < number2.Value;
        public static bool operator <=(BigInt number1, BigInt number2) => number1.Value <= number2.Value;

        public static BigInt operator +(BigInt number1, BigInt number2)
            => new BigInt((number1.Value + number2.Value).ToByteArray());

        public static BigInt operator -(BigInt number1, BigInt number2)
            => new BigInt((number1.Value - number2.Value).ToByteArray());

        public static BigInt operator *(BigInt number1, BigInt number2)
            => new BigInt((number1.Value * number2.Value).ToByteArray());

        public static BigInt operator /(BigInt number1, BigInt number2)
            => new BigInt((number1.Value / number2.Value).ToByteArray());

        public static BigInt operator %(BigInt number1, BigInt number2)
            => new BigInt((number1.Value % number2.Value).ToByteArray());

        public static BigInt InverseElementOnModulo(BigInt number, BigInt modulo)
        {
            var divisor = GreatestCommonDivisor(number, modulo, out var result, out var y);
            if (divisor.Value != 1)
                throw new InvalidOperationException();
            result = (result % modulo + modulo) % modulo;
            return result;
        }

        private static BigInt GreatestCommonDivisor(BigInt number, BigInt modulo, 
            out BigInt coefficient1, out BigInt coefficient2)
        {
            if (number.Value == 0)
            {
                coefficient1 = new BigInt("0");
                coefficient2 = new BigInt("1");
                return modulo;
            }

            var divisor = GreatestCommonDivisor(modulo % number, number,
                out var newCoefficient1, out var newCoefficient2);

            coefficient1 = newCoefficient2 - modulo / number * newCoefficient1;
            coefficient2 = newCoefficient1;
            return divisor;
        }
    }
}   