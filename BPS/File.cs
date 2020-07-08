﻿using System.Collections.Generic;

namespace BPS
{
    public class File
    {
        #region Vars

        private readonly List<Section> _sections;

        #endregion Vars


        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public File()
        {
            _sections = new List<Section>();
        }

        /// <summary>
        /// Constructor with a list of sections
        /// </summary>
        /// <param name="sections">File sections</param>
        internal File(List<Section> sections)
        {
            _sections = sections;
        }

        #endregion Constructors


        #region Methods

        #region Public

        /// <summary>
        /// Adds a new Data in the section
        /// </summary>
        /// <param name="section">Section to be add</param>
        /// <returns>If can add will return true, else false</returns>
        public bool Add(Section section)
        {
            if (!Exists(section.Name))
            {
                _sections.Add(section);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes all content of a BPS file
        /// </summary>
        public void RemoveAll()
        {
            _sections.Clear();
        }

        /// <summary>
        /// Removes the section that contains the name passed by parameter
        /// </summary>
        /// <param name="name">Name to remove</param>
        /// <returns>If can remove will return true, else false</returns>
        public bool Remove(string name)
        {
            if (Exists(name))
            {
                _sections.Remove(Find(name));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Finds all sections in file
        /// </summary>
        /// <returns>A array with all sections</returns>
        public Section[] FindAll()
        {
            return _sections.ToArray();
        }

        /// <summary>
        /// Finds a unique data that contains the key passed by parameter
        /// </summary>
        /// <param name="name">Name to find</param>
        /// <returns>The section if finds it, else return null</returns>
        public Section Find(string name)
        {
            foreach (Section s in _sections)
            {
                if (s.Name.Equals(name))
                {
                    return s;
                }
            }
            return null;
        }

        /// <summary>
        /// Check if a section exists
        /// </summary>
        /// <param name="name">A name to check</param>
        /// <returns>True if exists, else false</returns>
        public bool Exists(string name)
        {
            return Find(name) != null;
        }

        #endregion Public

        #endregion Methods

    }
}