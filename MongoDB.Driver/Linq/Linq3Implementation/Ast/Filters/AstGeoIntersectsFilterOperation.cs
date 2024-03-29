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

namespace MongoDB.Driver.Linq.Linq3Implementation.Ast.Filters
{
    internal sealed class AstGeoIntersectsFilterOperation : AstFilterOperation
    {
        private readonly BsonDocument _geometry;

        public AstGeoIntersectsFilterOperation(BsonDocument geometry)
        {
            _geometry = Ensure.IsNotNull(geometry, nameof(geometry));
        }

        public BsonDocument Geometry => _geometry;
        public override AstNodeType NodeType => AstNodeType.GeoIntersectsFilterOperation;

        public override AstNode Accept(AstNodeVisitor visitor)
        {
            return visitor.VisitGeoIntersectsFilterOperation(this);
        }

        public override BsonValue Render()
        {
            return new BsonDocument("$geoIntersects", new BsonDocument("$geometry", _geometry));
        }
    }
}
