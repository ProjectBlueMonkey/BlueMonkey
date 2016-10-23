using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using BlueMonkey.MobileApp.DataObjects;

namespace BlueMonkey.MobileApp.Models
{
    public class MobileServiceContext : DbContext
    {
        private const string connectionStringName = "Name=MS_TableConnectionString";

        public MobileServiceContext() : base(connectionStringName)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Expense> Expense { get; set; }
        public DbSet<ExpenseReceipt> ExpenseReceipt { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
            modelBuilder.Entity<Category>()
                .ToTable(nameof(Category));
            modelBuilder.Entity<Expense>()
                .ToTable(nameof(Expense));
            modelBuilder.Entity<ExpenseReceipt>()
                .ToTable(nameof(ExpenseReceipt));
            modelBuilder.Entity<Report>()
                .ToTable(nameof(Report));
            modelBuilder.Entity<User>()
                .ToTable(nameof(User));
        }
    }
}
