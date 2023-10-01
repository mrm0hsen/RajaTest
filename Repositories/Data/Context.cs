using Microsoft.EntityFrameworkCore;
using Repositories.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Data
{
    public class Context : DbContext
    {

        public Context(DbContextOptions<Context> opt) :base(opt)
        {

        }

        public DbSet<Certificate> Certificates { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<CertificateType> CertificateTypes { get; set; }

        public DbSet<UserCertificate> UserCertificate { get; set; }
    }
}
