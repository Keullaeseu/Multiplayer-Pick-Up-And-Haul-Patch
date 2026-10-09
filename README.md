# Multiplayer Pick Up And Haul Patch

A RimWorld Multiplayer compatibility patch for [Pick Up And Haul](https://steamcommunity.com/sharedfiles/filedetails/?id=1279012058).

This mod is designed to improve multiplayer synchronization when playing with the [Pick Up And Haul](https://steamcommunity.com/sharedfiles/filedetails/?id=1279012058) mod and RimWorld Multiplayer.

## Features

- Adds multiplayer compatibility for [Pick Up And Haul](https://steamcommunity.com/sharedfiles/filedetails/?id=1279012058).
- Makes the hauled-inventory unload order deterministic across clients (tie-breaks same-def stacks by synced thing ID).
- Guarantees cleanup of the work giver's static reservation caches even if haul job generation throws.

## Requirements

- RimWorld
- [Harmony](https://steamcommunity.com/sharedfiles/filedetails/?id=2009463077)
- RimWorld Multiplayer
  - [GitHub version](https://github.com/rwmt/Multiplayer) or [Steam Workshop](https://steamcommunity.com/sharedfiles/filedetails/?id=2606448745) version
- [Pick Up And Haul](https://steamcommunity.com/sharedfiles/filedetails/?id=1279012058)

The host and every connected player must use compatible versions of all required mods.

## Installation

### Steam Workshop

Subscribe to the required mods and add them to your RimWorld mod list in the following order:

1. Harmony
2. Core
3. Royalty, Ideology, Biotech, and Anomaly, if applicable
4. RimWorld Multiplayer
5. [Pick Up And Haul](https://steamcommunity.com/sharedfiles/filedetails/?id=1279012058)
6. [Multiplayer Pick Up And Haul Patch](https://github.com/Keullaeseu/Multiplayer-Pick-Up-And-Haul-Patch/releases/latest)

The patch should load after RimWorld Multiplayer and [Pick Up And Haul](https://steamcommunity.com/sharedfiles/filedetails/?id=1279012058).

### Manual Installation

1. Download the latest release from the [**Releases**](https://github.com/Keullaeseu/Multiplayer-Pick-Up-And-Haul-Patch/releases/latest) section.
2. Extract the mod folder into your RimWorld `Mods` directory.
3. Enable the required mods in RimWorld.
4. Use the recommended load order listed above.
5. Make sure every multiplayer player has the same mod list, configuration, and load order.

## Multiplayer Usage

All players should have the following mods installed and enabled:

- RimWorld Multiplayer
- [Pick Up And Haul](https://steamcommunity.com/sharedfiles/filedetails/?id=1279012058)
- [Multiplayer Pick Up And Haul Patch](https://github.com/Keullaeseu/Multiplayer-Pick-Up-And-Haul-Patch/releases/latest)

The host and all connected clients should use the same:

- RimWorld version
- RimWorld Multiplayer version
- Pick Up And Haul version
- Multiplayer Pick Up And Haul Patch version
- Mod configuration (including the Pick Up And Haul settings: corpses, animals, mechanoids, and minimum free inventory space)
- Mod load order

Do not add, remove, update, or reorder mods while players are connected to the same multiplayer session.

## Compatibility

This patch is intended to provide multiplayer compatibility for [Pick Up And Haul](https://steamcommunity.com/sharedfiles/filedetails/?id=1279012058)
itself (opportunistic multi-item hauling, hauled-inventory unloading, corpse/animal/mechanoid hauling rules).

It does not replace:

- [RimWorld Multiplayer](https://steamcommunity.com/sharedfiles/filedetails/?id=2606448745)
- [Pick Up And Haul](https://steamcommunity.com/sharedfiles/filedetails/?id=1279012058)

## Known Limitations

- Compatibility may be affected by future RimWorld updates.
- Compatibility may be affected by future updates to RimWorld Multiplayer or Pick Up And Haul.
- Pick Up And Haul settings are per-client and affect hauling decisions; all players should use identical settings to avoid desyncs.

## Credits

- [RimWorld Multiplayer on GitHub](https://github.com/rwmt/Multiplayer)
- [RimWorld Multiplayer on Steam Workshop](https://steamcommunity.com/sharedfiles/filedetails/?id=2606448745)
- [Pick Up And Haul on Steam Workshop](https://steamcommunity.com/sharedfiles/filedetails/?id=1279012058)
- [Pick Up And Haul on GitHub](https://github.com/Mehni/PickUpAndHaul)
- [Multiplayer Pick Up And Haul Patch](https://github.com/Keullaeseu)
