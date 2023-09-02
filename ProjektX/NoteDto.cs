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
        public string? color;

        public NoteDto() { }

        public NoteDto(DateTime date, string note)
        {
            this.date = date;
            this.note = note;
        }

        public NoteDto(DateTime date, string note, string? color)
        {
            this.date = date;
            this.note = note;
            this.color = color;
        }
        public object Clone()
        {
            return new NoteDto(date, note, color);
        }

        public Color getRealColor(string fakeColor)
        {
            switch (fakeColor)
            {
                case "Red":
                    return Color.Red;
                case "Yellow":
                    return Color.Yellow;
                case "HotPink":
                    return Color.HotPink;
                case "GreenYellow":
                    return Color.GreenYellow;
                case "Gray":
                    return Color.Gray;
                case "White":
                    return Color.White;
                    default:
                    MessageBox.Show("Что то не так с цветом, обратитесь к Илюхе", "Ошибка");
                    return Color.White;
            }
        }

    }
}
