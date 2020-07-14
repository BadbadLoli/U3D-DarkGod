

/// <summary>
/// 网络服务
/// </summary>
class NetSvc
{
    private static NetSvc instance = null;
    public static NetSvc Instance
    {
        get
        {
            if (instance == null) instance = new NetSvc();
            return instance;
        }
    }

    public void Init()
    {
        
    }
}
