# 🧠 Top-Down Herd Prototype (Unity)

<p align="center">
  <img src="Media/gameplay.gif" width="600"/>
</p>

---

## 🎯 Overview

A 2D top-down prototype built in Unity with a strong focus on **clean architecture, separation of concerns, and scalability**.

The project demonstrates how to structure gameplay systems using a layered approach with **pure domain logic, application use-cases, and Unity presentation layer**.

> ⚠️ This project focuses on **architecture and code quality**, not visuals.

---

## 🎮 Gameplay

- Click anywhere → Hero moves  
- Animals patrol within a defined radius  
- Hero collects animals into a herd  
- Max **5 animals** follow the hero  
- Animals form a **chain following system**  
- Deliver animals to the **Yard Zone**  
- Score increases per delivered animal  

---

## 🧱 Architecture

### 🔑 Core Idea

The project is built using a **layered architecture**:

```
Domain → Application → UnityPresentation
```

---

## 📦 Project Structure

```
Scripts/
├── Domain/               # Pure game logic (NO Unity dependencies)
├── Application/          # Gameplay use-cases and orchestration
└── UnityPresentation/    # Unity layer (Views, Input, Composition)
```

---

## 🧠 Domain Layer

Pure C# logic, completely independent from Unity.

Contains:
- HeroModel
- AnimalModel
- AnimalStateMachine
- HerdService
- ScoreService
- MovementService

✔ No MonoBehaviour  
✔ No Transform  
✔ No UnityEngine references (enforced via asmdef)  
✔ Fully testable  

---

## ⚙️ Application Layer

Orchestrates gameplay logic:

- GameplayUpdateService  
- AnimalSpawnService  
- AnimalSpawnTimerService  
- AnimalCollectionService  
- AnimalDeliveryService  
- HeroInputService  
- AnimalStateFactory  

---

### 🔄 Event-Driven Architecture

Gameplay communication is decoupled using an **Event Bus**:

```
AnimalDeliveryService
    → publishes AnimalDeliveredEvent

Handlers:
    → Score updated
    → Animal despawned
    → View removed
```

✔ No direct service dependencies  
✔ Flexible and scalable  
✔ Easy to extend  

---

## 🎨 UnityPresentation Layer

Responsible only for **visual representation**.

Contains:
- Views (HeroView, AnimalView, ScoreView)
- Binders (HeroViewBinder, AnimalViewBinder)
- View lifecycle (AnimalViewPool, AnimalViewRegistry)

---

### 🔗 Binding Flow

```
Model → Binder → View → Transform
```

---

## 🏗️ Composition Root & Installers

Scene assembly is handled via:

```
SceneCompositionRoot
    → WorldInstaller
    → GameplayInstaller
    → PresentationInstaller
```

Each installer:
- builds a specific part of the system  
- returns a **context object**  
- keeps dependencies explicit  

✔ No God object  
✔ Clear responsibility separation  

---

## 🔄 Lifecycle Management

Centralized via:

```
GameplayLifetime
```

Handles:
- Initialization  
- Update loop  
- Disposal  

---

## 🧠 Patterns Used

- State Pattern — animal behavior  
- Factory Pattern — per-animal states (no shared state)  
- Observer Pattern — UI updates  
- Event Bus — decoupled communication  
- Object Pool — efficient view reuse  
- Composition Root — dependency control  

---

## 🎯 Key Engineering Decisions

### ✔ No Unity in Domain
Ensures testability and clean architecture.

---

### ✔ Event-driven gameplay
Removes tight coupling between systems.

---

### ✔ Deterministic randomness abstraction
All randomness goes through `IRandomService`.

---

### ✔ Clean View lifecycle

```
Pool → controls activation  
Binder → controls binding  
```

---

### ✔ Assembly boundaries (asmdef)

```
Domain → no UnityEngine allowed  
Application → depends on Domain  
UnityPresentation → depends on both  
```

---

## 🧪 Tests

EditMode tests cover:

- Herd logic  
- Movement logic  
- Collection logic  
- Delivery flow  
- EventBus workflow  

✔ Runs without Unity scene  
✔ Fast and deterministic  

---

## ▶️ How to Run

1. Open `MainScene`
2. Ensure `SceneCompositionRoot` is present
3. Press Play

---

## 📌 Notes

This project demonstrates:

- Clean Architecture in Unity  
- Separation of concerns  
- Event-driven gameplay  
- Scalable systems  
- Production-ready structure  

---

## 👨‍💻 Author

Daniil Pavlenko  
Unity Game Developer