using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaboratoryWork1
{
    public class BigInt
    {
        private readonly List<byte> numbers = new List<byte>();
        public bool IsPositiveSign { get; }
        public int Count => numbers.Count;
        public byte this[int index] 
            => index >= 0 && index < Count 
                ? numbers[index] 
                : (byte) 0; 

        public string Value
        {
            get
            {
                var result = new StringBuilder();
                if (!IsPositiveSign)
                    result.Append("-");
                for (var i = Count - 1; i >= 0; --i)
                    result.Append(numbers[i]);
                return result.ToString();
            }
        }

        public BigInt()
        {
            IsPositiveSign = true;
            numbers.Add(0);
        }

        public BigInt(string value)
        {
            IsPositiveSign = value[0] != '-';
            if (!IsPositiveSign)
                value = value.Remove(0, 1);
            for (var i = value.Length - 1; i >= 0; --i)
                numbers.Add(byte.Parse(value[i].ToString()));
        }

        private BigInt(bool isPositiveSign, List<byte> numbers)
        {
            IsPositiveSign = isPositiveSign;
            this.numbers = numbers;
        }

        private BigInt(bool isPositiveSign, BigInt value)
        {
            IsPositiveSign = isPositiveSign;
            numbers = value.numbers;
        }

        public static BigInt operator+(BigInt value1, BigInt value2)
        {
            if (value1.IsPositiveSign && value2.IsPositiveSign)
                return new BigInt(true, Sum(value1, value2));
            if (!value1.IsPositiveSign && !value2.IsPositiveSign)
                return new BigInt(false, Sum(
                    new BigInt(true, value1),
                    new BigInt(true, value2)));
            if (value1 > value2)
                return value1 - new BigInt(true, value2);
            return value2 - new BigInt(true, value1);
        }

        private static List<byte> Sum(BigInt value1, BigInt value2)
        {
            var result = new List<byte>();
            var maxLength = Math.Max(value1.Count, value2.Count);

            var rank = 0;
            for (var i = 0; i < maxLength; ++i)
            {
                var value = value1[i] + value2[i] + rank;
                result.Add((byte)(value % 10));
                rank = value / 10;
            }
             
            if (rank > 0)
                result.Add((byte)rank);
            return result;
        }

        public static BigInt operator -(BigInt value1, BigInt value2)
        {
            if (value1.IsPositiveSign && value2.IsPositiveSign)
                return new BigInt(value1 > value2, Subtraction(value1, value2));
            if (!value1.IsPositiveSign && !value2.IsPositiveSign)
                return new BigInt(value1 > value2, 
                    Subtraction(
                        new BigInt(true, value1),
                        new BigInt(true, value2)));
            if (value1 > value2)
                return value1 + new BigInt(true, value2);
            return new BigInt(false, value2 + new BigInt(true, value1));
        }

        private static List<byte> Subtraction(BigInt value1, BigInt value2)
        {
            var resultSign = value1 > value2;
            var newValue1 = resultSign ? value1 : value2;
            var newValue2 = resultSign ? value2 : value1;

            var result = new List<byte>();
            var maxLength = Math.Max(value1.Count, value2.Count);

            var rank = 0;
            for (var i = 0; i < maxLength; ++i)
            {
                var value = newValue1[i] - newValue2[i] - rank;
                if (value < 0)
                {
                    value += 10;
                    rank = value / 10 + 1;
                }
                else rank = value / 10;

                result.Add((byte)(value % 10));
            }

            while (result[result.Count - 1] == 0)
                result.RemoveAt(result.Count - 1);
            return result;
        }
        public static bool operator ==(BigInt value1, BigInt value2)
        {
            if (value1.IsPositiveSign != value2.IsPositiveSign || value1.Count != value2.Count)
                return false;
            for (var i = 0; i < value1.Count; ++i)
                if (value1[i] != value2[i])
                    return false;
            return true;
        }

        public static bool operator !=(BigInt value1, BigInt value2) => !(value1 == value2);

        public static bool operator >(BigInt value1, BigInt value2)
            => value1.IsPositiveSign != value2.IsPositiveSign
                ? value1.IsPositiveSign
                : CompareWithPredicate(value1, value2, (x, y) => x > y);

        public static bool operator >=(BigInt value1, BigInt value2)
            => value1 > value2 || value1 == value2;

        public static bool operator <(BigInt value1, BigInt value2)
            => value1.IsPositiveSign != value2.IsPositiveSign 
                ? value2.IsPositiveSign 
                : CompareWithPredicate(value1, value2, (x, y) => x < y);

        public static bool operator <=(BigInt value1, BigInt value2)
            => value1 < value2 || value1 == value2;

        private static bool CompareWithPredicate(BigInt value1, BigInt value2, Func<int, int, bool> predicate)
        {
            if (value1.Count != value2.Count)
                return value1.IsPositiveSign
                    ? predicate(value1.Count, value2.Count)
                    : !predicate(value1.Count, value2.Count);

            for (var i = value1.Count - 1; i >= 0; --i)
                if (value1[i] != value2[i])
                    return value1.IsPositiveSign
                        ? predicate(value1[i], value2[i])
                        : !predicate(value1[i], value2[i]);
            return false;
        }
    }
}