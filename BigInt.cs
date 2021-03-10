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
    }
}   