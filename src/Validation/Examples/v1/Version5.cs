namespace Validation.Examples.v1;

public static class Version5
{
    public static bool ValidateCPF(string sourceCPF)
    {
        if (sourceCPF is null) return false;

        Span<int> cpfArray = stackalloc int[11];

        var count = 0;

        foreach (var c in sourceCPF)
            if (char.IsDigit(c))
            {
                if (count > 10)
                    return false;

                var teste = (c - '0');

                cpfArray[count] = (c - '0');
                count++;
            }

        if (count is not 11) return false;

        if (ValidateEqualsDigit(ref cpfArray)) return false;

        var totalDigitoI = 0;
        var totalDigitoII = 0;
        int modI;
        int modII;

        for (var position = 0; position < cpfArray.Length - 2; position++)
        {
            totalDigitoI += cpfArray[position] * (10 - position);
            totalDigitoII += cpfArray[position] * (11 - position);
        }

        modI = totalDigitoI % 11;
        if (modI < 2) modI = 0;
        else modI = 11 - modI;

        if (cpfArray[9] != modI)
            return false;

        totalDigitoII += modI * 2;

        modII = totalDigitoII % 11;
        if (modII < 2) modII = 0;
        else modII = 11 - modII;

        return cpfArray[10] == modII;
    }

    private static bool ValidateEqualsDigit(ref Span<int> input)
    {
        for (var i = 0; i < 11; i++)
            if (input[i] != input[0])
                return false;

        return true;
    }
}