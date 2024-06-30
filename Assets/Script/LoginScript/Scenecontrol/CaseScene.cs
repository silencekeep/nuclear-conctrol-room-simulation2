// 案例场景状态
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CaseScene : SceneState
{
    public CaseScene(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "CaseScene";
    }

    //public UnityEvent OnExitClick;
    public override void StateBegin()
    {
        // 取得开始按钮
        GameObject Exit = GameObject.Find("Exit");
        Button tmpBtn = Exit.GetComponent<Button>();
        if (tmpBtn != null)
            tmpBtn.onClick.AddListener(() => OnStartGameBtnClick(tmpBtn));
        //调用权限管理
        receive r = Exit.GetComponent<receive>();
        r.inition();

    }
    private void OnStartGameBtnClick(Button theButton)
    {
        //OnExitClick.Invoke();
        m_Controller.SetState(new LoginScene(m_Controller), "LoginScene");
    }


}
