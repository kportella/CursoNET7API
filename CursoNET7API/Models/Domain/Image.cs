using System.ComponentModel.DataAnnotations.Schema;

namespace CursoNET7API.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string FileName { get; set; }
        public string? FIleDescription { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string Path { get; set; }
    }
}
