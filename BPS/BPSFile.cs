using BPS.Auxiliary;
using System;
using System.Collections.Generic;

namespace BPS
{
    public class BPSFile
    {
        #region Vars

        /// <summary></summary>
        internal const string ERR_KEY_NOT_FOUND = "Key not found.";
        internal const string ERR_SECTION_NOT_FOUND = "Key not found.";

        /// <summary></summary>
        public List<Section> Sections { get; }

        #endregion Vars

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BPSFile()
        {
            Sections = new List<Section>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sections">File sections</param>
        internal BPSFile(List<Section> sections)
        {
            Sections = sections;
        }

        #endregion Constructors

        #region Methods

        #region Public

        public void DeleteAll()
        {
            Sections.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public bool AddSection(Section section)
        {
            if (!SectionExists(section.Name))
            {
                Sections.Add(section);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool AddData(string sectionName, Data data)
        {
            if (!SectionExists(sectionName))
            {
                AddSection(new Section(sectionName));
            }
            if (!DataExists(sectionName, data.Key))
            {
                FindSection(sectionName).AddData(data);
                return true;
            }
            return false;
        }

        public bool RemoveSection(string sectionName)
        {
            if (SectionExists(sectionName))
            {
                Sections.Remove(FindSection(sectionName));
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool RemoveData(string sectionName, string dataKey)
        {
            if (SectionExists(sectionName))
            {
                if (DataExists(sectionName, dataKey))
                {
                    FindSection(sectionName).RemoveData(dataKey);
                    return true;
                }
            }
            
            return false;
        }

        public Section FindLastSection()
        {
            return Sections[Sections.Count-1];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public Section FindSection(string sectionName)
        {
            foreach (Section s in Sections)
            {
                if (s.Name.Equals(sectionName))
                {
                    return s;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public Data FindData(string sectionName, string dataKey)
        {
            if (SectionExists(sectionName))
            {
                foreach (Data d in FindSection(sectionName).Data)
                {
                    if (d.Key.Equals(dataKey))
                    {
                        return d;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Find the data in the Sis file
        /// </summary>
        /// <param name="section">Section where the data is located</param>
        /// <param name="key">Data key</param>
        /// <returns>The required data</returns>
        public string FindValue(string section, string key)
        {
            if (SectionExists(section))
            {
                if (DataExists(section, key))
                {
                    return FindData(section, key).Value;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public bool SectionExists(string sectionName)
        {
            return FindSection(sectionName) != null ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool DataExists(string sectionName, string dataKey)
        {
            return FindData(sectionName, dataKey) != null ? true : false;
        }

        #endregion Public

        #region Private

        #endregion Private

        #endregion Methods

    }
}