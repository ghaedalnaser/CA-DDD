using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

    public abstract class ApiController : ControllerBase
    {
        protected readonly ISender Sender;
        protected ApiController(ISender sender) { 
            Sender = sender;
        }
    }

