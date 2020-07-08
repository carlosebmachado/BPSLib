using System.Collections.Generic;

namespace BPS
{
    public class Section
    {
        #region Vars

        /// <summary></summary>
        public string Name { get; set; }
        /// <summary></summary>
        public List<Data> Data { get; set; }

        #endregion Vars

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public Section(string name, List<Data> data)
        {
            Name = name;
            Data = data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public Section(string name)
        {
            Name = name;
            Data = new List<Data>();
        }

        #endregion Constructors

        #region Methods

        #region Public

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Add(Data data)
        {
            if (!Exists(data.Key))
            {
                Data.Add(data);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataKey"></param>
        /// <param name="newDataKey"></param>
        /// <returns></returns>
        public bool AlterKey(string dataKey, string newDataKey)
        {
            if (Exists(dataKey))
            {
                Find(dataKey).Key = newDataKey;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataKey"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public bool AlterValue(string dataKey, string newValue)
        {
            if (Exists(dataKey))
            {
                Find(dataKey).Value = newValue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void RemoveAll()
        {
            Data.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool Remove(string dataKey)
        {
            foreach (var d in Data)
            {
                if (d.Key.Equals(dataKey))
                {
                    Data.Remove(d);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public Data Find(string dataKey)
        {
            foreach(Data d in Data)
            {
                if (d.Key.Equals(dataKey))
                {
                    return d;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public string FindValue(string dataKey)
        {
            return Find(dataKey).Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool Exists(string dataKey)
        {
            return Find(dataKey) != null;
        }

        #endregion Public

        #endregion Methods

    }
}