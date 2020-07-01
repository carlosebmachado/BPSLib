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
            if (SectionExists(sectionName))
            {
                return FindSection(sectionName).Add(data);
            }
            return false;
        }

        public bool AlterSectionName(string sectionName, string newSectionName)
        {
            if (SectionExists(sectionName))
            {
                FindSection(sectionName).Name = newSectionName;
                return true;
            }
            return false;
        }

        public bool AlterDataKey(string sectionName, string dataKey, string newDataKey)
        {
            if (SectionExists(sectionName))
            {
                FindSection(sectionName).AlterKey(dataKey, newDataKey);
                return true;
            }
            return false;
        }

        public bool AlterValue(string sectionName, string dataKey, string newValue)
        {
            if (SectionExists(sectionName))
            {
                FindSection(sectionName).AlterValue(dataKey, newValue);
                return true;
            }
            return false;
        }

        /// <summary>
        /// It deletes all content of a BPS file.
        /// </summary>
        public void RemoveAll()
        {
            Sections.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
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
                FindSection(sectionName).Remove(dataKey);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
                return FindSection(sectionName).Find(dataKey);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectionName">Section where the data is located</param>
        /// <param name="dataKey">Data key</param>
        /// <returns>The required data</returns>
        public string FindValue(string sectionName, string dataKey)
        {
            return FindData(sectionName, dataKey).Value;
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

        #endregion Methods

    }
}