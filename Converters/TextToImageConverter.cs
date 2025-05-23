using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using TuwenDayinDian.Models;

namespace TuwenDayinDian.Converters
{
    public class TextToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MenuItem menuItem)
            {
                if (!string.IsNullOrWhiteSpace(menuItem.ImageIcon))
                {
                    return new BitmapImage(new Uri(menuItem.ImageIcon));
                }
                else if (!string.IsNullOrWhiteSpace(menuItem.MenuText))
                {
                    // TODO: Use the DrawText method to generate an image from the first character of the MenuText.
                    // Note: The following is a placeholder and should be replaced with actual image generation logic.
                    var visual = new DrawingVisual();
                    using (var context = visual.RenderOpen())
                    {
                        var text = new FormattedText(
                            menuItem.MenuText.Substring(0, 1),
                            CultureInfo.InvariantCulture,
                            System.Windows.FlowDirection.LeftToRight,
                            new Typeface("Microsoft YaHei UI"),
                            16,
                            Brushes.White);

                        context.DrawText(text, new System.Windows.Point(13 - text.Width / 2, 12 - text.Height / 2));  // Centered
                    }
                    var renderTargetBitmap = new RenderTargetBitmap(26, 26, 96, 96, PixelFormats.Default);
                    renderTargetBitmap.Render(visual);
                    return renderTargetBitmap;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
