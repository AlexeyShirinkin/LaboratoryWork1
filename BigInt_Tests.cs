using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LaboratoryWork1
{
    internal class BigIntTests
    {
        [TestCase("256", "256", true, TestName = "WhenSamePositiveNumbers")]
        [TestCase("256", "26", false, TestName = "WhenPositiveNumbers")]
        [TestCase("-256", "-256", true, TestName = "WhenSameNegativeNumbers")]
        [TestCase("-256", "-26", false, TestName = "WhenNegativeNumbers")]
        [TestCase("256", "-26", false, TestName = "WhenPositiveAndNegativeNumbers")]
        public void CorrectEqual(string value1, string value2, bool isEqual)
        {
            Assert.AreEqual(isEqual, new BigInt(value1) == new BigInt(value2));
        }

        [TestCase("256", "256", false, TestName = "WhenSamePositiveNumbers")]
        [TestCase("256", "26", true, TestName = "WhenPositiveNumbers")]
        [TestCase("-256", "-256", false, TestName = "WhenSameNegativeNumbers")]
        [TestCase("-26", "-256", true, TestName = "WhenNegativeNumbers")]
        [TestCase("26", "-256", true, TestName = "WhenPositiveAndNegativeNumbers")]
        public void CorrectCompare_GreaterThan(string value1, string value2, bool isEqual)
        {
            Assert.AreEqual(isEqual, new BigInt(value1) > new BigInt(value2));
        }

        [TestCase("264", "264", false, TestName = "WhenSamePositiveNumbers")]
        [TestCase("25", "26", true, TestName = "WhenPositiveNumbers")]
        [TestCase("-256", "-256", false, TestName = "WhenSameNegativeNumbers")]
        [TestCase("-257", "-256", true, TestName = "WhenNegativeNumbers")]
        [TestCase("-26", "26", true, TestName = "WhenPositiveAndNegativeNumbers")]
        public void CorrectCompare_LessThan(string value1, string value2, bool isEqual)
        {
            var first = new BigInt(value1);
            Assert.AreEqual(isEqual, first < new BigInt(value2));
        }

        [TestCase("847", "178", "1025", TestName = "WhenNumbersHaveSameLength")]
        [TestCase("95841", "177478", "273319", TestName = "WhenLeftNumberIsShorter")]
        [TestCase("177478", "95841", "273319", TestName = "WhenRightNumberIsShorter")]
        [TestCase("17747984891984498489498489", "494981874984984894984984", "18242966766969483384483473", TestName = "WhenBigNumbers")]
        public void CorrectAddition_WhenBothNumberArePositive(string value1, string value2, string expectedValue)
        {
            var number1 = new BigInt(value1);
            var number2 = new BigInt(value2);

            var actual = number1 + number2;

            Assert.AreEqual(new BigInt(expectedValue).Value, actual.Value);
        }

        [TestCase("-847", "-178", "-1025", TestName = "WhenNumbersHaveSameLength")]
        [TestCase("-95841", "-177478", "-273319", TestName = "WhenLeftNumberIsShorter")]
        [TestCase("-177478", "-95841", "-273319", TestName = "WhenRightNumberIsShorter")]
        [TestCase("-17747984891984498489498489", "-494981874984984894984984", "-18242966766969483384483473", TestName = "WhenBigNumbers")]
        public void CorrectAddition_WhenBothNumberAreNegative(string value1, string value2, string expectedValue)
        {
            var number1 = new BigInt(value1);
            var number2 = new BigInt(value2);

            var actual = number1 + number2;

            Assert.AreEqual(new BigInt(expectedValue).Value, actual.Value);
        }

        [TestCase("847", "178", "669", TestName = "WhenNumbersHaveSameLength")]
        [TestCase("95841", "177478", "-81637", TestName = "WhenLeftNumberIsShorter")]
        [TestCase("177478", "95841", "81637", TestName = "WhenRightNumberIsShorter")]
        [TestCase("17747984891984498489498489", "41949818749849848949840984", "-24201833857865350460342495",
            TestName = "WhenBigNumbers")]
        public void CorrectSubtraction_WhenBothNumberArePositive(string value1, string value2, string expectedValue)
        {
            var number1 = new BigInt(value1);
            var number2 = new BigInt(value2);

            var actual = number1 - number2;

            Assert.AreEqual(new BigInt(expectedValue).Value, actual.Value);
        }
    }
}