using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;


namespace WindowsFormsApplication1
{
    internal unsafe class _20221024
    {
        private BitmapData BmpData_In = new BitmapData();
        private BitmapData BmpData_Out = new BitmapData();
        private byte* ImgPtr_In { get; set; }
        private byte* ImgPtr_Out { get; set; }
        private int StepSize_In, ByteOfSkip_In, StepSize_Out, ByteOfSkip_Out;
        
        internal void Crosserosion(ref Bitmap _imgin, ref Bitmap _imgout, int _masksize)// Laplacian Operator
        {
            if (_masksize % 2 == 0)
                _masksize++;
            //byte[] b_temp = new byte[_masksize * _masksize]; ; 
            int bb = 0;
            int i_temp = 0;
            int maskskip = 1;
            BmpData_In = _imgin.LockBits(new Rectangle(0, 0, _imgin.Width, _imgin.Height), ImageLockMode.ReadOnly, _imgin.PixelFormat);
            Console.WriteLine(Convert.ToString(BmpData_In.Stride));
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
                    i_temp    = Convert.ToInt32(*ImgPtr_In);
                    int left  = Convert.ToInt32(*(ImgPtr_In - BmpData_In.Stride));
                    int right = Convert.ToInt32(*(ImgPtr_In + BmpData_In.Stride));
                    int down  = Convert.ToInt32(*(ImgPtr_In - 1));
                    int up    = Convert.ToInt32(*(ImgPtr_In + 1));
                    if (up==255 || right==255 || down==255 || left==255){i_temp = 0;}
                    
                  
                    
                    
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

    }
}