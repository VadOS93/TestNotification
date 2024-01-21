using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Notifications.BLL.Interfaces;
using Notifications.Exceptions;
using Notifications.Models;

using TestNotification.ViewData;

namespace TestNotification.Controllers;

/// <summary>
/// Basic actions with notifications
/// </summary>
[Route("api/notifications")]
[ApiController]
[Authorize]
public class NotificationsController : ControllerBase
{
	private readonly INotificationService _notificationService;

	public NotificationsController(INotificationService notificationService)
	{
		_notificationService = notificationService;
	}

	/// <summary>
	/// Adding notification
	/// </summary>
	/// <returns></returns>
	[HttpPost]
	[Route("")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Post([FromBody] Notification notification)
	{
		var result = await _notificationService.AddNotification(notification);
		return Ok(result);
	}

	/// <summary>
	/// Editing notification
	/// </summary>
	/// <returns></returns>
	[HttpPut]
	[Route("")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Edit([FromBody] Notification notification)
	{
		var result = await _notificationService.Edit(notification);
		return Ok(result);
	}

	/// <summary>
	/// Get notification by id
	/// </summary>
	/// <param name="nid"></param>
	/// <returns></returns>
	[HttpGet]
	[Route("{nid}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Get([FromRoute] Guid nid)
	{
		try
		{
			var result = await _notificationService.Get(nid);
			return Ok(result);
		}
		catch (NotificationNotFoundException)
		{
			return NotFound();
		}

	}

	/// <summary>
	/// Get notifications 
	/// </summary>
	/// <returns></returns>
	[HttpGet]
	[Route("")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Get()
	{
		var result = await _notificationService.Get();
		return Ok(result);
	}

	/// <summary>
	/// Delete notification
	/// </summary>
	/// <param name="nid"></param>
	/// <returns></returns>
	[HttpDelete]
	[Route("{nid}")]
	[ProducesResponseType(StatusCodes.Status202Accepted)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Delete([FromRoute] Guid nid)
	{
		try
		{
			await _notificationService.Delete(nid);
			return Accepted();
		}
		catch (NotificationNotFoundException)
		{
			return NotFound();
		}
	}

	/// <summary>
	/// Notification is viewed by user
	/// </summary>
	/// <param name="nid"></param>
	/// <returns></returns>
	[HttpPatch]
	[Route("{nid}")]
	[ProducesResponseType(StatusCodes.Status202Accepted)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> IsViewed([FromRoute] Guid nid)
	{
		try
		{
			var viewedNotification = await _notificationService.Viewed(nid);
			var viewed = new NotificationViewedModel(viewedNotification);
			return Accepted(viewed);
		}
		catch (NotificationNotFoundException)
		{
			return NotFound();
		}
	}
}