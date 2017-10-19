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
using System.Windows.Shapes;
using TransitionEffects;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace ExtensibleDemoApp
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class TransitionsWindow : Window
    {
        public TransitionsWindow()
        {
            InitializeComponent();
            this.timer = new DispatcherTimer(TimeSpan.FromSeconds(4), DispatcherPriority.Input, this.OnTimerTick, Dispatcher);
            this.Loaded += new RoutedEventHandler(Window2_Loaded);
        }

        void Window2_Loaded(object sender, RoutedEventArgs e)
        {
            List<BitmapSource> images = new List<BitmapSource>();
            for (int x = 0; x < 10 ; x++)
            {
                BitmapSource  bs = new BitmapImage(new Uri("TransitionImages/" + x + ".jpg", UriKind.Relative));
                // BitmapSource bs = new BitmapImage(new Uri( @"C:\Windows\Web\Wallpaper\img" + x.ToString() + ".jpg", UriKind.Absolute));                
                // bs.Freeze(); 
                images.Add(bs); 
            }
            // ((Image)this.photoHost.Child).Source = images[0];
            PhotoSlideShow.MoveFirst(); 
            this.PhotoSlideShow = new PhotoSlideShow();
            this.PhotoSlideShow.Images = images;
            this.timer.Start(); 

        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            
            if (this.PhotoSlideShow != null)
            {
                this.PhotoSlideShow.MoveNext();
            }
            this.ChangePhoto(true);
        }


        private void SwapChildren()
        {
            this.currentChild.Source = PhotoSlideShow.CurrentPhoto;
            this.oldChild.Source = PhotoSlideShow.PreviousPhoto;
            /* 
            Image temp = this.currentChild;
            this.currentChild = this.oldChild;
            this.oldChild = temp;
            this.currentChild.Width = double.NaN;
            this.currentChild.Height = double.NaN;
            if (this.photoHost != null)
            {
                this.photoHost.Child = this.currentChild;
            }*/ 

            this.oldChild.Effect = null;
        }


        private void ChangePhoto(bool applyTransitionEffect)
        {
            if (this.PhotoSlideShow != null /*&& !this.oldChild.ImageDownloadInProgress*/ )
            {
                if (applyTransitionEffect)
                {
                    this.SwapChildren();
                    this.ApplyTransitionEffect();
                }
                else
                {
                    // Apply the current slide show content. 
                    // Load the old child with the next photo so it will advance to the next photo if the user resumes play.

                    // PhotoSlideShow.MoveNext(); 

                    this.currentChild.Source = PhotoSlideShow.CurrentPhoto ;
                    this.oldChild.Source = PhotoSlideShow.PreviousPhoto;
                    // this.photoHost.Child = currentChild; 
                }
            }
        }


        private DispatcherTimer timer; 

        private static TransitionEffect[][] transitionEffects = new TransitionEffect[][]
        {
            new TransitionEffect[]
            {
                new ShrinkTransitionEffect(),
                new BlindsTransitionEffect(),
                new CloudRevealTransitionEffect(),
                new RandomCircleRevealTransitionEffect(),
                new FadeTransitionEffect(),
            },
            new TransitionEffect[]
            {
                new WaveTransitionEffect(),
                new RadialWiggleTransitionEffect(),
            },
            new TransitionEffect[]
            {  
                new BloodTransitionEffect(),
                new CircleStretchTransitionEffect(),
            },   
            new TransitionEffect[]
            {  
                new DisolveTransitionEffect(),
                new DropFadeTransitionEffect(),   
            },
            new TransitionEffect[]
            {
                new RotateCrumbleTransitionEffect(),
                new WaterTransitionEffect(),
                new CrumbleTransitionEffect(),
            },
            new TransitionEffect[]
            {
                new RadialBlurTransitionEffect(),
                new CircularBlurTransitionEffect(),
            },
            new TransitionEffect[]
            {
                new PixelateTransitionEffect(),
                new PixelateInTransitionEffect(),
                new PixelateOutTransitionEffect(),
            },
            new TransitionEffect[]
            {   
                new SwirlGridTransitionEffect(Math.PI * 4), 
                new SwirlGridTransitionEffect(Math.PI * 16),
                new SmoothSwirlGridTransitionEffect(Math.PI * 4),
                new SmoothSwirlGridTransitionEffect(Math.PI * 16),
                new SmoothSwirlGridTransitionEffect(-Math.PI * 8),
                new SmoothSwirlGridTransitionEffect(-Math.PI * 6),
            },
            new TransitionEffect[]
            {
                new MostBrightTransitionEffect(),
                new LeastBrightTransitionEffect(),
                new SaturateTransitionEffect(),
            },
            new TransitionEffect[]
            {
                new BandedSwirlTransitionEffect(Math.PI / 5.0, 50.0),
                new BandedSwirlTransitionEffect(Math.PI, 10.0),
                new BandedSwirlTransitionEffect(-Math.PI, 10.0),
            },
            new TransitionEffect[]
            {
                new CircleRevealTransitionEffect { FuzzyAmount= 0.0} , 
                new CircleRevealTransitionEffect { FuzzyAmount= 0.1},
                new CircleRevealTransitionEffect { FuzzyAmount=0.5} 
            },
            new TransitionEffect[]
            {
                /* (Point origin, Point normal, Point offset, double fuzziness)*/ 
                new LineRevealTransitionEffect{LineOrigin= new Point(-0.2, -0.2), LineNormal = new Point(1, 0), LineOffset= new Point(1.4, 0), FuzzyAmount = 0.2},
                new LineRevealTransitionEffect{LineOrigin= new Point(1.2, -0.2), LineNormal = new Point(-1, 0), LineOffset= new Point(-1.4, 0), FuzzyAmount = 0.2},
                new LineRevealTransitionEffect{LineOrigin= new Point(-.2, -0.2), LineNormal = new Point(0, 1), LineOffset= new Point(0, 1.4), FuzzyAmount = 0.2},
                new LineRevealTransitionEffect{LineOrigin= new Point(-0.2, 1.2), LineNormal = new Point(0, -1), LineOffset= new Point(0, -1.4), FuzzyAmount = 0.2},
                new LineRevealTransitionEffect{LineOrigin= new Point(-0.2, -0.2), LineNormal = new Point(1, 1), LineOffset= new Point(1.4, 1.4), FuzzyAmount = 0.2},
                new LineRevealTransitionEffect{LineOrigin= new Point(1.2, 1.2), LineNormal = new Point(-1, -1), LineOffset= new Point(-1.4, -1.4), FuzzyAmount = 0.2},
                new LineRevealTransitionEffect{LineOrigin= new Point(1.2, -0.2), LineNormal = new Point(-1, 1), LineOffset= new Point(-1.4, 1.4), FuzzyAmount = 0.2},
                new LineRevealTransitionEffect{LineOrigin= new Point(-0.2, 1.2), LineNormal = new Point(1, -1), LineOffset= new Point(1.4, -1.4), FuzzyAmount = 0.2},
            },
            new TransitionEffect[]
            {
                new RippleTransitionEffect(),
                new RippleTransitionEffect(),
                new RippleTransitionEffect(),
                new RippleTransitionEffect(),
                new RippleTransitionEffect(),
            },
            new TransitionEffect[]
            {
                new SlideInTransitionEffect{ SlideAmount= new Point(1, 0)},
                new SlideInTransitionEffect{ SlideAmount=new Point(0, 1)},
                new SlideInTransitionEffect{ SlideAmount=new Point(-1, 0)},
                new SlideInTransitionEffect{ SlideAmount=new Point(0, -1)},
            },
            new TransitionEffect[]
            {
                new SwirlTransitionEffect(Math.PI * 4),
                new SwirlTransitionEffect(-Math.PI * 4),
                new SwirlTransitionEffect(Math.PI * 4),
                new SwirlTransitionEffect(-Math.PI * 4),
            },
        };



        private static TransitionEffect[][] transitionEffectsSingle = new TransitionEffect[][]
        {
            new TransitionEffect[]
            {
                new ShrinkTransitionEffect(), 
            }, 
            new TransitionEffect[]
            { 
                new BlindsTransitionEffect(),
            }, 
            new TransitionEffect[]
            { 
                new CloudRevealTransitionEffect(),
            }, 
            new TransitionEffect [] 
            { 
                new RandomCircleRevealTransitionEffect(),
            }, 
            new  TransitionEffect [] 
            { 
                new FadeTransitionEffect(),
            }, 
            new TransitionEffect[]
            {
                new WaveTransitionEffect(),
            },   
            new TransitionEffect[]
            { 
                new RadialWiggleTransitionEffect(),
            },
            new TransitionEffect[]
            {  
                new BloodTransitionEffect(),
            },  
            new TransitionEffect[]
            { 
                new CircleStretchTransitionEffect(),
            },   
            new TransitionEffect[]
            {  
                new DisolveTransitionEffect(),
            },   
            new TransitionEffect[]
            {
                new DropFadeTransitionEffect(),   
            },
            new TransitionEffect[]
            {
                new RotateCrumbleTransitionEffect(),
            },   
            new TransitionEffect[]
            { 
                new WaterTransitionEffect(),
            },   
            new TransitionEffect[]
            { 
                new CrumbleTransitionEffect(),
            },
            new TransitionEffect[]
            {
                new RadialBlurTransitionEffect(),
            },   
            new TransitionEffect[]
            { 
                new CircularBlurTransitionEffect(),
            },
            new TransitionEffect[]
            {
                new PixelateTransitionEffect(),
                new PixelateInTransitionEffect(),
                new PixelateOutTransitionEffect(),
            },
            new TransitionEffect[]
            {   
                new SwirlGridTransitionEffect(Math.PI * 4), 
                new SwirlGridTransitionEffect(Math.PI * 16),
                new SmoothSwirlGridTransitionEffect(Math.PI * 4),
                new SmoothSwirlGridTransitionEffect(Math.PI * 16),
                new SmoothSwirlGridTransitionEffect(-Math.PI * 8),
                new SmoothSwirlGridTransitionEffect(-Math.PI * 6),
            },
            new TransitionEffect[]
            {
                new MostBrightTransitionEffect(),
                new LeastBrightTransitionEffect(),
            },   
            new TransitionEffect[]
            { 
                new SaturateTransitionEffect(),
            },
            new TransitionEffect[]
            {
                new BandedSwirlTransitionEffect(Math.PI / 5.0, 50.0),
                new BandedSwirlTransitionEffect(Math.PI, 10.0),
                new BandedSwirlTransitionEffect(-Math.PI, 10.0),
            },
            new TransitionEffect[]
            {
                new CircleRevealTransitionEffect { FuzzyAmount= 0.0} , 
                new CircleRevealTransitionEffect { FuzzyAmount= 0.1},
                new CircleRevealTransitionEffect { FuzzyAmount=0.5} 
            },
            new TransitionEffect[]
            {
                /* (Point origin, Point normal, Point offset, double fuzziness)*/ 
                new LineRevealTransitionEffect{LineOrigin= new Point(-0.2, -0.2), LineNormal = new Point(1, 0), LineOffset= new Point(1.4, 0), FuzzyAmount = 0.2},
                new LineRevealTransitionEffect{LineOrigin= new Point(1.2, -0.2), LineNormal = new Point(-1, 0), LineOffset= new Point(-1.4, 0), FuzzyAmount = 0.2},
                new LineRevealTransitionEffect{LineOrigin= new Point(-.2, -0.2), LineNormal = new Point(0, 1), LineOffset= new Point(0, 1.4), FuzzyAmount = 0.2},
                new LineRevealTransitionEffect{LineOrigin= new Point(-0.2, 1.2), LineNormal = new Point(0, -1), LineOffset= new Point(0, -1.4), FuzzyAmount = 0.2},
                new LineRevealTransitionEffect{LineOrigin= new Point(-0.2, -0.2), LineNormal = new Point(1, 1), LineOffset= new Point(1.4, 1.4), FuzzyAmount = 0.2},
                new LineRevealTransitionEffect{LineOrigin= new Point(1.2, 1.2), LineNormal = new Point(-1, -1), LineOffset= new Point(-1.4, -1.4), FuzzyAmount = 0.2},
                new LineRevealTransitionEffect{LineOrigin= new Point(1.2, -0.2), LineNormal = new Point(-1, 1), LineOffset= new Point(-1.4, 1.4), FuzzyAmount = 0.2},
                new LineRevealTransitionEffect{LineOrigin= new Point(-0.2, 1.2), LineNormal = new Point(1, -1), LineOffset= new Point(1.4, -1.4), FuzzyAmount = 0.2},
            },
            new TransitionEffect[]
            {
                new RippleTransitionEffect(),
                new RippleTransitionEffect(),
                new RippleTransitionEffect(),
                new RippleTransitionEffect(),
                new RippleTransitionEffect(),
            },
            new TransitionEffect[]
            {
                new SlideInTransitionEffect{ SlideAmount= new Point(1, 0)},
                new SlideInTransitionEffect{ SlideAmount=new Point(0, 1)},
                new SlideInTransitionEffect{ SlideAmount=new Point(-1, 0)},
                new SlideInTransitionEffect{ SlideAmount=new Point(0, -1)},
            },
            new TransitionEffect[]
            {
                new SwirlTransitionEffect(Math.PI * 4),
                new SwirlTransitionEffect(-Math.PI * 4),
                new SwirlTransitionEffect(Math.PI * 4),
                new SwirlTransitionEffect(-Math.PI * 4),
            },
        };


        private Random rand = new Random();

        private bool _useOrder = true;
        private int _nextEffect = 0;
        private int _usedTimes = 0;

        private string ExtractName(object o)
        {
            string s = o.ToString();
            int lastindex = s.LastIndexOf ( '.' ) ; 
            if ( lastindex != -1)
            {
                s = s.Substring(lastindex + 1); 
            }
            return s; 
        } 
        private void ApplyTransitionEffect()
        {
            TransitionEffect[] effectGroup = transitionEffects[this.rand.Next(transitionEffects.Length)];
            TransitionEffect effect = effectGroup[this.rand.Next(effectGroup.Length)];

            if (_useOrder)
            {
                effectGroup = transitionEffectsSingle[_nextEffect % transitionEffectsSingle.Length];
                effect = effectGroup[0];

                if (++_usedTimes == 2)
                {
                    _usedTimes = 0;
                    _nextEffect++; 
                }

                this.effectName.Text = ExtractName (effect); 
                if (_nextEffect == transitionEffectsSingle.Length)
                {
                    _useOrder = false;
                    this.effectName.Text = "mixed effects, random"; 
                }
                 
            }
              

            RandomizedTransitionEffect randEffect = effect as RandomizedTransitionEffect;
            if (randEffect != null)
            {
                randEffect.RandomSeed = this.rand.NextDouble();
            }

            DoubleAnimation da = new DoubleAnimation(0.0, 1.0, new Duration(TimeSpan.FromSeconds(2.0)), FillBehavior.HoldEnd);
            da.AccelerationRatio = 0.5;
            da.DecelerationRatio = 0.5;
            da.Completed += new EventHandler(this.TransitionCompleted);
            effect.BeginAnimation(TransitionEffect.ProgressProperty, da);

            VisualBrush vb = new VisualBrush(this.oldChild);
            vb.Viewbox = new Rect(0, 0, this.oldChild.ActualWidth, this.oldChild.ActualHeight);
            vb.ViewboxUnits = BrushMappingMode.Absolute;
            //this.oldChild.Width = this.oldChild.ActualWidth;
            //this.oldChild.Height = this.oldChild.ActualHeight;
            //this.oldChild.Measure(new Size(this.oldChild.ActualWidth, this.oldChild.ActualHeight));
            //this.oldChild.Arrange(new Rect(0, 0, this.oldChild.ActualWidth, this.oldChild.ActualHeight));

            effect.OldImage = vb;
            this.currentChild.Effect = effect;
        }


        private void TransitionCompleted(object sender, EventArgs e)
        {
            this.currentChild.Effect = null;
            if (this.PhotoSlideShow != null)
            {
                this.oldChild.Source = PhotoSlideShow.NextPhoto;
            }
        }

        PhotoSlideShow PhotoSlideShow = new PhotoSlideShow(); 

    }

    public class PhotoSlideShow
    {
        public PhotoSlideShow()
        {
        }

        public void MoveFirst()
        {
            lock (this)
            {
                _current = 0 ;                
            }
        }

        public void MoveNext()
        {
            lock (this)
            {
                ++_current;
                _current %= _images.Count; 
            } 
        }

        public void MovePrevious()
        {
            lock (this)
            {
                if (_current > 0)
                    --_current;
                else
                    _current = 0;  
            }
        }

        public BitmapSource PreviousPhoto
        {
            get
            {
                if (_current > 0)
                {
                    return _images[_current - 1];
                }
                else if (_current == 0 && _images.Count > 0)
                    return _images[_images.Count-1]; 


                return null;
            }
        } 


        public BitmapSource CurrentPhoto
        {
            get
            {
                if (_current != -1)
                {
                    return _images[_current];
                }
                return null; 
            }
        } 
         List<BitmapSource> _images;
         public List<BitmapSource> Images
        {
            get
            {
                return _images; 
            }
            set
            {
                _images = value; 
            } 
        }

        static int _current = -1; 
        public  BitmapSource NextPhoto
        {
            get
            {
                if (_images != null && _images.Count> 0 )
                {
                    return _images[(_current + 1) % _images.Count];
                } 
                return null; 
            } 

        } 
    } 
}
