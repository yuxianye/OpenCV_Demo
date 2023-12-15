using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OpenCvSharp.Extensions;
using System.IO;

namespace OpenCV_Demo
{
    /// <summary>
    /// Interaction logic for HoughCircles.xaml
    /// </summary>
    public partial class HoughCircles : System.Windows.Window
    {
        public HoughCircles()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            HoughCircles_water();
            // HoughCircles_mucai();


        }


        private void HoughCircles_mucai()
        {

            ImageNew.Source = new BitmapImage(new Uri("mucai.jpeg", UriKind.Relative));

            //ImageSource.Source = new ImageSource();
            Mat mat = new Mat("mucai.jpeg");
            Mat matClone = mat.Clone();
            Cv2.CvtColor(mat, mat, ColorConversionCodes.RGB2GRAY);//将彩色图像变成单通道灰度图像
                                                                  //霍夫圆检测：使用霍夫变换查找灰度图像中的圆。
            /*
             * 参数：
             *      1：输入参数： 8位、单通道、灰度输入图像
             *      2：实现方法：目前，唯一的实现方法是HoughCirclesMethod.Gradient
             *      3: dp      :累加器分辨率与图像分辨率的反比。默认=1
             *      4：minDist: 检测到的圆的中心之间的最小距离。(最短距离-可以分辨是两个圆的，否则认为是同心圆- src_gray.rows/8)
             *      5:param1:   第一个方法特定的参数。[默认值是100] canny边缘检测阈值低
             *      6:param2:   第二个方法特定于参数。[默认值是100] 中心点累加器阈值 – 候选圆心
             *      7:minRadius: 最小半径
             *      8:maxRadius: 最大半径
             */
            CircleSegment[] cs = Cv2.HoughCircles(mat, HoughModes.Gradient, 1, 60, 150, 50, 40, 105);

            //排序
            Array.Sort(cs, (cs1, cs2) =>
            {
                if (cs1 != null && cs1 != null)
                {
                    if (cs1.Center.Y > cs2.Center.Y)
                        return 1;
                    else if (cs1.Center.Y == cs2.Center.Y)
                    {
                        if (cs1.Center.X < cs2.Center.X)
                            return 1;
                        else return -1;
                    }
                    else
                        return -1;
                }
                return 0;

            });

            int index = 1;
            for (int i = 0; i < cs.Count(); i++)
            {
                //画圆
                Cv2.Circle(matClone, (OpenCvSharp.Point)cs[i].Center, (int)cs[i].Radius, new Scalar(255, 255, 0), 2);
                Cv2.PutText(matClone, (index++).ToString(), (OpenCvSharp.Point)cs[i].Center, 0, 0.4, new OpenCvSharp.Scalar(0, 0, 0), 2);
            }
            //pictureBox2.Image = BitmapConverter.ToBitmap(matClone);
            //b.StreamSource= 
            ImageNew.Source = BitmapToBitmapImage(BitmapConverter.ToBitmap(matClone));
            labelCount.Content = cs.Count();
        }




        private void HoughCircles_water()
        {

            ImageNew.Source = new BitmapImage(new Uri("HoughCircles.png", UriKind.Relative));

            //ImageSource.Source = new ImageSource();
            Mat mat = new Mat("HoughCircles.png");
            Mat matClone = mat.Clone();
            Cv2.CvtColor(mat, mat, ColorConversionCodes.RGB2GRAY);//将彩色图像变成单通道灰度图像
                                                                  //霍夫圆检测：使用霍夫变换查找灰度图像中的圆。
            /*
             * 参数：
             *      1：输入参数： 8位、单通道、灰度输入图像
             *      2：实现方法：目前，唯一的实现方法是HoughCirclesMethod.Gradient
             *      3: dp      :累加器分辨率与图像分辨率的反比。默认=1
             *      4：minDist: 检测到的圆的中心之间的最小距离。(最短距离-可以分辨是两个圆的，否则认为是同心圆- src_gray.rows/8)
             *      5:param1:   第一个方法特定的参数。[默认值是100] canny边缘检测阈值低
             *      6:param2:   第二个方法特定于参数。[默认值是100] 中心点累加器阈值 – 候选圆心
             *      7:minRadius: 最小半径
             *      8:maxRadius: 最大半径
             */
            CircleSegment[] cs = Cv2.HoughCircles(mat, HoughModes.Gradient, 1.245, 20, 30, 30, 10, 18);

            //排序
            Array.Sort(cs, (cs1, cs2) =>
            {
                if (cs1 != null && cs1 != null)
                {
                    if (cs1.Center.Y > cs2.Center.Y)
                        return 1;
                    else if (cs1.Center.Y == cs2.Center.Y)
                    {
                        if (cs1.Center.X < cs2.Center.X)
                            return 1;
                        else return -1;
                    }
                    else
                        return -1;
                }
                return 0;

            });

            int index = 1;
            for (int i = 0; i < cs.Count(); i++)
            {
                //画圆
                Cv2.Circle(matClone, (OpenCvSharp.Point)cs[i].Center, (int)cs[i].Radius, new Scalar(255, 255, 0), 2);
                Cv2.PutText(matClone, (index++).ToString(), (OpenCvSharp.Point)cs[i].Center, 0, 0.4, new OpenCvSharp.Scalar(0, 0, 0), 2);
            }
            //pictureBox2.Image = BitmapConverter.ToBitmap(matClone);
            //b.StreamSource= 
            ImageNew.Source = BitmapToBitmapImage(BitmapConverter.ToBitmap(matClone));
            labelCount.Content = cs.Count();
        }





      public BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap bitmap)
         {
             MemoryStream ms = new MemoryStream();
             bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
             BitmapImage bit3 = new BitmapImage();
             bit3.BeginInit();
             bit3.StreamSource = ms;
             bit3.EndInit();
             return bit3;
         }
    }
}
