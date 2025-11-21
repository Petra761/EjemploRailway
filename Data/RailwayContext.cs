using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EjemploRailway.Entities;

    public class RailwayContext : DbContext
    {
        public RailwayContext (DbContextOptions<RailwayContext> options)
            : base(options)
        {
        }

        public DbSet<EjemploRailway.Entities.Persona> Persona { get; set; } = default!;
    }
