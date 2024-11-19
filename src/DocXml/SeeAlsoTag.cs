namespace LoxSmoke.DocXml
{
    /// <summary>
    /// Seealso tag with optional cref and href attributes.
    /// </summary>
    public class SeeAlsoTag
    {
        /// <summary>
        /// Cref attribute value. This value is optional.
        /// </summary>
        public string Cref { get; set; }

        /// <summary>
        /// Href attribute value. This value is optional.
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// The title, if any, for this link.
        /// </summary>
        public string Text { get; set; }
    }
}
