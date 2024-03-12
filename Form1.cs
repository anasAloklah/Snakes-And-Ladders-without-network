using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace SnakesAndLadders
{
    public partial class Form1 : Form
    {
        Random r;
        int p1=1;
        int p2 = 1;
        
        int i = 0;
        int numOfPlayer = 5;
        int[] players;
        int i_roll = 0;
        Hashtable h_bord;
        Hashtable h_snakes;
        Hashtable h_Ladders;
        Hashtable h_Location;
        PictureBox[] smpal_pic;
        Label[] playerText;
        string[,] bord=new string [10,10];
        public Form1()
        {
            smpal_pic = new PictureBox[numOfPlayer];
            players = new int[numOfPlayer];
            playerText = new Label[numOfPlayer];
            foreach (var box in smpal_pic)
                this.Controls.Add(box);
            foreach (var box2 in playerText)
                this.Controls.Add(box2);
            for (int i = 0; i < numOfPlayer; i++)
            {
                playerText[i] = new Label();
                playerText[i].Text = "player" + (i + 1).ToString()+" :";
                playerText[i].Location = new Point(725, 90 + (i * 30));
                playerText[i].Enabled = true;
                playerText[i].Visible = true;
                this.Controls.Add(playerText[i]);
            }
            InitializeComponent();
        }
        /*
        private void goToladderAndSnake(int x, int y, int numPlayer)
        {
            string loc_S = (string)h_bord[x];
            string loc_D = (string)h_bord[y];
            int xps = int.Parse(loc_S.Split(',')[0]);
            int yps = int.Parse(loc_S.Split(',')[1]);
            int xpd = int.Parse(loc_D.Split(',')[0]);
            int ypd = int.Parse(loc_D.Split(',')[1]);
            double dis = Math.Sqrt(Math.Pow(xps - xpd, 2) + Math.Pow(yps - ypd, 2));
            for (int i = yps*60; i <= (int)dis*60; i+=60)
            {
                
                System.Threading.Thread.Sleep(500);
                if (numPlayer == 1)
                    pl1.Location = new Point(i * 60, xps * 60);
                else if (numPlayer == 2)
                    pl2.Location = new Point(i * 60, xps * 60);
            }
        }*/

        private void restGame()
        {
            for (int i = 0; i < numOfPlayer; i++)
            {
                players[i] = 1;
                
                smpal_pic[i].Location = new Point(0 * 60, 9 * 60);
                
            }
        }
        private void checkWinner()
        {
            for (int i = 0; i < numOfPlayer; i++)
            {
                if (players[i] >= 100)
                {
                    MessageBox.Show("the player"+(i+1)+" is win");
                    restGame();
                }
            }
        }
        private void goToPlyer(int x, int y, int numPlayer)
        {
            if (y > 100)
                y = 100;
            for (int i = x; i <= y; i++)
            {
                string loc = (string)h_bord[i];
                int xp = int.Parse(loc.Split(',')[0]);
                int yp = int.Parse(loc.Split(',')[1]);
                System.Threading.Thread.Sleep(100);
                smpal_pic[numPlayer].Location = new Point(yp * 60, xp * 60);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int x=r.Next(0, 6)+1;
            label1.Text = x.ToString();
            Image image = Image.FromFile("r"+x.ToString()+".jpg");
            randomPic.Image = image;
            button1.Text = "roll player :" + (((i+1) % numOfPlayer) + 1).ToString();
            i_roll = (i % numOfPlayer);
            
            goToPlyer(players[i_roll], players[i_roll] + x, i_roll);
            players[i_roll] += x;

            if (h_Ladders.ContainsKey(players[i_roll]))
            {
                // goToladderAndSnake(p1, (int)h_Ladders[p1], 1);
                playerText[i_roll].Text = "player" + (i_roll + 1).ToString() + " : "+ players[i_roll].ToString();
                if (h_bord.ContainsKey(players[i_roll]))
                {
                    string loc = (string)h_bord[players[i_roll]];
                    int xp = int.Parse(loc.Split(',')[0]);
                    int yp = int.Parse(loc.Split(',')[1]);
                    smpal_pic[i_roll].Location = new Point(yp * 60, xp * 60);
                }
                players[i_roll] = (int)h_Ladders[players[i_roll]];
                MessageBox.Show("the player"+ (i_roll+1)+" in ladder");
            }
            if (h_snakes.ContainsKey(players[i_roll]))
            {
                //goToladderAndSnake(p1, (int)h_snakes[p1], 1);
                playerText[i_roll].Text = "player" + (i_roll + 1).ToString() + " : " + players[i_roll].ToString();
                if (h_bord.ContainsKey(players[i_roll]))
                {
                    string loc = (string)h_bord[players[i_roll]];
                    int xp = int.Parse(loc.Split(',')[0]);
                    int yp = int.Parse(loc.Split(',')[1]);
                    smpal_pic[i_roll].Location = new Point(yp * 60, xp * 60);
                }
                players[i_roll] = (int)h_snakes[players[i_roll]];
                MessageBox.Show("the player" + (i_roll+1) + " pit from snakes");
            }
            if (h_bord.ContainsKey(players[i_roll]))
            {
                string loc = (string)h_bord[players[i_roll]];
                int xp = int.Parse(loc.Split(',')[0]);
                int yp = int.Parse(loc.Split(',')[1]);
                smpal_pic[i_roll].Location = new Point(yp * 60, xp * 60);
                playerText[i_roll].Text = "player" + (i_roll + 1).ToString() + " : " + players[i_roll].ToString();
            }
            i++;
            checkWinner();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            r = new Random();
            h_bord = new Hashtable();
            h_snakes = new Hashtable();
            h_Ladders = new Hashtable();
            h_Location = new Hashtable();
            button1.Text = "roll player :1";



            for (int i = 0; i < numOfPlayer; i++)
            {
                //this.Controls.Add(smpal_pic[i]);
                players[i] = 1;
                smpal_pic[i] = new PictureBox();
                Image image22 = Image.FromFile("player" + (i+1).ToString() + ".png");
                smpal_pic[i].Image = image22;
                smpal_pic[i].Location = new Point(0, 540);
                smpal_pic[i].Show();
                smpal_pic[i].Enabled = true;
                smpal_pic[i].Visible = true;
                smpal_pic[i].Size = new Size(60, 60);
               pictureBox1.Controls.Add(smpal_pic[i]);
                smpal_pic[i].BackColor = Color.Transparent;
                
            }
            
           
            //h_bord.Clear();
            int num_cile = 101;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if(i%2==0)
                    {
                        num_cile--;
                    }
                    else
                    {
                        num_cile++;
                    }
                    bord[i, j] = "c:0";
                    string po = i.ToString() + ',' + j.ToString();
                    string loc = (i * 60).ToString()+',' + (j * 60).ToString();
                    h_bord.Add(num_cile, po);
                }
                if(i%2==0)
                num_cile = num_cile - 11;
                else
                num_cile = num_cile - 9;
            }
            h_snakes.Add(17, 4); h_snakes.Add(68, 28); h_snakes.Add(78, 43); h_snakes.Add(95, 66);
            h_Ladders.Add(7, 14); h_Ladders.Add(20, 40); h_Ladders.Add(37, 57); h_Ladders.Add(47, 87);

        }
    }
}
