using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstApp.Model; 

namespace FirstApp
{
    //[DbConfigurationType]
    public class SalesLTContext : DbContext
    {
        public SalesLTContext(DbContextOptions<SalesLTContext> options)
            : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Customer>().ToTable("Customers", schemaName: "Ordering");
        //}
        public virtual DbSet<Customer> Customers { get; set; }
    }
}
