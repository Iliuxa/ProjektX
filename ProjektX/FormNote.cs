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
    public partial class FormNote : Form
    {
        private GenerateNoteDesign generate;
        private DataBase db;
        public Button buttonDay;

        public FormNote(Button button, DataBase db)
        {
            InitializeComponent();
            buttonDay = button;
            this.db = db;
            this.generate = new GenerateNoteDesign(this.db, this);
            this.generate.baseGenerate();
        }

        // Редактировать
        public void buttonEditClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            this.generate.getEditForm(button);
        }

    }
}
