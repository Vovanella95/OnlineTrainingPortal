using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Diagnostics;
using CoolName.Models;

namespace CoolName.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string source)
        {

            source =
      @"
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
namespace Foo
{
  public class Bar
  {
    static void Main(string[] args)
    {
      Bar.SayHello();
    }

    public static void SayHello()
    {" + source + 
       
      @"}
  }
}
      ";
            string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Foo.exe";
             
            Dictionary<string, string> providerOptions = new Dictionary<string, string>
            {
                {"CompilerVersion", "v3.5"}
            };
            CSharpCodeProvider provider = new CSharpCodeProvider(providerOptions);
            CompilerParameters compilerParams = new CompilerParameters(new string[] { "mscorlib.dll", "System.Core.dll" })
            { OutputAssembly = dataPath, GenerateExecutable = true };
            CompilerResults results = provider.CompileAssemblyFromSource(compilerParams, source);

            Process compiler = new Process();
            compiler.StartInfo.FileName = dataPath;
            compiler.StartInfo.UseShellExecute = false;
            compiler.StartInfo.RedirectStandardOutput = true;
            compiler.Start();
            string cc = compiler.StandardOutput.ReadToEnd();
            compiler.WaitForExit();
            return RedirectToAction("Result", new CodeTransporter() { Result = cc });
        }

        public ActionResult Result(CodeTransporter code)
        {
            return View(code);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}