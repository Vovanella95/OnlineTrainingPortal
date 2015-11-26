using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class UserData
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string AboutMe { get; set; }

        public double Level { get; set; }

        public byte[] Avatar { get; set; }

        public List<int> CompletedTasks { get; set; }

        public List<UserData> Friends { get; set; }

        public DateTime DateOfRegistration { get; set; }
    }
}