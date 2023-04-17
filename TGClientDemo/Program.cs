// See https://aka.ms/new-console-template for more information
using Starksoft.Net.Proxy;
using TL;

using var client = new WTelegram.Client(Config);
client.TcpHandler = async (address, port) =>
{
    var proxy = new Socks5ProxyClient("127.0.0.1", 10808);
    //var proxy = xNet.Socks5ProxyClient.Parse("host:port:username:password");
    return proxy.CreateConnection(address, port);
};
await client.LoginUserIfNeeded();

while (true)
{
    Console.WriteLine("请输入用户名：");
    var input = Console.ReadLine();
    try
    {
        var flag = await client.Account_CheckUsername(input);
        if (flag)
        {
            Console.WriteLine($"该用户名可用：{input}");
        }
        else
        {
            Console.WriteLine($"该用户名不可用：{input}");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"该用户名无效：{input}");
    }

}

Console.ReadKey();


static string Config(string what)
{
    switch (what)
    {
        case "api_id": return "13449615";
        case "api_hash": return "2fb2fbe5252ccfb3ca1620479f1231d0";
        case "phone_number": return "+8618831523519";
        case "verification_code": Console.Write("Code: "); return Console.ReadLine();
        //case "first_name": return "John";      // if sign-up is required
        //case "last_name": return "Doe";        // if sign-up is required
        //case "password": return "secret!";     // if user has enabled 2FA
        default: return null;                  // let WTelegramClient decide the default config
    }
}