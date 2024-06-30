// 登录场景状态
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginScene : SceneState
{
    public LoginScene(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "LoginScene";
    }

    public override void StateBegin()
    {
        // 取得开始按钮
        Button tmpBtn = GameObject.Find("确认").GetComponent<Button>();
        if (tmpBtn != null)
            tmpBtn.onClick.AddListener(() => OnStartGameBtnClick(tmpBtn));
    }
    private void OnStartGameBtnClick(Button theButton)
    {
        RegisterControl RC = GameObject.Find("登录注册界面").GetComponent<RegisterControl>();
        bool b = RC.EnterButton();
        if (b)
        {
            m_Controller.SetState(new CaseScene(m_Controller), "CaseScene");
        }
    }

}
