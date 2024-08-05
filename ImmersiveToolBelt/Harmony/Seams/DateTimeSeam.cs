using System;
using ImmersiveToolBelt.Harmony.Interfaces;

namespace ImmersiveToolBelt.Harmony.Seams
{
    public class DateTimeSeam : IDateTime
    {
        private readonly Func<DateTime> _now;
        private readonly Func<double> _totalSeconds;

        public DateTimeSeam(Func<DateTime> now = null, Func<double> totalSeconds = null)
        {
            _now = now ?? (() => DateTime.Now);
            _totalSeconds = totalSeconds ?? (() => DateTime.Now.TimeOfDay.TotalSeconds);
        }

        public DateTime Now()
        {
            return _now();
        }

        public double TotalSeconds()
        {
            return _totalSeconds();
        }
    }
}