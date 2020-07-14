using PEProtocol;
/// <summary>
/// 登录业务系统
/// </summary>
class LoginSys
{
    private static LoginSys instance = null;
    public static LoginSys Instance
    {
        get
        {
            if (instance == null) instance = new LoginSys();
            return instance;
        }
    }

    private CacheSvc cacheSvc = null;

    public void Init()
    {
        cacheSvc = CacheSvc.Instance;
        PECommon.Log("LoginSys Init Done.");
    }

    public void ReqLogin(MsgPack pack)
    {
        ReqLogin data = pack.msg.reqLogin;
        //当前账号是否已经上线
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.ReqLogin
        };

        if (cacheSvc.IsAcctOnLine(data.acct))
        {   //已上线：返回错误信息
            msg.err = (int)ErrorCode.AcctIsOnline;
        }
        else
        {
            //未上线：
            PlayerData pd = cacheSvc.GetPlayerData(data.acct, data.pass);
            //账号是否存在
            if (pd == null)
            {   //存在，检测密码
                msg.err = (int)ErrorCode.WrongPass;
            }
            else
            {
                msg.rspLogin = new RspLogin
                {
                    playerData = pd
                };

                cacheSvc.AcctOnline(data.acct, pack.session, pd);
            }
        }

        //回应客户端
        pack.session.SendMsg(msg);
    }
}
