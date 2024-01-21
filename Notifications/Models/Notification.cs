namespace Notifications.Models;

/// <summary>
/// Notification
/// </summary>
public class Notification
{
	/// <summary>
	/// Notification Id
	/// </summary>
	public Guid Id { get; set; }

	/// <summary>
	/// Notification image url
	/// </summary>
	public string? Img { get; set; }

	/// <summary>
	/// User who receives notification
	/// </summary>
	public Guid OwnerId { get; set; }

	/// <summary>
	/// Name of user who sends notification
	/// </summary>
	public string From { get; set; }

	/// <summary>
	/// Subject of notification
	/// </summary>
	public string? Subject { get; set; }

	/// <summary>
	/// Notification message
	/// </summary>
	public string? Message { get; set; }

	/// <summary>
	/// Client used to send message
	/// </summary>
	public string? ClientId { get; set; }

	/// <summary>
	/// Is this notification viewed by Owner
	/// </summary>
	public bool IsViewed { get; set; }

	/// <summary>
	/// Date when notification is viewed
	/// </summary>
	public DateTime? ViewedDate { get; set; }
}