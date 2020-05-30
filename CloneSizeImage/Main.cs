using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloneSizeImage {
    public partial class Main : Form {
        public string path;
        FileInfo file;
        List<ImagePath> clone = new List<ImagePath>();
        List<img> mass = new List<img>();
        ImagePath temp;
        Panel temp_panel;
        PictureBox temp_picbox;
        FileStream fs;
        public Main() {
            InitializeComponent();
        }
        private void выбратьПапкуToolStripMenuItem_Click(object sender, EventArgs e) {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                path = folderBrowserDialog1.SelectedPath;
                status.Text = "Выбрана папка: " + folderBrowserDialog1.SelectedPath;
                начатьПоискToolStripMenuItem.Enabled = true;
                clone.Clear();
                mass.Clear();
            }
        }
        private void начатьПоискToolStripMenuItem_Click(object sender, EventArgs e) {
            status.Text = "Инициализация...";

            foreach (string p in Directory.GetFiles(path))
            if(Path.GetExtension(p).ToLower() == ".png" || Path.GetExtension(p).ToLower() == ".jpg" || Path.GetExtension(p).ToLower() == ".gif" || Path.GetExtension(p).ToLower() == ".bmp") {
                file = new FileInfo(p);
                mass.Add(new img(p, file.Length));
            }

            //mass.Sort((p1, p2) => p1.size.CompareTo(p2.size));

            for (int i = 0; i < mass.Count; i++)
               if(!mass[i].use)
                clone.Add(generate_paths(i));

            foreach (ImagePath ip in clone.ToArray())
                if (ip.Count <= 1) clone.Remove(ip);

            status.Text = "Отрисовка копий...";

            foreach (ImagePath ip in clone)
                paint_clone(ip);

            status.Text = "Удалите не нужные копии двойным щелчком мыши.";
        }
        public void paint_clone(ImagePath ip) {
            temp_panel = new Panel();
            temp_panel.AutoScroll = true;
            temp_panel.Dock = DockStyle.Top;
            temp_panel.Height = 50;
            temp_panel.BorderStyle = BorderStyle.FixedSingle;
            body.Controls.Add(temp_panel);
            foreach (img i in ip.get_array) {
                temp_picbox = new PictureBox();
                temp_picbox.Tag = i.path;
                try {
                    fs = new FileStream(i.path, FileMode.Open);
                    temp_picbox.Image = Image.FromStream(fs);
                    fs.Close();
                }
                catch (ArgumentException e) {
                    temp_picbox.Image = Properties.Resources.errorfile;
                }
                temp_picbox.SizeMode = PictureBoxSizeMode.Zoom;
                temp_picbox.Width = 50;
                temp_picbox.Dock = DockStyle.Left;
                temp_picbox.DoubleClick += new EventHandler(pic_Click);
                temp_panel.Controls.Add(temp_picbox);
            }
        }
        public ImagePath generate_paths(int id) {
            temp = new ImagePath();
            for (int i = 0; i < mass.Count; i++)
                if (i == id && mass[id].use) temp.add_p(mass[id]);
                else if (mass[id].size == mass[i].size && !mass[i].use) {
                    mass[i].use = true;
                    temp.add_p(mass[i]);
                }
            return temp;
        }
        private void pic_Click(object sender, EventArgs e) {
            File.Delete(((PictureBox)sender).Tag.ToString());
            ((PictureBox)sender).Visible = false;
        }
    }
}
