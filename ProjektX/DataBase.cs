using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektX
{
    internal class DataBase
    {
        private string patchDb = "D:\\Internet Exploer\\DataBaseProjektX.txt";

        public DataBase()
        {

        }

        public string[,] getData(DateTime date)
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


            string[,] result = new string[40, 2];
            return result;
        }
    }
}
