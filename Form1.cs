namespace WindowsFighter
{
    public partial class Form1 : Form
    {
        Player pl = new(50, 50, 100);
        Enemy en = new(10,10,1,10);
        static int a_counter = 1;
        static int d_counter = 1;

        

        public Form1()
        {
            InitializeComponent();
            //pictureBox1.Image = imageList1.Images[2];
        }

        private void button5_Click(object sender, EventArgs e) //healing
        {
            if (pl.CurHP==pl.MaxHP)
            {
                label4.Text = $"HP: {pl.CurHP}";
                label7.Text = $"Money: {pl.Money}";
                textBox1.Text = "Der Spieler hat maximum HP.";
                pictureBox1.Image = imageList1.Images[7];
            }
            else
            {
                pl.Healing();
                label4.Text = $"HP: {pl.CurHP}";
                label7.Text = $"Money: {pl.Money}";
                textBox1.Text = "Der Spieler wurde geheilt.";
                progressBar1.Value = pl.HPBar();
                pictureBox1.Image = imageList1.Images[5];
            }
            
        }

        private void button1_Click(object sender, EventArgs e) //Start
        {
            button1.Visible = false;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            button6.Visible = true;
            button7.Visible = true;
            textBox1.Visible = true;
            
            label1.Visible = true; //Spieler
            label2.Visible = true; //En Name
            label3.Visible = true; //Fight Nr.
            label4.Visible = true; //Spieler HP
            label5.Visible = true;  //Spieler Att
            label6.Visible = true;  //Spieler Def
            label7.Visible = true;  //Spieler Money
            label8.Visible = true;   //Gegner Money
            label9.Visible = true;  //Gegner Def
            label10.Visible = true; //Gegner Att
            label11.Visible = true; //Gegner HP
            
            pl.Attack = 50;     //Werte werden zurückgesetzt.
            pl.Defense = 50;
            pl.Money = 100;
            en.Attack = 10;
            en.Defense = 10;
            en.Drop = 10;
            a_counter = 1;
            d_counter = 1;
            en.Level = 1;

            en.SetName();
            en.SetHp();
            pl.SetHp();
            textBox1.Text = $"Der Gegner erscheint! Es ist {en.Name}.";
            label2.Text = en.Name;
            label3.Text = $"Level: {en.Level}";
            label4.Text = $"HP: {pl.CurHP}";
            label5.Text = $"Attack: {pl.Attack}";
            label6.Text = $"Defense: {pl.Defense}";
            label7.Text = $"Money: {pl.Money}";
            label8.Text = $"Money: {en.Drop}";
            label9.Text = $"Defense: {en.Defense}";
            label10.Text = $"Attack: {en.Attack}";
            label11.Text = $"HP: {en.CurHP}";
            progressBar1.Visible = true;
            progressBar1.Minimum = 0;       //HPbar Spieler
            progressBar1.Maximum = 101;
            progressBar1.Value = 100;
            progressBar2.Visible = true;    //HPbar Gegner
            progressBar2.Minimum = 0;
            progressBar2.Maximum = 100;
            progressBar2.Value = 100;
            pictureBox1.Image=imageList1.Images[7];
        }

        private void button2_Click(object sender, EventArgs e) //exit
        {
            Thread.Sleep(1000);
            Environment.Exit(0);
        }

        private void button3_Click(object sender, EventArgs e) //Hit
        {
            int tempHP_p =en.Hit(pl.CurHP, en.Attack,pl.Defense);
            int tempHP_e = pl.Hit(en.CurHP, pl.Attack, en.Defense);
            if (tempHP_e<1 && tempHP_p>0) //Win
            {   
                pl.CurHP=tempHP_p;
                pl.Money = pl.Money + en.Drop;
                label11.Text = $"HP: 0";
                label4.Text = $"HP: {pl.CurHP}";
                label7.Text = $"Money: {pl.Money}";
                label8.Text = $"Money: 0";
                progressBar1.Value = pl.HPBar(); //Spuckt beim Start ab und zu den int Wert 101 aus.
                //progressBar1.Value = 50;
                progressBar2.Value = 0;
                //progressBar2.Value = 50;
                textBox1.Text = $"{en.Name} wurde besieht. Du findest {en.Drop} Münzen.";
                button8.Visible = true;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                textBox1.Visible = true;
                if (en.Name=="Georg, der Lord")
                {
                    pictureBox1.Image = imageList1.Images[3];
                }
                else
                {
                    pictureBox1.Image = imageList1.Images[1];
                }
            }
            else if (tempHP_e>0 && tempHP_p > 0)//Hit
            {
                pl.CurHP = tempHP_p;
                en.CurHP = tempHP_e;
                label4.Text = $"HP: {pl.CurHP}";
                label11.Text = $"HP: {en.CurHP}";
                progressBar1.Value = pl.HPBar();
                progressBar2.Value = en.HPBar();
                textBox1.Text = "Schlagabtausch!";
                if (en.Name == "Georg, der Lord")
                {
                    pictureBox1.Image = imageList1.Images[2];
                }
                else
                {
                    pictureBox1.Image = imageList1.Images[4];
                }
                
            }
            else if(tempHP_e > 0 && tempHP_p <= 0)  //dead
            {
                label4.Text = $"HP: 0";
                label11.Text = $"HP: {en.CurHP}";
                progressBar1.Value = 0;
                progressBar2.Value = en.HPBar();
                textBox1.Text = $"Du wurdest besiegt. {en.Name} feiert den Sieg.";
               
                button1.Visible = true;
                button3.Visible = false;
                button4.Visible =false;
                button5.Visible = false;
                button6.Visible = false;
                button7.Visible = false;
                textBox1.Visible = true;
                pictureBox1.Image = imageList1.Images[0];
            }
            else //gleichzeitiges Toeten
            {
                label4.Text = $"HP: 0";
                label11.Text = $"HP: 0";
                progressBar1.Value = 0;
                progressBar2.Value = 0;
                textBox1.Text = $"Die Opponenten sind unmächtig.";
                
                button1.Visible = true;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
                button7.Visible = false;
                textBox1.Visible = true;
                pictureBox1.Image = imageList1.Images[0];
            }
        }

        private void button8_Click(object sender, EventArgs e) //nextFight
        {
            en.EnemyTod();
            
            textBox1.Text = $"Der Gegner erscheint! Es ist {en.Name}.";
            label2.Text = en.Name;
            label3.Text = $"Level: {en.Level}";
            label4.Text = $"HP: {pl.CurHP}";
            label5.Text = $"Attack: {pl.Attack}";
            label6.Text = $"Defense: {pl.Defense}";
            label7.Text = $"Money: {pl.Money}";
            label8.Text = $"Money: {en.Drop}";
            label9.Text = $"Defense: {en.Defense}";
            label10.Text = $"Attack: {en.Attack}";
            label11.Text = $"HP: {en.CurHP}";
            progressBar2.Value = en.HPBar();
            button8.Visible = false;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            textBox1.Visible = true;
            pictureBox1.Image = imageList1.Images[7]; //<<<
        }

        private void button4_Click(object sender, EventArgs e) //Heavy
        {
            int tempHP_p = en.Hit(pl.CurHP, en.Attack, pl.Defense) - (int)Math.Floor(pl.Attack - 0.1 * en.Defense); //Eigenschaden beim Heavy
            int tempHP_e = pl.HeavyHit(en.CurHP, pl.Attack, en.Defense) ;
            if (tempHP_e < 1 && tempHP_p > 0)
            {
                pl.CurHP = tempHP_p;
                pl.Money = pl.Money + en.Drop;
                label11.Text = $"HP: 0";
                label4.Text = $"HP: {pl.CurHP}";
                label7.Text = $"Money: {pl.Money}";
                label8.Text = $"Money: 0";
                progressBar1.Value = pl.HPBar(); //Spuckt beim Start ab und zu den int Wert 101 aus.
                //progressBar1.Value = 50;
                progressBar2.Value = 0;
                //progressBar2.Value = 50;
                textBox1.Text = $"{en.Name} wurde besiegt. Du findest {en.Drop} Münzen.";
                button8.Visible = true;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                textBox1.Visible = true;
                if (en.Name == "Georg, der Lord")
                {
                    pictureBox1.Image = imageList1.Images[3];
                }
                else
                {
                    pictureBox1.Image = imageList1.Images[1];
                }

            }
            else if (tempHP_e > 0 && tempHP_p > 0)
            {
                pl.CurHP = tempHP_p;
                en.CurHP = tempHP_e;
                label4.Text = $"HP: {pl.CurHP}";
                label11.Text = $"HP: {en.CurHP}";
                progressBar1.Value = pl.HPBar();
                progressBar2.Value = en.HPBar();
                textBox1.Text = "Schlagabtausch!";
                if (en.Name == "Georg, der Lord")
                {
                    pictureBox1.Image = imageList1.Images[2];
                }
                else
                {
                    pictureBox1.Image = imageList1.Images[4];
                }

            }
            else if (tempHP_e > 0 && tempHP_p <= 0)
            {
                label4.Text = $"HP: 0";
                label11.Text = $"HP: {en.CurHP}";
                progressBar1.Value = 0;
                progressBar2.Value = en.HPBar();
                textBox1.Text = $"Du wurdest besiegt. {en.Name} feiert den Sieg.";
                
                button1.Visible = true;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
                button7.Visible = false;
                textBox1.Visible = true;
                pictureBox1.Image = imageList1.Images[0];

            }
            else
            {
                label4.Text = $"HP: 0";
                label11.Text = $"HP: 0";
                progressBar1.Value = 0;
                progressBar2.Value = 0;
                textBox1.Text = $"Die Opponenten sind unmächtig.";
               
                button1.Visible = true;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
                button7.Visible = false;
                textBox1.Visible = true;
                pictureBox1.Image = imageList1.Images[0];
            }
        }

        private void button6_Click(object sender, EventArgs e) //upgrade
        {
            int s_price=2;
            if (pl.Money>=s_price*a_counter)
            {
                pl.Attack = pl.Attack + 2;
                pl.Money = pl.Money - s_price*a_counter;
                a_counter++;
                pl.UpdateHP();
                label4.Text = $"HP: {pl.CurHP}";
                progressBar1.Value = pl.HPBar();
                label5.Text = $"Attack: {pl.Attack}";
                label7.Text = $"Money: {pl.Money}";
                textBox1.Text = "";
                pictureBox1.Image = imageList1.Images[6];

            }
            else
            {
                label5.Text = $"Attack: {pl.Attack}";
                label7.Text = $"Money: {pl.Money}";
                textBox1.Text = $"{s_price*a_counter} Münzen benötigt!";
                pictureBox1.Image = imageList1.Images[7];
            }
        }

        private void button7_Click(object sender, EventArgs e) //upgrade
        {
            int s_price = 2;
            if (pl.Money >= s_price * d_counter)
            {
                pl.Defense = pl.Defense + 2;
                pl.Money = pl.Money - s_price * d_counter;
                d_counter++;
                pl.UpdateHP();
                label4.Text = $"HP: {pl.CurHP}";
                progressBar1.Value = pl.HPBar();
                label6.Text = $"Defense: {pl.Defense}";
                label7.Text = $"Money: {pl.Money}";
                textBox1.Text = "";
                pictureBox1.Image = imageList1.Images[6];
            }
            else
            {
                label6.Text = $"Defense: {pl.Defense}";
                label7.Text = $"Money: {pl.Money}";
                textBox1.Text = $"{s_price * d_counter} Münzen benötigt!";
                pictureBox1.Image = imageList1.Images[7];
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imageList1.Images[2];
        }

        private void progressBar2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imageList1.Images[3];
        }
    }
    
}