using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProject_Group5
{
    public partial class Form1 : Form
    {
        int[,] Maze;
        //Ma trận biểu diễn mê cung
        int Lever;
        //Cấp độ
        int currentX;
        int currentY;
        //Tọa độ điểm đang đứng
        int endX;
        int endY;
        //Tọa độ điểm đích
        int monsterX;
        int monsterY;
        //Tọa độ của quái vật
        int NumberMoves = 0;
        //Số bước di chuyển của người chơi
        int NumberMovesMonster = 1;
        //Số bước di chuyển của quái vật
        int Time;
        //Đếm thời gian
        List<Tuple<int, int>> Trace;
        //Truy vết đường vừa đi của người chơi
        List<Tuple<int, int>> Path;
        //Đường về đích
        int p = 1;
        //Chế độ sương mù
        bool smoke = false;

        public Form1()
        {
            InitializeComponent();
            Lever = 1;
            load_Game(1);
        }
        private void load_Game(int Lever)
        {
            //--------------------------------
            Time = 0;
            timer1 = new Timer();
            timer1.Tick += timer1_Tick;
            timer1.Interval = 1000;
            //-------------------------------
            this.KeyPreview = true;
            //-------------------------------
            Maze = MazeLever(Lever);
            Trace = new List<Tuple<int, int>>();
            Path = new List<Tuple<int, int>>();
            NumberMoves = 0;
            NumberMovesMonster = 1;
            pictureBox1.Invalidate();
        }
        private int[,] MazeLever(int lever)
        {
            if (lever == 1)
            {
                smoke = false;
                Lever = 1;
                currentX = monsterX = 0;
                currentY = monsterY = 5;

                endX = 10;
                endY = 5;
                Maze = new[,]
                {
                    {1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1},
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                    {1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1},
                    {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1},
                    {1, 1, 0, 0, 1, 1, 0, 0, 0, 1, 1},
                    {1, 1, 0, 1, 1, 0, 0, 1, 0, 1, 1},
                    {1, 1, 0, 0, 0, 0, 1, 0, 0, 1, 1},
                    {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1},
                    {1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1},
                    {1, 0, 0, 1, 1, 0, 1, 1, 0, 0, 1},
                    {1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1}
                };
            }
            else if (lever == 2)
            {
                smoke = false;
                Time = 7;
                Lever = 2;
                currentX = monsterX = 1;
                currentY = monsterY = 0;

                endX = 13;
                endY = 14;
                Maze = new[,]
                {
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {2, 0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 0, 1, 0, 1},
                    {1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
                    {1, 0, 1, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1},
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1},
                    {1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 0, 0, 1},
                    {1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 1},
                    {1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1},
                    {1, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1},
                    {1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1},
                    {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                    {1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 0, 1},
                    {1, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1},
                    {1, 0, 1, 0, 0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 3},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
                };
            }
            else if (lever == 3)
            {
                smoke = true;
                Time = 25;
                Lever = 3;
                currentX = monsterX = 0;
                currentY = monsterY = 1;

                endX = 17;
                endY = 18;
                Maze = new[,]
                {
                  {1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                  {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                  {1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                  {1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                  {1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1},
                  {1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1},
                  {1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1},
                  {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1},
                  {1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1},
                  {1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1},
                  {1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1},
                  {1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1},
                  {1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                  {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1},
                  {1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1},
                  {1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1},
                  {1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1},
                  {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 3},
                  {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                };
            }
            else if (lever == 4)
            {
                smoke = false;
                Lever = 4;
                currentX = monsterX = 0;
                currentY = monsterY = 2;

                endX = 14;
                endY = 18;
                Maze = new[,]
                {
                    { 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
                    { 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, },
                    { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, },
                    { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, },
                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, },
                    { 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, },
                    { 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, },
                    { 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, },
                    { 1, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, },
                    { 1, 0, 0, 0, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, },
                    { 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 1, },
                    { 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, 1, },
                    { 1, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1, 0, 1, 0, 1, },
                    { 1, 1, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1, 1, 1, },
                    { 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 0, 3, },
                    { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 0, 1, },
                    { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, },
                    { 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, },
                    { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, },
                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, }
                };
            }
            else if (lever == 5)
            {
                smoke = true;
                Lever = 5;
                currentX = monsterX = 1;
                currentY = monsterY = 30;

                endX = 29;
                endY = 0;
                Maze = new[,]
                {
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 2},
                    {1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1},
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                    {1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1},
                    {1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1},
                    {1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1},
                    {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                    {1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1},
                    {1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1},
                    {1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1},
                    {1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1},
                    {1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1},
                    {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1},
                    {1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1},
                    {1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1},
                    {1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1},
                    {1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1},
                    {1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1},
                    {1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1},
                    {1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1},
                    {1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1},
                    {1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1},
                    {1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1},
                    {1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1},
                    {1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1},
                    {1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1},
                    {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1},
                    {1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1},
                    {3, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
                };
            }
            return Maze;
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int cellWidth = pictureBox1.Width / Maze.GetLength(1);
            int cellHeight = pictureBox1.Height / Maze.GetLength(0);
            for (int i = 0; i < Maze.GetLength(0); i++)
            {
                for (int j = 0; j < Maze.GetLength(1); j++)
                {
                    int x = j * cellWidth;
                    int y = i * cellHeight;
                    if (smoke == true)
                    {
                        bool isAdjacent = true;
                        if (Lever == 3)
                        {
                            isAdjacent = Math.Abs(currentX - i) <= 3 && Math.Abs(currentY - j) <= 3 && Math.Abs(currentX - i) + Math.Abs(currentY - j) > 0;
                        }
                        else
                        {
                            isAdjacent = Math.Abs(currentX - i) <= 5 && Math.Abs(currentY - j) <= 5 && Math.Abs(currentX - i) + Math.Abs(currentY - j) > 0;
                        }
                        if (isAdjacent || (currentX == i && currentY == j))
                        {
                            if (Maze[i, j] == 0)
                            {
                                g.FillRectangle(Brushes.White, x, y, cellWidth, cellHeight); // Tô màu trắng cho ô có thể di chuyển được
                            }
                            else if (Maze[i, j] == 2)
                            {
                                g.FillRectangle(Brushes.Blue, x, y, cellWidth, cellHeight); // Tô màu xanh cho ô đang di chuyển
                            }
                            else if (Maze[i, j] == 3)
                            {
                                g.FillRectangle(Brushes.Green, x, y, cellWidth, cellHeight); // Tô màu xanh lá cho điểm đến
                            }
                            else if (Maze[i, j] == 4)
                            {
                                g.FillRectangle(Brushes.Yellow, x, y, cellWidth, cellHeight); // Tô màu vàng cho các ô gợi ý
                            }
                            else if (Maze[i, j] == 5)
                            {
                                g.FillRectangle(Brushes.Red, x, y, cellWidth, cellHeight); // Tô màu xanh đỏ cho quái vật
                            }
                            else if (Maze[i, j] == 6)
                            {
                                g.FillRectangle(Brushes.Pink, x, y, cellWidth, cellHeight); // Tô màu xanh đỏ cho quái vật
                            }
                            else
                            {
                                g.FillRectangle(Brushes.Black, x, y, cellWidth, cellHeight); // Tô màu đen cho chướng ngại vật
                            }
                        }
                    }
                    else
                    {
                        if (Maze[i, j] == 0)
                        {
                            g.FillRectangle(Brushes.White, x, y, cellWidth, cellHeight); // Tô màu trắng cho ô có thể di chuyển được
                        }
                        else if (Maze[i, j] == 2)
                        {
                            g.FillRectangle(Brushes.Blue, x, y, cellWidth, cellHeight); // Tô màu xanh cho ô đang di chuyển
                        }
                        else if (Maze[i, j] == 3)
                        {
                            g.FillRectangle(Brushes.Green, x, y, cellWidth, cellHeight); // Tô màu xanh lá cho điểm đến
                        }
                        else if (Maze[i, j] == 4)
                        {
                            g.FillRectangle(Brushes.Yellow, x, y, cellWidth, cellHeight); // Tô màu vàng cho các ô gợi ý
                        }
                        else if (Maze[i, j] == 5)
                        {
                            g.FillRectangle(Brushes.Red, x, y, cellWidth, cellHeight); // Tô màu xanh đỏ cho quái vật
                        }
                        else if (Maze[i, j] == 6)
                        {
                            g.FillRectangle(Brushes.Pink, x, y, cellWidth, cellHeight); // Tô màu xanh đỏ cho quái vật
                        }
                        else
                        {
                            g.FillRectangle(Brushes.Black, x, y, cellWidth, cellHeight); // Tô màu đen cho chướng ngại vật
                        }
                    }
                }
            }
        }
        private void btnLen_Click(object sender, EventArgs e)
        {
            if (currentX - 1 >= 0 && Maze[currentX - 1, currentY] != 1)
            {
                Maze[currentX, currentY] = 0;
                Maze[currentX - 1, currentY] = 2;
                currentX--;
                Trace.Add(new Tuple<int, int>(currentX, currentY));
                pictureBox1.Invalidate();
                check_Winning_Conditions();
            }
            else
            {
                btnLen.Enabled = false;
            }
            btnPhai.Enabled = true;
            btnXuong.Enabled = true;
            btnTrai.Enabled = true;
            NumberMoves++;
        }
        private void btnXuong_Click(object sender, EventArgs e)
        {
            if (currentX + 1 <= Maze.GetLength(0) - 1 && Maze[currentX + 1, currentY] != 1)
            {
                Maze[currentX, currentY] = 0;
                Maze[currentX + 1, currentY] = 2;
                currentX++;
                Trace.Add(new Tuple<int, int>(currentX, currentY));
                pictureBox1.Invalidate();
                check_Winning_Conditions();
            }
            else
            {
                btnXuong.Enabled = false;
            }
            btnLen.Enabled = true;
            btnPhai.Enabled = true;
            btnTrai.Enabled = true;
            NumberMoves++;
        }
        private void btnTrai_Click(object sender, EventArgs e)
        {
            if (currentY - 1 >= 0 && Maze[currentX, currentY - 1] != 1)
            {
                Maze[currentX, currentY] = 0;
                Maze[currentX, currentY - 1] = 2;
                currentY--;
                Trace.Add(new Tuple<int, int>(currentX, currentY));
                pictureBox1.Invalidate();
                check_Winning_Conditions();
            }
            else
            {
                btnTrai.Enabled = false;
            }
            btnLen.Enabled = true;
            btnXuong.Enabled = true;
            btnPhai.Enabled = true;
            NumberMoves++;
        }
        private void btnPhai_Click(object sender, EventArgs e)
        {
            if (currentY + 1 <= Maze.GetLength(0) - 1 && Maze[currentX, currentY + 1] != 1)
            {
                Maze[currentX, currentY] = 0;
                Maze[currentX, currentY + 1] = 2;
                currentY++;
                Trace.Add(new Tuple<int, int>(currentX, currentY));
                pictureBox1.Invalidate();
                check_Winning_Conditions();
            }
            else
            {
                btnPhai.Enabled = false;
            }
            btnLen.Enabled = true;
            btnXuong.Enabled = true;
            btnTrai.Enabled = true;
            NumberMoves++;
        }
        private void check_Winning_Conditions()
        {
            if (currentX == endX && currentY == endY)
            {
                timer1.Stop();
                //timer1.Stop();
                // Lấy thời gian của timer hiện tại
                //int minutes = m;
                //int seconds = i;
                int CurrentLevel = Lever;
                int time = Time;
                int moves = NumberMoves;
                //int stepCount = NumberMoves;
                // Tính điểm dựa trên thời gian, số lần di chuyển và cấp độ
                int tinhdiem = TinhDiem(time, moves, CurrentLevel);
                // Hiển thị cập nhật điểm lên ListView
                ListViewItem lstItem = new ListViewItem("Level " + p.ToString());
                lstItem.SubItems.Add(label1.Text);
                lstItem.SubItems.Add(tinhdiem.ToString());
                listView1.Items.Add(lstItem);
                //int minutes = m;
                //int seconds = i;
                //int tinhdiem = TinhDiemTheoThoiGianVaSoLanDiChuyen()
                //ListViewItem lstItem = new ListViewItem("Player " + p.ToString());
                //lstItem.SubItems.Add(label1.Text);
                ////lstItem.SubItems.Add(tinhdiem.ToString());
                //listView1.Items.Add(lstItem);

                p++;
                //completedRounds++;

                if (Lever == 5)
                {
                    //if (completedRounds % 5 == 0)
                    //{
                    //    MessageBox.Show("Tổng điểm của bạn sau " + completedRounds + " vòng là: " + total, "Tổng Điểm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    listView1.Items.Clear();
                    //    p = 1;
                    //    total = 0;
                    //}
                    TinhTongDiem();
                    DialogResult result = MessageBox.Show("Hoàn thành trò chơi, bạn có muốn chơi lại?", "You win", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        load_Game(1);
                    }
                    listView1.Items.Clear();
                    p = 1;

                    tinhdiem = 0;
                }

                else
                {
                    DialogResult result = MessageBox.Show("Hoàn thành mức " + Lever.ToString() + ", tiếp tục sang mức tiếp theo?", "You win", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Lever++;
                        load_Game(Lever);
                    }
                }
            }
        }
        private void check_Losing_Conditions()
        {
            if (currentX == monsterX && currentY == monsterY)
            {
                timer1.Stop();
                DialogResult result = MessageBox.Show("Thất bại, bạn có muốn chơi lại?", "You lose", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    load_Game(1);
                }
                listView1.Items.Clear();
                p = 1;
                int diem = TinhDiem(Time, NumberMoves, Lever);
                diem = 0;

            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Lever == 2 || Lever == 3)
            {
                Time--;
                int Minute = Time / 60;
                int Second = Time % 60;
                label1.Text = Minute.ToString("00") + ":" + Second.ToString("00");
                if (Time == 0)
                {
                    timer1.Stop();
                    int diem = TinhDiem(Time, NumberMoves, Lever);
                    //XuLyDiem(diem);
                    DialogResult result = MessageBox.Show("Hết thời gian, bạn có muốn chơi lại?", "You lose", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        load_Game(1);
                    }
                }
            }
            else
            {
                Time++;
                int Minute = Time / 60;
                int Second = Time % 60;
                label1.Text = Minute.ToString("00") + ":" + Second.ToString("00");
                if (NumberMoves >= 5 && Lever > 3)
                {
                    if (NumberMovesMonster < 5)
                        monster_Movement_Speed(NumberMovesMonster);
                    else
                    {
                        monster_Movement_Speed(5);
                    }
                }
            }
        }

        private int TinhDiem(int time, int moves, int level)
        {
            int basePoint = 100; // Điểm cơ bản
            int diem = 0;

            switch (level)
            {

                case 1:
                    {
                        if (moves > 14 || time > 7)
                        {
                            diem = basePoint + (60 - time) * 5 - moves * 10;
                        }
                        diem = basePoint + (60 - time) * 5 + moves;

                        break;
                    }
                case 2:
                    // Điểm cộng thêm cho cấp độ 2
                    diem = basePoint - (7 - time * 10) + moves * 15;
                    break;
                case 3:
                    diem = basePoint + (60 - time) * 10 + moves * 20;
                    break;
                default:
                    diem = basePoint + (60 - time) * 5 + moves * 10;
                    break;
            }
            return diem;
        }

        private void TinhTongDiem()
        {
            int TotalScore = 0;

            foreach (ListViewItem item in listView1.Items)
            {
                TotalScore += int.Parse(item.SubItems[2].Text);
            }

            MessageBox.Show("Tổng điểm của bạn là: " + TotalScore, "Tổng Điểm", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Monster_Move()
        {
            if (Trace != null && Trace.Count >= 2)
            {
                Maze[monsterX, monsterY] = 0;
                Maze[Trace[1].Item1, Trace[1].Item2] = 5;
                monsterX = Trace[1].Item1;
                monsterY = Trace[1].Item2;
                Trace.RemoveAt(1);
                pictureBox1.Invalidate();
                check_Losing_Conditions();
            }
            else
            {
                return;
            }
        }
        private void monster_Movement_Speed(int S)
        {
            //Bước di chuyển của quái vật trong 1, giây
            for (int i = 0; i < S; i++)
            {
                Monster_Move();
                NumberMovesMonster++;
            }
        }
        private void btnPause_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                btnPause.Text = "Tạm dừng";
                timer1.Start();
            }
            else
            {
                btnPause.Text = "Tiếp tục";
                timer1.Stop();
            }
        }
        private void btnHelp_Click(object sender, EventArgs e)
        {
            if (NumberMoves >= 5)
            {
                reset_Maze();
                Help h = new Help(Maze);
                Path = h.FindPath(currentX, currentY, endX, endY);
                bool improve;
                if (Lever == 5)
                {
                    improve = h.HillClimbing(ref Path);
                    Tuple<int, int> Monster = Tuple.Create(monsterX, monsterY);
                    if (Path.Contains(Monster))
                    {
                        Path = h.bestPath(ref Path);
                    }
                }
                for (int i = 0; i < Path.Count / 4; i++)
                {
                    if (Maze[Path[i].Item1, Path[i].Item2] != 2)
                    {
                        Maze[Path[i].Item1, Path[i].Item2] = 4;
                    }
                }
                pictureBox1.Invalidate();
                btnHelp.Enabled = false;
            }
            else
            {
                btnHelp.Enabled = false;
            }
        }
        private void btnHelp_Check()
        {
            if (Lever >= 3 && NumberMoves >= (Path.Count / 4) + 3)
            {
                btnHelp.Enabled = true;
            }
            else
            {
                btnHelp.Enabled = false;
            }
        }
        private void btnReplay_Click(object sender, EventArgs e)
        {
            load_Game(1);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void reset_Maze()
        {
            for (int i = 0; i < Maze.GetLength(0); i++)
            {
                for (int j = 0; j < Maze.GetLength(1); j++)
                {
                    if (Maze[i, j] == 4)
                    {
                        Maze[i, j] = 0;
                    }
                }
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (timer1.Enabled == false)
            {
                timer1.Start();
            }
            if (e.KeyCode == Keys.W)
            {
                btnLen.PerformClick();
            }
            if (e.KeyCode == Keys.S)
            {
                btnXuong.PerformClick();
            }
            if (e.KeyCode == Keys.A)
            {
                btnTrai.PerformClick();
            }
            if (e.KeyCode == Keys.D)
            {
                btnPhai.PerformClick();
            }
            if (e.KeyCode == Keys.G)
            {
                btnPause.PerformClick();
            }
            btnHelp_Check();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Thoát trò chơi", "Thoát", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
