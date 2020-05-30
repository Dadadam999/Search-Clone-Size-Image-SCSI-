using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneSizeImage {
    public class img {
        public string path { get; set; }
        public long size { get; set; }
        public bool use { get; set; }
        public img() { use = false; }
        public img(string p, long s) {
            path = p;
            size = s;
            use = false;
        }
    }
}
