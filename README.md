## 영어 (English) ##
This is part of the source code used for the development of a unity vr game, a project undertaken in the second semester of 2023.  
The development period was about a month, and in that project, I did game development, presentation and demonstration, video production and editing.
The demonstration video can be viewed at the link below:  
https://drive.google.com/file/d/1lgUIsyv5EBpoGQSxtyuyJox27EzBtI2l/view?usp=sharing  

## Project Features ##
This comprehensive feature list outlines the functionalities implemented in the Unity project for a virtual farming and exploration game:

Directional Light: Simulates a full day within 360 seconds, adjusts the light angle to cast shadows, and changes the time of day every 120 seconds with a fade-in, fade-out effect to transition between morning, afternoon, and evening. Utilizes skybox assets to enhance realism and includes a dialogue system to indicate the time change.
#### XR Origin ####
Main Camera: Implements a fader screen and loading screen, providing natural transitions with fade-in and fade-out effects.  
Left Controller: Customizes the XR ray interactor with adjustments to interaction distance, color, and other settings.  
Right Controller: Similar to the left, with additional features like displaying health (HP), stamina, and money indicators. Integrates particles that attract crows when HP is low, reduces player speed when stamina is low, and includes save/load functionality with the Easy Save 3 asset. Also features a snap turn option.  
Hand Models: Implements hand models with natural skin tones and animations for grabbing/selecting.  
Body: Includes an inventory system on the left, right, and back, which follows the player and releases tracking when re-grabbed.  
#### Managers ####
BGM Manager: Randomly plays stored music clips and time-of-day sound effects, with a public function for volume control accessible from the menu.  
Audio Manager: Manages combat and eating sound effects.  
Tooltip Manager: Displays tooltips for selected fruits.  
Save Manager: Requests storage permissions on Android-based VR systems.  
Animal Spawning Manager: Efficiently spawns boars, tigers, and wolves using a spawning pool.  
#### Terrain ####
Land: Creates a lake with the Stylized Water asset, including sound effects, and uses texture stamps to form mountains. Decorates with trees and fences from nature assets.  
Farming Plot: Allows planting and growing of various plants, differentiated by seed type, with interactions for tilling and planting. Implements weed growth and removal.  
Sales Mechanics: Calculates selling price based on fruit size and sweetness, and adds money to the player's inventory upon sale.  
Purchase System: Facilitates buying seeds with varied costs and upgrades for gun damage.  
Barriers: Prevents players from leaving the map using colliders.  
Additional Objects: Adds trucks, roads, streetlights, boats, tables, and bridges to the map using assets.  
Resting Spots: Incorporates a bed that triggers a fade-in, fade-out effect and stamina recovery, along with a Zzz dialogue for sleep indication.  
House: Utilizes an asset for the house, with a collider that triggers door opening and closing animations and sound effects.
#### Tools ####
Watering Can: Creates water effects based on the angle, with sound effects.  
Hoe: Interacts with the farming plot for planting.  
Staff: Summons rain (particle effect) when shaken, along with corresponding sound effects.  
Gun: Implements two-handed grabbing and shooting mechanics with various sound effects and bullet trails.  
Seeds: Provides seeds for five different plant types for planting and growth.  
#### Particles ####
Soil Particle: Activates upon interaction with the hoe.  
Rain Particle: Adjustable for size, speed, radius, and quantity.  
Bird Particle: Follows the player when HP is below a certain threshold.  

#### ETC ####
Game Menu: Allows selection of BGM volume and tracks, pet choice, and teleportation guide implementation.  
Plants: Features various plants with positions for fruits, random sizes, and sweetness levels.  
Dialogue System: An external canvas displays customizable text messages.  
Animals: Implements sound effects for passive and hostile animals, such as rabbits, boars, tigers, and wolves, which interact with the player within a certain range or when shot.  
Additional Features: Optimizes the game using light and terrain baking and provides bug fixes. Suggests further enhancements like weed killers, plowing, and fertilizers.  


## 한국어 (Korean) ##
2023년 2학기에 진행한 식물키우기 VR 게임 제작에 사용한 소스 코드 중 일부입니다.  
개발 기간은 약 한 달이며, 해당 프로젝트에서 게임 개발, 발표 및 시연, 영상 제작 및 편집 등을 수행했습니다.  
시연 영상은 아래 링크에서 확인 가능합니다.  
https://drive.google.com/file/d/1lgUIsyv5EBpoGQSxtyuyJox27EzBtI2l/view?usp=sharing  

## 프로젝트 구현 기능 목록 ##
디렉셔널 라이트: 360초 내에 하루 전체를 시뮬레이션하며, 시간에 따라 빛의 각도를 조정하여 그림자를 생성합니다. 아침, 오후, 저녁으로 120초마다 시간대가 바뀌며 페이드 인, 페이드 아웃 효과로 전환됩니다. 스카이박스 에셋을 사용하여 현실감을 향상시키고, 시간 변경을 알리는 다이얼로그 시스템을 포함합니다.  

