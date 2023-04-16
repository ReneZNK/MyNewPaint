using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyNewPaint
{
    public partial class DocumentForm : Form
    {
        private int x, y;

        /// <summary>
        /// Битовая карта ....
        /// </summary>
        private Bitmap bmp;

        private Bitmap bmpTemp;
        Point p1 = new Point();
        Point p2 = new Point(); 
        public Pen pen = new Pen(MainForm.CurrentColor);
        Graphics g;

        public void SetSize()
        {
            
            bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
            pictureBox1.Image = bmp;
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            
        }

        public DocumentForm()
        {
            InitializeComponent();
            SetSize();
        }

        private void DocumentForm_ResizeEnd(object sender, EventArgs e)
        {
            SetSize();
        }

        public DocumentForm(Bitmap bmp)
        {
            InitializeComponent();
            this.bmp = bmp;
            
        }

        private void DocumentForm_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Left)
            {
                
                
                switch (MainForm.CurrentTool)
                {
                    case Tools.Pen:
                        g.DrawLine(pen, x,y, e.X, e.Y);
                        x= e.X;
                        y= e.Y;
                        break;
                    case Tools.Circle:
                        bmpTemp = (Bitmap)bmp.Clone();
                        g = Graphics.FromImage(bmpTemp);
                        g.DrawEllipse(pen, new Rectangle(x, y, e.X - x, e.Y-y));
                        pictureBox1.Image = bmpTemp;
                        break;
                    case Tools.Eraser:
                        pen.Color = Color.White;
                        g.DrawLine(pen, x, y, e.X, e.Y);
                        x = e.X;
                        y = e.Y;
                        break;
                    
                }

                
                pictureBox1.Invalidate();

            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            
            switch (MainForm.CurrentTool)
            {
                case Tools.Circle:
                    bmp = bmpTemp;
                    break;
                case Tools.Line:
                    p2 = e.Location;
                    Graphics g = Graphics.FromImage(bmp);
                    g.DrawLine(pen, p1, p2);

                    break;
            }
            pictureBox1.Invalidate();
        }

        private void DocumentForm_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
            if (e.Button == MouseButtons.Left)
            {
                p1 = e.Location;
            }
        }

        private void DocumentForm_Load(object sender, EventArgs e)
        {

        }

        public void SaveAs(string path)
        {
            bmp.Save(path);
        }

        private void DocumentForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(MousePosition, ToolStripDropDownDirection.Right);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPG(*.JPG)|*.jpg";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Save(saveFileDialog1.FileName);
                }
            }



        }

        
    }
}
