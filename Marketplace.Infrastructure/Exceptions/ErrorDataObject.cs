namespace Marketplace.Infrastructure.Exceptions
{
    public class ErrorDataObject
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public ErrorDataObject(string description, string type)
        {
            Description = description;
            Type = type;
        }
    }
}
