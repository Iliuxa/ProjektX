using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektX
{
    internal class GenerateDesign
    {
        public Button[] day = new Button[35];
        public Button[] dayOfWeek = new Button[7];
        public Form1 startForm;

        public GenerateDesign(Form1 form)
        {
            this.startForm = form;
        }

        public void startGenerate()
        {
            int locationX = 200;
            int locationY = 50;

            for (int i = 0; i < 7; i++)
            {
                this.dayOfWeek[i] = new Button();
                this.dayOfWeek[i].Width = 100;
                this.dayOfWeek[i].Height = 60;
                this.dayOfWeek[i].Location = new Point(locationX, locationY);
                this.dayOfWeek[i].Click += this.startForm.buttonDayOfWeekClick;

                this.startForm.Controls.Add(dayOfWeek[i]);

                locationX += 101;
            }

            locationX = 200;
            locationY += 61;

            for (int i = 0; i < 35; i++)
            {
                this.day[i] = new Button();
                this.day[i].Width = 100;
                this.day[i].Height = 100;
                this.day[i].Location = new Point(locationX, locationY);
                this.day[i].Click += this.startForm.buttonDayClick;

                this.startForm.Controls.Add(day[i]);

                locationX += 101;
                if ((i + 1) % 7 == 0)
                {
                    locationX = 200;
                    locationY += 101;
                }
            }
        }

        public void dayGenerate()
        {
            this.dayOfWeek[0].Text = "Пн";
            this.dayOfWeek[1].Text = "Вт";
            this.dayOfWeek[2].Text = "Ср";
            this.dayOfWeek[3].Text = "Чт";
            this.dayOfWeek[4].Text = "Пт";
            this.dayOfWeek[5].Text = "Сб";
            this.dayOfWeek[6].Text = "Вс";

            DateTime date = DateTime.Today;
            MessageBox.Show((int)date.DayOfWeek + "");
        }
    }
}
