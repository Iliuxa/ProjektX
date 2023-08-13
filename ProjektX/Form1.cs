using Microsoft.VisualBasic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ProjektX
{
    public partial class Form1 : Form
    {
        private GenerateDesign generate;
        private DataBase db;

        public Form1()
        {
            InitializeComponent();

            db = new DataBase();
            generate = new GenerateDesign(this, db);
            generate.startGenerate();
            generate.dayGenerate(DateTime.Today);
        }

        public void buttonDayClick(object sender, EventArgs e)
        {
            Button button = sender as Button;

            FormNote note = new FormNote();
            note.Show();
        }

        public void buttonDayOfWeekClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
        }

        public void DayMenuClick1(object sender, EventArgs e)
        {
            Button button = sender as Button;
        }

        public void DayMenuClick2(object sender, EventArgs e)
        {
            Button button = sender as Button;

        }

        public void previousNextClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            DateTime date = generate.currentDate;
            if (button.Name == "1")
            {
                date = date.AddMonths(1);
            }
            else
            {
                date = date.AddMonths(-1);
            }
            generate.dayGenerate(date);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}