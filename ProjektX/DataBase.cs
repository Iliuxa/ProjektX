using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ProjektX
{
    public class DataBase
    {
        public NoteDto[] note = new NoteDto[1000];
        public int noteLength;
        public int noteLengthMonth = 0;

        private string patchDb = "D:\\Internet Exploer\\DataBaseProjektX.txt";

        public DataBase()
        {
            FileStream? fstream = null;
            try
            {
                fstream = new FileStream(patchDb, FileMode.OpenOrCreate);
                byte[] buffer = Encoding.Default.GetBytes("{{" + new DateTime().ToString("d") + "}}");
                fstream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Ошибка("); //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!xuinya
            }
            finally
            {
                fstream?.Close();
            }
        }

        // Получает все данные из файла
        public void getData()
        {
            NoteDto[] result = new NoteDto[1000];
            this.readFile();
        }

        // Получает конкретный месяц
        public NoteDto[] getDataMonth(DateTime date)
        {
            NoteDto[] resultDto = new NoteDto[45];
            this.noteLengthMonth = 0;
            for (int i = 0; i < this.noteLength; i++)
            {
                if (this.note[i].date > date.AddDays(-6) && this.note[i].date < date.AddDays(45))
                {
                    resultDto[this.noteLengthMonth] = this.note[i];
                    this.noteLengthMonth++;
                }
            }
            return resultDto;
        }

        // Получает все данные из файла
        public NoteDto[] readFile()
        {
            this.noteLength = 0;
            Regex regex = new Regex(@"{{(.*)}}");
            MatchCollection matches;

            using (StreamReader reader = new StreamReader(patchDb))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    this.note[this.noteLength] = new NoteDto();
                    matches = regex.Matches(line);
                    if (matches.Count > 0)
                    {
                        if (this.noteLength != 0 && this.note[this.noteLength - 1].note.Length > 2)
                        {
                            this.note[this.noteLength - 1].note = this.note[this.noteLength - 1].note.Substring(0, this.note[this.noteLength - 1].note.Length - 2);
                        }

                        line = line.Substring(2);
                        this.note[this.noteLength].date = DateTime.ParseExact(line.Substring(0, line.Length - 2), "d", null);
                        this.noteLength++;
                    }
                    else
                    {
                        this.note[this.noteLength - 1].note += line + "\n";
                    }
                }
                reader.Close();
            }

            return this.note;
        }

        public void persist(NoteDto newNote)
        {
            bool flagEq = false;
            for (int i = 0; i < this.noteLength; i++)
            {
                if (this.note[i].date == newNote.date)
                {
                    this.note[i].note = newNote.note;
                    flagEq = true;
                    break;
                }
            }

            if (!flagEq) //добавить новую дату
            {
                this.noteLength++;
                if (this.noteLength == 0)
                {
                    this.note[this.noteLength - 1] = newNote;
                }
                else
                {
                    this.note[this.noteLength - 1] = newNote;
                    this.sortDate(this.note[this.noteLength - 1]);
                }
                
            }

        }

        public async void flush()
        {
            string text = "";

            for (int i = 0; i < this.noteLength; i++)
            {
                text += "{{" + this.note[i].date.ToString("d") + "}}" + "\n";
                text += this.note[i].note + "\n";
            }

            using (FileStream fstream = new FileStream(patchDb, FileMode.Truncate))
            {
                byte[] buffer = Encoding.Default.GetBytes(text);
                await fstream.WriteAsync(buffer, 0, buffer.Length);
                fstream.Close();
            }
        }

        private void sortDate(NoteDto currentNote)
        {
            int num = 0;
            bool flag = false;
            NoteDto changeNote = currentNote;
            for (int i = 0; i < this.noteLength; i++)
            {
                if (this.note[i].date > currentNote.date)
                {
                    num = i;
                    flag = true;
                    break;
                }
            }

            if (flag)
            {
                for (int i = num; i < this.noteLength; i++)
                {
                    NoteDto x = (NoteDto)this.note[i].Clone();
                    note[i] = (NoteDto)changeNote.Clone();
                    changeNote = (NoteDto)x.Clone(); ;
                }
            }

        }

    }
}