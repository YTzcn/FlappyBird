using System;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class Form1 : Form
    {
        // Skor ve hız değişkenleri tanımlanıyor
        int score = 0; // Oyuncunun puanı
        int gravity = 5; // Kuşun düşüş hızı
        int pipeSpeed = 8; // Boruların hareket hızı
        bool gameStarted = false; // Oyunun başlangıç durumu

        public Form1()
        {
            InitializeComponent(); // Form bileşenlerini başlat
        }

        // Timer tetiklenince çalışacak olan metot
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            scoreLabelControl.Text = "Skor: " + score; // Skoru güncelle

            // Kuşun düşüşünü simüle et
            pictureEdit1.Top += gravity;

            // Boruların sola doğru hareket etmesini sağla
            pictureEdit2.Left -= pipeSpeed;
            pictureEdit3.Left -= pipeSpeed;

            // Borular ekranın solundan çıktığında konumlarını sıfırla ve skoru artır
            if (pictureEdit2.Left < -50)
            {
                pictureEdit2.Left = 800; // Boru konumunu sıfırla
                pictureEdit3.Left = 800; // Diğer boruyu da sıfırla
                score++; // Skoru artır
            }

            // Kuşun form dışına çıkıp çıkmadığını kontrol et
            if (pictureEdit1.Top < -25 || pictureEdit1.Top > 500)
            {
                gameOver(); // Oyun bitiş fonksiyonunu çağır
            }

            // Kuşun borulara çarpıp çarpmadığını kontrol et
            if (pictureEdit1.Bounds.IntersectsWith(pictureEdit2.Bounds) || pictureEdit1.Bounds.IntersectsWith(pictureEdit3.Bounds))
            {
                gameOver(); // Oyun bitiş fonksiyonunu çağır
            }
        }

        // Oyun bittiğinde çağrılacak olan metot
        private void gameOver()
        {
            gameTimer.Stop(); // Timer'ı durdur
            scoreLabelControl.Text += " - Oyun Bitti!"; // Oyun bitti mesajını göster
            gameStarted = false; // Oyunun durduğunu belirt
        }

        // Başlat butonuna basıldığında çalışacak metot
        private void startButton_Click(object sender, EventArgs e)
        {
            if (!gameStarted) // Eğer oyun başlamamışsa
            {
                ResetGame(); // Oyunu sıfırla
                gameTimer.Start(); // Timer'ı başlat
                gameStarted = true; // Oyunun başladığını belirt
            }
        }

        // Durdur butonuna basıldığında çalışacak metot
        private void stopButton_Click(object sender, EventArgs e)
        {
            gameTimer.Stop(); // Timer'ı durdur
            gameStarted = false; // Oyunun durduğunu belirt
        }

        // Oyunu sıfırlama metodu
        private void ResetGame()
        {
            score = 0; // Skoru sıfırla
            pictureEdit1.Top = 227; // Kuşun başlangıç yüksekliği
            pictureEdit2.Left = 800; // Boruların başlangıç konumu
            pictureEdit3.Left = 800; // Diğer borunun başlangıç konumu
            scoreLabelControl.Text = "Skor: 0"; // Skor etiketini sıfırla
        }

        // Tuşa basıldığında çalışacak metot
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) // Eğer boşluk tuşuna basıldıysa
            {
                gravity = -10; // Kuşun yukarı hareket etmesini sağla
            }
        }

        // Tuş bırakıldığında çalışacak metot
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) // Eğer boşluk tuşu bırakıldıysa
            {
                gravity = 5; // Kuşun tekrar düşmeye başlamasını sağla
            }
        }
    }
}
