namespace BPS
{
    public class Data
    {
        #region Vars

        /// <summary></summary>
        public string Key { get; set; }

        /// <summary></summary>
        public string Value { get; set; }

        #endregion Vars

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public Data(string key, string value)
        {
            Key = key;
            Value = value;
        }

        #endregion Constructors
    }
}