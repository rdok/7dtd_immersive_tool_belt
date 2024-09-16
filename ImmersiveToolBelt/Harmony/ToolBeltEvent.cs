using System;

namespace ImmersiveToolBelt.Harmony
{
    public interface IToolBeltEvent : IDisposable
    {
        bool BackpackOnOpen { get; set; }
        bool BackpackOnClose { get; set; }
        DateTime ChangedAt { get; }

        void Trigger();
        bool IsAlive();
    }

    public class ToolBeltEvent : IToolBeltEvent
    {
        private bool _isBackpackOnOpen;
        private bool _isBackpackOnClose;
        DateTime _changedAt { get; set; } = DateTime.Now;

        public DateTime ChangedAt
        {
            get => _changedAt;
            private set => _changedAt = value;
        }

        public bool BackpackOnOpen
        {
            get => _isBackpackOnOpen;
            set
            {
                _isBackpackOnOpen = value;
                _isBackpackOnClose = !value;
                Trigger();
            }
        }

        public bool BackpackOnClose
        {
            get => _isBackpackOnClose;
            set
            {
                _isBackpackOnClose = value;
                _isBackpackOnOpen = !value;
                Trigger();
            }
        }

        public void Trigger()
        {
            ChangedAt = DateTime.Now;
        }

        public void Dispose()
        {
            ChangedAt = DateTime.MinValue;
        }

        public bool IsAlive()
        {
            return ChangedAt != DateTime.MinValue;
        }
    }
}