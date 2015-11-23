using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialRobot
{
    class BasicData
    {

         public const int La = 1224, Lb = 0, Lc = 725;

        public const int a1 = 150, a2 = 570, a3 = 155, d4 = 640;

        //float parameter_a, parameter_b, parameter_c;

       //public  int bolFrompd;

       // public  float toolV;

        //public void parameterFromParameterDesign(float a,float b,float c,float length,float toolv,int bol)
        //{

        //    parameter_a = a;
        //    parameter_b = b;
        //    parameter_c = c;
            
        //    toolV = toolv;

        //    bolFrompd = bol;
        
        //}


        public float[] SolvingPxyzFromXyz(float x, float y, float z,float parameter_a,float parameter_b,float parameter_c,float toollength, int bol)
        {
            float px=0, py=0, pz=0;

            if (bol == 0)
            {
                px = La + parameter_a + x;
                py = Lb + parameter_b + y;
                pz = parameter_c + z + toollength-369+95;
            }
            else
            {

                px = La + parameter_a -toollength - z;

                py = Lb + parameter_b - x;
                pz = parameter_c + y-369+95;
            
            }

            float[] Pxyz = { px, py, pz };

            return Pxyz;

        }

       

        public float[] InverseSolution(float px,float py,float pz,int bol)
        {

            float  theta1, theta2,theta3,theta4=0, theta5=0, theta6, p1, p2, p3, p4, p5, p6;
            
            theta1 = (float)Math.Atan2(py, px);

            float ParameterK = (float)(Math.Pow((Math.Cos(theta1) * px + Math.Sin(theta1) * py - a1), 2)
                    + pz * pz - a2*a2 - a3*a3 - d4*d4) / (2 * a2);

            theta3 = (float)(Math.Atan2(a3 , d4) -Math.Atan2(ParameterK ,Math.Sqrt(a3 *a3 + d4 *d4
                   - ParameterK  * ParameterK )));


            float theta23 = (float)(Math.Atan2((-(a3 + a2 * Math.Cos(theta3)) * pz+ (Math.Cos(theta1) 
                          * px + Math.Sin(theta1) * py - a1) *(a2 * Math.Sin(theta3) - d4)),
                          (-(d4- a2 * Math.Sin(theta3)) * pz + (Math.Cos(theta1) * px+ Math.Sin(theta1) 
                          * py - a1) * (a2 * Math.Cos(theta3)+ a3))));


            theta2 = theta23 - theta3;

            theta6 = 0;


            if (bol ==0)
            {

                theta4 = 0;

                theta5 = -(theta2 + theta3);

            }
            else if (bol == 1)
            {

                theta4 = (float)Math.Atan2(Math.Sin(theta1) , (Math.Cos(theta1) * Math.Cos(theta23)));

                theta5 = (float)(Math.Atan2((Math.Cos(theta1) * Math.Cos(theta23) * Math.Cos(theta4)
                       + Math.Sin(theta1) * Math.Sin(theta4)) ,( Math.Cos(theta1) * Math.Sin(theta23)))-Math.PI);

            }

            p1 =Round( (float)(1593 * theta1 * 180 / Math.PI));
            p2 =Round( (float)(1367.7 * (theta2 * 180 / Math.PI + 90)));
            p3 =Round( (float)(-1367.7 * theta3 * 180 / Math.PI));
            p4 =Round( (float)(-568.87 * theta4 * 180 / Math.PI));           
            p6 =Round( (float)(425.5 * theta6 * 180 / Math.PI));

           p5 =Round( (float)(-823.9 * (theta5 * 180 / Math.PI - 90)));
            
            
            float[] ThetasAndPs={theta1,theta2,theta3,theta4,theta5,theta6,p1,p2,p3,p4,p5,p6};

            return ThetasAndPs;
        }



        public float[] NormalSolutionFromThetas(float theta1, float theta2, float theta3, float theta4, float theta5, float theta6,int bol)
        {
            float px,py,pz,p1,p2,p3,p4,p5,p6;

            px = (float)(Math.Cos(theta1) * (a3 * Math.Cos(theta2 + theta3)
               - d4 * Math.Sin(theta3 + theta2) + a2 * Math.Cos(theta2) + a1));
            px = Round(px);

            py = (float)(Math.Sin(theta1) * (a3 * Math.Cos(theta2 + theta3)
               - d4 * Math.Sin(theta3 + theta2) + a2 *  Math.Cos(theta2) + a1));
            py = Round(py);

            pz = (float)(-a3 * Math.Sin(theta3 + theta2) - d4 * Math.Cos(theta2
                + theta3) - a2 * Math.Sin(theta2));
            pz = Round(pz);


            p1 =Round( (float)(1593 * theta1 * 180 / Math.PI));
            p2 =Round( (float)(1367.7 * (theta2 * 180 / Math.PI + 90)));
            p3 =Round( (float)(-1367.7 * theta3 * 180 / Math.PI));
            p4 =Round( (float)(-568.87 * theta4 * 180 / Math.PI));           
            p6 =Round( (float)(425.5 * theta6 * 180 / Math.PI));

            
                p5 =Round( (float)(-823.9 * (theta5 * 180 / Math.PI - 90)));
          

            float[] pxyzAndPs={px,py,pz,p1,p2,p3,p4,p5,p6};

            return pxyzAndPs ;
           
        }

        public float[] NormalSolutionFromPs(float p1,float p2,float p3,float p4,float p5,float p6,int bol)
        {
         float  theta1, theta2,theta3,theta4=0, theta5=0, theta6,px,py,pz;


            theta1 =(float)Math.PI*p1/1593/180;
            theta2 =(float )(Math.PI *(p2/1367.7-90)/180);
            theta3 =(float )(-Math.PI *p3/1367.7/180);
            theta4 =(float )(-Math.PI *p4/568.87/180);
            theta6 =(float )(Math.PI *p6/425.5/180);

                   
            theta5 =(float )(Math.PI *(-p5/823.9+90)/180);

             px = (float)(Math.Cos(theta1) * (a3 * Math.Cos(theta2 + theta3)
               - d4 * Math.Sin(theta3 + theta2) + a2 * Math.Cos(theta2) + a1));
            px = Round(px);

            py = (float)(Math.Sin(theta1) * (a3 * Math.Cos(theta2 + theta3)
               - d4 * Math.Sin(theta3 + theta2) + a2 *  Math.Cos(theta2) + a1));
            py = Round(py);

            pz = (float)(-a3 * Math.Sin(theta3 + theta2) - d4 * Math.Cos(theta2
                + theta3) - a2 * Math.Sin(theta2));
            pz = Round(pz);



            float[] pxyzAndThetas={px,py,pz,theta1,theta2,theta3,theta4,theta5,theta6};

            return pxyzAndThetas ;

        
        
        }







        private int Round(double x)
        {
            int y;
            float x1 =(float ) x;

            if (x1 >= 0)
            { 
             y=(int)(x1+0.5);
            
            }
            else 
              y=(int)(x1-0.5);
            return y;
        
        }
   




    }
    
}
