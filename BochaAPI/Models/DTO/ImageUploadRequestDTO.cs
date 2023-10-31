﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BochaAPI.Models.DTO
{
    public class ImageUploadRequestDTO
    {
        [Required]
        public IFormFile File { get; set; } //Acceptar el archivo fisico que se va a subir
        public string FileName { get; set; }
        public string? FileDescricion { get; set; }
        //public string FileExtension { get; set; }
        //public long FileSizeInBytes { get; set; }
        //public string FilePath { get; set; }
    }
}
