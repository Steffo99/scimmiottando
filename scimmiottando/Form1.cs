using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scimmiottando
{
    public partial class Form1 : Form
    {
        Label[][] squares = new Label[5][];
        int[] numbers = new int[5];
        int step = 0;

        public Form1()
        {
            InitializeComponent();
        }

        public void Reset()
        {
            numbers = new int[5];
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    squares[x][y].Text = " ";
                    squares[x][y].Tag = " ";
                }
            }
            step = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Initialize the grid
            for (int x = 0; x < 5; x++)
            {
                squares[x] = new Label[5];
                for (int y = 0; y < 5; y++)
                {
                    Label l = new Label();
                    squares[x][y] = l;
                    //Init the label
                    l.AutoSize = false;
                    l.Font = new System.Drawing.Font("Arial Black", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    l.ForeColor = System.Drawing.Color.White;
                    l.Location = new System.Drawing.Point(13 + x * 53, 13 + y * 53);
                    l.MaximumSize = new System.Drawing.Size(40, 40);
                    l.MinimumSize = new System.Drawing.Size(40, 40);
                    l.Name = "labelx" + x.ToString() + "y" + y.ToString();
                    l.Size = new System.Drawing.Size(40, 40);
                    l.Text = " ";
                    l.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    l.Click += square_Click;
                    Controls.Add(l);
                }
            }
            ResumeLayout();
            PerformLayout();
        }

        private void pictureBoxStart_Click(object sender, EventArgs e)
        {
            Reset();
            //Generate random numbers
            Random generator = new Random();
            for (int c = 0; c < 5; c++)
            {
                bool successful = false;
                while (!successful)
                {
                    int x = generator.Next(5);
                    int y = generator.Next(5);
                    numbers[c] = -1;
                    bool validnumber = false;
                    while (!validnumber)
                    {
                        int n = generator.Next(10);
                        validnumber = !numbers.Contains(n);
                        numbers[c] = n;
                    }
                    if(squares[x][y].Text == " ")
                    {
                        squares[x][y].Text = numbers[c].ToString();
                        squares[x][y].Tag = numbers[c].ToString();
                        successful = true;
                    }
                }
            }
            //Sort the numbers array
            Array.Sort<int>(numbers);
            //Start the timer
            timerShow.Start();
        }

        private void timerShow_Tick(object sender, EventArgs e)
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if(squares[x][y].Text != " ")
                    {
                        squares[x][y].Text = "█";
                    }
                }
            }
            timerShow.Stop();
        }

        private void square_Click(object sender, EventArgs e)
        {
            Label square = sender as Label;
            if(square.Tag.ToString() == numbers[step].ToString())
            {
                square.Text = " ";
                step += 1;
            }
            else if(square.Text != " ")
            {
                MessageBox.Show("Hai sbagliato!");
                Reset();
            }
            if(step >= 5)
            {
                MessageBox.Show("Hai vinto!");
                Reset();
            }
        }
    }
}
