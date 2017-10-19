using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ExtensibleDemoApp;
using System.Windows.Controls.Theming;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using TransitionEffects;

namespace SLEffectHarness
{    

    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            this.DataContext = new ViewModel();

            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel model = this.DataContext as ViewModel;
            Border parent = this.ParentPlaceHolder;
            for (int x = 0; x < model.Effects.Count; x++)
            {
                Border b = new Border();

                // b.DataContext = model.Effects[x];
                Binding binding = new Binding("DisplayedEffect");
                binding.Source = model.Effects[x];
                 
                b.SetBinding(Border.EffectProperty, binding);
                parent.Child = b;
                parent = b;

            }
            parent.Child = this.ContentPlaceHolder  ;
        }

        private void OnToggleTransitionsClick (object sender, RoutedEventArgs e)
        {

            if (!transition.IsRunning)
            {
                WriteableBitmap current = new WriteableBitmap((int)this.EffectsPlaceHolder.ActualWidth, (int)this.EffectsPlaceHolder.ActualHeight, PixelFormats.Bgr32);
                current.Render(this.EffectsPlaceHolder, new MatrixTransform());

                ImageBrush currentBrush = new ImageBrush();
                currentBrush.ImageSource = current;
                BloodTransitionEffect bloodEffect = new BloodTransitionEffect();

                Storyboard sb = TransitionHelper.CreateTransition
                    (bloodEffect, this.TransitionsPlaceHolder, TimeSpan.FromSeconds(1), 0, 1, currentBrush);

                sb.Completed += new EventHandler(sb_Completed);
                this.TransitionsPlaceHolder.Visibility = Visibility.Visible;
                sb.Begin();
                this.EffectsPlaceHolder.Visibility = Visibility.Collapsed;
                btnStop.Content = "Stop Transitions"; 
            }
            else
            {
                transition.StopTransitions(); 

                WriteableBitmap current = new WriteableBitmap((int)this.TransitionsPlaceHolder.ActualWidth, (int)this.TransitionsPlaceHolder.ActualHeight, PixelFormats.Bgr32);
                current.Render(this.TransitionsPlaceHolder, new MatrixTransform());

                ImageBrush currentBrush = new ImageBrush();
                currentBrush.ImageSource = current;
                RadialWiggleTransitionEffect bloodEffect = new RadialWiggleTransitionEffect();

                
                Storyboard sb = TransitionHelper.CreateTransition
                    (bloodEffect, this.EffectsPlaceHolder, TimeSpan.FromSeconds(1), 0, 1, currentBrush);

                //sb.Completed += new EventHandler(sb_Completed);
                this.EffectsPlaceHolder.Visibility = Visibility.Visible;
                sb.Begin();
                this.TransitionsPlaceHolder.Visibility = Visibility.Collapsed;
                btnStop.Content = "Start Transition"; 
            } 
        }

        void sb_Completed(object sender, EventArgs e)
        {
            this.TransitionsPlaceHolder.Visibility = Visibility.Visible; 
            this.EffectsPlaceHolder.Visibility = Visibility.Collapsed;
            transition.StartTransitions();           
        }

       



 

    }
}
