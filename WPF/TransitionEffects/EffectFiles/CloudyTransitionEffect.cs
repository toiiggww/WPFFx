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
    using System.Windows.Media;
    using System.Windows;
    using System.Windows.Media.Imaging;
    using System.Windows.Media.Effects;

    /// <summary>
    /// This is the implementation of an extensible framework ShaderEffect which loads
    /// a shader model 2 pixel shader. Dependecy properties declared in this class are mapped
    /// to registers as defined in the *.ps file being loaded below.
    /// </summary>
    public abstract class CloudyTransitionEffect : RandomizedTransitionEffect
    {
        #region Dependency Properties

        /// <summary>
        /// Dependency property which modifies the CloudImage variable within the pixel shader.
        /// </summary>
        protected static readonly DependencyProperty CloudImageProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("CloudImage", typeof(CloudyTransitionEffect), 2, SamplingMode.Bilinear);

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an instance and updates the shader's variables to the default values.
        /// </summary>
        internal CloudyTransitionEffect()
        {
#if !SILVERLIGHT 
            this.CloudImage = new ImageBrush(new BitmapImage(Global.MakePackUri("Images/clouds.png")));
#else 
            ImageBrush ib = new ImageBrush(); 
            ib.ImageSource = new BitmapImage(Global.MakePackUri("Images/clouds.png")); 
            this.CloudImage = ib;
#endif 
            UpdateShaderValue(CloudImageProperty);
        }

        #endregion

        /// <summary>
        /// Gets or sets the CloudImage variable within the shader.
        /// </summary>
        protected Brush CloudImage
        {
            get { return (Brush)GetValue(CloudImageProperty); }
            set { SetValue(CloudImageProperty, value); }
        }
    }
}
