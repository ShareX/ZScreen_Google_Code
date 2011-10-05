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
    public class Apng : IDisposable
    {
        private List<Frame> m_frames = new List<Frame>();

        public Apng()
        {
        }

        public void Dispose()
        {
            foreach (Frame frame in m_frames)
                frame.Dispose();
            m_frames.Clear();
        }

        public Frame this[int index]
        {
            get
            {
                if (index < m_frames.Count) return m_frames[index];
                else return null;
            }
            set
            {
                if (index < m_frames.Count) m_frames[index] = value;
            }
        }

        public void AddFrame(Frame frame)
        {
            m_frames.Add(frame);
        }

        public void AddFrame(Bitmap bmp, int num, int den)
        {
            m_frames.Add(new Frame(bmp, num, den));
        }

        private Bitmap ExtendImage(Bitmap source, Size newSize)
        {
            Bitmap result = new Bitmap(newSize.Width, newSize.Height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImageUnscaled(source, 0, 0);
            }
            return result;
        }

        public void WriteApng(string path, bool firstFrameHidden, bool disposeAfter)
        {
            Size maxSize = new Size();
            foreach (Frame frame in m_frames)
            {
                if (frame.Bitmap.Width > maxSize.Width) maxSize.Width = frame.Bitmap.Width;
                if (frame.Bitmap.Height > maxSize.Height) maxSize.Height = frame.Bitmap.Height;
            }
            for (int i = 0; i < m_frames.Count; i++)
            {
                Frame frame = m_frames[i];
                if (frame.Bitmap.Width != maxSize.Width || frame.Bitmap.Height != maxSize.Height)
                    frame.Bitmap = ExtendImage(frame.Bitmap, maxSize);
                ApngBasicWrapper.CreateFrameManaged(frame.Bitmap, frame.DelayNum, frame.DelayDen, i);
            }
            ApngBasicWrapper.SaveApngManaged(path, m_frames.Count, maxSize.Width, maxSize.Height, firstFrameHidden);
            if (disposeAfter) Dispose();
        }
    }
}