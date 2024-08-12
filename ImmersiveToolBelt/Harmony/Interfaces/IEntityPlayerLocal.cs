namespace ImmersiveToolBelt.Harmony.Interfaces
{
    public interface IEntityPlayerLocal
    {
        bool bPlayerStatsChanged { get; }
        IPlayerActionsLocal playerInput { get; }
    }
}