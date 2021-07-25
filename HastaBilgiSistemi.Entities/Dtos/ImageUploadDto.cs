using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Entities.Dtos
{
    public class ImageUploadDto
    {
        public string Name { get; set; }
        public string OldName { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
        public string FolderName { get; set; }
        public long Size { get; set; }
    }
}
