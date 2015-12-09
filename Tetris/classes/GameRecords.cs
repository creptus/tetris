using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Tetris.classes
{
    class GameRecords
    {
        private string pathToFile;
        private List<Record> records=new List<Record>();
        private int maxRecords = 9;

        public GameRecords(string pathToFileRecords)
        {
            pathToFile = pathToFileRecords;
            LoadFormFile();
        }

        private void LoadFormFile()
        {
            if (File.Exists(pathToFile))
            {
                string line;
                Record rec;
                StreamReader sr=new StreamReader(pathToFile);
                while ((line = sr.ReadLine()) != null)
                {
                    rec = new Record();
                    if (rec.LoadFromString(line))
                    {
                        records.Add(rec); 
                    }
                }
                sr.Close();
            }
        }

        public void Save()
        {
            int k = 0;
            StreamWriter sw = new StreamWriter(pathToFile);
            foreach (Record rec in records)
            {
                if (k == maxRecords) break;
                sw.WriteLine(rec.ToString());
                k++;
            }
            
            sw.Close();
        }

        public void Add(string name, int points)
        {
            if (name == "") name = " ";
            records.Add(new Record(name,points));
            records.Sort();
            if (records.Count > maxRecords) records.RemoveAt(records.Count-1);
        }

        public string[,] getList()
        {
            string[,] result=new string[2,records.Count];
            int k=0;
            records.Sort();
            foreach (Record rec in records)
            {
                result[0, k] = rec.name;
                result[1, k] = rec.points.ToString();
                k++;
            }
            return result;
        }

    }
}
