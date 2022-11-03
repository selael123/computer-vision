using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;



namespace WindowsFormsApplication1
{
    internal unsafe class HSVTransform
    {

        private BitmapData HSVData_In = new BitmapData();
        private BitmapData HSVData_Out = new BitmapData();
        private byte* ImgPtr_In { get; set; }
        private byte* ImgPtr_Out { get; set; }
        private int StepSize_In, ByteOfSkip_In, StepSize_Out, ByteOfSkip_Out;
        private double H1 , H_Form1 , H_Form2 , H,S,V;
        Color OutColor;

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }
        private void GetPixel_botton( ref Bitmap _imgin, ref Bitmap _imgout)
        {
            HSVData_In = _imgin.LockBits(new Rectangle(0, 0, _imgin.Width, _imgin.Height), ImageLockMode.ReadOnly, _imgin.PixelFormat);
            switch (HSVData_In.PixelFormat)
            {
                case PixelFormat.Format8bppIndexed:
                    StepSize_In = 1;
                    ByteOfSkip_In = HSVData_In.Stride - _imgin.Width;
                    break;
                case PixelFormat.Format24bppRgb:
                    StepSize_In = 3;
                    ByteOfSkip_In = HSVData_In.Stride - _imgin.Width * 3;
                    break;
                case PixelFormat.Format32bppArgb:
                    StepSize_In = 4;
                    ByteOfSkip_In = HSVData_In.Stride - _imgin.Width * 4;
                    break;
            }
            HSVData_Out = _imgout.LockBits(new Rectangle(0, 0, _imgout.Width, _imgout.Height), ImageLockMode.WriteOnly, _imgout.PixelFormat);
            StepSize_Out = 1;
            ByteOfSkip_Out = HSVData_Out.Stride - _imgout.Width;
            ImgPtr_In = (byte*)HSVData_In.Scan0.ToPointer();
            ImgPtr_Out = (byte*)HSVData_Out.Scan0.ToPointer();
            Bitmap myBitmap = new Bitmap(Convert.ToString(_imgout));
            
            for (int i = 0; i < _imgin.Height; i++)
            {
                for (int j = 0; j < _imgin.Width; j++)//doing HSV
                {
                    Color op  = myBitmap.GetPixel(j, i);
                    byte RGBmax = new byte[] { op.R, op.G, op.B }.Max();
                    byte RGBmin = new byte[] { op.R, op.G, op.B }.Min();
                    //////////////////H_Part///////////////////
                    H_Form1 = ((op.R - op.G) + (op.R - op.B)) * 0.5;
                    H_Form2 = (Math.Sqrt(Math.Pow((op.R - op.G) , 2)) + (op.R - op.B) * (op.G - op.B));
                    H1 = Math.Acos(H_Form1 / H_Form2);
                    if (op.B <= op.G) { H = H1; } else { H = 360 - H1; }
                    ///
                    /////////////S and V////////////////
                    S = (RGBmax - RGBmin) / RGBmax;
                    V = RGBmax / 255d;

                   Color OutColor = ColorFromHSV(H, S, V);

                }
                ImgPtr_In += ByteOfSkip_In;
                ImgPtr_Out += ByteOfSkip_Out;
            }
            _imgin.UnlockBits(HSVData_In);
            _imgout.UnlockBits(HSVData_Out);
        }
    }
}
