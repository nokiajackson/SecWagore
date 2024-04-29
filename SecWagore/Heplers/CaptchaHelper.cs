using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
internal class CaptchaHelper
{
    public static string GetCode(int length=6)
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static byte[] GetByteArray(string code)
    {
        Bitmap bitmap = new Bitmap(150, 50);
        Graphics graphics = Graphics.FromImage(bitmap);
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.Clear(Color.White);

        // 添加干擾線
        AddNoise(graphics, bitmap.Width, bitmap.Height);

        // 添加驗證碼文字
        Font font = new Font("Arial", 20, FontStyle.Bold);
        graphics.DrawString(code, font, Brushes.Black, new PointF(10, 10));

        // 將圖片保存為byte數組
        using (MemoryStream ms = new MemoryStream())
        {
            bitmap.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }
    }
    

    private static void AddNoise(Graphics graphics, int width, int height)
    {
        Random rand = new Random();
        Pen pen = new Pen(Color.LightGray);

        for (int i = 0; i < 10; i++)
        {
            int x1 = rand.Next(width);
            int y1 = rand.Next(height);
            int x2 = rand.Next(width);
            int y2 = rand.Next(height);
            graphics.DrawLine(pen, x1, y1, x2, y2);
        }
    }
    
}