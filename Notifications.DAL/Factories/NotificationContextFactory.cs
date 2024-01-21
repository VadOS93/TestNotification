using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Notifications.DAL.Contexts;

namespace Notifications.DAL.Factories;

/// <summary>
/// Notification Context Factory
/// </summary>
internal class NotificationContextFactory : IDesignTimeDbContextFactory<NotificationContext>
{
	/// <summary>
	/// Connection to my database
	/// </summary>
	private const string connectionString = "Data Source=DESKTOP-GKRRVMA;Initial Catalog = NotificationsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
	public NotificationContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<NotificationContext>();
		optionsBuilder.UseSqlServer(connectionString, x => x.MigrationsHistoryTable("__NotificationMigrationsHistory"));

		var context = Activator.CreateInstance(typeof(NotificationContext), new object[] { optionsBuilder.Options });

		return context as NotificationContext;
	}
}