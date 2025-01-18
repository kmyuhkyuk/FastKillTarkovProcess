using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace FastKillTarkovProcess.Helpers;

internal class InverseBooleanConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not bool valueBool)
        {
            throw new ArgumentException($"{nameof(value)} is not Bool");
        }

        return !valueBool;
    }

    [Obsolete(nameof(NotImplementedException), true)]
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}