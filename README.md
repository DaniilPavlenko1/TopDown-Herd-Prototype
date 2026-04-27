\# 🧠 Top-Down Herd Prototype (Unity)



!\[Gameplay](README/gameplay.gif)



\## 🎯 Overview



A small 2D top-down prototype built in Unity demonstrating clean architecture, scalable systems, and gameplay fundamentals.



The player controls a hero by clicking on the ground, collects animals into a herd, and delivers them to a yard to gain score.



> ⚠️ This project focuses on \*\*architecture and code quality\*\*, not visuals.



\---



\## 🎮 Gameplay



\* Click anywhere → Hero moves to target position

\* Animals patrol randomly within a defined area

\* Hero collects animals within radius

\* Max \*\*5 animals\*\* follow the hero

\* Animals form a \*\*chain (follow system)\*\*

\* Deliver animals to the \*\*Yard Zone\*\*

\* Score increases per delivered animal



\---



\## 🧱 Architecture



\### Core Principle



```

MonoBehaviour → View / Entry point

Pure C# classes → Logic / Services

```



\---



\### Systems Overview



| System                     | Responsibility                                   |

| -------------------------- | ------------------------------------------------ |

| GameBootstrapper           | Manual dependency injection                      |

| InputService               | Input abstraction (mouse click → world position) |

| HeroController / HeroMover | Player movement                                  |

| AnimalController           | Entry point for animal logic                     |

| AnimalStateMachine         | State management                                 |

| HerdService                | Manages collected animals                        |

| DeliveryService            | Handles delivery logic                           |

| ScoreService               | Score state + events                             |

| AnimalSpawner              | Spawning logic                                   |

| AnimalPool                 | Object reuse                                     |



\---



\## 🧠 Patterns Used



\### ✔ State Pattern



Animals use a state machine:



\* Patrol

\* FollowHero

\* Delivered (via pooling)



\---



\### ✔ Observer Pattern



```

ScoreService → ScoreView

```



\---



\### ✔ Service Pattern



\* HerdService

\* ScoreService

\* InputService



\---



\### ✔ Object Pool



\* AnimalPool avoids frequent Instantiate/Destroy



\---



\### ✔ Manual Dependency Injection



\* Implemented via GameBootstrapper



\---



\## 🧱 SOLID



\* \*\*S\*\* — Single Responsibility (small focused classes)

\* \*\*O\*\* — Open for extension (new states can be added)

\* \*\*L\*\* — States interchangeable via interface

\* \*\*I\*\* — Small interfaces (`IInputService`, `IHerdService`)

\* \*\*D\*\* — Dependencies injected, no singletons



\---



\## 🌍 Adaptive World System



The game dynamically adapts to different screen sizes:



\* Camera scales to fit gameplay area

\* Ground resizes to fill visible screen

\* SpawnArea adjusts based on available space

\* YardZone always positioned on the right side



This ensures consistent gameplay across devices.



\---



\## ⚙️ Performance Decisions



\* `OverlapCircleNonAlloc` used to avoid GC allocations in Update

\* Object Pooling for animals

\* Minimal allocations during gameplay loop

\* MoveTowards used for stable movement



\---



\## ▶️ How to Run



1\. Open `MainScene`

2\. Press \*\*Play\*\*



\---



\## 📦 Implemented Features



\* Hero movement by click

\* Animal AI (patrol + follow)

\* Herd system (max limit)

\* Delivery system

\* Score system

\* Object pooling

\* Adaptive screen layout



\---



\## 💡 Notes



The goal of this project was to demonstrate:



\* Clean and scalable architecture

\* Separation of concerns

\* Maintainable and testable code

\* Practical use of patterns in Unity



\---



\## 👨‍💻 Author



Daniil Pavlenko

Unity Game Developer





