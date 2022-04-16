namespace BookStore.Model.DTOs
{
    public class OperationDTO
    {
        public string State { get; set; }
        public string Message { get; set; }
        public string Response { get; set; }
        public OperationDTO(string state, string message, string response)
        {
            State = state;
            Message = message;
            Response = response;
        }
    }
}
