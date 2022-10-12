using RamsayInnovations.Domain;
using RamsayInnovations.Domain.AggregatesModels.StudentAggregate;
using RamsayInnovations.Infrastructure.SeedWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace RamsayInnovations.Infrastructure.Repository
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(Sample_DBContext context) : base(context)
        {

        }
    }
}
