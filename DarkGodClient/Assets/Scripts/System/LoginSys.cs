using PEProtocol;
using UnityEngine;

/// <summary>
/// 登录注册业务系统
/// </summary>
public class LoginSys : SystemRoot
{
    public static LoginSys Instance = null;

    public LoginWnd loginWnd;
    public CreateWnd createWnd;

    public override void InitSys()
    {
        base.InitSys();
        Instance = this;
        PECommon.Log("Init LoginSyc...");
    }

    /// <summary>
    /// 进入登陆场景
    /// </summary>
    public void EnterLogin()
    {
        //异步加载登录场景 并显示加载的进度 
        resSvc.AsyncLoadScene(Constants.SceneLogin, ()=> {
            //加载完成以后再打开注册登录界面
            loginWnd.SetWndState(true);
            audioSvc.PlayBGMusic(Constants.BGLogin);
        });
    }

    //登录回调
    public void RspLogin(GameMsg msg)
    {
        GameRoot.AddTips("登录成功");
        GameRoot.Instance.SetPlayerData(msg.rspLogin);

        if (msg.rspLogin.playerData.name == "")
        {
            //打开角色面板
            createWnd.SetWndState(true);
        }
        else
        {   //进入主城TODO

        }


        //关闭登录界面
        loginWnd.SetWndState(false);
    }
}
