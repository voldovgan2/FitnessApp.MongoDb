﻿/* Copyright 2015-present MongoDB Inc.
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

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MongoDB.Driver.Linq.Linq2Implementation.Expressions;
using MongoDB.Driver.Linq.Linq2Implementation.Expressions.ResultOperators;

namespace MongoDB.Driver.Linq.Linq2Implementation.Processors.Pipeline.MethodCallBinders
{
    internal sealed class FirstBinder : IMethodCallBinder<PipelineBindingContext>
    {
        public static IEnumerable<MethodInfo> GetSupportedMethods()
        {
            return MethodHelper.GetEnumerableAndQueryableMethodDefinitions("First")
                .Concat(MethodHelper.GetEnumerableAndQueryableMethodDefinitions("FirstOrDefault"));
        }

        public Expression Bind(PipelineExpression pipeline, PipelineBindingContext bindingContext, MethodCallExpression node, IEnumerable<Expression> arguments)
        {
            var source = pipeline.Source;
            if (arguments.Any())
            {
                source = BinderHelper.BindWhere(
                    pipeline,
                    bindingContext,
                    ExpressionHelper.GetLambda(arguments.Single()));
            }

            source = new TakeExpression(source, Expression.Constant(1));

            return new PipelineExpression(
                source,
                pipeline.Projector,
                new FirstResultOperator(
                    pipeline.Projector.Type,
                    pipeline.Projector.Serializer,
                    node.Method.Name == nameof(Enumerable.FirstOrDefault)));
        }
    }
}
