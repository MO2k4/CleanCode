# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a JetBrains ReSharper/Rider plugin that implements clean code analysis for C# and VB.NET, based on concepts from Uncle Bob's Clean Code book. The plugin provides static analysis warnings for code complexity issues like excessive indentation, too many dependencies, method length, etc.

## Build System & Development Commands

The project uses Gradle as the primary build system, with integration for .NET builds:

### Primary Commands
- `./gradlew buildPlugin` - Build the complete plugin (runs tests first, then builds)
- `./gradlew runIde` - Launch Rider with the plugin installed for testing
- `./gradlew publishPlugin` - Publish to JetBrains Plugin Repository (runs tests first)
- `./gradlew testDotNet` - Run .NET unit tests only
- `./gradlew compileDotNet` - Compile .NET components only

### Configuration
- Build configuration is controlled via `gradle.properties`
- `BuildConfiguration` can be `Debug` or `Release`
- `ProductVersion` sets the target Rider version (currently `2025.2`)
- .NET solution is at `src/dotnet/CleanCode.sln`

### Testing
- Run all tests: `./gradlew testDotNet`
- Direct .NET testing: `dotnet test src/dotnet/CleanCode.sln --logger GitHubActions`
- Plugin testing: Use `runIde` task to launch Rider with plugin
- Test project: `src/dotnet/MO.CleanCode.Tests/` contains NUnit tests using ReSharper Test Framework
- Tests automatically run before building or publishing

## Architecture

### Dual-Architecture Plugin Structure
The plugin follows JetBrains' hybrid architecture pattern:

1. **Rider Frontend (Kotlin/Java)**: Located in `src/rider/`
   - UI components and settings pages
   - Integration with Rider's settings system
   - Plugin descriptor: `src/rider/main/resources/META-INF/plugin.xml`

2. **.NET Backend (C#)**: Located in `src/dotnet/MO.CleanCode/`
   - Core analysis logic and ReSharper integration
   - Two project variants:
     - `MO.CleanCode.csproj` - ReSharper integration
     - `MO.CleanCode.Rider.csproj` - Rider integration

### Feature Implementation Pattern
Each analyzer follows a consistent structure in `src/dotnet/MO.CleanCode/Features/`:

- `[FeatureName]/[FeatureName]CheckCs.cs` - C# analysis logic
- `[FeatureName]/[FeatureName]CheckVb.cs` - VB.NET analysis logic
- `[FeatureName]/[FeatureName]Highlighting.cs` - Warning/error definitions

Current analyzers:
- `TooManyDependencies` - Constructor dependency injection warnings
- `ClassTooBig` - Class size analysis
- `ExcessiveIndentation` - Nesting depth warnings
- `ChainedReferences` - Law of Demeter violations
- `MethodTooLong` - Method length analysis
- `TooManyMethodArguments` - Parameter count warnings
- `FlagArguments` - Boolean parameter warnings
- `ComplexExpression` - Conditional complexity
- `MethodNameNotMeaningful` - Method naming conventions
- `HollowNames` - Generic type name detection

### Settings Architecture
- Frontend settings: `src/rider/main/kotlin/com/jetbrains/rider/plugins/cleancode/options/CleanCodeOptionsPage.kt`
- Backend settings: `src/dotnet/MO.CleanCode/Settings/`
- Settings are synchronized between frontend and backend

### Protocol Communication
- Uses JetBrains RD (Reactive Distributed) protocol for frontend-backend communication
- Protocol definitions in `protocol/` directory
- Generated code handles settings synchronization

## Key Dependencies

### .NET Side
- `JetBrains.ReSharper.SDK` - Core ReSharper APIs
- `Microsoft.CodeAnalysis.NetAnalyzers` - Roslyn analyzers
- Target framework: `.NET Framework 4.7.2`

### Rider Side
- `org.jetbrains.intellij.platform` - IntelliJ Platform Gradle Plugin
- Kotlin compilation target: JVM 17
- Requires Rider 2025.2+

### Testing Architecture
**Test Framework**: Uses `JetBrains.ReSharper.SDK.Tests` with NUnit for analyzer testing

**Test Structure**:
```
src/dotnet/MO.CleanCode.Tests/
├── CleanCodeTestBase.cs           # Base class for all analyzer tests
├── Features/                      # Test classes for each analyzer
│   ├── TooManyDependenciesTests.cs
│   ├── ClassTooBigTests.cs
│   ├── MethodTooLongTests.cs
│   └── [... other analyzer tests]
└── TestData/CSharp/               # C# code samples for testing
    ├── TooManyDependenciesTestData.cs
    ├── ClassTooBigTestData.cs
    └── [... test data files]
```

**Test Approach**: Each analyzer test:
1. Uses real C# code samples that should/shouldn't trigger warnings
2. Verifies highlighting count, positions, and messages
3. Tests with different settings configurations
4. Ensures edge cases are handled correctly

## Development Workflow

1. Make changes to .NET analyzers in `src/dotnet/MO.CleanCode/Features/`
2. Add/update corresponding tests in `src/dotnet/MO.CleanCode.Tests/Features/`
3. Update Rider UI if needed in `src/rider/`
4. Run tests with `./gradlew testDotNet`
5. Build with `./gradlew buildPlugin` (runs tests automatically)
6. Test with `./gradlew runIde`
7. For .NET-only changes, use `./gradlew compileDotNet` for faster builds

## Plugin Distribution

- Builds produce both Rider plugin (.zip) and ReSharper package (.nupkg)
- Output directory: `output/`
- Version controlled by `PluginVersion` in `gradle.properties`
- Automatic publishing on master branch via GitHub Actions