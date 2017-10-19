// <copyright file="TrainmasterCannonball.xaml.cs" company="Microsoft Corporation">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Michael S. Scherotter</author>
// <email>mischero@microsoft.com</email>
// <date>2008-10-10</date>
// <summary>Trainmaster Cannonbacll watch user control</summary>

namespace Synergist.BallWatch
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Browser;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;

    /// <summary>
    /// Ball Trainmaster Cannonball Watch user control
    /// </summary>
    [Description("Ball Trainmaster Cannonball Watch")]
    public partial class TrainmasterCannonball : UserControl
    {
        #region Member data
        /// <summary>
        /// True if the watch shows daytime, false if night
        /// </summary>
        private bool isDay = true;

        /// <summary>
        /// True if the about splash screen is shown
        /// </summary>
        private bool aboutShown;

        /// <summary>
        /// True if the top button is pressed
        /// </summary>
        private bool topButtonPressed;

        /// <summary>
        /// True if the bottom button is pressed
        /// </summary>
        private bool bottomButtonPressed;

        /// <summary>
        /// The state of the chronograph
        /// 0=stopped, 1 = running, 2=paused
        /// </summary>
        private int chronographState; 
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the TrainmasterCannonball class.
        /// </summary>
        public TrainmasterCannonball()
        {
            // Required to initialize variables
            InitializeComponent();
        }

        #endregion

        #region Implementation
        /// <summary>
        /// Update the seconds animation to add one second ticks
        /// </summary>
        /// <param name="animation">the animation</param>
        private static void UpdateSecondAnimation(DoubleAnimationUsingKeyFrames animation)
        {
            animation.KeyFrames.Clear();

            for (var i = 0; i <= 60; i++)
            {
                var keyFrame = new DiscreteDoubleKeyFrame()
                {
                    KeyTime = new TimeSpan(0, 0, i),
                    Value = i * 6
                };

                animation.KeyFrames.Add(keyFrame);
            }
        }

        /// <summary>
        /// event handler for when the page size changes
        /// </summary>
        /// <param name="sender">the page user control</param>
        /// <param name="e">the size changed event arguments</param>
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Resize(e.NewSize.Width, e.NewSize.Height);
        }

        /// <summary>
        /// Page load event handler
        /// </summary>
        /// <param name="sender">the page user control</param>
        /// <param name="e">the routed event args</param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            return; 

            this.Resize(ActualWidth, ActualHeight);

            UpdateSecondAnimation(SecondAnimation);
            UpdateSecondAnimation(ChronographSecondAnimation);

            this.RunWatch();

            Day.Begin();
        }

        /// <summary>
        /// Run the watch
        /// </summary>
        private void RunWatch()
        {
            var time = DateTime.Now;

            Run.Begin();

            Run.Seek(new TimeSpan(time.Hour, time.Minute, time.Second));

            DateNumber.Text = time.Day.ToString(System.Globalization.CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Resize the watch
        /// </summary>
        /// <param name="width">the width to resize to</param>
        /// <param name="height">the height to resize to</param>
        private void Resize(double width, double height)
        {
            if (width == 0 || height == 0)
            {
                return;
            }

            //double availHeight = (double) HtmlPage.Window.Eval("screen.availHeight");

            //height = Math.Min(height, availHeight);

            var scaleX = width / Watch.Width;
            var scaleY = height / Watch.Height;

            var scale = Math.Min(scaleX, scaleY);
            PageScale.ScaleX = scale;
            PageScale.ScaleY = scale;

            PageTranslation.X = (width - (Watch.Width * scale)) / 2;
            PageTranslation.Y = (height - (Watch.Height * scale)) / 2;
        }

        /// <summary>
        /// Event handler for pressing the top button
        /// </summary>
        /// <param name="sender">the top button</param>
        /// <param name="e">mouse button event args</param>
        private void OnPressTopButton(object sender, MouseButtonEventArgs e)
        {
            PressTopButton.Begin();

            Rectangle rect = sender as Rectangle;

            this.topButtonPressed = rect.CaptureMouse();

            switch (this.chronographState)
            {
                case 0:
                    // Stopped
                    StartChronograph.Begin();
                    this.chronographState = 1;
                    break;

                case 1:
                    // Running
                    StartChronograph.Pause();
                    this.chronographState = 2;
                    break;

                case 2:
                    // Paused
                    StartChronograph.Resume();
                    this.chronographState = 1;
                    break;
            }
        }

        /// <summary>
        /// Event handler for releaseing the top button
        /// </summary>
        /// <param name="sender">the top button</param>
        /// <param name="e">the mouse button event args</param>
        private void OnReleaseTopButton(object sender, MouseButtonEventArgs e)
        {
            if (this.topButtonPressed)
            {
                ReleaseTopButton.Begin();
                this.topButtonPressed = false;
            }
        }

        /// <summary>
        /// Press the bottom button
        /// </summary>
        /// <param name="sender">bottom button</param>
        /// <param name="e">mouse button event args</param>
        private void OnPressBottomButton(object sender, MouseButtonEventArgs e)
        {
            PressBottomButton.Begin();

            StartChronograph.Stop();

            this.chronographState = 0;

            var rectangle = sender as Rectangle;

            this.bottomButtonPressed = rectangle.CaptureMouse();
        }

        /// <summary>
        /// release the bottom button 
        /// </summary>
        /// <param name="sender">the bottom button</param>
        /// <param name="e">the mouse button event args</param>
        private void OnReleaseBottomButton(object sender, MouseButtonEventArgs e)
        {
            if (this.bottomButtonPressed)
            {
                ReleaseBottomButton.Begin();

                this.bottomButtonPressed = false;
            }
        }

        /// <summary>
        /// Switch to/from day to night
        /// </summary>
        /// <param name="sender">the day/night button</param>
        /// <param name="e">the mouse button event args</param>
        private void OnDay(object sender, MouseButtonEventArgs e)
        {
            if (this.isDay)
            {
                Night.Begin();
            }
            else
            {
                Day.Begin();
            }

            this.isDay = !this.isDay;
        }

        /// <summary>
        /// Open a hyperlink
        /// </summary>
        /// <param name="sender">the hyperlink</param>
        /// <param name="e">the mouse button event arguments</param>
        private void OpenLink(object sender, MouseButtonEventArgs e)
        {
            Canvas senderCanvas = sender as Canvas;

            HtmlPage.Window.Navigate(new Uri(senderCanvas.Tag.ToString(), UriKind.Absolute), "_blank");
        }

        /// <summary>
        /// Show the about splash screen
        /// </summary>
        /// <param name="sender">the about splash screen button</param>
        /// <param name="e">the mouse button event args</param>
        private void OnShowAbout(object sender, MouseButtonEventArgs e)
        {
            Storyboard animation;

            if (this.aboutShown)
            {
                animation = HideAbout;
            }
            else
            {
                animation = ShowAbout;
            }

            animation.Begin();

            this.aboutShown = !this.aboutShown;

            About.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Toggle full screen
        /// </summary>
        /// <param name="sender">the full screen button</param>
        /// <param name="e">the mouse button event args</param>
        private void OnFullScreen(object sender, MouseButtonEventArgs e)
        {
            var content = new System.Windows.Interop.Content();

            content.IsFullScreen = !content.IsFullScreen;
        }
        #endregion
    }
}