using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;

namespace BlueMonkey
{
    public static class ApplicationEnvironments
    {
        public static IScheduler DefaultScheduler { get; set; } = ReactivePropertyScheduler.Default;
    }
}
