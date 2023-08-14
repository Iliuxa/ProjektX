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
    internal class DataBase
    {
        public NoteDto note;

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

        public NoteDto[] getData(DateTime date)
        { 
            NoteDto[] result = new NoteDto[1000];
            return readFile(date);
        }

        private NoteDto[] readFile(DateTime date)
        {
            NoteDto[] result = new NoteDto[1000];

            Regex regex = new Regex(@"{{(\w*)}}");
            MatchCollection matches;

            using (StreamReader reader = new StreamReader(patchDb))
            {
                string? line;
                int i = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    result[i] = new NoteDto();
                    matches = regex.Matches(line);
                    if (matches.Count > 0)
                    {
                        line = line.Substring(2);
                        result[i].date = line.Substring(0, line.Length - 2);      
                        i++;
                    }
                    else
                    {
                        result[i - 1].note += line + "\n";
                    }
                }
                reader.Close();
            }

            return result;
        }
    }
}
//string path = "note1.txt";
//string text = "Hello World\nHello METANIT.COM";

//// полная перезапись файла 
//using (StreamWriter writer = new StreamWriter(path, false))
//{
//    await writer.WriteLineAsync(text);
//}
//// добавление в файл
//using (StreamWriter writer = new StreamWriter(path, true))
//{
//    await writer.WriteLineAsync("Addition");
//    await writer.WriteAsync("4,5");
//}
