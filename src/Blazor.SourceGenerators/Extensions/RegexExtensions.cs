﻿// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

using System.Text.RegularExpressions;

namespace Blazor.SourceGenerators.Extensions
{
    static class RegexExtensions
    {
        internal static string? GetMatchGroupValue(this Regex regex, string input, string groupName)
        {
            var match = regex.Match(input);
            if (match is null)
            {
                return default!;
            }

            return match.GetGroupValue(groupName);
        }

        internal static string? GetGroupValue(this Match match, string groupName)
        {
            if (match is { Success: true })
            {
                return match.Groups?[groupName]?.Value;
            }

            return default!;
        }
    }
}