namespace LaboratoryWork1
{
    public static class EuclideanAlgorithm
    {
        public static BigInt GreatestCommonDivisor(BigInt number, BigInt modulo,
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