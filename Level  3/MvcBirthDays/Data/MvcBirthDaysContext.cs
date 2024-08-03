using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcBirthDays.Models;

namespace MvcBirthDays.Data
{
    public class MvcBirthDaysContext : DbContext
    {
        public MvcBirthDaysContext(DbContextOptions<MvcBirthDaysContext> options)
            : base(options)
        {
        }

        public DbSet<MvcBirthDays.Models.Person> Person { get; set; } = default!;
        // public DbSet<MvcBirthDays.Models.FileModel> Files { get; set; } = default!;
    }
}
