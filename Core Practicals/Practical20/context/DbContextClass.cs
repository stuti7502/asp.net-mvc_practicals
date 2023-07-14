﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Practical20.Interfaces;
using Practical20.Models;

namespace Practical20.context
{
    public class DbContextClass : DbContext
    {
        private string _username;
        public DbContextClass(DbContextOptions<DbContextClass> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            var claimsPrincipal = httpContextAccessor.HttpContext?.User;
            _username = claimsPrincipal?.Claims?.SingleOrDefault(c => c.Type == "username")?.Value ?? "Unauthenticated user";
        }
        //public DbContextClass(DbContextOptions<DbContextClass> options) : base(options) 
        //{

        //}
        public DbSet<Student> Students { get; set; }
        public DbSet<AuditEntry> AuditEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditEntry>().Property(ae => ae.Changes).HasConversion(
                value => JsonConvert.SerializeObject(value),
                serializedValue => JsonConvert.DeserializeObject<Dictionary<string, object>>(serializedValue));
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            // Get audit entries
            var auditEntries = OnBeforeSaveChanges();

            // Save current entity
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            // Save audit entries
            await OnAfterSaveChangesAsync(auditEntries);
            return result;
        }

        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var entries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                // Dot not audit entities that are not tracked, not changed, or not of type IAuditable
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged || !(entry.Entity is IAuditable))
                    continue;

                var auditEntry = new AuditEntry()
                {
                    ActionType = entry.State == EntityState.Added ? "INSERT" : entry.State == EntityState.Deleted ? "DELETE" : "UPDATE",
                    EntityId = entry.Properties.Single(p => p.Metadata.IsPrimaryKey()).CurrentValue.ToString(),
                    EntityName = entry.Metadata.ClrType.Name,
                    Username = _username,
                    TimeStamp = DateTime.UtcNow,
                    Changes = entry.Properties.Select(p => new { p.Metadata.Name, p.CurrentValue }).ToDictionary(i => i.Name, i => i.CurrentValue),

                    // TempProperties are properties that are only generated on save, e.g. ID's
                    // These properties will be set correctly after the audited entity has been saved
                    TempProperties = entry.Properties.Where(p => p.IsTemporary).ToList(),
                };

                entries.Add(auditEntry);
            }

            return entries;
        }

        private Task OnAfterSaveChangesAsync(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            // For each temporary property in each audit entry - update the value in the audit entry to the actual (generated) value
            foreach (var entry in auditEntries)
            {
                foreach (var prop in entry.TempProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        entry.EntityId = prop.CurrentValue.ToString();
                        entry.Changes[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        entry.Changes[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }
            }

            AuditEntries.AddRange(auditEntries);
            return SaveChangesAsync();
        }

    }
}
