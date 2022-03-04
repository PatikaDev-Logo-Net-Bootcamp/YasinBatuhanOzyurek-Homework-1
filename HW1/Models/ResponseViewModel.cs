namespace HW1.Models
{
    // ResponseViewModel for user
    // this way both validation and response classes can be used in login page
    public class ResponseViewModel
    {
        public bool Success { get; set; }
        public string Data { get; set; }
        public string Error { get; set; }
       
        public Login login { get; set; }
    }
}
