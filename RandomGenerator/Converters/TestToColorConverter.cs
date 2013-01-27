using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace RandomGenerator.Converters
{
    [ValueConversion(typeof(double), typeof(SolidColorBrush))]
    public class TestToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double lowLimit,
                   highLimit;

            switch ((string) parameter)
            {
                case "Poker":
                    {
                        lowLimit = 2.16;
                        highLimit = 46.17;
                        break;
                    }
                case "SingleBits":
                case "BitsPairs":
                    {
                        lowLimit = 0.48625;
                        highLimit = 0.51375;
                        break;
                    }
                case "Series":
                    {
                        lowLimit = 0.01;
                        highLimit = 200000.0;
                        break;
                    }
                default:
                    {
                        lowLimit = -100.0;
                        highLimit = -0.1;
                        break;
                    }
            }

            var brush = new SolidColorBrush();
            if ((double) value > lowLimit && (double) value < highLimit || (string) parameter == "Correllation" && Math.Abs((double) value - 0.0) > 0.0001)
            {
                brush.Color = Color.FromRgb(0x19, 0x99, 0x12);
            }
            else
            {
                brush.Color = Color.FromRgb(0xF0, 0x2E, 0x12);
            }

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("You cannot convert back from color to single bits test!");
        }
    }
}
