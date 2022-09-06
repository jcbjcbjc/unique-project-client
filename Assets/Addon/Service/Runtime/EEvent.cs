public enum EEvent
{
    /// <summary>
    /// �������������صĳ�����
    /// </summary>
    BeforeLoadScene,
    /// <summary>
    /// �������ռ��غõĳ�����
    /// </summary>
    AfterLoadScene,

    GameStateChange,
    InputStateChange,
    /// <summary>
    /// Tick���������н�ɫ�ж�����֮��
    /// </summary>
    Tick,

    /// <summary>
    /// ������Ҫ��ʾ����Ϣ���������Ļ���꣬����ƫ����
    /// </summary>
    View,
    EndView,

    PlayerSelectSkill,
    /// <summary>
    /// ���������ܻ��ѵ�ʱ�䣬���ܵ�Ӱ�췶Χ
    /// </summary>
    PlayerCastSkill,
    /// <summary>
    /// ��������Ϣ���ͣ���Ϣ����
    /// </summary>
    Message,

    BeforeSave,
    /// <summary>
    /// ֻ������һ�������е�������Awake�ж������������ڵ�����ͨ�����¼�����
    /// </summary>
    AfterLoad,

    StartDialog,
    EndDialog,

    StartPerformance,
    EndPerformance,

    CameraSizeChange,
}
