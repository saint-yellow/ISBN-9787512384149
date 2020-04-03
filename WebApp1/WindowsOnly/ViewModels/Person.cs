using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WindowsOnly.ViewModels
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string GetFullName => FirstName + " " + LastName;
    }
}