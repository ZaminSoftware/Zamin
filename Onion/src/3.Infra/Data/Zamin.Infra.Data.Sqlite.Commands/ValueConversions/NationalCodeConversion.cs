﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Zamin.Infra.Data.Sqlite.Commands.ValueConversions;

public class NationalCodeConversion : ValueConverter<NationalCode, string>
{
    public NationalCodeConversion() : base(c => c.Value, c => NationalCode.FromString(c))
    {

    }
}
