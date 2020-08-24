/*
 * MIT License
 *
 * Copyright (c) 2020 Carlos Eduardo de Borba Machado
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections.Generic;

namespace BPS
{
    public class Section
    {
        #region Vars

        /// <summary>The name of the section</summary>
        public string Name { get; set; }
        private readonly List<Data> _data;

        #endregion Vars


        #region Constructors

        /// <summary>
        /// Constructor with name and a list of data
        /// </summary>
        /// <param name="name">The name of the section</param>
        /// <param name="data">A list of data</param>
        public Section(string name, List<Data> data)
        {
            Name = name;
            _data = data;
        }

        /// <summary>
        /// Constructor with just name
        /// </summary>
        /// <param name="name">The name of the section</param>
        public Section(string name)
        {
            Name = name;
            _data = new List<Data>();
        }

        #endregion Constructors


        #region Methods

        #region Public

        /// <summary>
        /// Adds a new Data in the section
        /// </summary>
        /// <param name="data">Data to be add</param>
        /// <returns>If can add will return true, else false</returns>
        public bool Add(Data data)
        {
            if (!Exists(data.Key))
            {
                _data.Add(data);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes all data of section
        /// </summary>
        public void RemoveAll()
        {
            _data.Clear();
        }

        /// <summary>
        /// Removes the data that contains the key passed by parameter
        /// </summary>
        /// <param name="key">Key to remove</param>
        /// <returns>If can remove will return true, else false</returns>
        public bool Remove(string key)
        {
            foreach (var d in _data)
            {
                if (d.Key.Equals(key))
                {
                    _data.Remove(d);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Finds all data in section
        /// </summary>
        /// <returns>A array with all data</returns>
        public Data[] FindAll()
        {
            return _data.ToArray();
        }

        /// <summary>
        /// Finds a unique data that contains the key passed by parameter
        /// </summary>
        /// <param name="key">Key to find</param>
        /// <returns>The data if finds it, else return null</returns>
        public Data Find(string key)
        {
            foreach(Data d in _data)
            {
                if (d.Key.Equals(key))
                {
                    return d;
                }
            }
            return null;
        }

        /// <summary>
        /// Check if a data exists
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>True if exists, else false</returns>
        public bool Exists(string key)
        {
            return Find(key) != null;
        }

        #endregion Public

        #endregion Methods

    }
}