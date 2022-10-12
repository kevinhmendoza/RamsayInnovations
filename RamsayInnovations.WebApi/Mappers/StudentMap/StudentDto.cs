using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RamsayInnovations.WebApi.Mappers.StudentMap
{
    public class StudentDto
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long Age { get; set; }
        public string Career { get; set; }
    }
}
