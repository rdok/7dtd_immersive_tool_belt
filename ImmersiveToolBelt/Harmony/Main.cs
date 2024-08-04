namespace ImmersiveToolBelt.Harmony
{
    public class Main
    {
        private static readonly ILogger Logger = new Logger();

        public class Init : IModApi
        {
            public void InitMod(Mod modInstance)
            {
                var type = GetType();
                var message = type.ToString();
                Logger.Info("Loading Patch: " + message);
                var harmony = new HarmonyLib.Harmony(message);
                harmony.PatchAll();
            }
        }
    }
}