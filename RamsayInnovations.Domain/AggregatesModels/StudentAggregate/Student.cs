using System;
using System.Collections.Generic;

#nullable disable

namespace RamsayInnovations.Domain
{
    public partial class Student : Entity<long>
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long Age { get; set; }
        public string Career { get; set; }
    }
}
