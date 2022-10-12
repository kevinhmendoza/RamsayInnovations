using Microsoft.EntityFrameworkCore;
using RamsayInnovations.Domain.AggregatesModels.StudentAggregate;
using RamsayInnovations.Domain.SeedWorks;
using RamsayInnovations.Infrastructure.Repository;
using System.Threading.Tasks;

namespace RamsayInnovations.Infrastructure.SeedWorks
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private Sample_DBContext _sample_DBContext;
        public UnitOfWork(Sample_DBContext sample_DBContext)
        {
            _sample_DBContext = sample_DBContext;
        }


        private IStudentRepository _studentRepository;
        public IStudentRepository StudentRepository { get { return _studentRepository ?? (_studentRepository = new StudentRepository(_sample_DBContext)); } }

        public Task<int> Commit()
        {
            try
            {
                return _sample_DBContext.SaveChangesAsync();

            }
            catch (DbUpdateException e)
            {
                string message = DynamicException.Formatted(e);
                throw new DynamicFormattedException(message);
            }

        }
        /// <summary>
        /// Disposes the current object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (disposing && _sample_DBContext != null)
            {
                ((DbContext)_sample_DBContext).Dispose();
                _sample_DBContext = null;
            }
        }
    }
}
