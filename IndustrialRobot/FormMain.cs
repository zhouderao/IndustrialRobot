using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;


namespace IndustrialRobot
{
    public partial class FormMain : Form
    {
        private BasicData basicdata = new BasicData();

        public FormMain()
        {
            InitializeComponent();
        }

        private void tpkma_Click(object sender, EventArgs e)
        {
        }

        private void btnNewStart_Click(object sender, EventArgs e)
        {
            this.txbax.Text = "";
            this.txbay.Text = "";
            this.txbaz.Text = "";

            this.txbPx.Text = "";
            this.txbPy.Text = "";
            this.txbPz.Text = "";

            this.txbTh1.Text = "";
            this.txbTh2.Text = "";
            this.txbTh3.Text = "";
            this.txbTh4.Text = "";
            this.txbTh5.Text = "";
            this.txbTh6.Text = "";

            this.txbP1.Text = "";
            this.txbP2.Text = "";
            this.txbP3.Text = "";
            this.txbP4.Text = "";
            this.txbP5.Text = "";
            this.txbP6.Text = "";
        }

        private void cmbMachineSurface_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txbax.Text = "";
            this.txbay.Text = "";
            this.txbaz.Text = "";

            this.txbPx.Text = "";
            this.txbPy.Text = "";
            this.txbPz.Text = "";

            this.txbTh1.Text = "";
            this.txbTh2.Text = "";
            this.txbTh3.Text = "";
            this.txbTh4.Text = "";
            this.txbTh5.Text = "";
            this.txbTh6.Text = "";

            this.txbP1.Text = "";
            this.txbP2.Text = "";
            this.txbP3.Text = "";
            this.txbP4.Text = "";
            this.txbP5.Text = "";
            this.txbP6.Text = "";
        }

        private void btnParameterDesignFinish_Click(object sender, EventArgs e)
        {
            if (this.txbpda.Text != "" && this.txbpdb.Text != "" && this.txbpdc.Text != ""
                && this.txbpdToolLength.Text != "" && this.txbpdToolV.Text != "")
            {
                float parameter_a, parameter_b, parameter_c, Length, toolV;
                //int bol;

                //if (cmbpdMachineSurface.Text == "XY平面")
                //    bol = 0;
                //else
                //    bol = 1;

                parameter_a = float.Parse(this.txbpda.Text);
                parameter_b = float.Parse(this.txbpdb.Text);
                parameter_c = float.Parse(this.txbpdc.Text);
                Length = float.Parse(this.txbpdToolLength.Text);
                toolV = float.Parse(this.txbpdToolV.Text);

                //this.basicdata.parameterFromParameterDesign(parameter_a, parameter_b, parameter_c, Length, toolV, bol);

            }
        }

