namespace RealEstate.API.Error
{
    public class APIValidationErrorResponse : APIResponse
    {
        public APIValidationErrorResponse()
            : base(400)
        {
            Errors = new List<string>();
        }

        public IEnumerable<string> Errors { get; set; }


    }
}
