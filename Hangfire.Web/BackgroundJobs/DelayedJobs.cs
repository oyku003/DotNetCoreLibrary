using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hangfire.Web.BackgroundJobs
{
    public class DelayedJobs
    {
        public static string AddWaterMarkJob(string fileName, string waterMarkText)
        {
            return Hangfire.BackgroundJob.Schedule(() => ApplyWaterMark(fileName, waterMarkText), TimeSpan.FromSeconds(20));
        }

        public static void ApplyWaterMark(string fileName, string waterMarkText)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pictures", fileName);

            using (var bitmap = Bitmap.FromFile(path))
            {
                using (Bitmap tempBitmap = new Bitmap(bitmap.Width, bitmap.Height))
                {
                    using (Graphics grp = Graphics.FromImage(tempBitmap))
                    {
                        grp.DrawImage(bitmap, 0, 0);

                        var font = new Font(FontFamily.GenericSansSerif, 25, FontStyle.Bold);

                        var color = Color.FromArgb(255, 0, 0);

                        //var brush = new SolidBrush(color)
                    }
                }
            }
        }
    }
}
