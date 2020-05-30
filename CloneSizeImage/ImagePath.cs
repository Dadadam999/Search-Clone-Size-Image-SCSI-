using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneSizeImage {
    public class ImagePath {
        List<img> paths = new List<img>();
        public void add_p(img add) { paths.Add(add); }
        public void remove_p(img remove) { paths.Remove(remove); }
        public int Count { get { return paths.Count; } }
        public img[] get_array { get { return paths.ToArray(); } }
    }
}
