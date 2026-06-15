using HarmonyLib;
using MelonLoader;
using GHPC.Crew;

[assembly: MelonInfo(typeof(Crew_Panic_Customizer_Mod.Crew_Panic_Customizer_), "Crew_Panic_Customizer", "1.0.0", "QwertyRyo")]
[assembly:MelonGame("Radian Simulations LLC", "GHPC")]

namespace Crew_Panic_Customizer_Mod
{
    public class Crew_Panic_Customizer_ : MelonMod
    {

            public static MelonPreferences_Entry<string> panic_choice;

        public override void OnInitializeMelon()
        {
                  MelonPreferences_Category cfg =
          MelonPreferences.CreateCategory("Crew Panic Customizer");
      panic_choice = cfg.CreateEntry<string>("Panic Choice", "Always");
      panic_choice.Comment =
          "Pick between the following choices: 'Always' makes the crew always panicked, 'Never' makes the crew  never panicked, leave blank for vanilla gameplay";

      MelonPreferences.Save();
      HarmonyInstance.PatchAll();

        }
    }

    [HarmonyPatch(typeof(CrewManager), "Update")]
    public static class CrewManager_Update_Patch
    {
        static void Postfix(CrewManager __instance)
        {
            if(Crew_Panic_Customizer_.panic_choice.Value == "Always")
            {
            Traverse.Create(__instance).Property("CrewArePanicked").SetValue(true);
            Traverse.Create(__instance).Field("_crewPanickedCountdown").SetValue(9999f);
            }
            else if (Crew_Panic_Customizer_.panic_choice.Value == "Never")
            {
                            Traverse.Create(__instance).Property("CrewArePanicked").SetValue(false);
            Traverse.Create(__instance).Field("_crewPanickedCountdown").SetValue(-1f);
            

            }
        }
    }
}