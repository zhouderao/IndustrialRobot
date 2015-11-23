using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Windows.Forms;
using System.ComponentModel;

using System.IO;



namespace IndustrialRobot
{
    class APTtoJBI
    {

       public string  Apt2JbiFirst(string fileName)
       {
            Txt2List txt2list=new Txt2List ();
            List2Txt list2txt=new List2Txt ();

            List<string> tmplist = new List<string>();
       
         
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            StreamReader sr = new StreamReader(fs);

            sr.BaseStream.Seek(0, SeekOrigin.Begin);

            string tmp = sr.ReadLine();

            while (tmp != null)
            {
                
                    if (tmp.StartsWith( "GOTO") || tmp.StartsWith( "CIRCLE"))
                    {
                        tmplist.Add(tmp);
                    }             
               
             tmp = sr.ReadLine();

            }

            
           sr.Close();
            fs.Close();
           string tmpjbistr="d:middleTranslation1.txt";
            list2txt.ListToTxtFile(tmplist,tmpjbistr );

            return tmpjbistr;
       }



       public List<string> Apt2Jbi_math(string fileName,float toollength ,
           float a ,float b ,float c ,float toolV,int bol)
       {
           Txt2List txt2list=new Txt2List ();
            List2Txt list2txt=new List2Txt ();

           BasicData basicData=new BasicData();                     

             List<string> tmplist = new List<string>();

             List<string> tmplistJbi = new List<string>();

             List<string> tmplistJbi_down = new List<string>();


          tmplist= txt2list.ReadTxtToList(fileName);

             int i=0;
             int j = 0;
             string tmpNext = null;
             string tmpNexttr = null;
             for (i = 0; i < tmplist.Count; i++)
             {

                 string tmpNow = tmplist[i];

                 if (i == tmplist.Count - 1)                 
                     tmpNext ="Nothing";
                 
                 else
                 tmpNext = tmplist[i + 1];
                 
                 if (i >= tmplist.Count -3)
                     tmpNexttr = "Nothing";

                 else
                 tmpNexttr = tmplist[i + 3];

                 if (tmpNow.StartsWith("GOTO"))
                 {
                     if (tmpNext.StartsWith("CIRCLE"))
                     {
                         float[][]  allNumbers= GotoCircleToMovl(tmpNow,tmpNext,tmplist [i+2]);

                         float[] pxyz = basicData.SolvingPxyzFromXyz(allNumbers[0][0],allNumbers [0][1],allNumbers [0][2],a,b,c,toollength,bol );

                         float[] locationP0and1 = basicData.InverseSolution(pxyz[0], pxyz[1], pxyz[2], bol);


                         float[] pxyz2 = basicData.SolvingPxyzFromXyz(allNumbers[3][0], allNumbers[3][1], allNumbers[3][2],a,b,c, toollength,bol);

                         float[] locationP2 = basicData.InverseSolution(pxyz2[0], pxyz2[1], pxyz2[2], bol);


                         float[] pxyz3 = basicData.SolvingPxyzFromXyz(allNumbers[2][0], allNumbers[2][1], allNumbers[2][2],a,b,c, toollength,bol);

                         float[] locationP3 = basicData.InverseSolution(pxyz3[0], pxyz3[1], pxyz3[2], bol);




                         string tmpLocationP0and1 = '=' + locationP0and1[6].ToString() + ',' + locationP0and1[7].ToString()
                                            + ',' + locationP0and1[8].ToString() + ',' + locationP0and1[9].ToString()
                                            + ',' + locationP0and1[10].ToString() + ',' + locationP0and1[11].ToString();

                         string tmpLocationP2 = '=' + locationP2[6].ToString() + ',' + locationP2[7].ToString()
                                              + ',' + locationP2[8].ToString() + ',' + locationP2[9].ToString()
                                              + ',' + locationP2[10].ToString() + ',' + locationP2[11].ToString();

                         string tmpLocationP3 = '=' + locationP3[6].ToString() + ',' + locationP3[7].ToString()
                                              + ',' + locationP3[8].ToString() + ',' + locationP3[9].ToString()
                                              + ',' + locationP3[10].ToString() + ',' + locationP3[11].ToString();




                         string tmpJbi_up_zero = "C" + string.Format("{0:00000}", j) + tmpLocationP0and1;
                         string tmpJbi_down_zero = "MOVL C" + string.Format("{0:00000}", j) + " V="+toolV.ToString ()+" PL=0";

                         j++;

                         string tmpJbi_up_one = "C" + string.Format("{0:00000}", j) + tmpLocationP0and1;
                         string tmpJbi_down_one = "MOVC C" + string.Format("{0:00000}", j) + " V=" + toolV.ToString() + " PL=0";

                         j++;

                         string tmpJbi_up_two = "C" + string.Format("{0:00000}", j) + tmpLocationP2;
                         string tmpJbi_down_two = "MOVC C" + string.Format("{0:00000}", j) + " V=" + toolV.ToString() + " PL=0";

                         j++;

                         string tmpJbi_up_three = "C" + string.Format("{0:00000}", j) + tmpLocationP3;
                         string tmpJbi_down_three = "MOVC C" + string.Format("{0:00000}", j) + " V=" + toolV.ToString() + " PL=0";



                         tmplistJbi.Add(tmpJbi_up_zero);
                         tmplistJbi_down.Add(tmpJbi_down_zero);                                                

                         tmplistJbi.Add(tmpJbi_up_one);
                         tmplistJbi_down.Add(tmpJbi_down_one);

                         tmplistJbi.Add(tmpJbi_up_two);
                         tmplistJbi_down.Add(tmpJbi_down_two);

                         tmplistJbi.Add(tmpJbi_up_three);
                         tmplistJbi_down.Add(tmpJbi_down_three);

                         if (tmpNexttr.StartsWith("CIRCLE"))
                         {

                             // i++;
                             i++;
                             j++;
                         }
                         else
                         {
                             i++;
                             i++;
                             j++;
                         }
                     }
                     else
                     {

                         float[] xyz = GotoLinesToMovl(tmpNow);

                         float[] pxyz = basicData.SolvingPxyzFromXyz(xyz[0], xyz[1], xyz[2],a,b,c, toollength,bol);

                         float[] locationP = basicData.InverseSolution(pxyz[0], pxyz[1], pxyz[2], bol);


                         string tmpLocationP = '=' + locationP[6].ToString() + ',' + locationP[7].ToString()
                                             + ',' + locationP[8].ToString() + ',' + locationP[9].ToString()
                                             + ',' + locationP[10].ToString() + ',' + locationP[11].ToString();

                         string tmpJbi_up = "C"+string.Format("{0:00000}", j) + tmpLocationP;
                         string tmpJbi_down = "MOVL C" + string.Format("{0:00000}", j) + " V=" + toolV.ToString() + " PL=0";

                         tmplistJbi.Add(tmpJbi_up);
                         tmplistJbi_down.Add(tmpJbi_down);
                         j++;

                     }

                 }


             }


           tmplistJbi.AddRange(tmplistJbi_down);

           return tmplistJbi;

       
       }


        
    public float[]  GotoLinesToMovl(string tmp)
    {
        char[] tmpch=new char[tmp.Length];
        tmpch = tmp.ToCharArray();

      float fl_x, fl_y, fl_z;

      char[] ch_x = new char[(int)(tmp.Length / 3)];
      char[] ch_y = new char[(int)(tmp.Length / 3)];
      char[] ch_z = new char[(int)(tmp.Length / 3)];

         //char[] ch_x;
         //char[] ch_y;
         //char[] ch_z;

       
        int i,j,k,firstDot,secondDot;
         
        firstDot=tmp.IndexOf(',',5);
        secondDot = tmp.IndexOf(',', firstDot+1);

        for(i=5;i<firstDot;i++)
        {
           char ch= tmpch[i];

            if((ch>=48&&ch<=57)||ch==46||ch==45)
            {
                ch_x[i-5] = ch;
                
             }
             
       }


        for (j = firstDot+1; j < secondDot; j++)
        {
           char ch = tmpch[j];

            if ((ch >= 48 && ch <= 57) || ch == 46 || ch == 45)
            {
                ch_y[j-firstDot-1] = ch;

            }

        }

        for (k = secondDot+1; k < tmp.Length; k++)
        {
          char  ch = tmpch[k];

            if ((ch >= 48 && ch <= 57) || ch == 46 || ch == 45)
            {
                ch_z[k - secondDot - 1] = ch;

            }
            else break;

        }

       

        fl_x =float.Parse(ch2str (ch_x));
        fl_y = float.Parse(ch2str(ch_y));
        fl_z = float.Parse(ch2str(ch_z));

        float[]  xyz={fl_x,fl_y,fl_z};

        return xyz;

    
    
    }

