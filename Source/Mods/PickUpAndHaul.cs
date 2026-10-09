using Multiplayer.Compat;
using Verse;

namespace MultiplayerPickUpAndHaulPatch.Source.Mods;

/// <summary>
///     Multiplayer Patch for Pick Up And Haul by Mehni,
///     Last Update: 28 Aug, 2025 @ 5:39pm
///     <see href="https://steamcommunity.com/sharedfiles/filedetails/?id=1279012058" />
///     <see href="https://github.com/Mehni/PickUpAndHaul" />
/// </summary>
[MpCompatFor("Mehni.PickUpAndHaul")]
public class PickUpAndHaul
{
    internal const string LogPrefix = "[Multiplayer Pick Up And Haul Patch]";

    public PickUpAndHaul(ModContentPack content)
    {
        LongEventHandler.ExecuteWhenFinished(LatePatch);
    }

    private static void LatePatch()
    {
        Log.Message($"{LogPrefix} Initializing...");

        FirstUnloadableThingPatch.Apply();
        JobOnThingCacheCleanupPatch.Apply();

        Log.Message($"{LogPrefix} Initialized.");
    }
}