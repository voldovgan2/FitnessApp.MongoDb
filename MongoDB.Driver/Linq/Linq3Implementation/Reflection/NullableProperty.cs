﻿/* Copyright 2010-present MongoDB Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System.Reflection;

namespace MongoDB.Driver.Linq.Linq3Implementation.Reflection
{
    internal static class NullableProperty
    {
        // private static fields
        private static readonly PropertyInfo __hasValue;
        private static readonly PropertyInfo __value;

        // static constructor
        static NullableProperty()
        {
            __hasValue = ReflectionInfo.Property((int? n) => n.HasValue);
            __value = ReflectionInfo.Property((int? n) => n.Value);
        }

        // public properties
        public static PropertyInfo HasValue => __hasValue;
        public static PropertyInfo Value => __value;
    }
}
