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

using MongoDB.Bson;
using MongoDB.Driver.Core.Misc;
using MongoDB.Driver.Linq.Linq3Implementation.Ast.Visitors;
using MongoDB.Driver.Linq.Linq3Implementation.Misc;
using System.Collections.Generic;
using System.Linq;

namespace MongoDB.Driver.Linq.Linq3Implementation.Ast.Stages
{
    internal sealed class AstUnsetStage : AstStage
    {
        private IReadOnlyList<string> _fields;

        public AstUnsetStage(IEnumerable<string> fields)
        {
            _fields = Ensure.IsNotNull(fields, nameof(fields)).AsReadOnlyList();
        }

        public IReadOnlyList<string> Fields => _fields;
        public override AstNodeType NodeType => AstNodeType.UnsetStage;

        public override AstNode Accept(AstNodeVisitor visitor)
        {
            return visitor.VisitUnsetStage(this);
        }

        public override BsonValue Render()
        {
            return new BsonDocument("$unset", RenderFields());
        }

        private BsonValue RenderFields()
        {
            if (_fields.Count == 1)
            {
                return _fields[0];
            }
            else
            {
                return new BsonArray(_fields);
            }
        }
    }
}
