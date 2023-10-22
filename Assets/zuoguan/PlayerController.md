一、角色控制器程序使用。

1、使用前需安装三个插件，通过菜单栏Window/Package Manager进入。注意先切换到Packages:Unity Registry，然后搜索cinemachine和input system和2D安装。

![](C:\Users\15861\AppData\Roaming\Typora\typora-user-images\1696505656151.png)

![](C:\Users\15861\AppData\Roaming\Typora\typora-user-images\1696505625775.png)

![1696505608857](C:\Users\15861\AppData\Roaming\Typora\typora-user-images\1696505608857.png)



2、layer

如果layer没有自动添加，请按下图顺序手动添加layer。

![1696505905454](C:\Users\15861\AppData\Roaming\Typora\typora-user-images\1696505905454.png)

 

二、使用及参数说明。

 程序前缀 Assets/zuoguan/Assets，以下文件皆在此目录下。



1、AD移动，W交互（需要与场景结合，暂未完成，留有接口），S蹲下，J攻击，K跳跃，L冲刺。

可打开Settings/InputSystem/PlayerInputActions修改

 

2、玩家状态参数。

**Data/Player States/PlayerState_Climb（扒平台状态）**

-Transition Duration 动画切换过渡时间（下同）

**Data/Player States/PlayerState_CoyoteTime（土狼时间状态）**

-CoyoteTime 土狼时间持续时间（玩家在该持续时间内离开平台仍可进行跳跃，增强手感）

-RunSpeed 土狼时间状态玩家移动速度

**Data/Player States/PlayerState_JumpUp（跳跃向上状态）**

-JumpForce 跳跃施加的向上初速度

-MoveSpeed 跳跃向上阶段左右移动速度

-CanSmallJump 是否开启大小跳（按住跳跃键时间越短跳的越矮）

-DeAcceleration 跳跃向上阶段减速度

**Data/Player States/PlayerState_Fall（跳跃下落状态）**

-SpeedCurve 下落过程中下落速度曲线

-MoveSpeed 下落过程左右移动速度

**Data/Player States/PlayerState_Land（跳跃落地状态）**

-StiffTime 落地僵直时间（用以播放落地动画使动作流畅，也可以设为0）

**Data/Player States/PlayerState_Idle（空闲状态）**

-Deceleration 跑步状态过渡到空闲状态的减速度

**Data/Player States/PlayerState_Run（跑步状态）**

-Acceleration空闲状态过渡到跑步状态的加速度

-RunSpeed 跑步速度

**Data/Player States/PlayerState_Sprint（冲刺状态）**

-SprintTime 冲刺时间

-SprintDistance 冲刺距离

**Data/Player States/PlayerState_FlySprint（空中冲刺状态）**

-SprintTime 冲刺时间

-SprintDistance 冲刺距离

**Data/Player States/PlayerState_Squat（下蹲状态）**

-Scale 下蹲后碰撞体高度缩放比例

 

3、Player挂载脚本参数

**PlayerController**

-FlySprintCD 空中冲刺CD

-SprintCD 冲刺CD

**PlayerInput**

-JumpInputBufferTime 跳跃缓冲预输入（改进手感）

**Player/GrounDetector（玩家脚下的小球判断玩家是否在地面上，可修改位置和半径）**

-DetectionRadius 判断玩家是否在地面的检测范围

-GroundLayer 地面层

**Player/LeftDetector（玩家脸上的小球判断玩家是否扒住平台，可修改位置和半径）**

-DetectionRadius 判断玩家是否要扒住平台的检测范围

-GroundLayer 地面层



 4、Enemy挂载脚本参数

**Enemy/ChaseDetector（追逐玩家的检测范围）**

-DetectionRadius 检测范围

-Layer Mask 玩家层

**Enemy/LeftDetector（敌人左边脚下的小球，判断是否撞墙和平台边界）**

-DetectionRadius 检测范围

-GroundLayer 地面层

-WallLyaer 墙层

**Enemy/RightDetector（敌人右边脚下的小球，判断是否撞墙和平台边界）**

-DetectionRadius 检测范围

-GroundLayer 地面层

-WallLyaer 墙层

 

 5、Enemy状态参数

 **EnemyState_Idle（不动状态）**

侦测到玩家发射子弹

 **EnemyState_Run（巡逻状态）**

-RunSpeed 速度

-Left Range 相对于敌人初始位置的左侧巡逻范围

-Right Range 相对于敌人初始位置的右侧巡逻范围

 **EnemyState_Chase（追逐状态）**

-RunSpeed 速度

-ExtRange 追逐过程中可超出巡逻范围的追逐距离，超出ExtRange 加巡逻范围后丢失仇恨

 **EnemyState_Attack（攻击状态）**

-BulletSpeend 子弹速度

-Interval 发射子弹时间间隔

-Duration 该状态持续时间，持续时间结束后回到巡逻状态

-BulletName



6、Prefabs说明

Ground：地面，可以站在上面

Platform：平台，可单向穿越，并站在上面，按s+k可以跳下平台

Wall：墙，不可通过

PlayerAfterImagePool：冲刺残影，必须放到场景中任意位置player才能有冲刺残影

SprintWall：墙，冲刺可通过

Player：黑色的表示武器，按j攻击可击杀Enemy

Skill_Sprint：按w交互可获得冲刺

Skill_FlySprint：按w交互可获得空中冲刺

Skill_Climb：按w交互可获得扒墙

Skill_Attack：按w交互可获得攻击

GroundSpike：地刺

SkillData：必须放入玩家第一个进入的场景（其他场景不用放入），保证场景切换玩家可以使用已获得的技能

SceneTrigger：场景切换触发器，参数设为对应场景名即可，以及要切换场景的出生的位置索引。（某一场景的所有出生位置加入到场景中的player/playerController脚本的pos参数中，索引从0开始）

例，LevelTest1中的SceneTrigger2，设置为LevelTest2，Index设为0，即切换到场景LevelTest2中的player上pos中的第一个位置。LevelTest1中的SceneTrigger，设置为LevelTest3，Index设为1，即切换到场景LevelTest3中的player上pos中的第二个位置。



LaserWall：激光墙，冲刺可通过

DestructibleWall：攻击可破坏的墙



7.敌人说明

Prefabs/EnemyType1：西装男

Prefabs/EnemyType2：护士女

Prefabs/EnemyType3：休闲男



Data/EnemyType1的state逻辑：巡逻->发现玩家开始追逐->超出范围发射子弹->失去仇恨恢复巡逻

Data/EnemyType2的state逻辑：巡逻

Data/EnemyType3的state逻辑：不动->发现玩家发射子弹->一段时间后恢复不动



##### 敌人制作

先确定要复制的敌人的动画，选择Prefabs中的某一类型的敌人复制到场景。再确定该敌人的ai逻辑，选择Data中某一类逻辑的敌人文件夹下的所有状态复制到自己的文件夹。

然后删掉敌人挂载的EnemyStateMachine脚本中的States参数，将之前复制的状态都拖入到states中。



例：要制作只巡逻的西装男，首先复制Prefabs/EnemyType1到场景命名为enemy1，然后复制Data/EnemyType2中的所有状态到自己的文件夹，文件夹命名为enemy1与敌人预制体对应。然后将reset一下enemy1预制体上EnemyStateMachine脚本，拖入文件夹enemy1中的所有状态到states，以及对应的初始状态到initialState，一个只巡逻的西装男就制作完成。
