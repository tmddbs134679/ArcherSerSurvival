# 🎵 Monster Sound Manager



## 📖 도입 배경

몬스터마다 고유한 피격음, 사망 음성 등을 재생해야 하는 요구가 있었고, 이를 Monster마다 사용하는 `AudioClip` 들을 Scriptable Object로 간편하게  처리하고 관리하고자 **`MonsterSoundManager`** 시스템을 도입하였습니다.



## 🧩 트러블 슈팅



### ❌ 초기 설계 시, 몬스터 사운드(AudioClip)를 `MonsterData` SO에 함께 포함시켜 처리할지, 아니면 별도의 `MonsterSoundData` SO로 분리할지에 대한 구조적 고민



**예상 문제**  : 사운드 데이터와 비전략적 정보(스탯, 타입 등)가 한 곳에 몰리면서 책임이 과도해짐



✅ **해결 방법**:
 사운드 정보만 별도의 `MonsterSoundData` (SO)로 분리하고, `MonsterSoundManager`에서 몬스터 타입(`EENEMYTYPE`)을 기준으로 해당 사운드 SO를 불러오는 방식으로 설계하였습니다.