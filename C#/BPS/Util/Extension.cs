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

namespace BPS.Util
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