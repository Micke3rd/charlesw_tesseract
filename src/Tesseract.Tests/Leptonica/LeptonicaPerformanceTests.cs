﻿

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace Tesseract.Tests.Leptonica
{
	[TestClass]
	[Ignore("Performance tests are disabled by default, theres probably a better way of doing this but for now it's ok")]
	public class LeptonicaPerformanceTests
	{
		[TestMethod]
		public void ConvertToBitmap()
		{
			const double BaseRunTime = 793.382;
			const int Runs = 1000;

			var sourceFilePath = Path.Combine("./Data/Conversion","photo_palette_8bpp.tif");
			using (var bmp = new Bitmap(sourceFilePath))
			{
				// Don't include the first conversion since it will also handle loading the library etc (upfront costs).
				using (var pix = PixConverter.ToPix(bmp))
				{
				}

				// copy 100 times take the average
				var watch = new Stopwatch();
				watch.Start();
				for (var i = 0; i < Runs; i++)
				{
					using (var pix = PixConverter.ToPix(bmp))
					{
					}
				}
				watch.Stop();

				var delta = watch.ElapsedTicks / (BaseRunTime * Runs);
				Console.WriteLine("Delta: {0}",delta);
				Console.WriteLine("Elapsed Ticks: {0}",watch.ElapsedTicks);
				Console.WriteLine("Elapsed Time: {0}ms",watch.ElapsedMilliseconds);
				Console.WriteLine("Average Time: {0}ms",(double)watch.ElapsedMilliseconds / Runs);

				Assert.AreEqual(delta,1.0,0.25);
			}
		}
	}
}

