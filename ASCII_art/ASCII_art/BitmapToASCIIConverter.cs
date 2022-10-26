using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ASCII_art
{
	public class BitmapToASCIIConverter
	{
		private readonly char[] _asciitable = { '.', ',', ':', '+', '*', '?', '%', 'S', '#', '@' };
		private readonly Bitmap _bitmap;

		public BitmapToASCIIConverter(Bitmap bitmap)
		{
			_bitmap = bitmap;
		}


		public char[][] Convert()
		{
			return Convert(_asciitable);
		}


		public char[][] ConvertAsNegative()
		{
			return Convert(_asciitable.Reverse().ToArray());
		}


		private char[][] Convert(char[] asciitable)
		{
			var result = new char[_bitmap.Height][];

			
			for (int y = 0; y < _bitmap.Height; ++y)
			{
				result[y] = new char[_bitmap.Width];
				for (int x = 0; x < _bitmap.Width; ++x)
				{
					int mapIndex = (int)Map(_bitmap.GetPixel(x, y).R, 0, 255, 0, asciitable.Length - 1);
					result[y][x] = asciitable[mapIndex];
				}
			}


			return result;
		}


		private float Map(float valueToMap, float start1, float stop1, float start2, float stop2)
		{
			return ((valueToMap - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
		}
	}
}
