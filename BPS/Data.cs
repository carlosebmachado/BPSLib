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

        #region Methods

        #region Public

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newKey"></param>
        /// <returns></returns>
        public void AlterKey(string newKey)
        {
            Key = newKey;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public void AlterValue(string newValue)
        {
            Value = newValue;
        }

        #endregion Public

        #endregion Methods
    }
}