using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

public static class Extensions
{
     
    public static Color FromScRgb(this Color c, double a, double r, double g, double b)
    {

        return Color.FromArgb((byte)Math.Ceiling(a * 255),
            (byte)Math.Ceiling(r * 255),
            (byte)Math.Ceiling(g * 255),
            (byte)Math.Ceiling(b * 255)); 

                                
    } 
} 

namespace System.ComponentModel
{
    public class BrowsableAttribute : Attribute
    {

        public BrowsableAttribute(bool unused)
        {
        }
    } 
} 
