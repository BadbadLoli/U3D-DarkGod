/// <summary>
/// 服务器初始化
/// </summary>
class ServerRoot
{
    private static ServerRoot instance = null;
    public static ServerRoot Instance
    {
        get
        {
            if (instance == null) instance = new ServerRoot();
            return instance;
        }
    }

    public void Init()
    {
        //数据层

        //服务层
        CacheSvc.Instance.Init();
        NetSvc.Instance.Init();

        //业务系统层
        LoginSys.Instance.Init();
    }

    public void Update()
    {
        NetSvc.Instance.Update();
    }
}
