﻿using System.Text.RegularExpressions;

namespace neophyte.api.Core.Utilities;

public static class SeatRange
{
    private static readonly Regex IsDigitMatcher = new(@"\D", RegexOptions.Compiled);
    private static readonly Regex IsLetterMatcher = new(@"\d", RegexOptions.Compiled);
    private const char Separator = '-';

    public static List<string> Parse(string seatRange)
    {
        var result = new List<string>();

        // if the range is empty or null, return an empty collection
        if (string.IsNullOrWhiteSpace(seatRange))
            return result;

        // normalize the data
        seatRange = seatRange.Trim().ToUpperInvariant();

        // if the value doesnt contain a '-' to show it is a range then return the value
        if (!seatRange.Contains(Separator))
            return new List<string>
            {
                seatRange
            };

        var parts = seatRange.Split(Separator);
        var start = Split(parts[0]);
        var end = Split(parts[1]);

        if (start.Prefix != end.Prefix)
            throw new Exception("Ranges cannot have different prefixes.");

        foreach (var value in Enumerable.Range(start.Value, end.Value - start.Value))
            result.Add($"{start.Prefix}{value}");

        return result;
    }

    private static (string Prefix, int Value) Split(string seatNumber)
    {
        var prefix = IsLetterMatcher.Replace(seatNumber, string.Empty);
        var value = int.Parse(IsDigitMatcher.Replace(seatNumber, string.Empty));
        return (prefix, value);
    }
}