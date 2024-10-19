using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class Form1 : Form
    {
        int score = 0;
        int gravity = 5; // Kuşun düşüş hızı
        int pipeSpeed = 8; // Boruların sola doğru hareket hızı
        bool gameStarted = false; // Oyunun başlamış olup olmadığını kontrol etmek için

        public Form1()
        {
            InitializeComponent();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            scoreLabelControl.Text = "Skor: " + score;
            // Kuşun düşüşünü simüle edin
            pictureEdit1.Top += gravity;

            // Boruların sola doğru hareket etmesini sağlayın
            pictureEdit2.Left -= pipeSpeed;
            pictureEdit3.Left -= pipeSpeed;

            // Borular ekranın solundan çıktığında yeniden konumlandırın
            if (pictureEdit2.Left < -50)
            {
                pictureEdit2.Left = 800;
                pictureEdit3.Left = 800;
                score++; // Skoru artır
            }

            // Skoru güncelle
            scoreLabelControl.Text = "Skor: " + score;

            // Kuşun form dışına çıkıp çıkmadığını kontrol edin
            if (pictureEdit1.Top < -25 || pictureEdit1.Top > 500)
            {
                gameOver();
            }

            // Kuşun borulara çarpıp çarpmadığını kontrol edin
            if (pictureEdit1.Bounds.IntersectsWith(pictureEdit2.Bounds) || pictureEdit1.Bounds.IntersectsWith(pictureEdit3.Bounds))
            {
                gameOver();
            }
        }

        private void gameOver()
        {
            gameTimer.Stop(); // Timer'ı durdur
            scoreLabelControl.Text += " - Oyun Bitti!";
            gameStarted = false; // Oyunun durduğunu belirt
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (!gameStarted) // Eğer oyun başlamamışsa
            {
                ResetGame(); // Oyunu sıfırlayın
                gameTimer.Start(); // Timer'ı başlat
                gameStarted = true; // Oyunun başladığını belirt
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            gameTimer.Stop(); // Timer'ı durdur
            gameStarted = false; // Oyunun durduğunu belirt
        }

        private void ResetGame()
        {
            // Skor ve pozisyonları sıfırlama
            score = 0;
            pictureEdit1.Top = 227; // Kuşun başlangıç yüksekliği
            pictureEdit2.Left = 800; // Boruların başlangıç konumu
            pictureEdit3.Left = 800; // Boruların başlangıç konumu
            scoreLabelControl.Text = "Skor: 0";
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = -10; // Kuşun yukarı hareket eder (yükselme hızı arttı)
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 5; // Kuş tekrar düşmeye başlar
            }
        }
    }
}
