using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Fluppy_Bird
{
    public partial class Form1 : Form
    {
        Random rd;

        int playerDropSpeed = 3;
        int playerJump = 35;
        int score = 0;

        public Form1()
        {            
            InitializeComponent();
            rd = new Random();
        }

        /// <summary>
        /// interval = 20 ms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (IsGameOver())                                        // 如果 IsGameOver() 為 true 則呼叫 GameOver() 方法。
            {
                GameOver();                                         
            }            
            PlayerDrop();                                            // 呼叫 PlayerDrop() 方法
            PictureBoxMove();                                        // 呼叫 PictureBoxMove() 方法    
            CloudMove();                                             // 呼叫 CloudMove() 方法
            BackGroundMove();                                        // 呼叫 BackGroundMove() 方法

            label1.Text = "Score : " + score;                        // 顯示分數
        }

        /// <summary>
        /// return true 如果 player 跟任何一個障礙物或是地板接觸，否則 return false。
        /// </summary>
        /// <returns></returns>
        private bool IsGameOver()
        {
            if (player.Top + player.Height >= ClientSize.Height  ||
                player.Bounds.IntersectsWith(pictureBox1.Bounds) ||
                player.Bounds.IntersectsWith(pictureBox2.Bounds) ||
                player.Bounds.IntersectsWith(pictureBox3.Bounds) ||
                player.Bounds.IntersectsWith(pictureBox4.Bounds)
                )
                return true;
            else
                return false;
        }

        /// <summary>
        /// 判定為 Game Over 後要執行的方法
        /// </summary>
        private void GameOver()
        {
            timer1.Enabled = false;                                  // timer1 不再計時(timer1.Enabled = false;)   
            playerJump = 0;                                          // playerJump = 0，按滑鼠player 不能夠再往上跑 
            player.Image = playerImageList.Images[2];                // 將 player 的圖片更改成角色死亡的畫面(playerImageList.Images[2;])
            label2.Text = "GAME OVER \r\n" + "Score : " + score;     // 將label2的文字改成增加顯示玩家的分數
            label2.Visible = true;                                   // label2 更改為可以看到(label2.Visible = true;)
        }

        /// <summary>
        ///  player 會持續往下掉，原先要設計成分數越高掉落速度越快，目前測試發現廖落速度太快會拉不回來，還需要修改。
        /// </summary>
        private void PlayerDrop()                                    
        {
            if (score < 10)
            {
                playerDropSpeed = 3;
            }
            else if (score < 20)
            {
                playerDropSpeed = 3;
            }
            else
            {
                playerDropSpeed = 3;
            }
            player.Top += playerDropSpeed;                           // player 每一次 timer1_Tick()，皆會落下 playerDropSpeed 的距離
        }

        /// <summary>
        /// 定義 pictureBox 向左移動，分數越高障礙物向左移動的速度就越快
        /// </summary>
        private void PictureBoxMove()
        {
            if (pictureBox1.Left + pictureBox1.Width < 0)           //  pictureBox 消失在視窗視野則重新生成一個新的 pictureBox
            {
                PictureBoxRecreate_Top(pictureBox1);                // 呼叫PPictureBoxRecreate_Top() 方法 => 定義上端障礙物重新生成的方法
                AddScore();                                         // 呼叫加分 AddScore() 方法 (障礙物重新生成 ==  player 已通過該障礙物 )
            }

            if (score < 10)                                         // 0 ~ 9 分 pictureBox 移動速度為 3
            {                                                       // 10 ~ 19 分 pictureBox 移動速度為 4
                pictureBox1.Left += -3;                             // 20 分以上 pictureBox 移動速度為 5
            }
            else if (score < 20)
            {
                pictureBox1.Left += -4;
            }
            else
            {
                pictureBox1.Left += -5;
            }


            if (pictureBox2.Left + pictureBox2.Width < 0)          
            {
                PictureBoxRecreate_Down(pictureBox2);               // 呼叫PictureBoxRecreate_Down() 方法
            }

            if (score < 10)
            {
                pictureBox2.Left += -3;                                  
            }
            else if (score < 20)
            {
                pictureBox2.Left += -4;
            }
            else
            {
                pictureBox2.Left += -5;
            }

            if (pictureBox3.Left + pictureBox3.Width < 0)           
            {
                PictureBoxRecreate_Top(pictureBox3);
                AddScore();
            }
            if (score < 10)
            {
                pictureBox3.Left += -3;                                  
            }
            else if (score < 20)
            {
                pictureBox3.Left += -4;
            }
            else
            {
                pictureBox3.Left += -5;
            }

            if (pictureBox4.Left + pictureBox4.Width < 0)           
            {
                PictureBoxRecreate_Down(pictureBox4);
            }
            if (score < 10)
            {
                pictureBox4.Left += -3;                                  
            }
            else if (score < 20)
            {
                pictureBox4.Left += -4;
            }
            else
            {
                pictureBox4.Left += -5;
            }
        }

        /// <summary>
        /// 控制上端障礙物重新生成的方法
        /// input Type PictureBox
        /// </summary>
        /// <param name="pictureBox"></param>
        private void PictureBoxRecreate_Top(PictureBox pictureBox)
        {
            pictureBox.Left = ClientSize.Width;                               // 從視窗最右邊生成
            pictureBox.Top = 0;                                               // Top座標等於零 

            pictureBox.Height = rd.Next(0, 400);                              // Height 控制在 0 ~ 400 ，否則會沒也通道可以讓 player 通過
        }

        private void AddScore()                                               // 每次呼叫 AddScore()，則 score++;
        {
            score++;
        }

        /// <summary>
        /// 控制下方障礙物重新生成的方法
        /// </summary>
        /// <param name="pictureBox"></param>
        private void PictureBoxRecreate_Down(PictureBox pictureBox)
        {
            if (pictureBox.Equals(pictureBox2))                                // pictureBox1 和 pictureBox2 為上下成對障礙物，
            {                                                                  // pictureBox2 和 pictureBox1 中間要間隔 150 的通道
                pictureBox.Top = pictureBox1.Height + 150;
            }
            else
            {
                pictureBox.Top = pictureBox3.Height + 150;                     // pictureBox3 和 pictureBox4 為上下成對障礙物，
            }                                                                  // pictureBox4 和 pictureBox3 中間要間隔 150 的通道

            pictureBox.Left = ClientSize.Width;                                // 從視窗最右邊重新生成
            pictureBox.Height = ClientSize.Height - pictureBox.Top;            // 新生成的下方障礙物長度(Height)為 ClientSize - 自己的 Top 座標
        }

        /// <summary>
        /// 定義背景雲朵的移動方式
        /// </summary>
        private void CloudMove()
        {
            if (cloud1.Left + cloud1.Width < 0)                                // 如果 cloud1 超出視窗範圍則重新生成一個
            {
                CloudRecreate(cloud1);                                         //超出視窗範圍，呼叫CloudRecreate() 方法
            }
            cloud1.Left += -3;                                                 

            if (cloud2.Left + cloud2.Width < 0)                                // 如果 cloud2 超出視窗範圍則重新生成一個
            {
                CloudRecreate(cloud2);                                         //超出視窗範圍，呼叫CloudRecreate() 方法
            }
            cloud2.Left += -2;
        }

        /// <summary>
        /// 定義 cloud 重新生成的方法
        /// input type : PictureBox
        /// </summary>
        /// <param name="cloud"></param>
        private void CloudRecreate(PictureBox cloud)                           
        {
            cloud.Left = ClientSize.Width;                                     // 新生成的 cloud 需要從視窗最右邊重新生成，因此 cloud.Left = ClientSize.Width;
            cloud.Top = rd.Next(0, 325);                                       // 至於 Y 座標則定義在 0~ 325 之間的隨機值
        }

        /// <summary>
        /// 定義背景地面物件移動方式 (有三個 background1 ~ background3)
        /// </summary>
        private void BackGroundMove()
        {
            if (background1.Left + background1.Width < 0)                    // background 超出視窗範圍則呼叫 BackGroundRecreate(0 方法
            {
                BackGroundRecreate(background1);                             // 呼叫 BackGroundRecreate() 方法
            }
            background1.Left += -2;                                          // timer1_Tick background 移動的距離

            if (background2.Left + background2.Width < 0)
            {
                BackGroundRecreate(background2);
            }
            background2.Left += -2;

            if (background3.Left + background3.Width < 0)
            {
                BackGroundRecreate(background3);
            }
            background3.Left += -2;
        }

        /// <summary>
        /// 定義 background 重新生成的方法 
        /// </summary>
        /// <param name="background"></param>
        private void BackGroundRecreate(PictureBox background)
        {
            background.Image = BG_imagelist.Images[rd.Next(0, 4)];          // 從 BG_imagelist 裡面隨機生成
            background.Left = ClientSize.Width;                             // 新生成 background 從視窗最右邊出現
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            player.Image = playerImageList.Images[1];                        // 按下滑鼠則將 player.Image 更改為 playerImageList.Image[1]
            player.Top += -playerJump;                                       // 玩家按一下滑鼠可以讓 player 上升 35 ((int)playerJunmp == 35) 
        }
        
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            player.Image = playerImageList.Images[0];                        // 偵測到 MouseUp 則將 player.Image 更改為 playerImageList.Image[0]
        }    
    }
}
