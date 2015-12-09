using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.classes
{
    class Record : IComparable 
    {
        public int CompareTo(object obj)
        {
            Record otherRecord = obj as Record;
            if (otherRecord != null)
            {
                if (this.points > otherRecord.points)
                    return -1;
                else
                    return 1;
                return 0;
                //return this.points.CompareTo(otherRecord.points);
            }
            else
                throw new ArgumentException("Object is not a Record");
        }

        public int points = 0;

        public string name=" ";
       

        public Record()
        { }

        public Record(string NameOfUser, int PointsOfUser)
        {
            name = NameOfUser;
            points = PointsOfUser;
        }

        
        public bool LoadFromString(string s)
        {
            string[] split = s.Split(new char[] { ',' });
            name = split[0];
            points = Convert.ToInt32(split[1]);

            if (name == "" || points < 100) return false; else return true;

        }

        public override string ToString()
        {

            return name+","+points.ToString();
        }
    }
}
