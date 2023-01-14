using Inform.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inform.DAL.DBContext
{
    public class InformDBContext : DbContext
    {
        public InformDBContext(DbContextOptions<InformDBContext> options)
         : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
