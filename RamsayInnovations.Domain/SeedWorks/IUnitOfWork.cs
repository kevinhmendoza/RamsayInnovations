using RamsayInnovations.Domain.AggregatesModels.StudentAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RamsayInnovations.Domain.SeedWorks
{
    public interface IUnitOfWork : IDisposable
    {

        IStudentRepository StudentRepository { get; }
        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        Task<int> Commit();
    }
}
