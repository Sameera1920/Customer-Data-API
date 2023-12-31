﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CustomerDataAPI.Data;
using CustomerDataAPI.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CustomerDataAPI.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public new DbSet<User> Users {get; set;}
    public DbSet<Address> Addresses {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>()
            .Property(e => e.Tags)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null!),
                v => System.Text.Json.JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null!),
                new ValueComparer<ICollection<string>>(
                     (c1, c2) => c1!.SequenceEqual(c2!),
                     c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                     c => (ICollection<string>)c.ToList()));
    }
}


    