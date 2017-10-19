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
    public class SwirlGridTransitionEffect : TransitionEffect
    {
        #region Dependency Properties

        /// <summary>
        /// Dependency property which modifies the TwistAmount variable within the pixel shader.
        /// </summary>
        public static readonly DependencyProperty TwistAmountProperty = DependencyProperty.Register("FuzzyAmount", typeof(double), typeof(SwirlGridTransitionEffect), new UIPropertyMetadata(Math.PI, PixelShaderConstantCallback(1)));

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an instance and sets the shader's twist variable to the specified values.
        /// </summary>
        /// <param name="twist">level of swirl twist</param>
        public SwirlGridTransitionEffect(double twist)
            : this()
        {
            this.TwistAmount = twist;
        }

        /// <summary>
        /// Creates an instance and updates the shader's variables to the default values.
        /// </summary>
        public SwirlGridTransitionEffect()
        {
            UpdateShaderValue(TwistAmountProperty);

            PixelShader shader = new PixelShader();
            shader.UriSource = Global.MakePackUri("ShaderSource/SwirlGridTransitionEffect.ps");
            PixelShader = shader;
        }

        #endregion

        /// <summary>
        /// Gets or sets the TwistAmount variable within the shader.
        /// </summary>
        public double TwistAmount
        {
            get { return (double)GetValue(TwistAmountProperty); }
            set { SetValue(TwistAmountProperty, value); }
        }        
    }
}
