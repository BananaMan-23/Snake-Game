using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mini_App_Game
{
    public partial class Form1 : Form
    {
        private int rI, rJ;
        private Label labelScore;
        private PictureBox fruit;
        private PictureBox[] snake = new PictureBox[400];
        private int dirX, dirY;
        private int _width = 900;
        private int _height = 800;
        private int _sizeOfSlides = 40;
        private int score = 0;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Snake";
            snake[0] = new PictureBox();
            snake[0].Location = new Point(201, 201);
            snake[0].Size = new Size(_sizeOfSlides - 1, _sizeOfSlides - 1);
            snake[0].BackColor = Color.Red;
            labelScore = new Label();
            labelScore.Text = "Score: 0";
            labelScore.Location = new Point(810, 10);
            this.Controls.Add(labelScore);
            this.Controls.Add(snake[0]);
            this.Width = _width;
            this.Height = _height;
            dirX = 1;
            dirY = 0;
            fruit = new PictureBox();
            fruit.BackColor = Color.Yellow;
            fruit.Size = new Size(_sizeOfSlides, _sizeOfSlides);
            _generateMap();
            _generateFruit();
            timer.Tick += new EventHandler(_update);
            timer.Interval = 100;
            timer.Start();
            this.KeyDown += new KeyEventHandler(OKP);
        }

        private void _checkBorders()
        {
            if (snake[0].Location.X < 0)
            {
                for (int i = 1; i <= score; i++)
                {
                    this.Controls.Remove(snake[i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirX = 1;
            }

            if (snake[0].Location.X > _height)
            {
                for (int i = 1; i <= score; i++)
                {
                    this.Controls.Remove(snake[i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirX = -1;
            }

            if (snake[0].Location.Y < 0)
            {
                for (int i = 1; i <= score; i++)
                {
                    this.Controls.Remove(snake[i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirY = 1;
            }

            if (snake[0].Location.Y > _height)
            {
                for (int i = 1; i <= score; i++)
                {
                    this.Controls.Remove(snake[i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirY = -1;
            }
        }

        private void _generateFruit()
        {
            Random random = new Random();
            rI = random.Next(0, _height - _sizeOfSlides);
            int tempI = rI % _sizeOfSlides;
            rI -= tempI;

            rJ = random.Next(0, _height - _sizeOfSlides);
            int tempJ = rJ % _sizeOfSlides;
            rJ -= tempJ;
            rI++;
            rJ++;
            fruit.Location = new Point(rI, rJ);
            this.Controls.Add(fruit);
        }
        private void _eatFruit()
        {
            if (snake[0].Location.X == rI && snake[0].Location.Y == rJ)
            {
                labelScore.Text = "Score: " + ++score;
                snake[score] = new PictureBox();
                snake[score].Location = new Point(snake[score - 1].Location.X + 40 * dirX, snake[score - 1].Location.Y - 40 * dirY);
                snake[score].Size = new Size(_sizeOfSlides - 1, _sizeOfSlides - 1);
                snake[score].BackColor = Color.Red;
                this.Controls.Add(snake[score]);
                _generateFruit();
            }
        }

        public void _generateMap()
        {
            for (int i = 0; i <= _width / _sizeOfSlides; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(0, _sizeOfSlides * i);
                pic.Size = new Size(_width - 100, 1);
                this.Controls.Add(pic);
            }
            for (int i = 0; i <= _height / _sizeOfSlides; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(_sizeOfSlides * i, 0);
                pic.Size = new Size(1, _height - 100);
                this.Controls.Add(pic);
            }
        }

        private void OKP(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    dirX = 1;
                    dirY = 0;
                    break;
                case "Left":
                    dirX = -1;
                    dirY = 0;
                    break;
                case "Up":
                    dirY = -1;
                    dirX = 0;
                    break;
                case "Down":
                    dirY = 1;
                    dirX = 0;
                    break;
            }
        }

        private void _eatItSelf()
        {
            for (int i = 1; i < score; i++)
            {
                if (snake[0].Location == snake[i].Location)
                {
                    for (int j = i; j <= score; j++)
                    {
                        this.Controls.Remove(snake[j]);
                    }
                    score = score - (score - i + 1);
                }
            }
        }

        private void _moveSnake()
        {
            for (int i = score; i >= 1; i--)
            {
                snake[i].Location = snake[i - 1].Location;
            }
            snake[0].Location = new Point(snake[0].Location.X + dirX * (_sizeOfSlides), snake[0].Location.Y + dirY * (_sizeOfSlides));
            _eatItSelf();
        }

        private void _update(Object myObject, EventArgs eventArgs)
        {
            _checkBorders();
            _eatFruit();
            _moveSnake();
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void cube_Click(object sender, EventArgs e)
        {

        }
    }
}
