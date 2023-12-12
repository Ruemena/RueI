﻿namespace RueI.Parsing.Tags.ConcreteTags;

using RueI.Parsing.Enums;
using RueI.Parsing.Records;

/// <summary>
/// Provides a way to handle size tags.
/// </summary>
[RichTextTag]
public class SizeTag : MeasurementTag
{
    private const string TAGFORMAT = "<size={0}>";

    /// <inheritdoc/>
    public override string[] Names { get; } = { "size" };

    /// <inheritdoc/>
    public override bool HandleTag(ParserContext context, MeasurementInfo info)
    {
        context.SizeTags.Push(context.Size);
        float value = info.Style switch
        {
            MeasurementUnit.Percentage => info.Value / 100 * Constants.DEFAULTSIZE,
            MeasurementUnit.Ems => info.Value * Constants.EMSTOPIXELS,
            _ => info.Value
        };

        context.Size = value;
        context.ResultBuilder.AppendFormat(TAGFORMAT, value);

        return true;
    }
}
