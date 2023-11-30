namespace WebAPI.Models
{
    public class TaskDTO
    {
        public Guid Id { get; set; }
        public string? Nama { get; set; }
        public string Tugas { get; set; }
        public string? Deskripsi { get; set; }
        public DateTime TanggalDeadline { get; set; }
    }
}
