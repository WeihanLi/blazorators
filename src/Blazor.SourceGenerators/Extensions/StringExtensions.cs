﻿// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Blazor.SourceGenerators.Extensions
{
    static class StringExtensions
    {
        internal static string CapitalizeFirstLetter(this string name) =>
            $"{char.ToUpper(name[0])}{name.Substring(1, name.Length - 1)}";

        internal static string LowerCaseFirstLetter(this string name) =>
            $"{char.ToLower(name[0])}{name.Substring(1, name.Length - 1)}";
    }
}