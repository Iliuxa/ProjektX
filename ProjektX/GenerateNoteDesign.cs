using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ProjektX
{
    internal class GenerateNoteDesign
    {
        public FormNote form;
        public DataBase db;
        private DateTime date;
        public Label[] noteBox = new Label[10];
        public Button[] editButton = new Button[10];
        public Button saveButton = new Button();
        public Button addButton = new Button();
        public Button closeEditButton = new Button();
        public RichTextBox editBox = new RichTextBox();
        public string[] notes;
        private int noteId;
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
            this.locationYLabel = 60;
            this.form.Width = 850;
            this.form.Height = 500;
            this.titleGenerate();
            for (int i = 0; i < db.noteLength; i++)
            {
                if (db.note[i].date == this.date)
                {
                    this.noteGenerate(i);
                    break;
                }
            }
            this.addSaveGenerate();
            this.saveButton.Visible = true;
            this.addButton.Visible = true;
            this.editBox.Visible = false;
        }

        public void getEditForm(Button button)
        {
            noteBox[noteId].DataBindings.Clear();
            this.saveButton.Visible = false;
            this.addButton.Visible = false;
            this.editBox.Visible = true;
            this.closeEditButton.Visible = true;

            this.noteId = int.Parse(button.Name.Remove(0, 1));

            editBox.Width = noteBox[0].Width;
            editBox.Height = 150;
            editBox.Location = new Point(this.locationXButton, noteBox[this.labelCount - 1].Bottom + 10);
            editBox.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            editBox.Text = this.noteBox[this.noteId].Text;

            //Привязка
            noteBox[this.noteId].DataBindings.Add(new Binding("Text", editBox, "Text"));

            this.closeEditButton.Text = "Изменить";
            this.closeEditButton.Name = "closeEdit";
            this.closeEditButton.Width = this.form.Width - 90;
            this.closeEditButton.Height = 45;
            this.closeEditButton.Font = new Font("Arial", 14, FontStyle.Regular);
            this.closeEditButton.Location = new Point(this.locationXButton, this.editBox.Bottom + 15);
            this.closeEditButton.Click += this.form.buttonCloseEditClick;

            this.form.Controls.Add(this.closeEditButton);
            this.form.Controls.Add(this.editBox);
            this.form.Height = this.closeEditButton.Bottom + 60;
        }

        public void closeEditForm()
        {
            for (int i = 0; i < this.labelCount; i++)
            {
                this.noteBox[i].Text = this.noteBox[i].Text;
            }
            this.deleteBind(); 
            this.saveButton.Visible = true;
            this.addButton.Visible = true;
            this.editBox.Visible = false;
            this.closeEditButton.Visible = false;
            regenerateEditForm();
        }

        private void deleteBind()
        {
            for (int i = 0; i < labelCount; i++)
            {
                noteBox[i].DataBindings.Clear();
            }
        }

        private void regenerateEditForm()
        {
            this.locationYLabel = 60;
            for (int i = 0; i < this.labelCount; i++)
            {
                this.noteBox[i].Location = new Point(this.locationXLabel, this.locationYLabel);
                Regex regex = new Regex(@"\n");
                MatchCollection matches = regex.Matches(this.noteBox[i].Text);
                this.noteBox[i].Height = 40 + matches.Count * 35;
                this.locationYLabel += this.noteBox[i].Height + 20;

                this.editButton[i].Location = new Point(locationXButton, this.noteBox[i].Top + this.noteBox[i].Height / 2 - 25);
            }
            this.addButton.Location = new Point(locationXButton, this.editButton[this.labelCount - 1].Bottom + 30);
            this.saveButton.Location = new Point(locationXButton + this.addButton.Width + 5, this.editButton[this.labelCount - 1].Bottom + 30);
            this.form.Height = this.saveButton.Bottom + 60;
        }

        private void addSaveGenerate()
        {
            int locationXSave = this.locationXButton;

            this.addButton.Width = this.form.Width / 2 - 60;
            this.addButton.Height = 45;
            this.addButton.Location = new Point(locationXSave, this.editButton[this.labelCount - 1].Bottom + 30);
            this.addButton.Font = new Font("Arial", 14, FontStyle.Regular);
            this.addButton.Click += this.form.buttonEditClick;
            this.form.Controls.Add(addButton);
            locationXSave += this.addButton.Width + 5;

            this.saveButton.Width = this.form.Width / 2 - 60;
            this.saveButton.Height = 45;
            this.saveButton.Location = new Point(locationXSave, this.editButton[this.labelCount - 1].Bottom + 30);
            this.saveButton.Font = new Font("Arial", 14, FontStyle.Regular);
            this.saveButton.Click += this.form.buttonSaveClick;
            this.form.Controls.Add(saveButton);

            addButton.Text = "Добавить";
            saveButton.Text = "Сохранить";
            addButton.Name = "Add";
            saveButton.Name = "Save";

            this.form.Height = this.saveButton.Bottom + 60;
        }

        private void noteGenerate(int i)
        {
            this.labelCount = 0;
            this.notes = db.note[i].note.Split(new char[] { '&' });
            for (int k = 0; k < notes.Length; k++)
            {
                this.noteBoxGenerate(notes[k], k);
                this.buttonGenerate(k);
            }
        }

        private void buttonGenerate(int number)
        {
            this.editButton[number] = new Button();
            this.editButton[number].Name = "e" + number;
            this.editButton[number].Image = System.Drawing.Image.FromFile("D:\\Internet Exploer\\ProjektX\\ProjektX\\x.png");
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
