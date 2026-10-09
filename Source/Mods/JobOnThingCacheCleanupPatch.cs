using System.Reflection;
using HarmonyLib;
using Multiplayer.Compat;
using Verse;

namespace MultiplayerPickUpAndHaulPatch.Source.Mods;

/// <summary>
///     Static cache cleanup for <c>WorkGiver_HaulToInventory.JobOnThing</c>.
/// </summary>
internal static class JobOnThingCacheCleanupPatch
{
    private static FieldInfo skipCellsField;
    private static FieldInfo skipThingsField;

    internal static void Apply()
    {
        try
        {
            var workGiverType = AccessTools.TypeByName("PickUpAndHaul.WorkGiver_HaulToInventory");
            if (workGiverType == null)
            {
                Log.Error(
                    $"{PickUpAndHaul.LogPrefix} Could not find type PickUpAndHaul.WorkGiver_HaulToInventory, skipping static cache cleanup patch.");
                return;
            }

            skipCellsField = AccessTools.Field(workGiverType, "skipCells");
            skipThingsField = AccessTools.Field(workGiverType, "skipThings");
            if (skipCellsField == null || skipThingsField == null)
            {
                Log.Error(
                    $"{PickUpAndHaul.LogPrefix} Could not find skipCells/skipThings fields, static cache cleanup will be skipped.");
                return;
            }

            var targetMethod = AccessTools.Method(workGiverType, "JobOnThing");
            if (targetMethod == null)
            {
                Log.Error(
                    $"{PickUpAndHaul.LogPrefix} Could not find WorkGiver_HaulToInventory.JobOnThing, skipping static cache cleanup patch.");
                return;
            }

            MpCompat.harmony.Patch(targetMethod,
                finalizer: new HarmonyMethod(typeof(JobOnThingCacheCleanupPatch), nameof(Finalizer)));
        }
        catch (Exception exception)
        {
            Log.Error($"{PickUpAndHaul.LogPrefix} Failed patching JobOnThing finalizer: {exception}");
        }
    }

    /// <summary>
    ///     <c>WorkGiver_HaulToInventory.JobOnThing</c> uses static <c>skipCells</c>/<c>skipThings</c> as implicit
    ///     parameters (set to new at entry, nulled at exit). If the method throws, the stale state leaks into the
    ///     next call and desyncs. This finalizer guarantees cleanup even on exception.
    /// </summary>
    private static void Finalizer()
    {
        try
        {
            skipCellsField?.SetValue(null, null);
            skipThingsField?.SetValue(null, null);
        }
        catch (Exception exception)
        {
            Log.Error($"{PickUpAndHaul.LogPrefix} Failed clearing static caches: {exception}");
        }
    }
}