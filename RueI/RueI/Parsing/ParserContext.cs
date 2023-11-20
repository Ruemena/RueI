namespace RueI.Parsing;

using System.Text;
using NorthwoodLib.Pools;
using RueI.Parsing.Tags;
using RueI.Parsing.Tags.ConcreteTags;

/// <summary>
/// Describes the state of a parser at a time.
/// </summary>
/// <remarks>
/// The <see cref="ParserContext"/> class provides a way for the general state of the parser,
/// such as the current line width or the vertical height, to be modified by passing it along.
/// Tags should modify this in order to change the end result of parsing.
/// </remarks>
public class ParserContext : TextInfo, IDisposable
{
    /// <summary>
    /// Gets a list of tags that the parser should add at the end.
    /// </summary>
    private readonly List<NoParamsTag> endingTags = new(10);

    /// <summary>
    /// Gets the end result string builder.
    /// </summary>
    public StringBuilder ResultBuilder { get; } = StringBuilderPool.Shared.Rent();

    /// <summary>
    /// Gets or sets the final offset for the element as a whole.
    /// </summary>
    public float NewOffset { get; set; } = 0;

    /// <summary>
    /// Gets a stack containing all of the nested sizes.
    /// </summary>
    public Stack<float> SizeTags { get; } = new();

    /// <summary>
    /// Gets or sets the current line width of the parser.
    /// </summary>
    public float CurrentLineWidth { get; set; } = 0;

    /// <summary>
    /// Gets or sets a value indicating whether the parser should parse tags other than noparse.
    /// </summary>
    public bool ShouldParse { get; set; } = true;

    /// <summary>
    /// Gets or sets the total width since a space.
    /// </summary>
    public float WidthSinceSpace { get; set; } = 0;

    /// <summary>
    /// Gets or sets a value indicating whether or not words are currently in no break.
    /// </summary>
    public bool NoBreak { get; set; } = false;

    /// <summary>
    /// Gets or sets the number of color tags that are nested.
    /// </summary>
    public int ColorTags { get; set; } = 0;

    /// <summary>
    /// Adds a <see cref="RichTextTag"/> to a list of tags that will be added to the end of the parser's result.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="RichTextTag"/> to be added as an ending tag (as a <see cref="SharedTag{Tags}"/>).</typeparam>
    public void AddEndingTag<T>()
        where T : NoParamsTag, new()
    {
        endingTags.Add(SharedTag<T>.Singleton);
    }

    /// <summary>
    /// Removes a <see cref="RichTextTag"/> from the list list of tags that will be added to the end of the parser's result.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="RichTextTag"/> to be removed from the ending tags (as a <see cref="SharedTag{Tags}"/>).</typeparam>
    public void RemoveEndingTag<T>()
        where T : NoParamsTag, new()
    {
        endingTags.Remove(SharedTag<T>.Singleton);
    }

    /// <summary>
    /// Applies the <see cref="endingTags"/> and closing <see cref="SizeTags"/> tags to this <see cref="ParserContext"/>.
    /// </summary>
    internal void ApplyClosingTags()
    {
        foreach (NoParamsTag tag in endingTags)
        {
            tag.HandleTag(this);
        }

        foreach (float t in SizeTags)
        {
            SharedTag<CloseSizeTag>.Singleton.HandleTag(this);
        }

        SizeTags.Clear();
        endingTags.Clear();
    }

    /// <summary>
    /// Disposes this ParserContext, returning the string builder to the pool.
    /// </summary>
    public void Dispose()
    {
        StringBuilderPool.Shared.Return(ResultBuilder);
    }
}

/*
* using RueI.Enums;
using System.Text;

namespace RueI.Records
{
/// <summary>
/// Represents the context of a parser, for parameters.
/// </summary>
/// <param name="ResultBuilder">The end result string builder.</param>
/// <param name="CurrentLineHeight">The current line height.</param>
/// <param name="CurrentLineWidth">The current width of the line that the parser is on. </param>
/// <param name="Size">Represents the current utilized </param>
/// <param name="NewOffset">The final offset of the element.</param>
/// <param name="CurrentCSpace">The current additional spacing between characters.</param>
/// <param name="ShouldParse">Whether or not tags are currently being parsed.</param>
/// <param name="IsMonospace">Whether or not the text is currently monospace.</param>
/// <param name="IsBold">Whether or not the text is currently bold.</param>
/// <param name="CurrentCase">The current case of the text.</param>
/// <param name="SizeTags">A stack containing all the nested sizes.</param>
/// <param name="ColorTags">The number of color tags that is currently nested.</param>
public record ParserContext {
    public StringBuilder ResultBuilder { get; set; }
    public float CurrentLineHeight { get; set; }
    public float CurrentLineWidth { get; set; }
    public float Size { get; set; }
    public float NewOffset { get; set; }
    public float CurrentCSpace { get; set; }
    public bool ShouldParse { get; set; }
    public bool IsMonospace { get; set; }
    public bool IsBold { get; set; }
    public CaseStyle CurrentCase { get; set; }
    public Stack<float> SizeTags { get; set; }
    public int ColorTag { get; set; }
}
}
*/