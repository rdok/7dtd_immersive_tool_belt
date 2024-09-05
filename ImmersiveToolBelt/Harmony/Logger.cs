namespace ImmersiveToolBelt.Harmony
{
    public class Logger : ILogger
    {
        public void Info(string message)
        {
            Log.Out("[ImmersiveToolBelt: " + message);
        }

        public void Debug(string message)
        {
#if DEBUG
            Log.Out("[ImmersiveToolBelt: " + message);
#endif
        }

        public void Warn(string message)
        {
#if DEBUG
            Log.Warning("[ImmersiveToolBelt " + message);
#endif
        }

        public void Warning(string message)
        {
#if DEBUG
            Log.Warning("[ImmersiveToolBelt " + message);
#endif
        }

        public void Error(string message)
        {
#if DEBUG
            Log.Error("[ImmersiveToolBelt" + message);
#endif
        }
    }

    public interface ILogger
    {
        void Info(string message);
        void Debug(string message);
        void Warn(string message);
        void Warning(string message);
        void Error(string message);
    }
}