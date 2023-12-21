using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;

namespace AutoClicker.Runtime.Helper
{
    public class ScreenHelper
    {
        public static Bitmap CaptureScreen(Rectangle fromArea)
        {
            var bitmap = new Bitmap(fromArea.Width, fromArea.Height);
            using var g = Graphics.FromImage(bitmap);
            g.CopyFromScreen(fromArea.X, fromArea.Y, 0, 0, bitmap.Size, CopyPixelOperation.SourceCopy);
            return bitmap;
        }

        public static System.Drawing.Point FindImageInRectangele(Rectangle fromArea, string templatePath)
        {
            Bitmap capturedScreen = CaptureScreen(fromArea);
            capturedScreen.Save($"{DateTime.Now.Millisecond}-1.png");
            new Bitmap(templatePath).Save($"{DateTime.Now.Millisecond}-2.png");
            var result = FindImage(capturedScreen, templatePath);
            return new System.Drawing.Point(result.X + fromArea.X, result.Y + fromArea.Y);
        }

        public static System.Drawing.Point FindImage(Bitmap sourceBitmap, string templatePath)
        {
            // Load source image and template
            string tempFilePathSource = $"{Path.GetTempFileName()}.png";
            sourceBitmap.Save(tempFilePathSource, ImageFormat.Png);
            using Mat source = Cv2.ImRead(tempFilePathSource, ImreadModes.Color);
            using Mat template = Cv2.ImRead(templatePath, ImreadModes.Color);

            // Create result matrix
            Mat result = new(source.Rows - template.Rows + 1, source.Cols - template.Cols + 1, MatType.CV_32FC1);

            // Perform template matching
            Cv2.MatchTemplate(source, template, result, TemplateMatchModes.CCoeffNormed);
            Cv2.MinMaxLoc(result, out double minVal, out double maxVal, out OpenCvSharp.Point minLoc, out OpenCvSharp.Point maxLoc);
            
            Cv2.Rectangle(source, maxLoc, new OpenCvSharp.Point(maxLoc.X + template.Width, maxLoc.Y + template.Height), Scalar.Red, 2);
            source.SaveImage($"{DateTime.Now.Millisecond}-3.png");

            return new System.Drawing.Point(maxLoc.X + (template.Width / 2), maxLoc.Y + (template.Height / 2));
        }

        #region Display Resolution

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117
        }


        public static double GetWindowsScreenScalingFactor(bool percentage = true)
        {
            //Create Graphics object from the current windows handle
            Graphics GraphicsObject = Graphics.FromHwnd(IntPtr.Zero);
            //Get Handle to the device context associated with this Graphics object
            IntPtr DeviceContextHandle = GraphicsObject.GetHdc();
            //Call GetDeviceCaps with the Handle to retrieve the Screen Height
            int LogicalScreenHeight = GetDeviceCaps(DeviceContextHandle, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(DeviceContextHandle, (int)DeviceCap.DESKTOPVERTRES);
            //Divide the Screen Heights to get the scaling factor and round it to two decimals
            double ScreenScalingFactor = Math.Round(PhysicalScreenHeight / (double)LogicalScreenHeight, 2);
            //If requested as percentage - convert it
            if (percentage)
            {
                ScreenScalingFactor *= 100.0;
            }
            //Release the Handle and Dispose of the GraphicsObject object
            GraphicsObject.ReleaseHdc(DeviceContextHandle);
            GraphicsObject.Dispose();
            //Return the Scaling Factor
            return ScreenScalingFactor;
        }

        public static System.Windows.Size GetDisplayResolution()
        {
            var sf = GetWindowsScreenScalingFactor(false);
            var screenWidth = Screen.PrimaryScreen.Bounds.Width * sf;
            var screenHeight = Screen.PrimaryScreen.Bounds.Height * sf;
            return new System.Windows.Size((int)screenWidth, (int)screenHeight);
        }

        #endregion
    }
}
