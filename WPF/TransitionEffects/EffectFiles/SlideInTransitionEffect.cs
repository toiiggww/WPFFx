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

#if SILVERLIGHT
    using UIPropertyMetadata = System.Windows.PropertyMetadata;
#endif 

    /// <summary>
    /// This is the implementation of an extensible framework ShaderEffect which loads
    /// a shader model 2 pixel shader. Dependecy properties declared in this class are mapped
    /// to registers as defined in the *.ps file being loaded below.
    /// </summary>
    public class SlideInTransitionEffect : TransitionEffect
    {
        #region Dependency Properties
        /// <summary>
        /// Dependency property which modifies the SlideAmount variable within the pixel shader.
        /// </summary>
        public static readonly DependencyProperty SlideAmountProperty = DependencyProperty.Register("SlideAmount", typeof(Point), typeof(SlideInTransitionEffect), new UIPropertyMetadata(new Point(1.0, 0.0), PixelShaderConstantCallback(1)));

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an instance and updates the shader's variables to the default values.
        /// </summary>
        public SlideInTransitionEffect()
        {
            PixelShader shader = new PixelShader();
            shader.UriSource = Global.MakePackUri("ShaderSource/SlideInTransitionEffect.ps");
            PixelShader = shader;

            UpdateShaderValue(SlideAmountProperty);
        }

        #endregion

        /// <summary>
        /// Gets or sets the SlideAmount variable within the shader.
        /// </summary>
        public Point SlideAmount
        {
            get { return (Point)GetValue(SlideAmountProperty); }
            set { SetValue(SlideAmountProperty, value); }
        }        
    }
}
