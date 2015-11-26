using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplication3.Models
{
    public class DefaultConnection : DbContext
    {
        public DbSet<CodeTask> CodeTasks { get; set; }
        public DbSet<UserData> UserDatas { get; set; }
    }

    public class CodeTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public byte[] Image { get; set; }
        public string TestFunction { get; set; }
        public string UserInputTemplate { get; set;  }
        public int Coast { get; set; }
        public DateTime Datastamp { get; set; }
    }

}