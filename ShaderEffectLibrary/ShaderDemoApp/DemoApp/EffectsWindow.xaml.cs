using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Media.Effects;
using Microsoft.Win32;
using System.IO;
using System.Windows.Interop;

namespace ExtensibleDemoApp
{

    public partial class EffectsWindow : Window
    {
        public EffectsWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModel();
            this.Loaded += new RoutedEventHandler(Window1_Loaded);
        }

        void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel model = this.DataContext as ViewModel ;

            Border parent = this.ParentPlaceHolder; 
            for (int x = 0; x < model.Effects.Count; x++)
            {
                Border b = new Border(); 

                // b.DataContext = model.Effects[x];
                Binding binding = new Binding("DisplayedEffect");
                binding.Source = model.Effects[x]; 
                
                b.SetBinding(Border.EffectProperty, binding); 
                parent.Child = b ; 
                parent = b;  
               
            }
            parent.Child = this.ContentPlaceHolder;
            this.MouseMove += new MouseEventHandler(Window1_MouseMove);
            this.contentTypes.SelectedIndex = 0; 
        }

        void Window1_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(this.ContentPlaceHolder);

            MousePositionAttachedBehavior.SetX(this, p.X);
            MousePositionAttachedBehavior.SetY(this, p.Y); 

        }

         

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
     
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPEG|.jpg"; 
            if (dlg.ShowDialog().Value)
            {
                string filename = dlg.FileName;
                

                int resolutionX = 96;
                int resolutionY = 96;
                try
                {
                    WindowInteropHelper wih = new WindowInteropHelper(Application.Current.MainWindow);
                    System.Drawing.Graphics desktop = System.Drawing.Graphics.FromHwnd(wih.Handle);
                    resolutionX = (int) desktop.DpiX;
                    resolutionY = (int) desktop.DpiY; 
                }
                finally
                { } 
                     
                //use RenderTargetBitmap
                RenderTargetBitmap rtb = new RenderTargetBitmap(
                    (int)ParentPlaceHolder.ActualWidth,
                    (int)ParentPlaceHolder.ActualHeight,
                    resolutionX, resolutionY, PixelFormats.Pbgra32);
                rtb.Render(ParentPlaceHolder);

                FileStream fs = new FileStream(filename, FileMode.Create);
                // BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                JpegBitmapEncoder encoder = new JpegBitmapEncoder(); 
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                encoder.Save(fs);
                fs.Close();
            } 
        }

    }
}
