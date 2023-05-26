using GenericRepository.Models;

namespace GenericRepository.Tests.MoqData;

public class SampleModel : BaseModel
{
    public string Title { get; set; }
    public  string Description { get; set; }

    public void FillSampleData()
    {
        this.Title = "Sample title";
        this.Description = "Sample description";
    }
}