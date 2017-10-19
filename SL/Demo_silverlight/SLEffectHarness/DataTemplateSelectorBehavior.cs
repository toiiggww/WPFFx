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

namespace SLEffectHarness
{
    public static class Extensions
    {
        public static object TryFindResource(this FrameworkElement uie, object key) 
        {
            if (uie != null)
            {
                return RecurseLookup(uie, key); 
            }
            return null; 
        }

        public static object RecurseLookup(FrameworkElement uie, object key)
        {
            if (uie != null)
            {
                object o = uie.Resources[key];
                if (o != null)
                    return o;

                else
                {
                    return RecurseLookup(VisualTreeHelper.GetParent(uie) as FrameworkElement, key);  
                } 
            }
            return null; 
        } 
    } 

    public class DataTemplateSetterBehavior
    {


        public static string GetTemplateName(DependencyObject obj)
        {
            return (string)obj.GetValue(TemplateNameProperty);
        }

        public static void SetTemplateName(DependencyObject obj, string value)
        {
            obj.SetValue(TemplateNameProperty, value);
        }

        // Using a DependencyProperty as the backing store for TemplateName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TemplateNameProperty =
            DependencyProperty.RegisterAttached("TemplateName", typeof(string), typeof(DataTemplateSetterBehavior),

            new PropertyMetadata(new PropertyChangedCallback(OnTemplateChanged)));

        public static void OnTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if ( d != null )  
            { 
                FrameworkElement e = d as FrameworkElement;
                DataTemplate dt = e.TryFindResource(args.NewValue) as DataTemplate; 
                if ( dt != null ) 
                {
                    ContentControl ct = d as ContentControl ; 
                    if ( ct != null) 
                    ct.ContentTemplate = dt ; 
                }                               
            } 
        } 

    }
}
