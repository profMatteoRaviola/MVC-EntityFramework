using Microsoft.EntityFrameworkCore;
using MVC_EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_EntityFramework
{
    public class Context : DbContext
    {
        //serve fare questo per non cablare la connection string nel context, ma per leggerla dal file di config json
        //vedere Startup.ConfigureServices
        public Context(DbContextOptions<Context> options): base(options) { }
        public DbSet<Cliente> Clienti { get; set; } //entità Clienti
        public DbSet<Pianta> Piante { get; set; } //entità Piante
        public DbSet<Ordine> Ordini { get; set; } //entità Ordini

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){ }
         non serve il metodo, faccio funzionare tutto con la Dependecy Injection nella classe Startup*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().HasMany(p => p.Ordini).WithOne(p => p.Cliente).IsRequired();

            modelBuilder.Entity<Pianta>().HasOne(p => p.Fiore).WithOne(p => p.Pianta).IsRequired(false);
            modelBuilder.Entity<Pianta>().HasOne(p => p.Frutta).WithOne(p => p.Pianta).IsRequired(false);
            modelBuilder.Entity<Pianta>().HasOne(p => p.Giardino).WithOne(p => p.Pianta).IsRequired(false);

        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){ }
         non serve il metodo, faccio funzionare tutto con la Dependecy Injection nella classe Startup*/

        public DbSet<MVC_EntityFramework.Models.Fiore> Fiore { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){ }
         non serve il metodo, faccio funzionare tutto con la Dependecy Injection nella classe Startup*/

        public DbSet<MVC_EntityFramework.Models.Giardino> Giardino { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){ }
         non serve il metodo, faccio funzionare tutto con la Dependecy Injection nella classe Startup*/

        public DbSet<MVC_EntityFramework.Models.Frutta> Frutta { get; set; }
    }
}
