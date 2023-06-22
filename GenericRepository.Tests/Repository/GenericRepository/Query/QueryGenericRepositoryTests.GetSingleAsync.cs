using GenericRepository.Data;
using GenericRepository.Interfaces.Repository;
using GenericRepository.Models;
using GenericRepository.Repository;
using GenericRepository.Tests.MoqData;
using Moq;

namespace GenericRepository.Tests.Repository.GenericRepository.Query;

public partial class QueryGenericRepositoryTests
{
    private readonly Mock<QueryContext> _queryContextMoq;
    private readonly IQueryGenericRepository<SampleModel> _queryGenericRepository;
    public QueryGenericRepositoryTests()
    {
        _queryContextMoq = new ();
        _queryGenericRepository = new QueryGenericRepository<SampleModel>(_queryContextMoq.Object);
    }

    [Fact]
    public void GetSingleAsync_WhenCalled_ReturnsModel()
    {
        
    }

    
}