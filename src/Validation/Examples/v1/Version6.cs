namespace Validation.Examples.v1;

public static class Version6
{
    public static bool ValidateCPF(string sourceCPF)
    {
        if (sourceCPF is null) return false;

        Span<int> cpfArray = stackalloc int[11];

        var totalDigitoI = 0;
        var totalDigitoII = 0;
        var count = 0;

        var lastDigit = -1;
        var allSame = true;

        foreach (var c in sourceCPF)
            if (char.IsDigit(c))
            {
                if (count > 10) return false;

                var digit = c - '0';

                if (count != 0 && digit != lastDigit)
                    allSame = false;

                cpfArray[count] = digit;
                lastDigit = digit;

                if (count < 9)
                {
                    totalDigitoI += digit * (10 - count);
                    totalDigitoII += digit * (11 - count);
                }

                count++;
            }

        if (count is not 11 || allSame) return false;

        var modI = totalDigitoI % 11;
        modI = modI < 2 ? 0 : 11 - modI;

        if (cpfArray[9] != modI) return false;

        totalDigitoII += modI * 2;

        var modII = totalDigitoII % 11;
        modII = modII < 2 ? 0 : 11 - modII;

        return cpfArray[10] == modII;
    }
}