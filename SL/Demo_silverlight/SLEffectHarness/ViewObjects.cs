using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Effects;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ExtensibleDemoApp
{

    public static class DesignTimeHelper
    {

        public static bool IsInDesignMode
        {
            get
            {
                return
                    ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue));
            }
        }
    }

      
       

 
    public class ContentViewItem  : INotifyPropertyChanged 
    {
        string _DisplayName;

        public string DisplayName
        {
            get { return _DisplayName; }
            set { _DisplayName = value; OnPropertyChanged("DisplayName");  }
        }
        

        string _templateKey;
        public string TemplateKey
        {
            get { return _templateKey; }
            set { _templateKey = value; OnPropertyChanged("TemplateKey"); }
        }

        object _content;

        public object Content
        {
            get { return _content; }
            set { _content = value; OnPropertyChanged("Content"); }
        }          

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler ph = PropertyChanged;
            if (ph != null)
            {
                ph(this, new PropertyChangedEventArgs(name));
            }
        } 
        #endregion
    }


    //public class ContentTemplateSelector : DataTemplateSelector
    //{
    //    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    //    {
    //        ContentViewItem cv = item as ContentViewItem;
    //        if (cv != null)
    //        {
    //            FrameworkElement fe = container as FrameworkElement;
    //            if (fe != null)
    //            {
    //                DataTemplate dt = fe.FindResource(cv.TemplateKey) as DataTemplate;
    //                if (dt != null)
    //                    return dt; 
    //            }
    //        }             
    //        return base.SelectTemplate(item, container);
    //    }
    //} 
    
    public class EffectViewItem : INotifyPropertyChanged 
    {
        public EffectViewItem(Effect e)
        {
            _effect = e;  
        } 
        private Effect _effect;
        private bool _checked;
        private string _name ; 
        public string DisplayName
        {
            get
            {
                if (_name == null || _name == string.Empty)
                {

                    
                    string name = _effect.ToString(); 
                    int index = name.LastIndexOf('.') ; 
                    if ( index != -1 ) 
                        name = name.Substring(index+1);
                    //if (name.Length > 22)
                    //{
                    //    index = name.LastIndexOf("Effect");
                    //    if (index != -1)
                    //        name = name.Substring(0, index);
                    //}
                    return name;
                } 
                return _name; 
            }
            set
            {
                _name = value;
                OnPropertyChanged("DisplayName"); 
            }
        }

        public Effect DisplayedEffect
        {
            get
            {
                if (IsChecked)
                    return _effect;
                else
                    return null; 
            } 
        } 

        public Effect Effect
        {
            get
            {
                return _effect; 
            }
            set
            {
                _effect = value;
            } 
        }

        public bool IsChecked
        {
            get
            {
                return _checked;
            }
            set
            {
                bool oldvalue = _checked; 
                _checked = value;
                if (oldvalue != _checked)
                {
                    OnPropertyChanged("IsChecked");
                    OnPropertyChanged("DisplayedEffect"); 
                } 
            } 
        }

        private bool UseMousePosition
        {
            get
            {
                return false;
            }
        } 
        private double _mouseX;
        private double _mouseY; 

        public double MouseX
        {
            get
            { 
                return _mouseX ;  
            }
            set
            {
                _mouseX = value;
                if (IsChecked && UseMousePosition)
                    OnPropertyChanged("MouseX"); 
            } 
        }

        public double MouseY
        {
            get
            {
                return _mouseY;
            }
            set
            {
                _mouseY = value;
                if (IsChecked && UseMousePosition)
                    OnPropertyChanged("MouseY");
            }
        }
 

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler ph = PropertyChanged;
            if (ph != null)
            {
                ph(this, new PropertyChangedEventArgs(name)); 
            } 
        } 
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    
}
