# 🧟‍♂️ Enemy FSM 구조

이 프로젝트는 **적 몬스터의 상태 기반 AI(FSM)** 를 구현한 구조입니다.

 공통 `State` 인터페이스를 기반으로, 몬스터 종류별로 다양한 상태를 세분화하여 설계하였습니다.
 FSM은 몬스터의 애니메이션, 행동, 스탯을 관리하며, **유지보수성/확장성/재사용성**을 고려해 구조화되어 있습니다.

------



## 📖 도입 배경

게임 내에서 다양한 종류의 적 몬스터가 등장함에 따라, 각 몬스터의 행동(대기, 추격, 공격, 스턴 등)을
 **일관된 방식으로 관리**할 수 있는 구조가 필요했습니다.

기존에는 몬스터마다 고유한 `Update` 로직 내에서 직접 행동 조건을 분기하며 구현했지만,
 이 방식은 코드의 **중복과 혼란**, 그리고 **유지보수성의 저하**를 야기했습니다.

따라서 아래와 같은 목적을 달성하기 위해 **FSM(Finite State Machine)** 기반 구조를 도입하였습니다:

- 각 상태(`Idle`, `Attack`, `Dead` 등)를 **클래스로 분리**하여 역할을 명확하게 관리
- **공통 동작 추상화**를 통해 코드 재사용성 향상
- 다양한 몬스터에 대해 **유연하게 FSM을 조합**할 수 있도록 설계
- 추후 몬스터 추가 시, 기존 FSM 구조를 기반으로 **손쉽게 상태 확장 가능**



## 📌 목차

- [FSM 구조 개요](#fsm-구조-개요)
- [기본 State 인터페이스 및 추상 클래스](#기본-state-인터페이스-및-추상-클래스)
- [공통 Enemy 상태 목록](#공통-enemy-상태-목록)
- [몬스터 특화 상태 목록](#몬스터-특화-상태-목록)
- [StateMachine 구성](#statemachine-구성)



## 🧩 FSM 구조 개요

```
State
 └── EnemyBaseState
       ├── EnemyIdleState
       ├── EnemyChasingState
       ├── EnemyAttackState
       ├── EnemyStunState
       ├── EnemyDeadState
       ├── EnemyPatrolState
       └── EnemySkillState
             ├── GoblinIdleState
             ├── GoblinAttackState
             ├── OrgeIdleState
             └── OrgeChasingState
```

> 공통 상태(`EnemyBaseState` 하위)는 대부분의 몬스터에서 공유되며, 몬스터에 따라 개별 상태 클래스를 오버라이드하거나 확장합니다.





## 🧱 기본 State 인터페이스 및 추상 클래스

| 클래스명         | 설명                                                         |
| ---------------- | ------------------------------------------------------------ |
| `State`          | 모든 상태가 구현해야 하는 기본 인터페이스 (`Enter`, `Tick`, `Exit`) |
| `EnemyBaseState` | 공통적으로 사용되는 기능 구현, 각 상태의 부모 클래스 역할    |





## 🧬 몬스터 특화 상태 목록

| 몬스터 | 상태    | 클래스명            |
| ------ | ------- | ------------------- |
| Goblin | Idle    | `GoblinIdleState`   |
| Goblin | Attack  | `GoblinAttackState` |
| Orge   | Idle    | `OrgeIdleState`     |
| Orge   | Chasing | `OrgeChasingState`  |





## 🧠 StateMachine 구성

| 클래스명             | 설명                                               |
| -------------------- | -------------------------------------------------- |
| `StateMachine`       | 공통 FSM 시스템 (Animator, Stat 등 공통 정보 포함) |
| `GoblinStateMachine` | Goblin 전용 FSM (Goblin 관련 상태 추가)            |
| `OrgeStateMachine`   | Orge 전용 FSM                                      |
| `DocStateMachine`    | Doc 몬스터 전용 FSM                                |
| `SkeletStateMachine` | Skelet 몬스터 전용 FSM                             |

> `StateMachine`은 각 몬스터 프리팹에 붙어 있으며, FSM을 초기화하고 상태 전환을 관리합니다.



## 🧩 트러블 슈팅



### ❌ 문제 1: 상태 전환 시 `new` 연산 → GC 및 퍼포먼스 저하 우려

초기 설계에서는 매 상태 전환 시마다 `new`로 상태 객체를 생성했습니다.
 이로 인해 **GC(Garbage Collection) 빈도 증가**, 특히 **빈번한 상태 전환**이 발생하는 몬스터에게 성능 이슈가 발생했습니다.

✅ **해결 방법**:
 상태 객체를 사전에 생성하고, 아래처럼 **Dictionary 캐싱 구조**를 통해 재사용하도록 변경했습니다.

```
public Dictionary<EENEMYSTATE, EnemyBaseState> States = new();
```

- 모든 상태는 초기화 시 `States` 딕셔너리에 등록
- 전환 시에는 `States[stateType]`을 재사용하여 GC 발생 방지

------



### ❌ 문제 2: 스킬 상태 확장 시 클래스 구조 과도한 상속 문제

`EnemySkillState` 내에서 개별 스킬을 별도 클래스로 관리하려 했지만,
 이 방식은 스킬이 늘어날수록 **클래스 수 증가 및 상속 깊이**로 인한 **복잡도 폭발**이 예상되었습니다.

✅ **해결 방법**:
 `List<BaseSkill>` 형태로 스킬 데이터를 받아 상태 내에서 **동적 실행**하는 방식으로 변경했습니다.

```
public List<BaseSkill> Skills;
```

- 각 몬스터는 자신의 스킬 리스트만 정의하면 되고,
- `EnemySkillState`는 해당 리스트를 순회하며 스킬을 실행
- OOP + 데이터 주도 방식으로 확장성 확보



## 📌 향후 확장 방향

- 몬스터 특화 상태의 중복 코드 제거 → `Strategy Pattern` 적용 고려
- 상태 전이 조건 분리 → `TransitionManager` 도입 고려