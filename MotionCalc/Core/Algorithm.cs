using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;

namespace Core
{
    public class Algorithm
    {
        private static int intRed, intBlue, intGreen, intYellow, intPurple, intBlack, intWhite, intGray;
        private Point anchor;
        private MCvScalar colorLow, colorUp;
        private Mat kernel, imgHsv, imgThreshold, hierarchy;


        public Algorithm()
        {
            this.colorLow = new MCvScalar();
            this.colorUp = new MCvScalar();

            this.imgHsv = new Mat();
            this.imgThreshold = new Mat();
            this.hierarchy = new Mat ();
            this.anchor = new Point(-1, -1);
            this.kernel = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(2, 2), this.anchor);

            intRed = Color.Red.ToArgb();
            intBlue = Color.Blue.ToArgb();
            intGreen = Color.Green.ToArgb();
            intYellow = Color.Yellow.ToArgb();
            intPurple = Color.Purple.ToArgb();
            intBlack = Color.Black.ToArgb();
            intWhite = Color.White.ToArgb();
            intGray = Color.Gray.ToArgb();
        }

        private void getColorBoundary(int color)
        {
            if (color == intRed)
            {
                this.colorLow.V0 = 156;
                this.colorLow.V1 = 43;
                this.colorLow.V2 = 46;
                this.colorUp.V0 = 180;
                this.colorUp.V1 = 255;
                this.colorUp.V2 = 255;
            }
            else if (color == intBlue)
            {
                this.colorLow.V0 = 100;
                this.colorLow.V1 = 43;
                this.colorLow.V2 = 46;
                this.colorUp.V0 = 124;
                this.colorUp.V1 = 255;
                this.colorUp.V2 = 255;
            }
            else if (color == intGreen)
            {
                this.colorLow.V0 = 35;
                this.colorLow.V1 = 43;
                this.colorLow.V2 = 46;
                this.colorUp.V0 = 77;
                this.colorUp.V1 = 255;
                this.colorUp.V2 = 255;
            }
            else if (color == intYellow)
            {
                this.colorLow.V0 = 26;
                this.colorLow.V1 = 43;
                this.colorLow.V2 = 46;
                this.colorUp.V0 = 34;
                this.colorUp.V1 = 255;
                this.colorUp.V2 = 255;
            }
            else if (color == intPurple)
            {
                this.colorLow.V0 = 125;
                this.colorLow.V1 = 43;
                this.colorLow.V2 = 46;
                this.colorUp.V0 = 155;
                this.colorUp.V1 = 255;
                this.colorUp.V2 = 255;
            }
            else if (color == intBlack)
            {
                this.colorLow.V0 = 0;
                this.colorLow.V1 = 0;
                this.colorLow.V2 = 0;
                this.colorUp.V0 = 180;
                this.colorUp.V1 = 255;
                this.colorUp.V2 = 46;
            }
            else if (color == intWhite)
            {
                this.colorLow.V0 = 0;
                this.colorLow.V1 = 0;
                this.colorLow.V2 = 221;
                this.colorUp.V0 = 180;
                this.colorUp.V1 = 30;
                this.colorUp.V2 = 255;
            }
            else if (color == intGray)
            {
                this.colorLow.V0 = 0;
                this.colorLow.V1 = 0;
                this.colorLow.V2 = 46;
                this.colorUp.V0 = 180;
                this.colorUp.V1 = 43;
                this.colorUp.V2 = 220;
            }
        }

        public List<Point> RecognizeColor(Mat sourceFrame, int color,ref Mat testFrame)
        {
            List<Point> locList = new List<Point> ();

            this.getColorBoundary(color);

            CvInvoke.CvtColor(sourceFrame, imgHsv, Emgu.CV.CvEnum.ColorConversion.Bgr2Hsv);
            CvInvoke.InRange(imgHsv, new ScalarArray(this.colorLow), new ScalarArray(this.colorUp), imgThreshold);

            CvInvoke.MorphologyEx(imgThreshold, imgThreshold, MorphOp.Open, kernel, this.anchor, 1, 0, new MCvScalar());
            CvInvoke.MorphologyEx(imgThreshold, imgThreshold, MorphOp.Close, kernel, this.anchor, 1, 0, new MCvScalar());

            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours(imgThreshold, contours, this.hierarchy, RetrType.External, ChainApproxMethod.ChainApproxSimple);
                int shapeCount = contours.Size;
                for (int i = 0; i < shapeCount; i++)
                {
                    CvInvoke.ApproxPolyDP(contours[i], contours[i], 5, true);
                    Rectangle rect = CvInvoke.BoundingRectangle(contours[i]);

                    CvInvoke.DrawContours(testFrame, contours, i, new MCvScalar(255), -1);

                    int rectArea = rect.Width * rect.Height;
                    double rectRatio = rect.Width * 1.0d / rect.Height;
                    if (rectArea < Constants.MinRecogRectArea || rectArea > Constants.MaxRecogRectArea || 
                        rectRatio<Constants.MinRecogRectWHRatio || rectRatio > Constants.MaxRecogRectWHRatio)
                        continue;
                    
                    locList.Add(rect.Location);
                    CvInvoke.DrawContours(imgThreshold, contours, i, new MCvScalar(255), -1);
                    testFrame = imgThreshold;
                }
            }
            return locList;
        }
    }
}