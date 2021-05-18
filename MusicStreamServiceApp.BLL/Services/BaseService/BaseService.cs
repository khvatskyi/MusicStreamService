using AutoMapper;
using MusicStreamServiceApp.DAL.Interfaces;

namespace MusicStreamServiceApp.BLL.Services
{
    public abstract class BaseService
    {
        protected IMapper Mapper { get; }
        protected IUnitOfWork UnitOfWork { get; }

        protected BaseService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
    }
}
