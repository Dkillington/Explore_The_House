# Explore the House

A point-and-click adventure for Windows. Explore rooms, collect items, and read clues to uncover the house's story.

## Download and play

[**Download ExploreTheHouse.exe**](https://github.com/Dkillington/Explore_The_House/releases/latest/download/ExploreTheHouse.exe)

Save the EXE anywhere and double-click it. Windows 10 (1809+) or Windows 11, 64-bit is required. The game, original audio, pictures, and .NET runtime are included. No installer or separate runtime is needed. The first launch extracts the bundled native runtime automatically.

## Build from source

Install the .NET 8 SDK or newer on Windows. Original audio is distributed in the playable release rather than Git. Supply the WAV files matching the project file's audio entries:

```powershell
./scripts/Build-Release.ps1 -AudioDirectory 'C:\path\to\original\Audio'
```

The script checks every required audio file and produces `artifacts/release/ExploreTheHouse.exe`. Audio is embedded in the executable, so launching from a different working directory works. Images remain WPF resources. Run the EXE with `--verify-assets` to check its audio without opening the game.
