﻿// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

using Blazor.SourceGenerators.Extensions;
using Blazor.SourceGenerators.Types;

namespace Blazor.SourceGenerators.CSharp
{
    public record CSharpType(
        string RawName,
        string RawTypeName,
        bool IsNullable = false,
        CSharpAction? ActionDeclation = null)
    {
        /// <summary>
        /// Gets a string representation of the C# type as a parameter declaration. For example,
        /// <c>"DateTime date"</c> might be returned from a <see cref="CSharpType"/> with
        /// <c>"date"</c> as its <see cref="RawName"/> and <c>"DateTime"</c>
        /// as its <see cref="RawTypeName"/>.
        /// </summary>
        public string ToParameterString()
        {
            var isCallback = ActionDeclation is not null;
            var typeName = TypeMap.PrimitiveTypes.IsPrimitiveType(RawTypeName)
                ? TypeMap.PrimitiveTypes[RawTypeName]
                : isCallback
                    ? "string" // When the action is a callback, we require `T` instance and callback names.
                    : RawTypeName;

            var parameterName = ToArgumentString();

            return IsNullable
                ? $"{typeName}? {parameterName} = null"
                : $"{typeName} {parameterName}";
        }

        public string ToArgumentString()
        {
            var isCallback = ActionDeclation is not null;
            var parameterName = isCallback
                ? $"on{RawName.CapitalizeFirstLetter()}MethodName"
                : RawName.LowerCaseFirstLetter();

            return parameterName;
        }
    }
}