using CommonWordsInAllFilesAPI;
using CommonWordsInAllFilesAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;

namespace SelfHostingWithConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var config= new HttpSelfHostConfiguration("http://localhost:1234");
            WebApiConfig.Register(config);
            var server = new HttpSelfHostServer(config);
            var task = server.OpenAsync();
            task.Wait();
            Console.WriteLine("Web API Server has started at http://localhost:1234");
            Console.ReadLine();
        }
    }
}
