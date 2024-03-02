﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zamin.Core.Domain.Toolkits.ValueObjects;

namespace Zamin.Infra.Data.Sqlite.Commands.ValueConversions;

public class TitleConversion : ValueConverter<Title, string>
{
    public TitleConversion() : base(c => c.Value, c => Title.FromString(c))
    {

    }
}