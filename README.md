---
ai_generated: true
model: "openai/gpt-5.3-codex@unknown"
operator: "johnmillerATcodemag-com"
chat_id: "retrofit-ai-provenance-20260323"
prompt: |
  retrofit the AI provenance in this project
started: "2026-03-23T00:00:00Z"
ended: "2026-03-23T00:30:00Z"
task_durations:
  - task: "provenance audit"
    duration: "00:10:00"
  - task: "metadata retrofit"
    duration: "00:15:00"
  - task: "log scaffolding"
    duration: "00:05:00"
total_duration: "00:30:00"
ai_log: "ai-logs/2026/03/23/retrofit-ai-provenance-20260323/conversation.md"
source: "github-copilot-chat"
---

# 20260323-aiasd

A basic WPF calculator UI built with C# and .NET 8.

## Features

- Basic arithmetic operations: addition, subtraction, multiplication, division
- Percentage calculation
- Trigonometric functions: `sin`, `cos`, `tan` (degrees)
- Memory functionality: store, recall, add, subtract values
- Error handling for invalid math operations (for example, divide by zero)
- Desktop UI with display, number pad, operator keys, memory keys, and trig keys

## Prerequisites

- .NET 8 SDK or later installed

## Getting Started

1. Clone the repository.
2. Navigate to the project directory:
   ```bash
   cd 20260323-aiasd
   ```
3. Run the calculator:
   ```bash
   dotnet run
   ```

## Usage

1. Launch the app with `dotnet run`.
2. Use the number pad and operator buttons to perform calculations.
3. Use `sin`, `cos`, and `tan` buttons for trigonometric operations on the displayed value.
4. Use `MC`, `MR`, `M+`, and `M-` for memory operations.
5. Use `CE` for clear entry and `CA` for clear all.

### Trigonometric Examples

- `sin(30)` = 0.5
- `cos(60)` = 0.5
- `tan(45)` = 1

### Percentage Examples

- `100 % 20` = 20 (20% of 100)
- `50 % 30` = 15 (30% of 50)
- `200 % 5` = 10 (5% of 200)

### Memory Controls

- **MC** — Clear memory
- **MR** — Recall memory to display
- **M+** — Add displayed value to memory
- **M-** — Subtract displayed value from memory

## Project Structure

- `Calculator.csproj`: .NET 8 project file
- `MainWindow.xaml`: WPF UI layout
- `MainWindow.xaml.cs`: UI interaction and calculator behavior
- `App.xaml` / `App.xaml.cs`: WPF app bootstrap
- `README.md`: Project overview and setup notes

## AI-Assisted Artifacts

- [README.md](README.md): Project documentation with embedded provenance metadata. Log: [conversation.md](ai-logs/2026/03/23/retrofit-ai-provenance-20260323/conversation.md)
- [.github/instructions/evergreen-software-development.instructions.md](.github/instructions/evergreen-software-development.instructions.md): Core principles for evergreen software development. Log: [conversation.md](ai-logs/2026/03/23/create-evergreen-instructions-20260323/conversation.md)
- [.github/instructions/dotnet.instructions.md](.github/instructions/dotnet.instructions.md): .NET 8 project and build guidance for this WPF app. Log: [conversation.md](ai-logs/2026/03/23/create-technology-instructions-20260323/conversation.md)
- [.github/instructions/csharp.instructions.md](.github/instructions/csharp.instructions.md): C# guidance for calculator logic and event handlers. Log: [conversation.md](ai-logs/2026/03/23/create-technology-instructions-20260323/conversation.md)
- [.github/instructions/wpf.instructions.md](.github/instructions/wpf.instructions.md): WPF guidance for code-behind and window behavior. Log: [conversation.md](ai-logs/2026/03/23/create-technology-instructions-20260323/conversation.md)
- [.github/instructions/xaml.instructions.md](.github/instructions/xaml.instructions.md): XAML guidance for calculator layout and control wiring. Log: [conversation.md](ai-logs/2026/03/23/create-technology-instructions-20260323/conversation.md)
- [.github/prompts/meta/create-technology-instructions.instructions.prompt.md](.github/prompts/meta/create-technology-instructions.instructions.prompt.md): Promptfile that inspects the repo and creates or updates technology-specific instruction files. Log: [conversation.md](ai-logs/2026/03/23/create-technology-instructions-prompt-20260323/conversation.md)
- [Calculator.csproj](Calculator.csproj): WPF project configuration with embedded provenance metadata.
- [App.xaml](App.xaml): WPF application declaration with embedded provenance metadata.
- [App.xaml.cs](App.xaml.cs): WPF application bootstrap class with embedded provenance metadata.
- [MainWindow.xaml](MainWindow.xaml): Calculator UI layout with embedded provenance metadata.
- [MainWindow.xaml.cs](MainWindow.xaml.cs): Calculator logic and event handlers with embedded provenance metadata.
- [conversation.md](ai-logs/2026/03/23/retrofit-ai-provenance-20260323/conversation.md): AI chat transcript.
- [summary.md](ai-logs/2026/03/23/retrofit-ai-provenance-20260323/summary.md): AI session resumability summary.
- [conversation.md](ai-logs/2026/03/23/create-evergreen-instructions-20260323/conversation.md): AI chat transcript for evergreen instruction creation.
- [summary.md](ai-logs/2026/03/23/create-evergreen-instructions-20260323/summary.md): AI session resumability summary for evergreen instruction creation.
- [conversation.md](ai-logs/2026/03/23/create-technology-instructions-20260323/conversation.md): AI chat transcript for technology instruction creation.
- [summary.md](ai-logs/2026/03/23/create-technology-instructions-20260323/summary.md): AI session resumability summary for technology instruction creation.
- [conversation.md](ai-logs/2026/03/23/create-technology-instructions-prompt-20260323/conversation.md): AI chat transcript for technology instruction prompt creation.
- [summary.md](ai-logs/2026/03/23/create-technology-instructions-prompt-20260323/summary.md): AI session resumability summary for technology instruction prompt creation.

## Building the Project

```bash
dotnet build
```

## Contributing

1. Create a feature branch.
2. Make your changes.
3. Open a pull request.
