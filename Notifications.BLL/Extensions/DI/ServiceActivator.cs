using Microsoft.Extensions.DependencyInjection;

using Notifications.BLL.Interfaces;
using Notifications.BLL.Services;

namespace Notifications.BLL.Extensions.DI;

/// <summary>
/// Service activator
/// </summary>
public static class ServiceActivator
{
	public static IServiceCollection ActivateBasicServices(this IServiceCollection services)
	{
		services.AddScoped<INotificationService, NotificationService>();
		return services;
	}
}