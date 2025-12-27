using LEA.WebApi.Domain.Models;
using LEA.WebApi.Service.Interfaces;
using LEA.WebApi.Service.Services;
using LEA.WebApi.Service.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LEA.WebApi.Web.Controllers
{
    [ApiController]
    [Route("api/prediction")]
    public class PredictionController : ControllerBase
    {
        private readonly IPredictionService _service;

        public PredictionController(IPredictionService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Predict([FromBody] PredictionRequestViewModel request)
        {
            var result = _service.Predict(request);
            return Ok(result);
        }
    }
}
