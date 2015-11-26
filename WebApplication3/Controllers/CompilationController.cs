using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class CompilationController : Controller
    {
        DefaultConnection context = new DefaultConnection();
        string mainCode = @"using System;

namespace ConsoleApplication1
    {
        class Program
        {

            @UserFunction

            @TestFunction
        
            static void Main(string[] args)
            {
                
                TestFunction();

            }
        }
    }";

        // GET: Compilation
        public ActionResult Index(CompileResult result)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Compile(int id)
        {
            return View(new CompileItem() { TaskId = id });
        }

        [HttpPost]
        public ActionResult Compile(CompileItem src)
        {
            var task = context.CodeTasks.First(w => w.Id == src.TaskId);
            var code = mainCode.Replace("@UserFunction", src.UserInput).Replace("@TestFunction", task.TestFunction);
            var exeName = AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + "\\" + "dd.exe";
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();
            parameters.ReferencedAssemblies.Add("System.Drawing.dll");
            parameters.GenerateInMemory = true;
            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = exeName;
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

            Process pr = new Process();
            pr.StartInfo.RedirectStandardOutput = true;
            pr.StartInfo.FileName = exeName;
            pr.StartInfo.UseShellExecute = false;
            pr.Start();
            var t = pr.StandardOutput.ReadToEnd();
            pr.WaitForExit();

            System.IO.File.Delete(exeName);

            if(t == "True")
            {
                return View("Index", new CompileResult() { Result = "Success!!! " + t });
            }
            return View("Index", new CompileResult() { Result = t });
        }

        public static CodeTask GetCodeTaskById(int id)
        {
            DefaultConnection con = new DefaultConnection();
            return con.CodeTasks.First(w => w.Id == id);
        }
    }
}