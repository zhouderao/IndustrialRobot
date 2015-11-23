using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO ;

namespace IndustrialRobot
{
    class List2Txt
    {

        public void ListToTxtFile(List<string> list, string fileName)
        {

            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);

            StreamWriter sw = new StreamWriter(fs);


            sw.Flush();

            sw.BaseStream.Seek(0, SeekOrigin.Begin);

            for (int i = 0; i < list.Count; i++)
                sw.WriteLine(list[i]);

            sw.Flush();
            sw.Close();
            fs.Close();
        }
    }
}
