# unique-project-client
 
 ### 1 Introduction
 a Client FrameWork Corresponding to Server FrameWork https://github.com/jcbjcbjc/Moba-Unique-Server
 
 ### 2 demo演示
 - **目前提供后端服务器进行测试ip和port已设定**
 - 后端服务器地址为https://github.com/jcbjcbjc/Moba-Unique-Server
 - 在本地clone两个项目其中一个项目的账号和密码改为1234567891和1234567891现在默认的是123456789和123456789
 - 然后打开0场景开始游戏，右侧按钮为匹配，左侧为退出游戏，在两个客户端上按下匹配按钮等待10s左右,会出现物体，**等帧数开始跑之后**按wsad即可使物体同步移动
 
 ### 3 效果图 
![Y Q`1XI(TTK8})RKSAATC3D](https://user-images.githubusercontent.com/91889375/196090576-ec4a3566-15d0-4012-a3a4-88ea4e6a7a40.png)


![$}X`F3AF NG5JGD1T2_2{L3](https://user-images.githubusercontent.com/91889375/196090616-884d1d29-4c38-4339-bf95-f454c1421678.png)

 
 ### 4 主要技术栈
 - 游戏引擎：Unity
 - 网络框架：C# .net原生API  由事件驱动
 - 网络协议：基于UDP的可靠协议KCP
 - 整体架构：
![image](https://user-images.githubusercontent.com/91889375/169481208-853e91c1-23ef-4fee-8994-20e91fa49703.png)
- 同步技术：帧同步（实现预测回滚技术）具体实现方法见https://github.com/jcbjcbjc/Moba-Unique-Server
- 序列化工具：Protobuf

实现功能：
- 登录注册服务
- 完整的User服务
- 实现房间管理和**多人实时匹配**机制
- **提供多客户端实时同步帧功能**
- 支持replay和**实时观战机制**
- 支持TCP协议KCP协议websocket协议
- 支持聊天服务
- 支持**断线重连**机制
         
### 5 优势
开发方便，只需写入对应逻辑和UI开发即可完成开发（现在已给出简单的demo）

