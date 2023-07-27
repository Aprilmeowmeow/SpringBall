using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using WMPLib;

namespace _005
{
    public partial class Form1 : Form
    {
        bool goRight;
        bool goLeft;
        int speed = 10;
        int ballx = 5;
        int bally = 5;
        int score = 0;
        private Random rnd = new Random();
        private readonly WindowsMediaPlayer player = new WindowsMediaPlayer();
        private readonly SoundPlayer sp = new SoundPlayer();


        public Form1()
        {
            InitializeComponent();
            foreach (Control x in this.Controls)
            {
                if ( x is PictureBox && x.Tag == "block")
                {
                    Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                    x.BackColor = randomColor;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            player.URL = "H002.mp3";
            player.controls.play();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // then we set the car left boolean to true
            if (e.KeyCode == Keys.Left && button1.Left > 0)
            {
                goLeft = true;
            }
            // if player pressed the right key and the player left plus player width is less then the panel1 width
            // then we set the player right to true
            if (e.KeyCode == Keys.Right && button1.Left + button1.Width < 920)
            {
                goRight = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ball.Left += ballx;
            ball.Top += bally;

            label1.Text = "score: " + score;

            if (goLeft)
            {
                button1.Left -= speed;
            }
            if (goRight)
            {
                button1.Left += speed;
            }

            if (button1.Left < 1)
            {
                goLeft = false; // stop the car from going off screen
            }
            else if (button1.Left + button1.Width > 920)
            {
                goRight = false;
            }
            if (ball.Left + ball.Width > ClientSize.Width || ball.Left < 0)
            {
                ballx = -ballx; // this will bounce the object from the left or right border
            }

            if (ball.Top < 0 || ball.Bounds.IntersectsWith(button1.Bounds))
            {
                bally = -bally; // this will bounce the object from top and bottom border
            }

            if (ball.Top + ball.Height > ClientSize.Height)
            {
                GameOver();
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "block")
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        bally = -bally;
                        score++;
                    }
                }
            }

            if (score > 30)
            {
                GameOver();
                MessageBox.Show("you win!");
            }
        }

        private void GameOver()
        {
            timer1.Stop();
        }
    }
}