      //GotoCircleToMovj 的返回值是二维数组，第一行是goto的xyz值，第二行是circle的xyzijkr的值
      //第三行是circle下一行goto的xyz值,第四行是c点的xyz值
    public float[][] GotoCircleToMovl(string tmpgoto, string tmpcircle, string tmpnextgoto)
    {
        float[] gotoxyz = GotoLinesToMovl(tmpgoto);
        float[] nextgotoxyz = GotoLinesToMovl(tmpnextgoto );
        float[] circlexyzijk = numbersFromCircle(tmpcircle );

        float[] T = new float[3];
        float[] AB = new float[3];


       AB[0]= nextgotoxyz [0]-gotoxyz [0];
       AB[1]=nextgotoxyz [1]-gotoxyz [1];
       AB[2] = nextgotoxyz[2] - gotoxyz[2];



        if (circlexyzijk[5] > 0)
        {
            T[0] = circlexyzijk[1] - gotoxyz[1];
            T[1] = gotoxyz[0] - circlexyzijk[0];
            T[2] = 0;
        }
        else if (circlexyzijk[5] < 0)
        {
            T[0] = -circlexyzijk[1] + gotoxyz[1];
            T[1] =- gotoxyz[0] +circlexyzijk[0];
            T[2] = 0;        
        }

        float cosTheta = AB[0] * T[0] + AB[1] * T[1];

        float[] D = new float[3];
        D[0]=(float )((gotoxyz[0]+nextgotoxyz[0])*0.5);
        D[1] = (float)((gotoxyz[1] + nextgotoxyz[1]) * 0.5);
        D[2] = gotoxyz[2] ;

        float[] C = new float[3];

        if (cosTheta < 0)
        {
            C[0] =(float )( circlexyzijk[0] + circlexyzijk[6] * (D[0] - circlexyzijk[0])
                / Math.Sqrt(Math.Pow((D[0] - circlexyzijk[0]), 2) + Math.Pow((D[1] - circlexyzijk[1]), 2)));

            C[1] = (float)(circlexyzijk[1] + circlexyzijk[6] * (D[1] - circlexyzijk[1])
                / Math.Sqrt(Math.Pow((D[0] - circlexyzijk[0]), 2) + Math.Pow((D[1] - circlexyzijk[1]), 2)));
           
            C[2] = gotoxyz[2];
        
        }
        else if (cosTheta > 0)
        {
            C[0] = (float)(circlexyzijk[0] + circlexyzijk[6] * (-D[0] + circlexyzijk[0])
                    / Math.Sqrt(Math.Pow((D[0] - circlexyzijk[0]), 2) + Math.Pow((D[1] - circlexyzijk[1]), 2)));

            C[1] = (float)(circlexyzijk[1] + circlexyzijk[6] * (-D[1] +circlexyzijk[1])
                / Math.Sqrt(Math.Pow((D[0] - circlexyzijk[0]), 2) + Math.Pow((D[1] - circlexyzijk[1]), 2)));

            C[2] = gotoxyz[2];
        
        }

        float[][] allNumbers={gotoxyz,circlexyzijk ,nextgotoxyz ,C};

        //allNumbers[0][]=gotoxyz ; 


        return allNumbers ;
    }

