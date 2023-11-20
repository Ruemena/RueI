﻿namespace RueI.Parsing.Tags.ConcreteTags;

using RueI.Enums;

/// <summary>
/// Provides a way to handle smallcaps tags.
/// </summary>
public class SmallcapsTag : NoParamsTag
{
    /// <inheritdoc/>
    public override string[] Names { get; } = { "smallcaps" };

    /// <inheritdoc/>
    public override bool HandleTag(ParserContext context)
    {
        context.CurrentCase = CaseStyle.Smallcaps;
        context.ResultBuilder.Append("<smallcaps>");
        context.RemoveEndingTag<SmallcapsTag>();
        return true;
    }
}

/// <summary>
/// Provides a way to handle closing smallcaps tags.
/// </summary>
public class CloseSmallcapsTag : NoParamsTag
{
    /// <inheritdoc/>
    public override string[] Names { get; } = { "/smallcaps" };

    /// <inheritdoc/>
    public override bool HandleTag(ParserContext context)
    {
        return true; // always does nothing
    }
}

/// <summary>
/// Provides a way to handle allcaps tags.
/// </summary>
public class AllcapsTag : NoParamsTag
{
    /// <inheritdoc/>
    public override string[] Names { get; } = { "allcaps", "uppercase" };

    /// <inheritdoc/>
    public override bool HandleTag(ParserContext context)
    {
        if (context.CurrentCase != CaseStyle.Uppercase)
        {
            context.CurrentCase = CaseStyle.Uppercase;
            context.ResultBuilder.Append("<allcaps>");
            context.AddEndingTag<SmallcapsTag>();
        }

        return true;
    }
}

/// <summary>
/// Provides a way to handle closing smallcaps tags.
/// </summary>
public class CloseAllcapsTag : NoParamsTag
{
    /// <inheritdoc/>
    public override string[] Names { get; } = { "/allcaps" };

    /// <inheritdoc/>
    public override bool HandleTag(ParserContext context)
    {
        if (context.CurrentCase == CaseStyle.Uppercase)
        {
            SharedTag<SmallcapsTag>.Singleton.HandleTag(context); // equivalent
        }

        return true;
    }
}

/// <summary>
/// Provides a way to handle lowercase tags.
/// </summary>
public class LowercaseTag : NoParamsTag
{
    /// <inheritdoc/>
    public override string[] Names { get; } = { "lowercase" };

    /// <inheritdoc/>
    public override bool HandleTag(ParserContext context)
    {
        if (context.CurrentCase != CaseStyle.Lowercase)
        {
            context.CurrentCase = CaseStyle.Lowercase;
            context.ResultBuilder.Append("<lowercase>");
            context.AddEndingTag<SmallcapsTag>();
        }

        return true;
    }
}

/// <summary>
/// Provides a way to handle closing smallcaps tags.
/// </summary>
public class CloseLowercase : NoParamsTag
{
    /// <inheritdoc/>
    public override string[] Names { get; } = { "/lowercase" };

    /// <inheritdoc/>
    public override bool HandleTag(ParserContext context)
    {
        if (context.CurrentCase == CaseStyle.Lowercase)
        {
            SharedTag<SmallcapsTag>.Singleton.HandleTag(context); // equivalent
        }

        return true;
    }
}