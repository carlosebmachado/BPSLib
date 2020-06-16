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

        public void AddData(Data data)
        {
            Data.Add(data);
        }

        public bool RemoveData(string key)
        {
            foreach(var d in Data)
            {
                if (d.Key.Equals(key))
                {
                    Data.Remove(d);
                    return true;
                }
            }
            return false;
        }

        #endregion Public

        #endregion Methods

    }
}