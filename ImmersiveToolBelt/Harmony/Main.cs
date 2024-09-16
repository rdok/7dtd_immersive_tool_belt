using System;
using ImmersiveToolBelt.Harmony.Interfaces;
using ImmersiveToolBelt.Harmony.Seams;

namespace ImmersiveToolBelt.Harmony
{
    public class Main
    {
        private static readonly ILogger Logger = new Logger();

        public class Init : IModApi
        {
            public void InitMod(Mod modInstance)
            {
                ServiceRegistry.Add<IToolBeltEvent>((args) => new ToolBeltEvent());
                ServiceRegistry.Add<IXUiView>((args) => new XUiViewSeam((ViewComponent)args[0]));
                ServiceRegistry.Add<IDateTime>((args) =>
                {
                    var nowFunc = args.Length > 0 ? (Func<DateTime>)args[0] : null;
                    var totalSecondsFunc = args.Length > 1 ? (Func<double>)args[1] : null;
                    return new DateTimeSeam(nowFunc, totalSecondsFunc);
                });

                const string id = "uk.co.rdok.7daystodie.mods.immersive_tool_belt";
                Logger.Debug("Loading Patch: " + id);
                var harmony = new HarmonyLib.Harmony(id);
                harmony.PatchAll();
            }
        }
    }
}