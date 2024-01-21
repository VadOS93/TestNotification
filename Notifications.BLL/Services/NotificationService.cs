using Notifications.BLL.Interfaces;
using Notifications.DAL.Contexts;
using Notifications.Exceptions;
using Notifications.Models;

namespace Notifications.BLL.Services;

/// <summary>
/// Notification Service
/// </summary>
internal sealed class NotificationService : INotificationService
{
	private readonly NotificationContext _context;

	public NotificationService(NotificationContext context)
	{
		_context = context;
	}

	/// <inheritdoc/>
	public async Task<Notification> AddNotification(Notification notification)
	{
		_context.Notifications.Add(notification);
		await _context.SaveChangesAsync();
		return notification;
	}

	/// <inheritdoc/>
	public async Task<Notification> Get(Guid id)
	{
		var result = _context.Notifications.Where(x => x.Id == id).SingleOrDefault();

		if (result is null)
			throw new NotificationNotFoundException();

		return result;
	}

	/// <inheritdoc/>
	public async Task<IEnumerable<Notification>> Get()
	{
		var result = _context.Notifications.ToArray();
		return result;
	}

	/// <inheritdoc/>
	public async Task<Notification> Edit(Notification notification)
	{
		_context.Notifications.Update(notification);
		await _context.SaveChangesAsync();
		return notification;
	}

	/// <inheritdoc/>
	public async Task Delete(Guid id)
	{
		var notificationToDelete = _context.Notifications.SingleOrDefault(x => x.Id == id);

		if (notificationToDelete is null)
			throw new NotificationNotFoundException();

		_context.Notifications.Remove(notificationToDelete);
		await _context.SaveChangesAsync();
	}

	/// <inheritdoc/>
	public async Task<Notification> Viewed(Guid id)
	{
		var notification = _context.Notifications.SingleOrDefault(x => x.Id == id);

		if (notification is null)
			throw new NotificationNotFoundException();

		notification.IsViewed = true;
		notification.ViewedDate = DateTime.UtcNow;
		var result = _context.Notifications.Update(notification);
		await _context.SaveChangesAsync();
		return result.Entity;
	}
}