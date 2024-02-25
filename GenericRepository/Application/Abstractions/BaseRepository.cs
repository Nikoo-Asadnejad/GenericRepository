using GenericRepository.Application.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository.Application.Abstractions;

public abstract class BaseRepository
{
    private readonly IUnitOfWork _unitOfWork;
    protected BaseRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}