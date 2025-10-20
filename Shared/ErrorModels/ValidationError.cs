namespace Shared.ErrorModels
{
    public class ValidationError
    {
        public string Failed { get; set; } = null!;

        public IEnumerable<string> Errors { get; set; } = [];
    }
}