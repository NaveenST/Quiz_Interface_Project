using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Form2 : Form
    {
        question[] questions = new question[10];
        DataClasses1DataContext db = new DataClasses1DataContext();
        Random rand = new Random();
        int curr = 0, numcorrect = 0; 
        public Form2()
        {
            InitializeComponent();
        }
        private void generateQuiz()
        {
            var query =
                from question in db.questions
                select question;
            int numResults = query.Count();

            for (int i = 0; i < questions.Length; i++)
            {
                int pos = rand.Next(numResults);
                while(Array.Exists(questions, element  => element != null && element.Id == pos + 1))
                {
                    pos = rand.Next(numResults);
                }
                questions[i] = query.ToList().ElementAt(pos);
            } 
        }
        private void nextQuestion()
        {
            label1.Text = questions[curr].question1;
            radioButton1.Text = questions[curr].correct_answer;
            radioButton2.Text = questions[curr].ans;
            radioButton3.Text = questions[curr].ans2;
            radioButton4.Text = questions[curr].ans3;
            RadioButton[] rbs = groupBox1.Controls.OfType<RadioButton>().ToArray();

            for (int i = 0; i < rbs.Length; i++)
            {
                string tmp = rbs[i].Text;
                int num = rand.Next(rbs.Length);
                rbs[i].Text = rbs[num].Text;
                rbs[num].Text = tmp;

            }
            curr++;
         

        }
        private void checkQuestions()
        {
            RadioButton rb = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(element => element.Checked);
            if (rb != null)
            {
                if (rb.Text == questions[curr - 1].correct_answer)
                {
                    numcorrect++;
                    MessageBox.Show("Your are correct!", "Result", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Your are incorrect!", "Result", MessageBoxButtons.OK);
                }
                if (curr == questions.Length)
                {
                    MessageBox.Show("You scored a " + (((double)numcorrect / questions.Length) * 100) + "% on the quiz.", "Result", MessageBoxButtons.OK);
                    this.Close();
                }
                else
                {
                    rb.Checked = false;
                    nextQuestion();

                }
            }
            else
            {
                MessageBox.Show("You must choose a response", "Result", MessageBoxButtons.OK);
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(curr == 0)
            {
                generateQuiz();
                nextQuestion();
                groupBox1.Visible = true;
                button1.Text = "Submit";

            }
            else if(curr <= questions.Length)
            {
                checkQuestions();
            
            }

        }

    }
}
