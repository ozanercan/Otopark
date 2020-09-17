using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Classes
{
    public static class CommonOperations
    {
        public static class Application {
            public static void Restart()
            {
                System.Windows.Forms.Application.Restart();
            }
            public static void Stop()
            {
                System.Windows.Application.Current.Shutdown();
            }

            public static string GetVersion()
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        /// <summary>
        /// Tawk.to adlı uygulamanın direk görüşme bağlantısını içerir.
        /// </summary>
        public static void StartLiveChat()
        {
            Process.Start(@"https://tawk.to/chat/5f5da07ef0e7167d000fd5d6/default");
        }
        

}
}
