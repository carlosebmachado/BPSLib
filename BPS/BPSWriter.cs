using BPS.Util;
using System;
using System.IO;

namespace BPS
{
    public class BPSWriter
    {
        #region Vars

        /// <summary></summary>
        private const string KV_HEADER = "# BPS File";
        /// <summary></summary>
        private const string KV_NEXTLINE = "\n";
        /// <summary></summary>
        private const string KV_LAB = "<";
        /// <summary></summary>
        private const string KV_RAB = ">";
        /// <summary></summary>
        private const string KV_TAB = "    ";
        /// <summary></summary>
        private const string KV_SEPARATOR = ":";

        #endregion Vars

        #region Methods

        #region Public

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bpsFile"></param>
        /// <returns></returns>
        public static void Write(BPSFile bpsFile, string path)
        {
            try
            {
                StreamWriter file = new StreamWriter(Extension.Normalize(path));

                file.WriteLine(KV_HEADER + KV_NEXTLINE);

                foreach (Section section in bpsFile.Sections)
                {
                    file.WriteLine(KV_LAB + section.Name);
                    foreach (Data data in section.Data)
                    {
                        file.WriteLine(KV_TAB + data.Key + KV_SEPARATOR + data.Value);
                    }
                    file.WriteLine(KV_RAB + KV_NEXTLINE);
                }
                file.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Public

        #endregion Methods

    }
}