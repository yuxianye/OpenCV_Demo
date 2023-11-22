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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OpenCV_Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            BGR2GRAY();
        }

        private void test()
        {
            //例1
            Mat src = new Mat(@"C:\Users\Public\Pictures\Sample Pictures\郁金香.png", ImreadModes.Grayscale);
            Mat dst = new Mat();

            Cv2.Canny(src, dst, 50, 200);
            Cv2.ImShow("src image", src);
            Cv2.ImShow("dst image", dst);
            Cv2.WaitKey(0);


            //例2 创建一张大小为400*600颜色为白色背景的三通道彩色图像
            //int d = 100;
            //Mat img = new Mat(400, 600, MatType.CV_8UC3, new Scalar(255, 255, 255));
            ////
            //Cv2.Line(img, 250, 100, 50, 200, new Scalar(0, 255, 0), 2);
            //Cv2.Rectangle(img, new Rect(50, 50, d, d + 100), new Scalar(0, 0, 255), -1);
            //Cv2.Circle(img, new Point(50, 50), 25, new Scalar(255, 255, 0), -1);

            //Cv2.PutText(img, "OpenCV", new Point(220, 100), HersheyFonts.HersheyComplex, 3, Scalar.Blue, 15);
            //Cv2.PutText(img, "OpenCV", new Point(220, 100), HersheyFonts.HersheyComplex, 3, Scalar.Yellow, 5);

            ////显示图像
            //Cv2.ImShow("img", img);
            ////延时等待按键按下
            //Cv2.WaitKey(0);
        }



        private void BGR2GRAY()
        {
            using (Mat img = new Mat(@"opencv.jpg", ImreadModes.AnyColor))
            {
                //创建显示框并显示图片（自动图片大小的）
                OpenCvSharp.Window windows = new OpenCvSharp.Window("source", img);
                //创建图像保存容器
                Mat mat = new Mat();
                Cv2.CvtColor(img, mat, ColorConversionCodes.BGR2GRAY);
                windows = new OpenCvSharp.Window("grayMat", mat);
                //保存图像，会返回一个bool值
                Cv2.ImWrite(@"opencv2.jpg", mat);
                Cv2.WaitKey(0);

            }

        }
    }
}
