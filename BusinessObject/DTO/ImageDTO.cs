using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class ImageDTO
    {
        public Stream FileStream { get; set; }
        public string ImageType { get; set; }
        public string FileName { get; set; }
    }
}
