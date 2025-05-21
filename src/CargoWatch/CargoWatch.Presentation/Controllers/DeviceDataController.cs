using CargoWatch.Application.CQRS.AddDataSet;
using CargoWatch.Application.CQRS.DownloadDeviceConfiguration;
using CargoWatch.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CargoWatch.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceDataController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeviceDataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PostData([FromBody] DataSetModel data)
        {
            if (data == null || data.SenderId == Guid.Empty)
                return BadRequest("Invalid payload");

            var command = new AddDataSetCommand(data);
            var resultId = await _mediator.Send(command);

            return Ok(new { id = resultId });
        }

        [HttpGet("{id}/download")]
        public async Task<IActionResult> DownloadConfiguration(Guid id)
        {
            var json = await _mediator.Send(new DownloadDeviceConfigurationCommand(id));
            var fileName = $"device_{id}.txt";
            var bytes = System.Text.Encoding.UTF8.GetBytes(json);

            return File(bytes, "text/plain", fileName);
        }
    }
}
