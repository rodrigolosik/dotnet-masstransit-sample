using MassTransit;
using MasstransitRabbitMqSample.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace MasstransitRabbitMqSample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    public readonly IPublishEndpoint _publishEndpoint;

    public NotificationController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    public async Task<IActionResult> Notify(NotificationDto notificationDto)
    {
        await _publishEndpoint.Publish<INotificationCreated>(new
        {
            NotificationDate = notificationDto.NotificationDate,
            NotificationMessage = notificationDto.NotificationMessage,
            NotificationType = notificationDto.NotificationType
        });

        return Ok();
    }

}
