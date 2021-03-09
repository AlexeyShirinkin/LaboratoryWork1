using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaboratoryWork1
{
    public class BigInt
    {
        private readonly List<byte> number = new List<byte>();
        public bool IsPositiveSign { get; }
        public int Count => number.Count;
        public byte this[int index] => number[index];

        public string Value
        {
            get
            {
                var result = new StringBuilder();
                if (!IsPositiveSign)
                    result.Append("-");
                for (var i = Count - 1; i >= 0; --i)
                    result.Append(number[i]);
                return result.ToString();
            }
        }

        public BigInt()
        {
            IsPositiveSign = true;
            number.Add(0);
        }

        public BigInt(string value)
        {
            IsPositiveSign = value[0] != '-';
            if (!IsPositiveSign)
                value = value.Remove(0, 1);
            for (var i = value.Length - 1; i >= 0; --i)
                number.Add(byte.Parse(value[i].ToString()));
        }

        private BigInt(bool isPositiveSign, List<byte> number)
        {
            IsPositiveSign = isPositiveSign;
            this.number = number;
        }

        public static BigInt operator+(BigInt value1, BigInt value2)
        {
            var result = new List<byte>();
            var rank = 0;
            var index = 0;
            for (; index < value1.Count && index < value2.Count; ++index)
            {
                var value = value1[index] + value2[index] + rank;
                result.Add((byte)(value % 10));
                rank = value / 10;
            }

            var remainder = index < value1.Count
                ? value1
                : value2;

            for (; index < remainder.Count; ++index)
            {
                var value = remainder[index] + rank;
                result.Add((byte)(value % 10));
                rank = value / 10;
            }
            if (rank > 0)
                result.Add((byte)rank);

            return new BigInt(value1.IsPositiveSign, result);
        }

        public static BigInt operator -(BigInt value1, BigInt value2)
        {
            var resultSign = value1 > value2;
            var newValue1 = value1 > value2 ? value1 : value2;
            var newValue2 = value1 > value2 ? value2 : value1;

            var result = new List<byte>();
            var rank = 0;
            var index = 0;
            for (; index < newValue1.Count && index < newValue2.Count; ++index)
            {
                var value = newValue1[index] - newValue2[index] - rank;
                if (value < 0)
                {
                    value += 10;
                    rank = value / 10 + 1;
                }
                else rank = value / 10;

                result.Add((byte) (value % 10));
            }

            var remainder = index < newValue1.Count
                ? newValue1
                : newValue2;

            for (; index < remainder.Count; ++index)
            {
                var value = remainder[index] - rank;
                if (value < 0)
                {
                    value += 10;
                    rank = value / 10 + 1;
                }
                else rank = value / 10;

                result.Add((byte)(value % 10));
            }

            if (rank > 0)
                result.Add((byte) rank);
            while (result[result.Count - 1] == 0)
                result.RemoveAt(result.Count - 1);

            return new BigInt(resultSign, result);
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