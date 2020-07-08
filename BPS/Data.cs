namespace BPS
{
    public class Data
    {
        #region Vars

        /// <summary>The data key</summary>
        public string Key { get; set; }

        /// <summary>The data value</summary>
        public string Value { get; set; }

        #endregion Vars


        #region Constructors

        /// <summary>
        /// Constructor with key and value
        /// </summary>
        /// <param name="key">The key of the data</param>
        /// <param name="value">The value of the data</param>
        public Data(string key, string value)
        {
            Key = key;
            Value = value;
        }

        #endregion Constructors
    }
}
