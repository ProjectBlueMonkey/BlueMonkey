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
    public class DateTimeService : IDateTimeService
    {
        /// <summary>
        /// Get it today.
        /// </summary>
        public DateTime Today => DateTime.Today;
    }
}
