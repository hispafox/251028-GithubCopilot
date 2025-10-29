namespace TaskManagementApi.Models
{
    public class TodoTask
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public DateTime Deadline { get; set; }
    }
}