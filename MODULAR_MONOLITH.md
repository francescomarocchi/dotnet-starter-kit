# Modular Monolith Structure

The solution now follows a clean, module-oriented structure.

## Shared projects

- `Core`
  - dispatcher abstractions and implementation
  - business strategy abstractions/selector
  - DI scanning extensions for handlers and strategies

- `BuildingBlocks.Modularity`
  - `IModule`
  - `AddModules(...)`
  - `MapModules()`

## Authentication module

- `Modules.Authentication.Application`
  - command contracts and handlers
  - authentication application interfaces and DTOs

- `Modules.Authentication.Infrastructure`
  - concrete infrastructure implementations
  - DI registration extensions

- `Modules.Authentication`
  - module composition class
  - module endpoint mapping

## Products module

- `Modules.Products.Application`
  - command/query contracts and handlers
  - strategy implementations and repository contracts
  - product domain entity

- `Modules.Products.Infrastructure`
  - concrete repository implementations
  - DI registration extensions

- `Modules.Products`
  - module composition class
  - module endpoint mapping

## Host

- `DigitStarterKit`
  - thin composition root
  - middleware + OpenAPI setup
  - module discovery/composition only

## Tests

- `Core.Tests` for core utility behavior
- `Modules.Authentication.Tests` for authentication module wiring
- `Modules.Products.Tests` for products module wiring and strategy path selection

## Removed projects

- `Core.Application`
- `Core.Application.Tests`
- `Core.Domain`
- shared `Infrastructure`

This keeps features and domain models inside their owning modules.
