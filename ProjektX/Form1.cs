using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ProjektX
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            GenerateDesign generate = new GenerateDesign(this);
            generate.startGenerate();
            generate.dayGenerate(DateTime.Today);

        }

        public void buttonDayClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.Text = "xx";

            //MessageBox.Show("cccc");
        }

        public void buttonDayOfWeekClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.Text = "xx";

            //MessageBox.Show("cccc");
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}