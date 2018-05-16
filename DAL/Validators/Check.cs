using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Validators
{
    /// <summary>
    /// Static class for check condition null
    /// </summary>
    public static class Check
    {
        /// <summary>
        /// Check reference argument on null 
        /// </summary>
        /// <typeparam name="T">reference type</typeparam>
        /// <param name="value">value for check</param>
        /// <returns>value</returns>
        public static T NotNull<T>(T value) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException($"Argument {nameof(value)} is null");
            }

            return value;
        }

        /// <summary>
        /// Check nullable argument on null 
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="value">value for check</param>
        /// <returns>value</returns>
        public static T? NotNull<T>(T? value) where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException($"Argument {nameof(value)} is null");
            }

            return value;
        }
    }
}
