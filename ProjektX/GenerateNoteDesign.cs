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
        public Label emptyLabel = new Label();
        public Button[] editButton = new Button[10];
        public Button[] deleteButton = new Button[10];
        public Button saveButton = new Button();
        public Button addButton = new Button();
        public Button closeEditButton = new Button();
        public RichTextBox editBox = new RichTextBox();
        public string[] notes;
        private int noteId;
        private int labelCount = 0;
        private int locationXLabel = 125;
        private int locationYLabel = 60;
        private int locationXButton = 40;
        private int locationYButton = 60;
        const string addMode = "Add";
        public string mode = "Add";

        public GenerateNoteDesign(DataBase db, FormNote form)
        {
            this.db = db;
            this.form = form;
            this.date = DateTime.ParseExact(form.buttonDay.Name, "d", null);
        }

        public void baseGenerate()
        {
            bool isEmpty = true;
            this.locationYLabel = 60;
            this.form.Width = 850;
            this.form.Height = 500;
            this.titleGenerate();
            for (int i = 0; i < db.noteLength; i++)
            {
                if (db.note[i].date == this.date)
                {
                    this.noteGenerate(i);
                    isEmpty = false;
                    break;
                }
            }
            this.addSaveGenerate(isEmpty);
            this.saveButton.Visible = true;
            this.addButton.Visible = true;
            this.editBox.Visible = false;
        }

        public void getEditForm(Button button)
        {
            this.mode = "Add";
            string buttonName = button.Name == "Add" ? button.Name : button.Name.Remove(0, 1);
            if (buttonName != addMode)
            {
                this.mode = "Edit";
                noteBox[noteId].DataBindings.Clear();
                this.noteId = int.Parse(buttonName);
            }
            this.saveButton.Visible = false;
            this.addButton.Visible = false;
            this.emptyLabel.Visible = false;
            this.editBox.Visible = true;
            this.closeEditButton.Visible = true;

            editBox.Width = 750;
            editBox.Height = 150;
            int locY = this.labelCount == 0 ? 100 : noteBox[this.labelCount - 1].Bottom + 10;
            editBox.Location = new Point(this.locationXButton, locY);
            editBox.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            this.closeEditButton.Text = "Добавить";
            editBox.Text = "";
            //Привязка
            if (buttonName != addMode)
            {
                editBox.Text = this.noteBox[this.noteId].Text;
                noteBox[this.noteId].DataBindings.Add(new Binding("Text", editBox, "Text"));
                this.closeEditButton.Text = "Изменить";
            }

            this.closeEditButton.Name = "closeEdit";
            this.closeEditButton.Width = this.form.Width - 90;
            this.closeEditButton.Height = 45;
            this.closeEditButton.Font = new Font("Arial", 14, FontStyle.Regular);
            this.closeEditButton.Location = new Point(this.locationXButton, this.editBox.Bottom + 15);
            if (!this.form.isButtonCloseEditClick)
            this.closeEditButton.Click += this.form.buttonCloseEditClick;

            this.form.Controls.Add(this.closeEditButton);
            this.form.Controls.Add(this.editBox);
            this.form.Height = this.closeEditButton.Bottom + 60;
        }

        public NoteDto getEditData()
        {
            string note = "";
            for (int i = 0; i < this.labelCount; i++)
            {
                if (noteBox[i].Text != "")
                {
                    note += noteBox[i].Text + "&";
                }
            }
            note = note.Substring(0, note.Length - 1);
            return new NoteDto(this.date, note);
        }

        public void deleteNote(Button button)
        {
            int deleteId = int.Parse(button.Name.Substring(1));
            this.noteBox[deleteId].Dispose();
            this.editButton[deleteId].Dispose();
            this.deleteButton[deleteId].Dispose();
        }

        public void closeEditForm(Button button = null)
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
            if (this.mode == addMode)
            {
                this.getOneNote();
            }
            regenerateEditForm();
        }

        private void deleteBind()
        {
            for (int i = 0; i < labelCount; i++)
            {
                noteBox[i].DataBindings.Clear();
            }
        }

        private void getOneNote()
        {
            this.noteBoxGenerate(editBox.Text, this.labelCount);
            this.buttonGenerate(this.labelCount - 1);
        }

        private void regenerateEditForm()
        {
            this.saveButton.Enabled = true;

            this.locationYLabel = 60;
            for (int i = 0; i < this.labelCount; i++)
            {
                this.noteBox[i].Location = new Point(this.locationXLabel, this.locationYLabel);
                Regex regex = new Regex(@"\n");
                MatchCollection matches = regex.Matches(this.noteBox[i].Text);
                this.noteBox[i].Height = 40 + matches.Count * 35;
                this.locationYLabel += this.noteBox[i].Height + 20;

                this.editButton[i].Location = new Point(locationXButton, this.noteBox[i].Top + this.noteBox[i].Height / 2 - 25);
                this.deleteButton[i].Location = new Point(locationXButton + 45, this.noteBox[i].Top + this.noteBox[i].Height / 2 - 25);
            }
            this.addButton.Location = new Point(locationXButton, this.editButton[this.labelCount - 1].Bottom + 30);
            this.saveButton.Location = new Point(locationXButton + this.addButton.Width + 5, this.editButton[this.labelCount - 1].Bottom + 30);
            this.form.Height = this.saveButton.Bottom + 60;
        }

        private void addSaveGenerate(bool isEmpty = false)
        {
            this.emptyLabel.Text = "Пока заметок нет(";
            this.emptyLabel.Font = new Font("Arial", 15, FontStyle.Bold);
            this.emptyLabel.Location = new Point(this.locationXLabel, 100);
            this.emptyLabel.Size = new Size(500, 50);
            this.form.Controls.Add(this.emptyLabel);

            int locationXSave = this.locationXButton;

            this.addButton.Width = this.form.Width / 2 - 60;
            this.addButton.Height = 45;
            this.addButton.Font = new Font("Arial", 14, FontStyle.Regular);
            this.addButton.Click += this.form.buttonEditClick;
            locationXSave += this.addButton.Width + 5;

            this.saveButton.Width = this.form.Width / 2 - 60;
            this.saveButton.Height = 45;
            this.saveButton.Font = new Font("Arial", 14, FontStyle.Regular);
            this.saveButton.Click += this.form.buttonSaveClick;
            this.saveButton.Enabled = true;

            if (isEmpty)
            {
                this.addButton.Location = new Point(this.locationXButton, this.form. Bottom - 60);
                this.saveButton.Location = new Point(this.locationXButton + this.addButton.Width + 5, this.form.Bottom - 60);
                this.saveButton.Enabled = false;
                this.emptyLabel.Visible = true;
            }
            else
            {
                this.addButton.Location = new Point(this.locationXButton, this.editButton[this.labelCount - 1].Bottom + 30);
                this.saveButton.Location = new Point(this.locationXButton + this.addButton.Width + 5, this.editButton[this.labelCount - 1].Bottom + 30);
                this.emptyLabel.Visible = false;
            }

            this.form.Controls.Add(addButton);
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

            this.deleteButton[number] = new Button();
            this.deleteButton[number].Name = "d" + number;
            this.deleteButton[number].Image = System.Drawing.Image.FromFile("D:\\Internet Exploer\\ProjektX\\ProjektX\\i.png");
            this.deleteButton[number].ImageAlign = ContentAlignment.MiddleCenter;
            this.deleteButton[number].Width = 40;
            this.deleteButton[number].Height = 40;
            this.deleteButton[number].Location = new Point(locationXButton + 45, this.noteBox[number].Top + this.noteBox[number].Height / 2 - 25);
            this.deleteButton[number].Click += this.form.buttonDeleteClick;
            this.form.Controls.Add(this.deleteButton[number]);
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
