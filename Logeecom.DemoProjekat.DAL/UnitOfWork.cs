using Logeecom.DemoProjekat.DAL.Models;
using Logeecom.DemoProjekat.DAL.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Logeecom.DemoProjekat.Exceptions;

namespace Logeecom.DemoProjekat.DAL
{
    public class UnitOfWork
    {
        private readonly DemoDbContext context;
        public CategoryRepository CategoryRepository { get; }
        public ProductRepository ProductRepository { get; }
        public UserRepository UserRepository { get; }

        public UnitOfWork(DemoDbContext context)
        {
            this.context = context;

            this.CategoryRepository = new CategoryRepository(this.context);
            this.ProductRepository = new ProductRepository(this.context);
            this.UserRepository = new UserRepository(this.context);
        }

        public void Save()
        {
            try
            {
                this.context.SaveChanges();
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException exception && exception.Number == 2601)
            {
                throw new UniqueConstraintException();
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException exception && exception.Number == 547)
            {
                throw new ForeignKeyConstraintException();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new PrimaryKeyConstraintException();
            }
        }
    }
}
