using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Assign
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        string word = "";
        List<Label> labels = new List<Label>();
        int amount = 0;

        public enum BodyParts
        {
            Head,
            Left_Eye,
            Right_Eye,
            Mouth,
            Left_Arm,
            Right_Arm,
            Body,
            Left_Leg,
            Right_Leg

        }

        void DrawBodyParts(BodyParts bp)
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.White, 2);
            if (bp == BodyParts.Head)
            {
                g.DrawEllipse(p, 40, 103, 60, 60);
            }
            else if (bp == BodyParts.Left_Eye)
            {
                SolidBrush s = new SolidBrush(Color.White);
                g.FillEllipse(s, 50, 125, 5, 5);
            }
            else if (bp == BodyParts.Right_Eye)
            {
                SolidBrush s = new SolidBrush(Color.White);
                g.FillEllipse(s, 85, 125, 5, 5);
            }
            else if (bp == BodyParts.Mouth)
            {
                g.DrawLine(p, new Point(53,150) , new Point(85,150));

            }
            else if (bp == BodyParts.Body)
            {
                g.DrawLine(p, new Point(70, 165) , new Point(70, 250));
            }
            else if (bp == BodyParts.Left_Arm)
            {
                g.DrawLine(p, new Point(70, 180), new Point(35, 235));
            }
            else if (bp == BodyParts.Right_Arm)
            {
                g.DrawLine(p, new Point(70,180), new Point(105, 235));
            }
            else if (bp == BodyParts.Left_Leg)
            {
                g.DrawLine(p, new Point(70, 250), new Point(55,290));
            }
            else if (bp == BodyParts.Right_Leg)
            {
                g.DrawLine(p, new Point(70,250), new Point(85, 290));
            }
        }

        void DrawHangPost()
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.White, 10);
            g.DrawLine(p, new Point(150,352) , new Point(150,50));

            g.DrawLine(p, new Point(155, 50), new Point(75, 50));

            g.DrawLine(p, new Point(70, 45), new Point(70, 105));
        }

        string GetRandomWord()
        {
            string wordList = System.IO.File.ReadAllText("words.txt");
            string [] words = wordList.Split('\n');
            Random ran = new Random();
            word = words[ran.Next(0,words.Length -1)];
            return word;
        }

        void MakeLabels()
        {
            word = GetRandomWord();
            char[] chars = word.ToCharArray();
            int space = 400 / chars.Length - 1;
            for (int i = 0; i < chars.Length - 1; i++)
            {
                labels.Add(new Label());
                labels[i].Location = new Point((i * space) + 15, 165);
                labels[i].Text = "__";
                labels[i].Parent = groupBox2;
                labels[i].BringToFront();
                labels[i].CreateControl();
            }
            
            label1.Text = "Word Length: " + (chars.Length - 1).ToString();
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            DrawHangPost();
            MakeLabels();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            char letter = textBox1.Text.ToLower().ToCharArray()[0];

            if (!char.IsLetter(letter))
            {
                MessageBox.Show("You must submit a letter", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(word.Contains(letter))
            {
                char [] letters = word.ToCharArray();
                for (int i = 0; i < letters.Length; i++)
                {

                    if (letters[i] == letter)
                    {
                        labels[i].Text = letter.ToString();
                    }
                }
                textBox1.Text = "";
                textBox2.Text = "";
                foreach (Label l in labels)
                    if (l.Text == "__") return;
                MessageBox.Show("You have won!", "Congratulations!");
                ResetGame();
            }


            else
            {
                MessageBox.Show("The letter you guessed is not in the word", "sorry", MessageBoxButtons.OK);
                label2.Text += " " + letter + ",";
                DrawBodyParts((BodyParts)amount);
                amount++;
                if (amount == 9)
                {
                    MessageBox.Show("Sorry but you lost " + " The word was " + word ,"Sorry");
                    ResetGame();
                }
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }



        void ResetGame()
        {
            Graphics g = panel1.CreateGraphics();
            g.Clear(panel1.BackColor);
            GetRandomWord();
            MakeLabels();
            DrawHangPost();
            label2.Text = "Missed: ";
            textBox1.Text = "";
            textBox2.Text = "";
            amount = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int correct = 1;

            string guessedword = textBox2.Text + "\r";

            correct = string.Compare(word, guessedword);

            if (correct == 0)
            {
                MessageBox.Show("Congratulations, You have won!", "Congrats");
                ResetGame();

            }
            else
            {
                MessageBox.Show("Sorry the word you guessed was incorrect");
                DrawBodyParts((BodyParts)amount);
                amount++;
                textBox1.Text = "";
                textBox2.Text = "";
                if (amount == 9)
                {
                    MessageBox.Show("Sorry but you lost " + " The word was " + word, "Sorry");
                    ResetGame();
                }
            }
        }


    }
}
