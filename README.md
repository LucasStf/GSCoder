[![CodeFactor](https://www.codefactor.io/repository/github/lucasstf/gscoder/badge/main)](https://www.codefactor.io/repository/github/lucasstf/gscoder/overview/main)

# GSCoder

GSCoder is an open source and cross platform GSC IDE.

That work on Windows, Linux and Mac OS


# Installation

- [Windows]
```
```
- [Linux]
```
```
- [MacOS]
```
```

# Running GSCoder

Windows uses `Wpf` and `GTK`. (For the best experience use GTK instead of WPF).

Linux uses `GTK`.

MacOS uses `MacOS`.

## Building GSCoder from source

### All platforms

- .NET 6 SDK (can be obtained from [here](https://dotnet.microsoft.com/download/dotnet/6.0) - You want the SDK for your platform, Linux users should install via package manager where possible)

#### Windows
GTK+ for Windows Runtime Environment Installer [here](https://github.com/tschoonj/GTK-for-Windows-Runtime-Environment-Installer/releases/tag/2022-01-04)


#### Linux

Required packages (some packages may be pre-installed for your distribution)

- GTK+3

#### MacOS [Experimental]

No other dependencies.

# Features

- Fully platform-native GUI
  - Windows: `Windows Presentation Foundation`
  - Linux: `GTK+3`
  - MacOS: `MonoMac`