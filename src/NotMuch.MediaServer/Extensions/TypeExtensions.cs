using System.Collections.Generic;

namespace NotMuch.MediaServer.Extensions;

internal static class TypeExtensions
{
    public static TType As<TType>(this object obj)
    {
        return (TType)obj;
    }
}
