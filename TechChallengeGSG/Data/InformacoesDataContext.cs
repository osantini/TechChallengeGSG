using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechChallengeGSG.Models;

    public class InformacoesDataContext : DbContext
    {
        public InformacoesDataContext (DbContextOptions<InformacoesDataContext> options)
            : base(options)
        {
        }

        public DbSet<TechChallengeGSG.Models.InformacoesModel> InformacoesModel { get; set; } = default!;
    }