        private void btnNormal_Click_1(object sender, EventArgs e)
        {
            float theta1, theta2, theta3, theta4, theta5, theta6, p1, p2, p3, p4, p5, p6,ax,ay,az;
            int bol;


            if (cmbMachineSurface.Text == "XY平面")
                bol = 0;
            else
                bol = 1;

//由theta 值求pxyz
            if ((this.txbTh1.Text != "" && this.txbTh2.Text != ""
                && this.txbTh3.Text != "" && this.txbTh4.Text != ""
                && this.txbTh5.Text != "" && this.txbTh6.Text != "")
                && (this.txbP1.Text == "" || this.txbP2.Text == ""
                || this.txbP3.Text == "" || this.txbP4.Text == ""
                || this.txbP5.Text == "" || this.txbP6.Text == ""))
            {
                theta1 = (float)((float.Parse(this.txbTh1.Text)) * Math.PI / 180);
                theta2 = (float)((float.Parse(this.txbTh2.Text)) * Math.PI / 180);
                theta3 = (float)((float.Parse(this.txbTh3.Text)) * Math.PI / 180);
                theta4 = (float)((float.Parse(this.txbTh4.Text)) * Math.PI / 180);
                theta5 = (float)((float.Parse(this.txbTh5.Text)) * Math.PI / 180);
                theta6 = (float)((float.Parse(this.txbTh6.Text)) * Math.PI / 180);

                ax = (float)(-Math.Cos(theta1) * (Math.Cos(theta2+theta3) * Math.Cos(theta4) * Math.Cos(theta5)
                        + Math.Sin(theta2+theta3) * Math.Cos(theta5))
                            - Math.Sin(theta1) * Math.Sin(theta4) * Math.Sin(theta5));


                ay = (float)(-Math.Sin(theta1) * (Math.Cos(theta2 + theta3) * Math.Cos(theta4) * Math.Cos(theta5)
                        + Math.Sin(theta2 + theta3) * Math.Cos(theta5))
                            + Math.Cos(theta1) * Math.Sin(theta4) * Math.Sin(theta5));
                az = (float)(Math.Sin(theta2 + theta3) * Math.Cos(theta4) * Math.Sin(theta5)
                             - Math.Cos(theta2 + theta3) * Math.Cos(theta5));


                this.txbax.Text = ax.ToString("0.####");
                this.txbay.Text = ay.ToString("0.####");
                this.txbaz.Text = az.ToString("0.####");




                float[] pxyzAndPs = this.basicdata.NormalSolutionFromThetas(theta1, theta2, theta3, theta4, theta5, theta6, bol);

                this.txbPx.Text = pxyzAndPs[0].ToString();
                this.txbPy.Text = pxyzAndPs[1].ToString();
                this.txbPz.Text = pxyzAndPs[2].ToString();

                this.txbP1.Text = pxyzAndPs[3].ToString();
                this.txbP2.Text = pxyzAndPs[4].ToString();
                this.txbP3.Text = pxyzAndPs[5].ToString();
                this.txbP4.Text = pxyzAndPs[6].ToString();
                this.txbP5.Text = pxyzAndPs[7].ToString();
                this.txbP6.Text = pxyzAndPs[8].ToString();
            }
                //由六个p值求pxyz
            else if ((this.txbTh1.Text == "" || this.txbTh2.Text == ""
                || this.txbTh3.Text == "" || this.txbTh4.Text == ""
               || this.txbTh5.Text == "" || this.txbTh6.Text == "")
                && (this.txbP1.Text != "" && this.txbP2.Text != ""
                && this.txbP3.Text != "" && this.txbP4.Text != ""
                && this.txbP5.Text != "" && this.txbP6.Text != ""))
            {
                p1 = float.Parse(this.txbP1.Text);
                p2 = float.Parse(this.txbP2.Text);
                p3 = float.Parse(this.txbP3.Text);
                p4 = float.Parse(this.txbP4.Text);
                p5 = float.Parse(this.txbP5.Text);
                p6 = float.Parse(this.txbP6.Text);

                float[] pxyzAndThetas = this.basicdata.NormalSolutionFromPs(p1, p2, p3, p4, p5, p6, bol);

                this.txbPx.Text = pxyzAndThetas[0].ToString();
                this.txbPy.Text = pxyzAndThetas[1].ToString();
                this.txbPz.Text = pxyzAndThetas[2].ToString();

                this.txbTh1.Text = (pxyzAndThetas[3] * 180 / Math.PI).ToString();
                this.txbTh2.Text = (pxyzAndThetas[4] * 180 / Math.PI).ToString();
                this.txbTh3.Text = (pxyzAndThetas[5] * 180 / Math.PI).ToString();
                this.txbTh4.Text = (pxyzAndThetas[6] * 180 / Math.PI).ToString();
                this.txbTh5.Text = (pxyzAndThetas[7] * 180 / Math.PI).ToString();
                this.txbTh6.Text = (pxyzAndThetas[8] * 180 / Math.PI).ToString();

                ax = (float)(-Math.Cos(pxyzAndThetas [3]) * (Math.Cos(pxyzAndThetas [4]+pxyzAndThetas[5]) * Math.Cos(pxyzAndThetas[6]) * Math.Cos(pxyzAndThetas[7])
                        + Math.Sin(pxyzAndThetas[4] + pxyzAndThetas[5]) * Math.Cos(pxyzAndThetas[7]))
                            - Math.Sin(pxyzAndThetas[3]) * Math.Sin(pxyzAndThetas[6]) * Math.Sin(pxyzAndThetas[7]));


                ay=(float)(-Math.Sin(pxyzAndThetas [3]) * (Math.Cos(pxyzAndThetas [4]+pxyzAndThetas[5]) * Math.Cos(pxyzAndThetas[6]) * Math.Cos(pxyzAndThetas[7])
                        + Math.Sin(pxyzAndThetas[4] + pxyzAndThetas[5]) * Math.Cos(pxyzAndThetas[7]))
                            + Math.Cos(pxyzAndThetas[3]) * Math.Sin(pxyzAndThetas[6]) * Math.Sin(pxyzAndThetas[7]));;
                az = (float)(Math.Sin(pxyzAndThetas[4] + pxyzAndThetas[5])*Math.Cos(pxyzAndThetas[6])*Math.Sin(pxyzAndThetas[7])
                             -Math.Cos(pxyzAndThetas[4] + pxyzAndThetas[5])*Math.Cos(pxyzAndThetas[7]));


                this.txbax.Text = ax.ToString("0.####");
                this.txbay.Text = ay.ToString("0.####");
                this.txbaz.Text = az.ToString("0.####");


            }

        }

