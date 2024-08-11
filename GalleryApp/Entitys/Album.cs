using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalleryApp.Entitys
{
    public class Album
    {
        public string Name { get; set; } = string.Empty;
        public List<MyImage> Images { get; set; } = new List<MyImage>();
    }
}
