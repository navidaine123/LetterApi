using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers.Common
{
    public static class ExceptionMessage
    {
        /// <summary>
        /// Value cannot be empty or whitespace only string.
        /// </summary>
        public static string EmptyString
        {
            get { return "Value cannot be empty or whitespace only string."; }
        }

        public static string TypeIsNotEnum
        {
            get { return "T must be an enumerated type"; }
        }
    }
}