        private void btnInversion_Click_1(object sender, EventArgs e)
        {
            float px, py, pz;
            int bol;


            if (cmbMachineSurface.Text == "XY平面")
                bol = 0;
            else
                bol = 1;


            if (this.txbPx.Text != "" && this.txbPy.Text != ""
                && this.txbPz.Text != "")
            {
                px = float.Parse(this.txbPx.Text);
                py = float.Parse(this.txbPy.Text);
                pz = float.Parse(this.txbPz.Text);

                float[] thetasAndPs = this.basicdata.InverseSolution(px, py, pz, bol);

                this.txbTh1.Text = (thetasAndPs[0] * 180 / 3.14).ToString();
                this.txbTh2.Text = (thetasAndPs[1] * 180 / 3.14).ToString();
                this.txbTh3.Text = (thetasAndPs[2] * 180 / 3.14).ToString();
                this.txbTh4.Text = (thetasAndPs[3] * 180 / 3.14).ToString();
                this.txbTh5.Text = (thetasAndPs[4] * 180 / 3.14).ToString();
                this.txbTh6.Text = (thetasAndPs[5] * 180 / 3.14).ToString();

                this.txbP1.Text = thetasAndPs[6].ToString();
                this.txbP2.Text = thetasAndPs[7].ToString();
                this.txbP3.Text = thetasAndPs[8].ToString();
                this.txbP4.Text = thetasAndPs[9].ToString();
                this.txbP5.Text = thetasAndPs[10].ToString();
                this.txbP6.Text = thetasAndPs[11].ToString();

            }
        }

        private void btnAPT_Click(object sender, EventArgs e)
        {

            Txt2List txt2list = new Txt2List();
            List2Txt list2txt = new List2Txt();

            List<string> tmplist = new List<string>();


            OpenFileDialog openDlg = new OpenFileDialog();

            openDlg.Filter = "txt_APT文件|*.txt";

            if (openDlg.ShowDialog() != DialogResult.OK) return;

            string fileName = openDlg.FileName;

            StreamReader sr = new StreamReader(fileName, Encoding.Default);

            this.rtxbapt.Text = sr.ReadToEnd();

            sr.Close();


            tmplist = txt2list.ReadTxtToList(fileName);
            list2txt.ListToTxtFile(tmplist, "D:middleTranslation0.txt");


        }

       

