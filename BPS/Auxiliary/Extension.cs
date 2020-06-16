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
            int length = path.Length;
            if (length > 4)
            {
                if (!path.Substring(length - 4, 4).Equals(FILENAME_EXTENSION))
                {
                    return path + FILENAME_EXTENSION;
                }
                else
                {
                    return path;
                }
            }
            else if (path.Equals(FILENAME_EXTENSION))
            {
                return path;
            }
            else
            {
                return path + FILENAME_EXTENSION;
            }
        }

        #endregion Public

        #endregion Methods
    }
}