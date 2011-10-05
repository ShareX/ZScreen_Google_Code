/*  HaRepacker - WZ extractor and repacker
 * Copyright (C) 2009, 2010 haha01haha01
   
 * This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

 * This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

 * You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace SharpApng
{
    public class Frame : IDisposable
    {
        private int m_num;
        private int m_den;
        private Bitmap m_bmp;

        public void Dispose()
        {
            m_bmp.Dispose();
        }

        public Frame(Bitmap bmp, int num, int den)
        {
            this.m_num = num;
            this.m_den = den;
            this.m_bmp = bmp;
        }

        public int DelayNum
        {
            get
            {
                return m_num;
            }
            set
            {
                m_num = value;
            }
        }

        public int DelayDen
        {
            get
            {
                return m_den;
            }
            set
            {
                m_den = value;
            }
        }

        public Bitmap Bitmap
        {
            get
            {
                return m_bmp;
            }
            set
            {
                m_bmp = value;
            }
        }
    }

}