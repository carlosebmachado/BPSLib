namespace BPS.Auxiliary
{
    internal class Extension
    {
        #region Vars

        internal const string FILENAME_EXTENSION = ".bps";

        #endregion Vars

        #region Methods

        #region Public

        /// <summary>
        /// Insert BPS extension on filename
        /// </summary>
        /// <param name="path">File path</param>
        internal static string Normalize(string path)
        {
            string normalized = "";
            if (path.Length > 4)
            {
                int length = path.Length;
                if (!path.Substring(length - 4, 4).Equals(FILENAME_EXTENSION))
                {
                    normalized = path + FILENAME_EXTENSION;
                }
            }
            else if (!path.Equals(FILENAME_EXTENSION))
            {
                normalized = path + FILENAME_EXTENSION;
            }
            return normalized;
        }

        #endregion Public

        #endregion Methods
    }
}