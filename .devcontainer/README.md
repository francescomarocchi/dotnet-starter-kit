# Dev Container

This folder enables opening the repository inside a Dev Container while reusing the root `compose.yaml`.

## Files

- `devcontainer.json`: Dev Container definition.
- `docker-compose.devcontainer.yml`: Override file used only for Dev Container sessions.

## Rider support

- `devcontainer.json` includes `customizations.jetbrains.backend = Rider` so JetBrains tooling can pick Rider as the preferred backend.
- Open the repo in Rider using the Dev Container flow (or JetBrains Gateway), then select the `digitstarterkit` service.

## Notes

- The main Compose file still defines the build (`DigitStarterKit/Dockerfile`, `target: dev`) and mounts the source tree.
- The override keeps the container alive so IDE tooling can attach and run commands interactively.
- `postCreateCommand` runs `dotnet restore DigitStarterKit.sln` after the container is created.
