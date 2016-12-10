using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMonkey.TimeService
{
    /// <summary>
    /// Provide services on DateTime.
    /// </summary>
    public interface IDateTimeService
    {
        /// <summary>
        /// Get it today.
        /// </summary>
        DateTime Today { get; }
    }
}
