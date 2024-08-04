namespace ImmersiveToolBelt.Harmony.Interfaces
{
    public interface IEntityPlayerLocal
    {
        bool bPlayerStatsChanged { get; }
        ILocalPlayerUI PlayerUI { get; }
    }
}