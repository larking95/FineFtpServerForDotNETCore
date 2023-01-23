
using System.Threading.Tasks;

namespace FineFtpConsoleApp
{    
    internal class Program
    {
        static void Main(string[] args)
        {
            string local_loot = @"C:\\";

            //// Create an FTP Server on port 2121. We use 2121 instead of the default port
            //// 21, as your application would need root privileges to open port 21.
            FineFtpWrapper.FtpServer server = new FineFtpWrapper.FtpServer("127.0.0.1", 2121);

            //// Add the well known anonymous user and some normal users. The anonymous user
            //// can log in with username "anonyous" or "ftp" and any password. The normal
            //// users have to provide their username and password. 
            server.addUser("TestUser", "abc", local_loot, FineFtpWrapper.Permission.All);

            //// Start the FTP server with 4 threads. More threads will increase the
            //// performance with multiple clients, but don't over-do it.
            server.start(1);

            //// Prevent the application from exiting immediatelly
            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}