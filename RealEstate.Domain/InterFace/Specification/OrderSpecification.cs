﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstate.Domain.InterFace.Specification
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderSpecification
    {
        NameAsc, NameDesc, PriceAsc, PriceDesc
    }
}
