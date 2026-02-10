# Unity Hexagonal Architecture Starter Project

A sample starter project for building Unity games using **Hexagonal Architecture** (also known as Ports and Adapters architecture). This project provides a solid foundation with proper layer separation, dependency inversion, and scalable patterns.

## 🎯 What is Hexagonal Architecture?

Hexagonal Architecture is an architectural pattern that separates your application into:

- **Core/Domain** (Business logic, rules, entities)
- **Ports** (Interfaces defining how the core interacts with outside)
- **Adapters** (Implementations of ports for specific technologies/frameworks)

**Key Principle:** Dependencies flow **inward**, not outward. The core doesn't depend on Unity, databases, or any external framework.

```
┌─────────────────────────────────────────────────────────┐
│                    PRESENTATION LAYER                    │
│  (UI, Screens, Views - Unity MonoBehaviour components)  │
└────────────────────┬────────────────────────────────────┘
                     │ depends on
┌────────────────────▼────────────────────────────────────┐
│                  APPLICATION LAYER                       │
│         (Ports, Use Cases, DTOs, Business Logic)        │
└────────────────────▲────────────────────────────────────┘
                     │ implements
┌────────────────────┴────────────────────────────────────┐
│                 INFRASTRUCTURE LAYER                     │
│   (Unity-specific implementations, DB, APIs, Storage)   │
└─────────────────────────────────────────────────────────┘
```

## 📁 Project Structure

```
Assets/_Project/
├── Application/                      # Core business logic & ports
│   ├── DTOs/                        # Data Transfer Objects
│   └── Ports/                       # Interfaces (contracts)
│       ├── EventBus/                # IEventBus
│       ├── Persistence/             # Storage interfaces
│       │   ├── Storage/             # IStorage, ILocalStorage
│       │   └── Variables/           # IStorageVariable, IUserStorage
│       ├── Services/                # IStorageService, etc.
│       └── ServiceLocator/          # IServiceLocator
│
├── Core/                            # Domain entities (no dependencies)
│   ├── Data/                        # Domain data models
│   └── Enums/                       # Shared enums
│
├── Infrastructure/                  # Framework implementations
│   ├── Base/                        # BaseService base class
│   ├── Console/                     # UnityConsole implementation
│   ├── Enums/                       # Infrastructure enums
│   ├── GameTime/                    # UnityGameTime implementation
│   ├── Persistence/                 # Storage implementations
│   │   ├── Storage/                 # LocalStorage
│   │   ├── Variables/               # UserStorage
│   │   └── StorageService           # Main storage service
│   ├── ServiceLocator/              # ServiceLocator implementation
│   └── Services/                    # DummyService, etc.
│
├── Presentation/                    # UI & Views
│   └── UI/
│       └── Screens/                 # Screen views
│           ├── Base/                # BaseScreenView
│           └── DummyScreenView.cs
│
├── Bootstrap/                       # Initialization & DI setup
│   ├── Base/                        # Base classes for installers
│   │   ├── BaseServiceInstaller.cs
│   │   └── BaseScreenInstaller.cs
│   ├── Interfaces/                  # IServiceInstaller
│   ├── Enums/                       # InstallStatus
│   ├── ServiceInstallers/           # Service initialization
│   │   ├── ServiceInstaller.cs
│   │   └── Services/
│   │       ├── StorageInstaller.cs
│   │       └── DummyInstaller.cs
│   └── ScreenInstallers/            # Screen initialization
│
└── Configs/                         # ScriptableObject configs
    └── Installer/
        ├── ServicesInstallLocator.cs
        └── ScreenInstallLocator.cs
```

## ✨ Key Features

- **Clean Architecture**: Proper separation of concerns with dependency inversion
- **Service Locator Pattern**: Centralized service management via `IServiceLocator`
- **Event Bus**: Decoupled communication via `IEventBus`
- **Async Initialization**: UniTask-based async service/screen initialization
- **Type-Safe Storage**: Generic storage system with ports for flexibility
- **Boot Blocker System**: Control whether installation failures should block app startup

## 🚀 Getting Started

### 1. Understanding the Layers

| Layer | Purpose | Dependencies |
|-------|---------|--------------|
| **Application** | Business rules, ports (interfaces) | None (core) |
| **Infrastructure** | Unity/DB/API implementations | Implements Application ports |
| **Presentation** | UI, Screens, Views | Application ports only |
| **Bootstrap** | Initialization, wiring | Can access all layers |

### 2. Creating a New Service

