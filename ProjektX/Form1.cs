using Microsoft.VisualBasic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ProjektX
{
    public partial class Form1 : Form
    {
        private GenerateBaseDesign generate;
        public DataBase db;

        public Form1()
        {
            InitializeComponent();

            this.db = new DataBase();
            generate = new GenerateBaseDesign(this, db);
            generate.startGenerate();
            generate.dayGenerate(DateTime.Today);
        }

        // Клик на число
        public void buttonDayClick(object sender, EventArgs e)
        {
            Button button = sender as Button;

            FormNote noteForm = new FormNote(button, this.db);
            noteForm.Show();
        }

        // Клик на неделю
        public void buttonDayOfWeekClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
        }

        public void DayMenuClick1(object sender, EventArgs e)
        {
            Button button = sender as Button;
            ToolStripMenuItem copyMenuItem = sender as ToolStripMenuItem;
            FormColor colorForm = new FormColor(copyMenuItem.Name, this.db, generate);
            colorForm.Show();
        }

        public void DayMenuClick2(object sender, EventArgs e)
        {
            Button button = sender as Button;

        }

        // Переключение месяца
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

        public DataBase getBd()
        {
            return this.db;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}