        private void btnJBI_Click(object sender, EventArgs e)
        {
            APTtoJBI apt2jbi = new APTtoJBI();
            Txt2List txt2list = new Txt2List();
            List2Txt list2txt = new List2Txt();

            List<string> tmplist = new List<string>();



           
            float parameter_a, parameter_b, parameter_c, Length, toolV;
            int bol;

            if (cmbpdMachineSurface.Text == "XY平面")
                bol = 0;
            else
                bol = 1;

            parameter_a = float.Parse(this.txbpda.Text);
            parameter_b = float.Parse(this.txbpdb.Text);
            parameter_c = float.Parse(this.txbpdc.Text);
            Length = float.Parse(this.txbpdToolLength.Text)+375;
            toolV = float.Parse(this.txbpdToolV.Text);


            SaveFileDialog sfdlg = new SaveFileDialog();
            sfdlg.ShowDialog();
            sfdlg.Filter = "文本文件|*.JBI";

            string pathstr = apt2jbi.Apt2JbiFirst("D:middleTranslation0.txt");

            tmplist = apt2jbi.Apt2Jbi_math(pathstr,Length ,parameter_a ,parameter_b ,parameter_c ,toolV,bol );

            string tmpstrin = apt2jbi.writingJBItoText(tmplist);



            StreamReader sr = new StreamReader(tmpstrin, Encoding.Default);

            this.rtxbjbi.Text = sr.ReadToEnd();

            sr.Close();




            if (sfdlg.ShowDialog() == DialogResult.OK)
            {
                string savefile = sfdlg.FileName;

                string namestr = Path.GetFileNameWithoutExtension(savefile);
                string directoryname = Path.GetDirectoryName(savefile);


                int count = (int)(tmplist.Count / 2 / 850 + 1);
                int offset = 0;


                for (int i = 0; i < count; i++)
                {
                    int pointNumbers = 0;
                    int tmpoffset = offset;

                    FileStream fsbegin = new FileStream(@directoryname + "\\" + namestr + i.ToString(), FileMode.Create, FileAccess.Write);

                    StreamWriter swbegin = new StreamWriter(fsbegin);

                    swbegin.Flush();

                    swbegin.BaseStream.Seek(0, SeekOrigin.Begin);

                    swbegin.WriteLine("/JOB");
                    swbegin.WriteLine("//NAME " + namestr + i.ToString());
                    swbegin.WriteLine("//POS");

                    if (count == 1)
                    {
                        pointNumbers = tmplist.Count / 2 + 2;

                    }

                    else if ((i < count - 1) && count > 1)
                    {

                        if (tmplist[tmplist.Count / 2 + (i + 1) * 850].StartsWith("MOVL"))
                        {
                            pointNumbers = 850 - offset + 2;
                            offset = 0;

                        }
                        else if (tmplist[tmplist.Count / 2 + (i + 1) * 850 + 1].StartsWith("MOVL"))
                        {
                            pointNumbers = 850 + 1 - offset + 2;
                            offset = 1;

                        }
                        else if (tmplist[tmplist.Count / 2 + (i + 1) * 850 + 2].StartsWith("MOVL"))
                        {
                            pointNumbers = 850 + 2 - offset + 2;
                            offset = 2;

                        }
                        else if (tmplist[tmplist.Count / 2 + (i + 1) * 850 + 3].StartsWith("MOVL"))
                        {
                            pointNumbers = 850 + 3 - offset + 2;
                            offset = 3;

                        }
                    }
                    else if (i == count - 1 && count > 1)
                    {
                        pointNumbers = tmplist.Count / 2 - 850 * i - tmpoffset + 2;
                    }
                    swbegin.WriteLine("///NPOS " + pointNumbers.ToString() + ",0,0,0,0,0");
                    swbegin.WriteLine("///TOOL 0");
                    swbegin.WriteLine("///POSTYPE PULSE");
                    swbegin.WriteLine("///PULSE");

                    swbegin.WriteLine("C00000=0,0,0,0,0,0");

                    int line = 1;

                    for (int tmpN = tmpoffset; tmpN < pointNumbers - 2; tmpN++, line++)
                    {

                        string tmpstr = tmplist[850 * i + tmpN];
                        string str = "C" + string.Format("{0:00000}", line) + tmpstr.Remove(0, 6);

                        swbegin.WriteLine(str);

                        if (tmpN == pointNumbers - 3)
                        {

                            if (tmplist[i * 850 + tmpN + 1].StartsWith("MOVL"))
                            {
                            }
                            else if (tmplist[i * 850 + tmpN + 2].StartsWith("MOVL"))
                            {
                                line++;
                                tmpstr = tmplist[850 * i + tmpN + 1];
                                str = "C" + string.Format("{0:00000}", line) + tmpstr.Remove(0, 6);
                                swbegin.WriteLine(str);

                            }
                            else if (tmplist[i * 850 + tmpN + 3].StartsWith("MOVL"))
                            {
                                line++;
                                tmpstr = tmplist[850 * i + tmpN + 1];
                                str = "C" + string.Format("{0:00000}", line) + tmpstr.Remove(0, 6);
                                swbegin.WriteLine(str);

                                line++;
                                tmpstr = tmplist[850 * i + tmpN + 2];
                                str = "C" + string.Format("{0:00000}", line) + tmpstr.Remove(0, 6);
                                swbegin.WriteLine(str);


                            }
                            else if (tmplist[i * 850 + tmpN + 4].StartsWith("MOVL"))
                            {
                                line++;
                                tmpstr = tmplist[850 * i + tmpN + 1];
                                str = "C" + string.Format("{0:00000}", line) + tmpstr.Remove(0, 6);
                                swbegin.WriteLine(str);

                                line++;
                                tmpstr = tmplist[850 * i + tmpN + 2];
                                str = "C" + string.Format("{0:00000}", line) + tmpstr.Remove(0, 6);
                                swbegin.WriteLine(str);

                                line++;
                                tmpstr = tmplist[850 * i + tmpN + 3];
                                str = "C" + string.Format("{0:00000}", line) + tmpstr.Remove(0, 6);
                                swbegin.WriteLine(str);



                            }
                        }
                    }
                    swbegin.WriteLine("C" + string.Format("{0:00000}", line) + "=0,0,0,0,0,0");

                    swbegin.WriteLine("//INST");
                    swbegin.WriteLine("///DATE " + DateTime.Now.ToString("yyyy/MM/dd  HH:mm"));
                    swbegin.WriteLine("///ATTR SC,RW");
                    swbegin.WriteLine("///GROUP1 RB2");
                    swbegin.WriteLine("NOP");


                    swbegin.WriteLine("MOVL C00000 V=110.00 PL=0");

                    for (int j = 0; j < pointNumbers - 2; j++)
                    {
                        string tmpstr = tmplist[tmplist.Count / 2 + i * 850 + tmpoffset + j];
                        string str = tmpstr.Substring(0, 6) + string.Format("{0:00000}", j + 1) + " V=110.00 PL=0";
                        swbegin.WriteLine(str);

                    }

                    swbegin.WriteLine("MOVL C" + string.Format("{0:00000}", pointNumbers - 1) + " V=110.00 PL=0");

                    swbegin.WriteLine("END");

                    swbegin.Flush();
                    swbegin.Close();
                    fsbegin.Close();



                }
            }
        }

        private void txbpdToolLength_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }




    }
}
