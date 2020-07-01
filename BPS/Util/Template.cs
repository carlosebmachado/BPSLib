using System;

namespace BPS.Util
{
    internal class Template
    {
        #region Vars

        /// <summary></summary>
        internal int Var { get; set; }

        #endregion Vars

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="var"></param>
        public Template(int var)
        {
            Var = var;
        }

        #endregion Constructors

        #region Methods

        #region Public

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool PublicMethod()
        {
            return PrivateMethod();
        }

        #endregion Public

        #region Private

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool PrivateMethod()
        {
            return true;
        }

        #endregion Private

        #endregion Methods
    }
}
