using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjektX
{
    internal class GenerateNoteDesign
    {
        public FormNote form;
        public DataBase db;
        private DateTime date;
        public Label[] noteBox = new Label[10];
        public Button[] editButton = new Button[10];
        public RichTextBox editBox = new RichTextBox();
        public string[] notes;
        private int labelCount = 0;
        private int locationXLabel = 80;
        private int locationYLabel = 60;
        private int locationXButton = 40;
        private int locationYButton = 60;

        public GenerateNoteDesign(DataBase db, FormNote form)
        {
            this.db = db;
            this.form = form;
            this.date = DateTime.ParseExact(form.buttonDay.Name, "d", null);
        }

        public void baseGenerate()
        {
            this.form.Width = 850;
            this.form.Height = 500;
            this.titleGenerate();
            this.noteGenerate();
        }

        public void getEditForm(Button button)
        {
            editBox.Width = noteBox[0].Width;
            editBox.Height = 150;
            editBox.Location = new Point(this.locationXButton, noteBox[this.labelCount - 1].Bottom + 10);
            editBox.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            editBox.Text = this.notes[int.Parse(button.Name.Remove(0, 1))];

            this.form.Controls.Add(this.editBox);
            this.form.Height = this.editBox.Bottom + 120;
        }

        private void noteGenerate()
        {
            this.labelCount = 0;
            for (int i = 0; i < db.noteLength; i++)
            {
                if (db.note[i].date == this.date)
                {
                    this.notes = db.note[i].note.Split(new char[] { '&' });
                    for(int k = 0; k < notes.Length; k++)
                    {
                        this.noteBoxGenerate(notes[k], k);
                        this.buttonGenerate(k);
                    }
                    break;
                }
            }
        }

        private void buttonGenerate(int number)
        {
            this.editButton[number] = new Button();
            this.editButton[number].Name = "e" + number;
            this.editButton[number].Image = Image.FromFile("D:\\Internet Exploer\\ProjektX\\ProjektX\\x.png");
            this.editButton[number].ImageAlign = ContentAlignment.MiddleCenter;
            this.editButton[number].Width = 40;
            this.editButton[number].Height = 40;
            this.editButton[number].Location = new Point(locationXButton, this.noteBox[number].Top + this.noteBox[number].Height / 2 - 25);
            this.editButton[number].Click += this.form.buttonEditClick;

            this.form.Controls.Add(this.editButton[number]);
        }

        private void noteBoxGenerate(string text, int number)
        {
            this.noteBox[number] = new Label();
            this.noteBox[number].Name = "e" + number;
            this.noteBox[number].Text = text;
            this.noteBox[number].Font = new Font("Segoe UI", 14, FontStyle.Regular);
            this.noteBox[number].Location = new Point(this.locationXLabel, this.locationYLabel);
            this.noteBox[number].Width = 750;

            Regex regex = new Regex(@"\n");
            MatchCollection matches = regex.Matches(text);
            this.noteBox[number].Height = 40 + matches.Count * 35;

            this.form.Controls.Add(this.noteBox[number]);
            this.locationYLabel += this.noteBox[number].Height + 20;
            if (number > 3)
            {
                this.form.Height = this.noteBox[number].Bottom + 100;
            }
            this.labelCount++;
        }

        private void titleGenerate()
        {
            Label title = new Label();
            title.Text = this.date.ToString("D", CultureInfo.CurrentCulture);
            title.Width = 300;
            title.Height = 30;
            title.Location = new Point(300, 20);
            this.form.Width = 850;
            title.Font = new Font("Arial", 16, FontStyle.Bold);
            this.form.Controls.Add(title);
        }

    }
}