    public float[] numbersFromCircle(string tmpstr)
    { 
    
    char[] tmpch=new char[tmpstr.Length];
        tmpch = tmpstr.ToCharArray();

      float fl_x, fl_y, fl_z,fl_i,fl_j,fl_k,fl_r;

          char[] ch_x = new char[10];
          char[] ch_y = new char[10];
          char[] ch_z = new char[10];
          char[] ch_i = new char[10];
          char[] ch_j = new char[10];
          char[] ch_k = new char[10];
          char[] ch_r = new char[10];



        int i,j,k,firstDot,secondDot,thirdDot,fourthDot,fifthDot,sixthDot;
         
        firstDot=tmpstr.IndexOf(',',5);
        secondDot = tmpstr.IndexOf(',', firstDot+1);
        thirdDot=tmpstr.IndexOf(',',secondDot +1);
        fourthDot = tmpstr.IndexOf(',', thirdDot+1);
        fifthDot=tmpstr.IndexOf(',',fourthDot+1);
        sixthDot = tmpstr.IndexOf(',', fifthDot+1);


        for(i=7;i<firstDot;i++)
        {
           char ch= tmpch[i];

            if((ch>=48&&ch<=57)||ch==46||ch==45)
            {
                ch_x[i-7] = ch;
                
             }             
       }


        for (j = firstDot+1; j < secondDot; j++)
        {
           char ch = tmpch[j];

            if ((ch >= 48 && ch <= 57) || ch == 46 || ch == 45)
            {
                ch_y[j-firstDot-1] = ch;

            }
        }

        for (k = secondDot+1; k <thirdDot ; k++)
        {
          char  ch = tmpch[k];

            if ((ch >= 48 && ch <= 57) || ch == 46 || ch == 45)
            {
                ch_z[k - secondDot - 1] = ch;

            }           
        }


  for (j = thirdDot+1; j < fourthDot; j++)
        {
           char ch = tmpch[j];

            if ((ch >= 48 && ch <= 57) || ch == 46 || ch == 45)
            {
                ch_i[j-thirdDot-1] = ch;

            }
        }

        for (k = fourthDot+1; k <fifthDot ; k++)
        {
          char  ch = tmpch[k];

            if ((ch >= 48 && ch <= 57) || ch == 46 || ch == 45)
            {
                ch_j[k - fourthDot - 1] = ch;

            }           
        }

          for (j = fifthDot+1; j < sixthDot; j++)
        {
           char ch = tmpch[j];

            if ((ch >= 48 && ch <= 57) || ch == 46 || ch == 45)
            {
                ch_k[j-fifthDot-1] = ch;

            }

        }

        for (k = sixthDot+1; k <tmpstr.Length  ; k++)
        {
          char  ch = tmpch[k];

            if ((ch >= 48 && ch <= 57) || ch == 46 || ch == 45)
            {
                ch_r[k - sixthDot - 1] = ch;

            } 
            else break;

        }
       

        fl_x =float.Parse(ch2str (ch_x));
        fl_y = float.Parse(ch2str(ch_y));
        fl_z = float.Parse(ch2str(ch_z));
        fl_i =float.Parse(ch2str (ch_i));
        fl_j = float.Parse(ch2str(ch_j));
        fl_k = float.Parse(ch2str(ch_k));
        fl_r =float.Parse(ch2str (ch_r));


        float[] xyzijk = { fl_x, fl_y, fl_z, fl_i, fl_j, fl_k, fl_r };

        return xyzijk;
    
    
   
    }
    public string ch2str(char[] ch)
    {
        string tmp = "";
        for (int i = 0;i<ch.Length ; i++)
        {
            tmp = tmp + ch[i];
        
        }

            return tmp;
    
    }


