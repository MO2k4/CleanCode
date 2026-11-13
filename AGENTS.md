# Repository Guidelines

## Project Structure & Module Organization
- Root Gradle build drives Kotlin UI and .NET analyzers.
- Rider UI/metadata: `src/rider/main/{kotlin,java,resources}` (plugin entry: `src/rider/main/resources/META-INF/plugin.xml`).
- .NET analyzers and tests: `src/dotnet/MO.CleanCode` and `src/dotnet/MO.CleanCode.Tests` (feature tests under `Features/`, test data under `TestData/CSharp/`).
- Solution: `src/dotnet/CleanCode.sln`. Build outputs go to `build/` and `output/`.

## Build, Test, and Development Commands
- `./gradlew buildPlugin` — builds Kotlin part, restores/builds .NET, packages plugin (zip in `build/distributions/` and copied to `output/`).
- `./gradlew runIde` — runs Rider with the plugin in a sandbox for manual testing.
- `./gradlew testDotNet` — runs .NET tests via `dotnet test` with GitHub Actions logger.
- `dotnet test src/dotnet/CleanCode.sln` — run tests directly.
- `./gradlew publishPlugin -PPublishToken=...` — packages and pushes Rider + NuGet (release only).

## Coding Style & Naming Conventions
- Kotlin/Java (JVM 17): JetBrains formatter; 4‑space indent; classes `UpperCamelCase`, functions/props `lowerCamelCase`.
- C# (net472): 4‑space indent; types/methods `PascalCase`, locals `camelCase`, private fields `_camelCase`.
- Keep feature folders consistent (e.g., `Features/TooManyMethodArguments/` with `...Check(Cs|Vb).cs` and `...Highlighting.cs`).

## Testing Guidelines
- Framework: NUnit. Place tests in `src/dotnet/MO.CleanCode.Tests/Features/*Tests.cs`.
- Add minimal test data to `TestData/CSharp/FeatureNameTestData.cs` mirroring scenarios.
- Run with `./gradlew testDotNet`. Cover positive and negative cases for each analyzer rule.

## Commit & Pull Request Guidelines
- Style seen in history: `fix:`, `Feature:`, `Bump`, `Update`. Prefer short, imperative messages; Conventional Commits welcome.
- PRs must include: clear description, linked issue, tests updated/added, and screenshots for UI/Options changes.
- Keep changes scoped; avoid drive‑by formatting. Update `CHANGELOG.md` for user‑visible changes.

## Security & Configuration Tips
- Secrets: never commit tokens. Set `PublishToken` via `-PPublishToken` or environment. Product/version in `gradle.properties`.

## Agent‑Specific Notes
- Prefer small, focused patches; match existing folder and class naming patterns.
- Do not modify unrelated tasks or build logic. When adding analyzers, wire options/messages in both .NET and `plugin.xml` as needed.