#### XR Origin ####
메인 카메라: 페이더 스크린과 로딩 스크린을 구현하여 자연스러운 전환 효과를 제공  
왼쪽 컨트롤러: 상호작용 거리, 색상 및 기타 설정을 조정하여 XR 레이 인터랙터를 맞춤 설정  
오른쪽 컨트롤러: 왼쪽과 유사한 기능을 갖추고 있으며, 추가로 HP, 스테미나, 돈 지표를 표시, HP가 낮을 때 까마귀를 끌어당기는 파티클, 스테미나가 낮을 때 플레이어 속도 감소, Easy Save 3 에셋을 사용한 저장/로드 기능, 스냅턴  
손 모델: 자연스러운 피부 톤과 잡기/선택 애니메이션을 갖춘 손 모델 구현  
몸체: 좌, 우, 등 뒤에 인벤토리 시스템을 구현, 잡혀있는 객체가 콜라이더 안에 들어오면 추적하여 플레이어를 따라오고, 플레이어가 다시 그랩하면 추적하는 기능 구현  
#### 매니저 ####
BGM 매니저: 저장된 음악 클립을 무작위로 재생, 시간대별 소리 효과, 메뉴에서 접근할 수 있는 공용 볼륨 제어  
오디오 매니저: 전투 및 먹는 소리 효과를 관리  
툴팁 매니저: 선택된 과일의 툴팁을 표시  
저장 매니저: 저장 공간 권한이 없는 경우 안드로이드 기반 VR 시스템에 요청  
동물 생성 매니저: 스포닝 풀을 사용하여 멧돼지, 호랑이 및 늑대를 효율적 생성  
#### 지형 ####
땅: Stylized Water 에셋을 이용해 호수 구현(3가지 효과음, 1가지 배경음), 지형 텍스쳐 스탬프를 이용해 산 구현, 기타 나무, 울타리 등 구현  
농장 밭: 식물을 심을 수 있는 기능 구현, 씨앗의 종류에 따라 밭 위에서 바로 자랄 수 있는 식물 존재, 밭을 갈고 난 후에 자랄 수 있는 식물이 따로 존재, 호미와 콜라이더 충돌 시 효과음, 파티클, 생성 개체와의 거리와 쿨타임 계산으로 진흙이 겹치지 않고 생성되게 구현, 랜덤한 시간마다 자라나는 잡초 구현 (위치 벗어날 시 삭제)  
판매 메커니즘: 과일의 크기와 당도를 기준으로 판매 가격을 계산, 판매 시 플레이어의 인벤토리에 돈을 추가  
구매 시스템: 식물 씨앗별로 구매 기능을 구현, 씨앗별로 구매 금액이 다르며 구입 성공/실패에 따른 효과음 차등 구현, 총의 데미지 업그레이드 기능 구현  
장벽: 콜라이더를 사용하여 플레이어가 맵을 벗어나지 못하게 설정  
추가 오브젝트: 에셋을 사용하여 맵에 트럭, 도로, 가로등, 보트, 테이블, 다리를 추가  
휴식 장소: 침대가 플레이어가 가까이 오면 페이드 인, 페이드 아웃 효과를 발동시키고, 스테미너를 회복시키며, 수면 표시를 위한 Zzz 다이얼로그를 표시  
집: 플레이어가 콜라이더 안으로 들어가면 문이 열리고 닫히는 애니메이션과 사운드 효과를 발동  
#### 도구 ####
물뿌리개: 각도에 따라 물 효과를 생성하며, 사운드 효과가 구현, 씨앗과 상호작용  
호미: 농장 밭과 상호작용하여 일부 식물 심기를 가능하게 변경  
지팡이: 흔들면 비 파티클 효과와 함께 관련 사운드 효과를 구현  
총: 그랩의 자연스러움을 위해 투 핸드 그랩 이용해 구현, 총알 발사 기능 및 다양한 발사음 기능, 총알 흔적 구현  
씨앗: 심기 및 성장을 위한 다섯 가지 식물의 씨앗 (가지, 호박, 순무, 토마토, 당근)  
#### 파티클 ####
토양 파티클: 호미와 상호작용할 때 활성화  
비 파티클: 크기, 속도, 반경 및 수량을 조절  
새 파티클: HP가 특정 임계치 이하일 때 플레이어를 따라다니게 구현  
#### 기타 ####
게임 메뉴: BGM 볼륨 및 트랙 선택, 펫 선택, 텔레포트 가이드 구현  
식물: 종류에 따라 포지션 배열로 열매의 위치, 열매의 사이즈, 당도 랜덤 구현  
다이얼로그 - 외부에서 접근하여 텍스트를 띄울 수 있는 캔버스  
동물(에셋) - 일정 시간마다 울음소리 등 효과음 구현  
	중립 동물 - 토끼, 근처의 풀을 삭제  
	적대 동물 - 멧돼지, 호랑이, 늑대 - 플레이어가 감지 범위 안에 들어오거나 총에 맞을 시 플레이어를 쫓아 공격하게 구현, hp 패널 구현  
	펫 - 플레이어를 따라다니는 강아지, 고양이 구현  
기타 - 라이트 및 지형 베이킹을 통한 최적화 및 버그 해결  
추가 구현해볼만한 기능 - 제초제, 뿌리 뽑기, 비료 등  
