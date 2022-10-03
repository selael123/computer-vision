using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;


namespace WindowsFormsApplication1
{
    class _20221003
    {
        internal unsafe class VisionTransform
        {
            private BitmapData BmpData_In = new BitmapData();
            private BitmapData BmpData_Out = new BitmapData();
            private byte* ImgPtr_In { get; set; }
            private byte* ImgPtr_Out { get; set; }
            private int StepSize_In, ByteOfSkip_In, StepSize_Out, ByteOfSkip_Out;

            internal void AverageFilter(ref Bitmap _imgin, ref Bitmap _imgout, int _masksize)//AverageFilter
            {
                if (_masksize % 2 == 0)
                _masksize++;
            int i_temp = 0; int bb = 0;
            double d_temp = 0;
            int maskskip = (_masksize - 1) / 2;
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
                for (int j = maskskip; j < _imgin.Width - maskskip; j++)
                {
                    i_temp = 0;
                    for (int m = 0; m < _masksize; m++)
                    {
                        for (int n = 0; n < _masksize; n++)
                        {
                            i_temp += Convert.ToInt32(*(ImgPtr_In + (m - 1) * BmpData_In.Stride + (n - maskskip)));
                        }
                    }
                    d_temp = (double)(i_temp) / (double)(_masksize * _masksize);
                    *ImgPtr_Out = Convert.ToByte(d_temp); ;
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

            internal void MediumFilter(ref Bitmap _imgin, ref Bitmap _imgout, int _masksize)//MediumFilter

            {
                if (_masksize % 2 == 0)
                    _masksize++;
                byte[] b_temp = new byte[_masksize * _masksize]; ; int bb = 0;
                int maskskip = (_masksize - 1) / 2;
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
                    for (int j = maskskip; j < _imgin.Width - maskskip; j++)
                    {
                        for (int m = 0; m < _masksize; m++)
                        {
                            for (int n = 0; n < _masksize; n++)
                            {
                                b_temp[n + m * _masksize] = *(ImgPtr_In + (m - 1) * BmpData_In.Stride + (n - maskskip));
                            }
                        }
                        Array.Sort(b_temp);
                        *ImgPtr_Out = b_temp[((_masksize * _masksize) - 1) / 2];
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

            internal void  LaplacianOperator(ref Bitmap _imgin, ref Bitmap _imgout, int _masksize)// Laplacian Operator
            {
                if (_masksize % 2 == 0)
                    _masksize++;
                //byte[] b_temp = new byte[_masksize * _masksize]; ; 
                int bb = 0;
                int i_temp =0;
                int maskskip = (_masksize - 1) / 2;
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
                    for (int j = maskskip; j < _imgin.Width - maskskip; j++)
                    {
                        i_temp = Convert.ToInt32(*ImgPtr_In) * -4;
                        i_temp += Convert.ToInt32(*(ImgPtr_In - BmpData_In.Stride));
                        i_temp += Convert.ToInt32(*(ImgPtr_In + BmpData_In.Stride));
                        i_temp += Convert.ToInt32(*(ImgPtr_In - 1));
                        i_temp += Convert.ToInt32(*(ImgPtr_In + 1));
                        i_temp = Math.Abs(i_temp);
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
}