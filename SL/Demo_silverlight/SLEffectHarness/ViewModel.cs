using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.IO;
using System.Windows.Media;

namespace ExtensibleDemoApp
{
    public class MediaSelectCommand : ICommand
    {

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return true; 
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
             
                       
        }

        #endregion
    }
    public class RTBCommand : ICommand
    {

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return true; 
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            
        }



        #endregion
    }

    public class ShowTransEffectsCommand : ICommand
    {

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
         }



        #endregion
    } 

    public static class AppCommands
    {
        static MediaSelectCommand _msCommand;
        public static MediaSelectCommand MediaSelectCommand
        {
            get
            {
                if (_msCommand == null)
                    _msCommand = new MediaSelectCommand();

                return _msCommand;
            }
        }

        static RTBCommand _rtbCommand;

        public static RTBCommand RTBCommand
        {
            get
            {
                if (_rtbCommand == null)
                    _rtbCommand = new RTBCommand();

                return _rtbCommand;
            }
        }

        static ShowTransEffectsCommand _showTECommand;

        public static ShowTransEffectsCommand ShowTransitionsWindow
        {
            get
            {
                if (_showTECommand == null)
                    _showTECommand = new ShowTransEffectsCommand();

                return _showTECommand;
            }
        }

/*
        static RTBCommand _rtbCommand;

        public static RTBCommand RTBCommand
        {
            get
            {
                if (_rtbCommand == null)
                    _rtbCommand = new RTBCommand();

                return _rtbCommand;
            }
        }
 * 
 */
    } 


    public class ViewModel 
    {

 

        public List<EffectViewItem> Effects
        {
            get
            {
                return EffectsList.All;
            }
        }

        public List<ContentViewItem> ContentTypes
        {
            get
            {
                return ContentList.All; 

            }
        } 

    }



    public static class ContentList
    {
        static List<ContentViewItem> _content;
        public static List<ContentViewItem> All
        {
            get
            {
                if (_content == null)
                {
                    _content = new List<ContentViewItem>();
                    
                    _content.Add(new ContentViewItem { DisplayName = "Vectors", TemplateKey = "vectorsTemplate" });
                    _content.Add(new ContentViewItem { DisplayName = "Media", TemplateKey = "mediaTemplate" });
                    _content.Add(new ContentViewItem { DisplayName = "Controls", TemplateKey = "controlsTemplate" });
                    _content.Add(new ContentViewItem { DisplayName = "Instructions", TemplateKey = "DefaultInstructions" });

                }
                return _content;
            }
        }
    }
    public static class EffectsList
    {
        static List<EffectViewItem> _effects;
        public static List<EffectViewItem> All
        {
            get
            {
                if (_effects == null)
                {
                    _effects = new List<EffectViewItem>();                    
                    _effects.Add(new EffectViewItem(new ShaderEffectLibrary.BloomEffect()));
                    _effects.Add(new EffectViewItem(new ShaderEffectLibrary.BrightExtractEffect()));
                    _effects.Add(new EffectViewItem(new ShaderEffectLibrary.ColorKeyAlphaEffect()));
                    _effects.Add(new EffectViewItem(new ShaderEffectLibrary.ColorToneEffect()));
                    _effects.Add(new EffectViewItem(new ShaderEffectLibrary.ContrastAdjustEffect()));
                    _effects.Add(new EffectViewItem(new ShaderEffectLibrary.DirectionalBlurEffect()));
                    _effects.Add(new EffectViewItem(new ShaderEffectLibrary.EmbossedEffect()));
                    _effects.Add(new EffectViewItem(new ShaderEffectLibrary.GloomEffect()));
                    _effects.Add(new EffectViewItem(new ShaderEffectLibrary.GrowablePoissonDiskEffect()));
                    _effects.Add(new EffectViewItem(new ShaderEffectLibrary.InvertColorEffect()));
                    _effects.Add(new EffectViewItem(new ShaderEffectLibrary.LightStreakEffect()));
                    _effects.Add(new EffectViewItem(new ShaderEffectLibrary.MagnifyEffect()));
                    _effects.Add(new EffectViewItem(new ShaderEffectLibrary.MonochromeEffect()));
                    _effects.Add(new EffectViewItem(new ShaderEffectLibrary.PinchEffect()));
                    _effects.Add(new EffectViewItem(new ShaderEffectLibrary.PixelateEffect()));
                    _effects.Add(new EffectViewItem(new ShaderEffectLibrary.RippleEffect()));
                    _effects.Add(new EffectViewItem(new ShaderEffectLibrary.SharpenEffect()));
                    _effects.Add(new EffectViewItem(new ShaderEffectLibrary.SmoothMagnifyEffect()));
                    _effects.Add(new EffectViewItem(new ShaderEffectLibrary.SwirlEffect()));
                    _effects.Add(new EffectViewItem(new ShaderEffectLibrary.ToneMappingEffect()));
                    _effects.Add(new EffectViewItem(new ShaderEffectLibrary.ToonShaderEffect()));
                    _effects.Add(new EffectViewItem(new ShaderEffectLibrary.ZoomBlurEffect()));

                }
                return _effects;
            }

        }
    } 
      
}
