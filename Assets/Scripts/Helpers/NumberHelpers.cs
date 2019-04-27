using System;
using UnityEngine;

public static class NumberHelpers
{
    public static int GetDigitAtPosition(this int number, int position)
    {
        int digit = (int)(number / Math.Pow(10, position - 1)) % 10;
        return digit;
    }
}