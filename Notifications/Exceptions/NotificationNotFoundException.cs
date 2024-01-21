namespace Notifications.Exceptions;

/// <summary>
/// Occurs when notification is not found in database
/// </summary>
public class NotificationNotFoundException : Exception
{
	public NotificationNotFoundException() : base()
	{

	}
}