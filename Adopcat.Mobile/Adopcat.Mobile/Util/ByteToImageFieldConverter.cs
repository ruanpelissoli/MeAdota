using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace Adopcat.Mobile.Util
{
    public class ByteToImageFieldConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ImageSource retSource = null;
            if (value != null)
            {
                byte[] imageAsBytes;
                if (value is IList<byte[]>)
                    imageAsBytes = (value as IList<byte[]>)[0];
                else
                    imageAsBytes = (byte[])value;

                retSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
            }
            return retSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
