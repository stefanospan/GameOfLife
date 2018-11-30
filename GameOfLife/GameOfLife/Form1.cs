using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Form1 : Form
    {

        int blockWidth = 20;
        int blockHeight = 20;
        int blocksVer = 10;
        int blocksHor = 10;
        int blocksSpace = 2;
        int[,] blocks;
        Random rnd = new Random();
        int started = 0;

        public Form1()
        {
            InitializeComponent();
            blocks = new int[blocksVer, blocksHor];
            for(int y = 0; y < blocksVer; y++)
            {
                for(int x = 0; x < blocksHor; x++)
                {
                    int z = rnd.Next(0, 3);
                    if(z == 1)
                    {
                        blocks[y, x] = 1;
                    } else
                    {
                        blocks[y, x] = 0;
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void UpdateLife()
        {
            for(int y = 0; y < blocksVer; y++)
            {
                for(int x = 0; x < blocksHor; x++)
                {
                    if(x == 0 || x == blocksHor - 1 || y == 0 || y == blocksVer - 1)
                    {
                        blocks[y, x] = 0;
                    }
                    else
                    {
                        CheckNeighbors(y, x);
                    }
                }
            }
        }

        private void CheckNeighbors(int y, int x)
        {
            if(CountNeighbors(y, x) < 2 || CountNeighbors(y, x) > 3)
            {
                blocks[y, x] = 0;
            } else if(blocks[y, x] == 0 && CountNeighbors(y, x) == 3)
            {
                blocks[y, x] = 1;
            }
        }

        public int CountNeighbors(int y, int x)
        {
            int count = 0;
            if (blocks[y - 1, x - 1] == 1)
            {
                count++;
            }
            if (blocks[y - 1, x] == 1)
            {
                count++;
            }
            if (blocks[y - 1, x + 1] == 1)
            {
                count++;
            }
            if (blocks[y, x - 1] == 1)
            {
                count++;
            }
            if (blocks[y, x + 1] == 1)
            {
                count++;
            }
            if (blocks[y + 1, x - 1] == 1)
            {
                count++;
            }
            if (blocks[y + 1, x] == 1)
            {
                count++;
            }
            if (blocks[y + 1, x + 1] == 1)
            {
                count++;
            }
            Console.WriteLine(count.ToString());
            return count;
        }

        private void Draw()
        {
            this.Controls.Clear();
            PictureBox[] pbs = new PictureBox[blocksHor * blocksVer];
            int tempIndex = 0;
            for (int y = 0; y < blocksVer; y++)
            {
                for (int x = 0; x < blocksHor; x++)
                {
                    PictureBox pb = new PictureBox();
                    pb.Width = blockWidth;
                    pb.Height = blockHeight;
                    if(blocks[y, x] == 1)
                    {
                        pb.BackColor = Color.Black;
                    } else
                    {
                        pb.BackColor = Color.White;
                    }
                    pb.Top = y * (blockHeight + blocksSpace);
                    pb.Left = x * (blockWidth + blocksSpace);
                    pbs[tempIndex] = pb;
                    tempIndex++;
                }
            }
            for(int i = 0; i < tempIndex; i++)
                this.Controls.Add(pbs[i]);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateLife();
            Draw();
        }
    }
}
