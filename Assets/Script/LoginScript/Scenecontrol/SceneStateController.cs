using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// 场景状态控制者
public class SceneStateController
{
    private SceneState m_State;
    private bool m_bRunBegin = false;

    public SceneStateController()
    { }

    // 设定状态
    public void SetState(SceneState State, string LoadSceneName)
    {
        //Debug.Log ("SetState:"+State.ToString());
        m_bRunBegin = false;

        // 载入场景
        LoadScene(LoadSceneName);

        // 通知前一个State結束
        if (m_State != null)
            m_State.StateEnd();

        // 设定
        m_State = State;
    }

    // 载入
    private void LoadScene(string LoadSceneName)
    {
        if (LoadSceneName == null || LoadSceneName.Length == 0)
            return;
        SceneManager.LoadSceneAsync(LoadSceneName);
    }

    // 更新
    [System.Obsolete]
    public void StateUpdate()
    {
        // 是否還在載入
        if (Application.isLoadingLevel)
            return;

        // 通知新的State開始
        if (m_State != null && m_bRunBegin == false)
        {
            m_State.StateBegin();
            m_bRunBegin = true;
        }

        if (m_State != null)
            m_State.StateUpdate();
    }
}