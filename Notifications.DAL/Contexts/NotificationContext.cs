using Microsoft.EntityFrameworkCore;

using Notifications.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.DAL.Contexts;

/// <summary>
/// Context
/// </summary>
public class NotificationContext : DbContext
{
	public DbSet<Notification> Notifications { get; set; }
	public NotificationContext()
	{
		Database.EnsureCreated();
	}
	public NotificationContext(DbContextOptions<NotificationContext> options) : base(options)
	{
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Notification>(entity =>
		{
			entity.HasKey(x => x.Id);
		});
	}
}