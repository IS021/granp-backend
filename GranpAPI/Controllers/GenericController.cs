using Granp.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Granp.Controllers
{
    public abstract class GenericController<T> : ControllerBase where T : class
    {
        protected readonly ILogger<T> _logger;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public GenericController(ILogger<T> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}