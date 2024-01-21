using Notifications.Models;

namespace Notifications.BLL.Interfaces;

/// <summary>
/// Notification Service interface
/// </summary>
public interface INotificationService
{
	/// <summary>
	/// Add notification
	/// </summary>
	/// <param name="notification"></param>
	/// <returns></returns>
	Task<Notification> AddNotification(Notification notification);

	/// <summary>
	/// Delete notification by id
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	Task Delete(Guid id);

	/// <summary>
	/// Edit notification
	/// </summary>
	/// <param name="notification"></param>
	/// <returns></returns>
	Task<Notification> Edit(Notification notification);

	/// <summary>
	/// Get notification by id
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	Task<Notification> Get(Guid id);

	/// <summary>
	/// Get all notifications
	/// </summary>
	/// <returns></returns>
	Task<IEnumerable<Notification>> Get();

	/// <summary>
	/// Notification IsViewed becomes true
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	Task<Notification> Viewed(Guid id);
}