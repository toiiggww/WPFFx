// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.


namespace TransitionEffects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Media.Effects;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// This is the implementation of an extensible framework ShaderEffect which loads
    /// a shader model 2 pixel shader. Dependecy properties declared in this class are mapped
    /// to registers as defined in the *.ps file being loaded below.
    /// </summary>
    public class CircularBlurTransitionEffect : TransitionEffect
    {
        #region Dependency Properties
        /// <summary>
        /// Dependency property which modifies the TrigImage variable within the pixel shader.
        /// </summary>
        protected static readonly DependencyProperty TrigImageProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("TrigImage", typeof(CircularBlurTransitionEffect), 2, SamplingMode.Bilinear);

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an instance and updates the shader's variables to the default values.
        /// </summary>
        public CircularBlurTransitionEffect()
        {
            PixelShader shader = new PixelShader();
            shader.UriSource = Global.MakePackUri("ShaderSource/CircularBlurTransitionEffect.ps");
            PixelShader = shader;
#if !SILVERLIGHT 
            this.TrigImage = new ImageBrush(new BitmapImage(Global.MakePackUri("Images/sincosatan.png")));
#else 
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(Global.MakePackUri("Images/sincosatan.png"));
            this.TrigImage = ib;
#endif 
            UpdateShaderValue(TrigImageProperty);
        }

        #endregion

        /// <summary>
        /// Gets or sets the TrigImage variable within the shader.
        /// </summary>
        protected Brush TrigImage
        {
            get { return (Brush)GetValue(TrigImageProperty); }
            set { SetValue(TrigImageProperty, value); }
        }
    }
}
