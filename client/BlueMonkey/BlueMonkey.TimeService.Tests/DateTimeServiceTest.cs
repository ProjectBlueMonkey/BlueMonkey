using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BlueMonkey.TimeService.Tests
{
    public class DateTimeServiceTest
    {
        [Fact]
        public void Today()
        {
            var before = DateTime.Today;
            var actual = new DateTimeService().Today;
            Assert.True(
                actual == before ||
                actual == DateTime.Today);
        }
    }
}
