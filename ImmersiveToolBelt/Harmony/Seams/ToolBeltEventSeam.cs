namespace ImmersiveToolBelt.Harmony.Seams
{
    public interface IToolBeltEvent
    {
        void Trigger();
    }

    public class ToolBeltEventSeam : IToolBeltEvent
    {
        public void Trigger() => Harmony.ToolBeltEvent.Trigger();
    }
}