using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace ProjektX
{
    public class GenerateBaseDesign
    {
        public Button[] day = new Button[35];
        public Button[] dayOfWeek = new Button[7];
        public Button month = new Button();
        public Button[] previousNext = new Button[2];
        public Font fontDay = new Font("Arial", 15, FontStyle.Regular);
        public Font fontDayOfWeek = new Font("Arial", 15, FontStyle.Regular);
        public int interval = 1;
        public Form1 startForm;
        public DataBase db;
        public DateTime currentDate;

        public GenerateBaseDesign(Form1 form, DataBase db)
        {
            this.startForm = form;
            this.db = db;
        }

        // создаёт кнопки
        public void startGenerate()
        {
            int locationX = 30;
            int locationY = 30;
            // месяц
            this.month.Location = new Point(locationX, locationY);
            this.month.Width = 700 + interval * 7;
            this.month.Height = 60;
            this.month.Font = new Font("Arial", 16, FontStyle.Bold);
            this.startForm.Controls.Add(this.month);

            // дни недели
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
            this.dayOfWeek[0].Text = "Пн";
            this.dayOfWeek[1].Text = "Вт";
            this.dayOfWeek[2].Text = "Ср";
            this.dayOfWeek[3].Text = "Чт";
            this.dayOfWeek[4].Text = "Пт";
            this.dayOfWeek[5].Text = "Сб";
            this.dayOfWeek[6].Text = "Вс";

            // дни
            locationX = 30;
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

                locationX += 100 + interval;
                if ((i + 1) % 7 == 0)
                {
                    locationX = 30;
                    locationY += 100 + interval;
                }
            }

            // переключатель месяца
            for (int i = 0; i < previousNext.Length; i++)
            {
                this.previousNext[i] = new Button();
                this.previousNext[i].Width = 350 + interval * 3;
                this.previousNext[i].Height = 35;
                this.previousNext[i].Location = new Point(locationX, locationY);
                this.previousNext[i].Font = fontDay;
                this.previousNext[i].Click += this.startForm.previousNextClick;
                this.startForm.Controls.Add(previousNext[i]);
                locationX += 350 + interval * 4;
            }
            previousNext[0].Text = "<";
            previousNext[1].Text = ">";
            previousNext[0].Name = "0";
            previousNext[1].Name = "1";

            this.startForm.Width = this.month.Right + 50;
            this.startForm.Height = this.day[34].Top + 200;
        }

        // Ставит чиселки в кнопки
        public void dayGenerate(DateTime date)
        {
            this.clearDay();
            this.currentDate = date;
            string monthYear = date.ToString("MMMM yyyy", CultureInfo.CurrentCulture);
            this.month.Text = monthYear.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture) + monthYear.Substring(1);

            int dayNow = (int)date.Day - 1;
            DateTime startDay = date.AddDays(-dayNow);
            db.getData();
            NoteDto[] dataDb = db.getDataMonth(startDay);

            int countRevers = (int)startDay.DayOfWeek == 0 ? 6 : (int)startDay.DayOfWeek - 1;
            startDay = startDay.AddDays(-countRevers);

            int countDb = 0;
            for (int i = 0; i < day.Length; i++)
            {
                // контекстное меню
                ContextMenuStrip dayMenu = new ContextMenuStrip();
                ToolStripMenuItem copyMenuItem = new ToolStripMenuItem("Изменить цвет");
                ToolStripMenuItem pasteMenuItem = new ToolStripMenuItem("Вставить");
                dayMenu.Items.AddRange(new[] { copyMenuItem, pasteMenuItem });
                copyMenuItem.Click += this.startForm.DayMenuClick1;
                pasteMenuItem.Click += this.startForm.DayMenuClick2;

                this.day[i].Text = startDay.Day.ToString();
                this.day[i].Name = startDay.ToString("d");
                copyMenuItem.Name = startDay.ToString("d");
                this.day[i].ContextMenuStrip = dayMenu;

                if (startDay == DateTime.Today) // покраска сегодняшнего дня
                {
                    this.day[i].BackColor = Color.LightCyan;
                }

                if (db.noteLengthMonth > countDb) // комент под числом
                {
                    if (dataDb[countDb].date == startDay)
                    {
                        this.day[i].Text = "\n" + this.day[i].Text + "\n○---";

                        if (dataDb[countDb].color != null)
                        {
                            Color realColor = (new NoteDto()).getRealColor(dataDb[countDb].color);
                            this.day[i].BackColor = realColor;
                        }

                        countDb++;
                    }
                }

                if (this.currentDate.Month != startDay.Month) // покраска дня другого месяца
                {
                    this.day[i].BackColor = Color.LightGray;
                }
                startDay = startDay.AddDays(1);
            }

        }

        // чистит кнопки дня
        private void clearDay()
        {
            for (int i = 0; i < day.Length; i++)
            {
                this.day[i].Text = "";
                this.day[i].BackColor = Color.White;
            }
        }

    }
}
