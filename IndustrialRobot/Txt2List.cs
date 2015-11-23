using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace IndustrialRobot
{
    class Txt2List
    {


        public List<string> ReadTxtToList(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            List<string> list = new List<string>();

            StreamReader sr = new StreamReader(fs);

            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            string tmp = sr.ReadLine();

            while (tmp != null)
            {
                list.Add(tmp);
                tmp = sr.ReadLine();

            }

            sr.Close();
            fs.Close();

            return list;
        }
    
    }
}
