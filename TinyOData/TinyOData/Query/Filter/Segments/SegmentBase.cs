namespace TinyOData.Query.Filter.Segments
{
    using System.Linq.Expressions;

    /// <summary>
    /// Base class for all $filter query segments.
    /// </summary>
    public abstract class SegmentBase
    {
        public Expression AssociatedExpression { get; protected set; }
    }
}