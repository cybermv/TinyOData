namespace TinyOData.Query.Filter.Segments
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Class that represents a linear collection of filter segments.
    ///
    /// </summary>
    public class FilterSegments : IEnumerable<SegmentBase>
    {
        private readonly List<SegmentBase> _segments;

        /// <summary>
        /// Instatiates a new <see cref="FilterSegments"/> collection with
        /// no segments set
        /// </summary>
        public FilterSegments()
        {
            this._segments = new List<SegmentBase>();
        }

        /// <summary>
        /// Adds a new segment to the end of the line
        /// </summary>
        /// <param name="segment">The segment to add</param>
        public void Append(SegmentBase segment)
        {
            this._segments.Add(segment);
        }

        /// <summary>
        /// Gets a segment with the given index from the line
        /// </summary>
        /// <param name="index">Index of the segment</param>
        /// <returns>The found segment</returns>
        public SegmentBase this[int index] { get { return _segments[index]; } }

        #region IEnumerable

        public IEnumerator<SegmentBase> GetEnumerator()
        {
            return this._segments.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion IEnumerable
    }
}