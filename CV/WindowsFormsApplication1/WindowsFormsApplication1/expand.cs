using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;


namespace WindowsFormsApplication1
{
    internal unsafe class expand
    {   
        private BitmapData BmpData_In = new BitmapData();
        private BitmapData BmpData_Out = new BitmapData();
        private byte* ImgPtr_In { get; set; }
        private byte* ImgPtr_Out { get; set; }
        private int  ByteOfSkip_In, ByteOfSkip_Out;
        int total_temp = 0;
        internal void expand_fun(ref Bitmap _imgin, ref Bitmap _imgout)
        {
            
            //byte[] b_temp = new byte[_masksize * _masksize]; ; 
            int bb = 0;
            int i_temp = 0;
            int maskskip = 1;
            BmpData_In = _imgin.LockBits(new Rectangle(0, 0, _imgin.Width, _imgin.Height), ImageLockMode.ReadOnly, _imgin.PixelFormat);
            ByteOfSkip_In = BmpData_In.Stride - _imgin.Width;
            BmpData_Out = _imgout.LockBits(new Rectangle(0, 0, _imgout.Width, _imgout.Height), ImageLockMode.WriteOnly, _imgout.PixelFormat);
            ByteOfSkip_Out = BmpData_Out.Stride - _imgout.Width;
            ImgPtr_In = (byte*)BmpData_In.Scan0.ToPointer();
            ImgPtr_Out = (byte*)BmpData_Out.Scan0.ToPointer();
            ImgPtr_In += (maskskip * BmpData_In.Stride);
            ImgPtr_Out += (maskskip * BmpData_Out.Stride);
            bb += (maskskip * BmpData_Out.Stride);

            for (int i = maskskip; i < _imgin.Height - maskskip; i++)
            {
                ImgPtr_In = ImgPtr_In + maskskip;
                ImgPtr_Out = ImgPtr_Out + maskskip;
                bb = bb + maskskip;
                //0 1 0
                //1 1 1
                //0 1 0
                for (int j = maskskip; j < _imgin.Width - maskskip; j++)
                {
                    i_temp = Convert.ToInt32(*ImgPtr_In);
                    int left = Convert.ToInt32(*(ImgPtr_In - BmpData_In.Stride));
                    int right = Convert.ToInt32(*(ImgPtr_In + BmpData_In.Stride));
                    int down = Convert.ToInt32(*(ImgPtr_In - 1));
                    int up = Convert.ToInt32(*(ImgPtr_In + 1));
                    if (up == 0 || right == 0 || down == 0 || left == 0) { i_temp = 0; }




                    //if (i_temp >= otsuvalue) i_temp = 255; else i_temp = 0;

                    *ImgPtr_Out = Convert.ToByte(i_temp);
                    ++ImgPtr_In;
                    ++ImgPtr_Out;
                    bb++;
                }
                ImgPtr_In += ByteOfSkip_In + maskskip;
                ImgPtr_Out += ByteOfSkip_Out + maskskip;
                bb += ByteOfSkip_Out + maskskip;
            }
            _imgin.UnlockBits(BmpData_In);
            _imgout.UnlockBits(BmpData_Out);


        }
        private int getedmetrix(int w, byte* inim)
        {
            
            for (int y = 1; y < w/2; y++)
            {
                for (int x = 1; x < w/2; x++)
                {
                    total_temp  = Convert.ToInt32(*inim);
                    total_temp += Convert.ToInt32(*(inim + x + (y * BmpData_In.Stride)));
                    total_temp += Convert.ToInt32(*(inim + x - (y * BmpData_In.Stride)));
                    total_temp += Convert.ToInt32(*(inim - x + (y * BmpData_In.Stride)));
                    total_temp += Convert.ToInt32(*(inim - x - (y * BmpData_In.Stride)));
                    total_temp += Convert.ToInt32(*(inim - x));
                    total_temp += Convert.ToInt32(*(inim + x));
                    total_temp += Convert.ToInt32(*(inim - (y * BmpData_In.Stride)));
                    total_temp += Convert.ToInt32(*(inim + (y * BmpData_In.Stride)));
                }
            }
            //Console.WriteLine(total_temp);
            if (total_temp == 0)
            {                
                return 1;
            }
            else return 255;
            
        }
        internal void erosiond_fun(ref Bitmap _imgin, ref Bitmap _imgout)
        {

            //byte[] b_temp = new byte[_masksize * _masksize]; ; 
            int bb = 0;
            int i_temp = 0;
            int maskskip = 1;
            BmpData_In = _imgin.LockBits(new Rectangle(0, 0, _imgin.Width, _imgin.Height), ImageLockMode.ReadOnly, _imgin.PixelFormat);
            ByteOfSkip_In = BmpData_In.Stride - _imgin.Width;
            BmpData_Out = _imgout.LockBits(new Rectangle(0, 0, _imgout.Width, _imgout.Height), ImageLockMode.WriteOnly, _imgout.PixelFormat);
            ByteOfSkip_Out = BmpData_Out.Stride - _imgout.Width;
            ImgPtr_In = (byte*)BmpData_In.Scan0.ToPointer();
            ImgPtr_Out = (byte*)BmpData_Out.Scan0.ToPointer();
            ImgPtr_In += (maskskip * BmpData_In.Stride);
            ImgPtr_Out += (maskskip * BmpData_Out.Stride);
            bb += (maskskip * BmpData_Out.Stride);

            for (int i = maskskip; i < _imgin.Height - maskskip; i++)
            {
                ImgPtr_In = ImgPtr_In + maskskip;
                ImgPtr_Out = ImgPtr_Out + maskskip;
                bb = bb + maskskip;
                //0 0 0 0 0
                //0 0 0 0 0
                //0 0 0 0 0
                //0 0 0 0 0
                //0 0 0 0 0
                for (int j = maskskip; j < _imgin.Width - maskskip; j++)
                {
                    //i_temp = Convert.ToInt32(*ImgPtr_In);
                    //int left = Convert.ToInt32(*(ImgPtr_In - BmpData_In.Stride));
                    //int right = Convert.ToInt32(*(ImgPtr_In + BmpData_In.Stride));
                    //int down = Convert.ToInt32(*(ImgPtr_In - 1));
                    //int up = Convert.ToInt32(*(ImgPtr_In + 1));
                    //int ul = Convert.ToInt32(*(ImgPtr_In + 1 - BmpData_In.Stride));
                    //int ur = Convert.ToInt32(*(ImgPtr_In + 1 + BmpData_In.Stride));
                    //int dl = Convert.ToInt32(*(ImgPtr_In + 1 - BmpData_In.Stride));
                    //int dr = Convert.ToInt32(*(ImgPtr_In + 1 + BmpData_In.Stride));

                    //if (up   == 255 || right == 255 || down == 255 || 
                    //    left == 255 || ul    == 255 || ur == 255 || 
                    //    dl   == 255 || dr    == 255)   i_temp = 255; 

                    if (getedmetrix(5,ImgPtr_In) == 1)
                    {
                        i_temp = 0;
                    }
                    else i_temp = 255;
                    


                    //if (i_temp >= otsuvalue) i_temp = 255; else i_temp = 0;

                    * ImgPtr_Out = Convert.ToByte(i_temp);
                    ++ImgPtr_In;
                    ++ImgPtr_Out;
                    bb++;
                }
                ImgPtr_In += ByteOfSkip_In + maskskip;
                ImgPtr_Out += ByteOfSkip_Out + maskskip;
                bb += ByteOfSkip_Out + maskskip;
            }
            _imgin.UnlockBits(BmpData_In);
            _imgout.UnlockBits(BmpData_Out);


        }
    }
}
