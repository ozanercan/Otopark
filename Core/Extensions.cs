using System;

namespace Core
{
    public static class Extensions
    {
        public static bool ToBoolean(this object parameter)
        {
            return Convert.ToBoolean(parameter);
        }

        public static bool ToBoolean(this int parameter)
        {
            return Convert.ToBoolean(parameter);
        }

        public static double ToDouble(this object parameter)
        {
            return Convert.ToDouble(parameter);
        }

        public static int ToInt32(this object parameter)
        {
            return Convert.ToInt32(parameter);
        }

        public static DateTime ToDateTime(this object parameter)
        {
            return Convert.ToDateTime(parameter);
        }
    }
}
