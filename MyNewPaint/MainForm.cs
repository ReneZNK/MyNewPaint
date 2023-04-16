using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyNewPaint
{
    public partial class MainForm : Form
    {
        
        public MainForm()
        {
            
            InitializeComponent();
            CurrentColor = Color.Black;
            CurrentTool = Tools.Pen;
            trackBar1.Visible = false;
        }
        

        /// <summary>
        /// Текущий цвет.
        /// </summary>
        public static Color CurrentColor { get; set; } 

        public static Tools CurrentTool { get; set; }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new DocumentForm();

            f.MdiParent = this;

            f.Show();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new AboutBoxForm();
            f.ShowDialog();
        }

        private void каскадомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
            


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.Red;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.Green;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.Blue;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            var d = new ColorDialog();
            if (d.ShowDialog() == DialogResult.OK)
                CurrentColor = d.Color;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Pen;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Circle;
        }
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Line;
        }
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Eraser;
            
        }
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            ToggleVisibility();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
            SaveAs();
        }

        public void SaveAs()
        {
            
            var d = ActiveMdiChild as DocumentForm;
            saveFileDialog1.Filter = "JPG(*.JPG)|*.jpg";
            if (d != null)
            {
                
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    d.SaveAs(saveFileDialog1.FileName);
                }

            }
            
        }
        public void EventCancel(FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
        
        public void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var d = ActiveMdiChild as DocumentForm;
            var result = MessageBox.Show("Сохранить перед выходом?", "Внимание",
                          MessageBoxButtons.YesNoCancel,
                          MessageBoxIcon.Question);
            switch(result)
            {
                case DialogResult.Yes:
                    if(d != null)
                    {
                        SaveAs();
                    }
                    else
                    {
                        MessageBox.Show("Сохранять нечего");
                    }
                    break;
                case DialogResult.Cancel:
            
                    e.Cancel = true;    
                    break;
            }
            
        }

        public void SetVisibility(bool vissible)
        {
            trackBar1.Visible = vissible;
        }
        public void ToggleVisibility()
        {
            SetVisibility(!trackBar1.Visible);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
        }

        

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            var selected = ActiveMdiChild as DocumentForm;
            selected.pen.Width = trackBar1.Value;
            


        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = ActiveMdiChild as DocumentForm;
            if(selected == null)
            {
                сохранитьКакToolStripMenuItem.Enabled =false;
            }
            else
            {
                сохранитьКакToolStripMenuItem.Enabled = true;
            }
        }
    }
}
