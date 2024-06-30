public abstract class SceneState
{
    // 状态名称
    private string m_StateName = "SceneState";
    public string StateName
    {
        get { return m_StateName; }
        set { m_StateName = value; }
    }

    // 状态拥有者
    protected SceneStateController m_Controller = null;

    // 构造
    public SceneState(SceneStateController Controller)
    {
        m_Controller = Controller;
    }

    // 开始
    public virtual void StateBegin()
    { }

    // 結束
    public virtual void StateEnd()
    { }

    // 更新
    public virtual void StateUpdate()
    { }

    public override string ToString()
    {
        return string.Format("[I_SceneState: StateName={0}]", StateName);
    }

}