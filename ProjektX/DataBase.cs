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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Ошибка(");
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
    }
}