**Step 1:** Define the port in `Application/Ports/Services/`
```csharp
namespace Project.Application.Ports.Services
{
    public interface IAudioService
    {
        void PlaySound(string id);
        void StopAll();
    }
}
```

**Step 2:** Implement in `Infrastructure/`
```csharp
namespace Project.Infrastructure.Audio
{
    public class UnityAudioService : MonoBehaviour, IAudioService
    {
        public void PlaySound(string id) { /* Unity AudioSource code */ }
        public void StopAll() { /* Stop all sounds */ }
    }
}
```

**Step 3:** Add to `IServiceLocator`
```csharp
// Application/Ports/ServiceLocator/IServiceLocator.cs
public interface IServiceLocator
{
    IAudioService AudioService { get; set; }
    // ... other services
}
```

**Step 4:** Add to `ServiceLocator` implementation
```csharp
// Infrastructure/ServiceLocator/ServiceLocator.cs
public class ServiceLocator : IServiceLocator
{
    public IAudioService AudioService { get; set; }
}
```

**Step 5:** Create installer in `Bootstrap/ServiceInstallers/Services/`
```csharp
public class AudioInstaller : BaseServiceInstaller<AudioService>
{
    protected override async UniTask InitializeModule()
    {
        Service = gameObject.AddComponent<UnityAudioService>();
        await Service.Initialize();
    }
}
```

**Step 6:** Use in Presentation layer
```csharp
public class MyScreenView : BaseScreenView
{
    public override async UniTask InitializeScreen(IEventBus eventBus, IServiceLocator serviceLocator)
    {
        // Access service through interface
        serviceLocator.AudioService.PlaySound("bgm");
    }
}
```

### 3. Creating a New Screen

**Step 1:** Create your screen view in `Presentation/UI/Screens/`
```csharp
public class MainMenuScreenView : BaseScreenView
{
    public override async UniTask InitializeScreen(IEventBus eventBus, IServiceLocator serviceLocator)
    {
        // Initialize your screen
    }

    protected override async UniTask BeforeShowScreen()
    {
        // Setup before showing
    }

    protected override async UniTask AfterHideScreen()
    {
        // Cleanup after hiding
    }
}
```

**Step 2:** Create installer in `Bootstrap/ScreenInstallers/Screens/`
```csharp
public class MainMenuScreenInstaller : BaseScreenInstaller
{
    protected override async UniTask InitializeScreen(IEventBus eventBus, IServiceLocator serviceLocator)
    {
        // Instantiate and initialize your screen view
    }
}
```

## 📝 Architecture Rules

### ✅ DO:
- Define interfaces in `Application/Ports`
- Implement interfaces in `Infrastructure`
- Use `IServiceLocator` to access services from Presentation
- Keep business logic in Application layer
- Keep Unity-specific code in Infrastructure layer
- Make Presentation depend only on Application ports

### ❌ DON'T:
- Let Presentation depend on Infrastructure classes directly
- Put business logic in MonoBehaviour classes
- Use concrete types in Presentation layer
- Create circular dependencies between layers
- Mix UI code with business logic

## 🔧 Built-in Services

| Service | Interface | Location |
|---------|-----------|----------|
| Event Bus | `IEventBus` | `Application/Ports/EventBus/` |
| Storage | `IStorageService`, `IUserStorage` | `Application/Ports/` |
| Game Time | `IGameTime` | `Application/Ports/` |
| Console | `IConsole` | `Application/Ports/` |
| Service Locator | `IServiceLocator` | `Application/Ports/ServiceLocator/` |

## 📦 Dependencies

- **Unity** - Game engine
- **UniTask** - Async/await for Unity (via Cysharp)
- **ScriptableObject** - Configuration management

## 🎓 Why Hexagonal Architecture?

### Benefits:
- **Testability**: Easy to unit test core logic without Unity
- **Flexibility**: Swap implementations (e.g., different storage backends) without changing core
- **Maintainability**: Clear boundaries make code easier to understand and modify
- **Reusability**: Core logic can be reused in different contexts
- **Team Collaboration**: Teams can work on different layers independently

### When to Use:
- Medium to large projects
- Projects requiring extensibility
- Teams wanting clean separation of concerns
- Projects that may need platform-specific implementations

## 🤝 Contributing

This is a starter project. Feel free to fork and modify for your own games.

## 📄 License

This project is provided as-is for educational and commercial use.

---

**Built with ❤️ using Unity and Hexagonal Architecture principles**