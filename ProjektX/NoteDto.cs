using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace ProjektX
{
    public class NoteDto
    {
        public DateTime date;
        public string note;

        public NoteDto() { }

        public NoteDto(DateTime date, string note)
        {
            this.date = date;
            this.note = note;
        }

        public object Clone()
        {
            return new NoteDto(date, note);
        }
    }
}
