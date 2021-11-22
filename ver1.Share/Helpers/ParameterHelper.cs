using demo_ver1.Share.Enums;

namespace demo_ver1.Share.Helpers;
public static class ParameterHelper
{
    public static ParameterType Convert(Type type)
    {
        if (type.Equals(typeof(string)) || type.Equals(typeof(String)))
        {
            return ParameterType.String;
        }

        if (type.Equals(typeof(int)) || type.Equals(typeof(Int32)))
        {
            return ParameterType.Integer;
        }

        if (type.Equals(typeof(double)) || type.Equals(typeof(Double)) || type.Equals(typeof(float)))
        {
            return ParameterType.Double;
        }

        if (type.Equals(typeof(bool)) || type.Equals(typeof(Boolean)))
        {
            return ParameterType.Boolean;
        }

        return ParameterType.UnSupported;
    }

    public static object Default(ParameterType type)
    {
        return type switch
        {
            ParameterType.Boolean => false,
            ParameterType.Double => (double)0.0,
            ParameterType.Integer => 0,
            _ => "",
        };
    }

    public static bool ValidateValue(ParameterType type, string value)
    {
        return type switch
        {
            ParameterType.Boolean => Boolean.TryParse(value, out _),
            ParameterType.Double => Double.TryParse(value, out _),
            ParameterType.Integer => Int32.TryParse(value, out _),
            ParameterType.String => true,
            _ => false,
        };
    }
}
