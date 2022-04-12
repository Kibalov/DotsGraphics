using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PPRzad8
{
    public partial class Form1 : Form
    {
        double[,,] dots;
        public Form1()
        {
            InitializeComponent();
            ReadFromTXTData();
        }

        private void ReadFromTXTData()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult = openFileDialog.ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                dots = new double[2, 2, 100];
                StreamReader streamReader = new StreamReader(openFileDialog.FileName);
                for (int i = 0; i < 100; i++)
                {
                    string[] nums = streamReader.ReadLine().Split('\t');
                    dots[0, 0, i] = Convert.ToDouble(nums[0]);
                    dots[0, 1, i] = Convert.ToDouble(nums[1]);
                    dots[1, 0, i] = Convert.ToDouble(nums[2]);
                    dots[1, 1, i] = Convert.ToDouble(nums[3]);
                }
                streamReader.Close();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //if (dots != null)
            //{
            //    Brush brushOrange = new SolidBrush(Color.Orange);
            //    Brush brushBlue = new SolidBrush(Color.Blue);
            //    for (int i = 0; i < 100; i++)
            //    {
            //        e.Graphics.FillEllipse(brushBlue, (int)dots[0, 0, i] * Width/45, (int)dots[0, 1, i] * Height / 60, 5, 5);
            //        e.Graphics.FillEllipse(brushOrange, (int)dots[1, 0, i] * Width / 45, (int)dots[1, 1, i] * Height / 60, 5, 5);
            //    }
            //}
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                chart1.Series[0].Points.AddXY(dots[0, 0, i], dots[0, 1, i]);
                chart1.Series[1].Points.AddXY(dots[1, 0, i], dots[1, 1, i]);

            }
            Graphics graphics = chart1.CreateGraphics();
            MessageBox.Show(chart1.Series[0].Points.FindMaxByValue("X", 0).XValue.ToString() + " " +
                chart1.Series[0].Points.FindMinByValue("X", 0).XValue.ToString() + " " +
                chart1.Series[0].Points.FindMaxByValue("Y", 0).YValues[0].ToString() + " " +
                chart1.Series[0].Points.FindMinByValue("Y", 0).YValues[0].ToString() + " \n" +
                chart1.Series[1].Points.FindMaxByValue("X", 0).XValue.ToString() + " " +
                chart1.Series[1].Points.FindMinByValue("X", 0).XValue.ToString() + " " +
                chart1.Series[1].Points.FindMaxByValue("Y", 0).YValues[0].ToString() + " " +
                chart1.Series[1].Points.FindMinByValue("Y", 0).YValues[0].ToString());

            chart1.Series[2].Points.AddXY(chart1.Series[1].Points.FindMaxByValue("X", 0).XValue,
                chart1.Series[1].Points.FindMaxByValue("X", 0).YValues[0]);
            chart1.Series[2].Points.AddXY(chart1.Series[1].Points.FindMinByValue("Y", 0).XValue,
                chart1.Series[1].Points.FindMinByValue("Y", 0).YValues[0]);
            chart1.Series[2].Points.AddXY(chart1.Series[1].Points.FindMinByValue("X", 0).XValue,
                chart1.Series[1].Points.FindMinByValue("X", 0).YValues[0]);
            chart1.Series[2].Points.AddXY(chart1.Series[1].Points.FindMaxByValue("Y", 0).XValue,
                chart1.Series[1].Points.FindMaxByValue("Y", 0).YValues[0]);

            chart1.Series[3].Points.AddXY(chart1.Series[0].Points.FindMaxByValue("X", 0).XValue,
                chart1.Series[0].Points.FindMaxByValue("X", 0).YValues[0]);
            chart1.Series[3].Points.AddXY(chart1.Series[0].Points.FindMinByValue("Y", 0).XValue,
                chart1.Series[0].Points.FindMinByValue("Y", 0).YValues[0]);
            chart1.Series[3].Points.AddXY(chart1.Series[0].Points.FindMinByValue("X", 0).XValue,
                chart1.Series[0].Points.FindMinByValue("X", 0).YValues[0]);
            chart1.Series[3].Points.AddXY(chart1.Series[0].Points.FindMaxByValue("Y", 0).XValue,
                chart1.Series[0].Points.FindMaxByValue("Y", 0).YValues[0]);
        }

        private void chart1_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.DrawRectangle(new Pen(Color.Blue, 2), 475, 49, 175, 63);
            //e.Graphics.DrawEllipse(new Pen(Color.Orange, 2), 73, 74, 410, 183);
        }

        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            MessageBox.Show(e.X + "  " + e.Y);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            double x = Convert.ToDouble(textBox1.Text);
            double y = Convert.ToDouble(textBox2.Text);
            //chart1.Series[1].Points.FindMaxByValue("X", 0).XValue.ToString() + " " +
            //chart1.Series[1].Points.FindMinByValue("X", 0).XValue.ToString() + " " +
            //chart1.Series[1].Points.FindMaxByValue("Y", 0).YValues[0].ToString() + " " +
            //chart1.Series[1].Points.FindMinByValue("Y", 0).YValues[0].ToString());


            bool obl1 = false;
            double xmax = chart1.Series[0].Points.FindMaxByValue("X", 0).XValue;
            double xmin = chart1.Series[0].Points.FindMinByValue("X", 0).XValue;
            double ymax = chart1.Series[0].Points.FindMaxByValue("Y", 0).YValues[0];
            double ymin = chart1.Series[0].Points.FindMinByValue("Y", 0).YValues[0];
            if (x > xmin && x < xmax && y < ymax && y > ymin)
            {
                obl1 = true;
            }


            chart1.Series[4].Points.AddXY(x, y);
            double a = Math.Abs(chart1.Series[1].Points.FindMaxByValue("X", 0).XValue - chart1.Series[1].Points.FindMinByValue("X", 0).XValue);
            double b = Math.Abs(chart1.Series[1].Points.FindMaxByValue("Y", 0).YValues[0] - chart1.Series[1].Points.FindMinByValue("Y", 0).YValues[0]);
            double x0 = (chart1.Series[1].Points.FindMaxByValue("X", 0).XValue + chart1.Series[1].Points.FindMinByValue("X", 0).XValue) / 2;
            double y0 = (chart1.Series[1].Points.FindMaxByValue("Y", 0).YValues[0] + chart1.Series[1].Points.FindMinByValue("Y", 0).YValues[0]) / 2;

            chart1.Series[5].Points.AddXY(x0, y0);
            x -= x0;
            y -= y0;
            //MessageBox.Show(x+" "+y+" "+a+" "+b+" "+x0+" "+y0);

            bool obl2 = false;
            if ((Math.Pow(x,2)/Math.Pow(a/2,2) + Math.Pow(y, 2) / Math.Pow(b/2, 2)) <= 1)
                obl2 = true;

            if (obl1 && obl2)
                MessageBox.Show("Точка попадает в обе области");
            else if (obl1)
                MessageBox.Show("Точка попадает в первую область");
            else if (obl2)
                MessageBox.Show("Точка попадает во вторую область");
            else
                MessageBox.Show("Точка не попадает ни в какую область");
        }
    }
}
