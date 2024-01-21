using Notifications.Models;

namespace TestNotification.ViewData;

/// <summary>
/// Model showing that notification is viewed
/// </summary>
public class NotificationViewedModel
{
	/// <summary>
	/// Notification Id
	/// </summary>
	public Guid Id { get; set; }

	/// <summary>
	/// Is this notification viewed by Owner
	/// </summary>
	public bool IsViewed { get; set; }

	/// <summary>
	/// Date when notification is viewed
	/// </summary>
	public DateTime? ViewedDate { get; set; }

    public NotificationViewedModel(Notification notification)
    {
		Id = notification.Id;
		IsViewed = notification.IsViewed;
		ViewedDate = notification.ViewedDate;
    }
}