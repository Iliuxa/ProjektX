using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace ProjektX
{
    internal class GenerateDesign
    {
        public Button[] day = new Button[35];
        public Button[] dayOfWeek = new Button[7];
        public Button month = new Button();
        public Font fontDay = new Font("Arial", 15, FontStyle.Regular);
        public Font fontDayOfWeek = new Font("Arial", 15, FontStyle.Regular);
        public int interval = 1;
        public Form1 startForm;

        public GenerateDesign(Form1 form)
        {
            this.startForm = form;
        }

        public void startGenerate()
        {
            int locationX = 200;
            int locationY = 30;

            this.month.Location = new Point(locationX, locationY);
            this.month.Width = 700 + interval * 7;
            this.month.Height = 60;
            this.month.Font = new Font("Arial", 16, FontStyle.Bold); ;
            this.startForm.Controls.Add(this.month);

            locationY += 60;

            for (int i = 0; i < dayOfWeek.Length; i++)
            {
                this.dayOfWeek[i] = new Button();
                this.dayOfWeek[i].Width = 100;
                this.dayOfWeek[i].Height = 60;
                this.dayOfWeek[i].Location = new Point(locationX, locationY);
                this.dayOfWeek[i].Font = fontDayOfWeek;
                this.dayOfWeek[i].Click += this.startForm.buttonDayOfWeekClick;

                this.startForm.Controls.Add(dayOfWeek[i]);

                locationX += 100 + interval;
            }

            locationX = 200;
            locationY += 60 + interval;

            for (int i = 0; i < day.Length; i++)
            {
                this.day[i] = new Button();
                this.day[i].Width = 100;
                this.day[i].Height = 100;
                this.day[i].Location = new Point(locationX, locationY);
                this.day[i].Font = fontDay;
                this.day[i].Click += this.startForm.buttonDayClick;

                this.startForm.Controls.Add(day[i]);

                locationX += 101;
                if ((i + 1) % 7 == 0)
                {
                    locationX = 200;
                    locationY += 100 + interval;
                }
            }
        }

        public void dayGenerate(DateTime date)
        {
            string monthYear = date.ToString("MMMM yyyy", CultureInfo.CurrentCulture);
            this.month.Text = monthYear.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture) + monthYear.Substring(1);

            this.dayOfWeek[0].Text = "Пн";
            this.dayOfWeek[1].Text = "Вт";
            this.dayOfWeek[2].Text = "Ср";
            this.dayOfWeek[3].Text = "Чт";
            this.dayOfWeek[4].Text = "Пт";
            this.dayOfWeek[5].Text = "Сб";
            this.dayOfWeek[6].Text = "Вс"; 

            int dayNow = (int)date.Day - 1;
            DateTime startDay = date.Subtract(new TimeSpan(dayNow, 0, 0, 0));

            for (int i = (int)startDay.DayOfWeek - 1, numDay = 1; i < day.Length; i++, numDay++)
            {
                this.day[i].Text = numDay + "";
                if (startDay == date)
                {
                    this.day[i].BackColor = Color.LightGray;
                }


                startDay = startDay.AddDays(1);
                if ((int)startDay.Day == 1)
                {
                    break;
                }
            }

        }
    }
}
