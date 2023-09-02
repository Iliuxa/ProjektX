using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektX
{
    public partial class FormColor : Form
    {
        public Button[] color = new Button[6];
        public Button buttonDay;
        public DateTime date;
        public GenerateBaseDesign generate;

        private const int colorSize = 180;
        private DataBase db;
        private int locationX = 0;
        private int locationY = 0;

        public FormColor(string date, DataBase db, GenerateBaseDesign generate)
        {
            InitializeComponent();
            this.db = db;
            this.generate = generate;
            this.date = DateTime.ParseExact(date, "d", null);
            this.generateColor();
        }

        // Клик на цвет
        public void colorClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string color = button.Name;
            for (int i = 0; i < db.noteLength; i++)
            {
                if (db.note[i].date == this.date)
                {
                    db.note[i].color = color;
                    this.db.persist(db.note[i]);
                    this.db.flush();
                    generate.dayGenerate(generate.currentDate);
                    this.Close();
                    return;
                }
            }
            MessageBox.Show("Что то не получилось(", "Ошибка");
            this.Close();
        }

        private void generateColor()
        {
            this.Width = 3 * colorSize + 19;
            this.Height = 2 * colorSize + 48;

            for (int i = 0; i < color.Length; i++)
            {
                this.color[i] = new Button();
                this.color[i].Width = colorSize;
                this.color[i].Height = colorSize;
                this.color[i].Location = new Point(locationX, locationY);
                this.color[i].Click += this.colorClick;
                this.Controls.Add(color[i]);

                locationX += colorSize;
                if (i == 2)
                {
                    locationX = 0;
                    locationY += colorSize;
                }
            }

            this.color[0].BackColor = Color.Red;
            this.color[1].BackColor = Color.Yellow;
            this.color[2].BackColor = Color.HotPink;
            this.color[3].BackColor = Color.GreenYellow;
            this.color[4].BackColor = Color.Gray;
            this.color[5].BackColor = Color.White;

            this.color[0].Name = "Red";
            this.color[1].Name = "Yellow";
            this.color[2].Name = "HotPink";
            this.color[3].Name = "GreenYellow";
            this.color[4].Name = "Gray";
            this.color[5].Name = "White";

        }
    }
}
