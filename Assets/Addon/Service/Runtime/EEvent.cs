public enum EEvent
{
    /// <summary>
    /// 参数：即将加载的场景号
    /// </summary>
    BeforeLoadScene,
    /// <summary>
    /// 参数：刚加载好的场景号
    /// </summary>
    AfterLoadScene,

    GameStateChange,
    InputStateChange,
    /// <summary>
    /// Tick发生在所有角色行动结束之后
    /// </summary>
    Tick,

    /// <summary>
    /// 参数：要显示的信息，对象的屏幕坐标，窗口偏移量
    /// </summary>
    View,
    EndView,

    PlayerSelectSkill,
    /// <summary>
    /// 参数：技能花费的时间，技能的影响范围
    /// </summary>
    PlayerCastSkill,
    /// <summary>
    /// 参数：消息类型，消息内容
    /// </summary>
    Message,

    BeforeSave,
    /// <summary>
    /// 只存在于一个场景中的物体在Awake中读档，持续存在的物体通过此事件读档
    /// </summary>
    AfterLoad,

    StartDialog,
    EndDialog,

    StartPerformance,
    EndPerformance,

    CameraSizeChange,
}
