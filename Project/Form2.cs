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
        question[] questions = new question[1];
        DataClasses1DataContext db = new DataClasses1DataContext();
        public Form2()
        {
            InitializeComponent();
        }
        private void generateQuiz()
        {
            var query =
                from question in db.questions
                where question.Id == 1
                select question;
            questions[0] = query.First();
        }
        private void nextQuestion()
        {
            label1.Text = questions[0].question;
            radioButton1.Text = questions[0].ans

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            generateQuiz();
            nextQuestion();
        }
    }
}
