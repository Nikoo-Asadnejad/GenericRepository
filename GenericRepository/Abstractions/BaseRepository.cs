using GenericRepository.Abstractions.Interfaces.UnitOfWork;

namespace GenericRepository.Abstractions;

public abstract class BaseRepository
{
    private readonly IUnitOfwork _unitOfwork;

    public BaseRepository(IUnitOfwork unitOfwork)
    {
        _unitOfwork = unitOfwork;
    }
}