    public string writingJBItoText(List <string>tmplist)
    {
        List2Txt list2txt=new List2Txt ();



       //list2txt.ListToTxtFile(tmplist, "D;middleTranslation2.txt");
       

        FileStream fsbegin = new FileStream("d:middleTranslation2.txt", FileMode.Create, FileAccess.Write);

        StreamWriter swbegin = new StreamWriter(fsbegin);

        swbegin.Flush();

        swbegin.BaseStream.Seek(0, SeekOrigin.Begin);

        swbegin.WriteLine("/JOB");
        swbegin.WriteLine("//NAME ***");
        swbegin.WriteLine("//POS");
        swbegin.WriteLine("///NPOS " + ((int)(tmplist.Count / 2+2)).ToString() + ",0,0,0,0,0");
        swbegin.WriteLine("///TOOL 5");
        swbegin.WriteLine("///POSTYPE PULSE");
        swbegin.WriteLine("///PULSE");
        

        for (int i = 0; i < tmplist.Count; i++)
        {
            if (i == (int)tmplist.Count / 2)
            {
                
                swbegin.WriteLine("//INST");
                swbegin.WriteLine("///DATE " + DateTime.Now.ToString("yyyy/MM/dd  HH:mm"));
                swbegin.WriteLine("///ATTR SC,RW");
                swbegin.WriteLine("///GROUP1 RB2");
                swbegin.WriteLine("NOP");
            }
            swbegin.WriteLine(tmplist[i]);
        }

        swbegin.WriteLine("END");

        swbegin.Flush();
        swbegin.Close();
        fsbegin.Close();

        
        string str="d:middleTranslation2.txt";
          return  str;
    
    
    
    }

    
    }
}
