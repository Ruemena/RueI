﻿namespace RueI.Parsing.Tags.ConcreteTags;

using RueI.Parsing.Enums;
using RueI.Parsing.Records;

/// <summary>
/// Provides a way to handle line-height tags.
/// </summary>
[RichTextTag]
public class LineHeightTag : MeasurementTag
{
    private const string TAGFORMAT = "<line-height={0}>";

    /// <inheritdoc/>
    public override string[] Names { get; } = { "line-height" };

    /// <inheritdoc/>
    public override bool HandleTag(ParserContext context, MeasurementInfo info)
    {
        var (value, style) = info;

        float convertedValue = style switch
        {
            MeasurementUnit.Percentage => value / 100 * Constants.DEFAULTHEIGHT,
            MeasurementUnit.Ems => value * Constants.EMSTOPIXELS,
            _ => value
        };

        context.CurrentLineHeight = convertedValue;
        context.ResultBuilder.AppendFormat(TAGFORMAT, convertedValue);

        context.AddEndingTag<CloseLineHeightTag>();

        return true;
    }
}
