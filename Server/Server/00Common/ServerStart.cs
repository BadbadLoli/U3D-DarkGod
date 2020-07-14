/// <summary>
/// 服务端主入口
/// </summary>
class ServerStart
{
    static void Main(string[] args)
    {
        ServerRoot.Instance.Init();
        while (true)
        {
            ServerRoot.Instance.Update();
        }
    }
}
