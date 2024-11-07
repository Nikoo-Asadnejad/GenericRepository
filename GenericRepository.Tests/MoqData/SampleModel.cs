using GenericRepository.Domain;

namespace GenericRepository.Tests.MoqData;

public class SampleEntity : Entity
{
    public string Title { get; set; }
    public  string Description { get; set; }

    public void FillSampleData()
    {
        this.Title = "Sample title";
        this.Description = "Sample description";
    }
}