using System.Reflection;
using HarmonyLib;
using Multiplayer.Compat;
using Verse;

namespace MultiplayerPickUpAndHaulPatch.Source.Mods;

/// <summary>
///     Deterministic unload ordering for
///     <c>JobDriver_UnloadYourHauledInventory.FirstUnloadableThing</c>.
/// </summary>
internal static class FirstUnloadableThingPatch
{
    internal static void Apply()
    {
        try
        {
            var jobDriverType = AccessTools.TypeByName("PickUpAndHaul.JobDriver_UnloadYourHauledInventory");
            if (jobDriverType == null)
            {
                Log.Error(
                    $"{PickUpAndHaul.LogPrefix} Could not find type PickUpAndHaul.JobDriver_UnloadYourHauledInventory, skipping FirstUnloadableThing patch.");
                return;
            }

            var targetMethod = AccessTools.Method(jobDriverType, "FirstUnloadableThing");
            if (targetMethod == null)
            {
                Log.Error(
                    $"{PickUpAndHaul.LogPrefix} Could not find JobDriver_UnloadYourHauledInventory.FirstUnloadableThing, skipping its patch.");
                return;
            }

            MpCompat.harmony.Patch(targetMethod,
                transpiler: new HarmonyMethod(typeof(FirstUnloadableThingPatch), nameof(Transpiler)));
        }
        catch (Exception exception)
        {
            Log.Error($"{PickUpAndHaul.LogPrefix} Failed patching FirstUnloadableThing: {exception}");
        }
    }

    /// <summary>
    ///     <c>carriedThings</c> is a <c>HashSet&lt;Thing&gt;</c> whose enumeration order is not deterministic
    ///     across clients. The mod orders by def category and def name, which still ties for multiple stacks of
    ///     the same def. Appending <c>ThenBy(thingIDNumber)</c> makes the unload order fully deterministic.
    ///     Inserts after every <c>ThenBy</c> so the final tie-breaker is always the synced thing ID.
    /// </summary>
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var patchedCount = 0;

        foreach (var instruction in instructions)
        {
            yield return instruction;

            if (instruction.operand is MethodInfo methodInfo && methodInfo.Name.Contains("ThenBy"))
            {
                yield return CodeInstruction.Call(typeof(FirstUnloadableThingPatch), nameof(SortByThingIDNumber));
                patchedCount++;
            }
        }

        if (patchedCount == 0)
            throw new Exception(
                $"{PickUpAndHaul.LogPrefix} Failed patching Pick Up And Haul: FirstUnloadableThing (ThenBy call not found)");
    }

    /// <summary>
    ///     Injected by the transpiler, so it must stay public: the patched method lives in the
    ///     PickUpAndHaul assembly and would not see a private method here.
    /// </summary>
    public static IOrderedEnumerable<Thing> SortByThingIDNumber(IOrderedEnumerable<Thing> carriedThings)
    {
        return carriedThings.ThenBy(static thing => thing.thingIDNumber);
    }
}