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
    public class LineRevealTransitionEffect : TransitionEffect
    {
        #region Dependency Properties

        /// <summary>
        /// Dependency property which modifies the LineOrigin variable within the pixel shader.
        /// </summary>
        public static readonly DependencyProperty LineOriginProperty = DependencyProperty.Register("LineOrigin", typeof(Point), typeof(LineRevealTransitionEffect), new UIPropertyMetadata(new Point(-0.2, -0.2), PixelShaderConstantCallback(1)));

        /// <summary>
        /// Dependency property which modifies the LineNormal variable within the pixel shader.
        /// </summary>
        public static readonly DependencyProperty LineNormalProperty = DependencyProperty.Register("LineNormal", typeof(Point), typeof(LineRevealTransitionEffect), new UIPropertyMetadata(new Point(1.0, 1.0), PixelShaderConstantCallback(2)));

        /// <summary>
        /// Dependency property which modifies the LineOffset variable within the pixel shader.
        /// </summary>
        public static readonly DependencyProperty LineOffsetProperty = DependencyProperty.Register("LineOffset", typeof(Point), typeof(LineRevealTransitionEffect), new UIPropertyMetadata(new Point(1.4, 1.4), PixelShaderConstantCallback(3)));

        /// <summary>
        /// Dependency property which modifies the FuzzyAmount variable within the pixel shader.
        /// </summary>
        public static readonly DependencyProperty FuzzyAmountProperty = DependencyProperty.Register("FuzzyAmount", typeof(double), typeof(LineRevealTransitionEffect), new UIPropertyMetadata(0.2, PixelShaderConstantCallback(4)));

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an instance and updates the shader's variables to the default values.
        /// </summary>
        public LineRevealTransitionEffect()
        {
            UpdateShaderValue(LineOriginProperty);
            UpdateShaderValue(LineNormalProperty);
            UpdateShaderValue(LineOffsetProperty);
            UpdateShaderValue(FuzzyAmountProperty);

            PixelShader shader = new PixelShader();
            shader.UriSource = Global.MakePackUri("ShaderSource/LineRevealTransitionEffect.ps");
            PixelShader = shader;
        }

        #endregion

        /// <summary>
        /// Gets or sets the LineOrigin variable within the shader.
        /// </summary>
        public Point LineOrigin
        {
            get { return (Point)GetValue(LineOriginProperty); }
            set { SetValue(LineOriginProperty, value); }
        }

        /// <summary>
        /// Gets or sets the LineNormal variable within the shader.
        /// </summary>
        public Point LineNormal
        {
            get { return (Point)GetValue(LineNormalProperty); }
            set { SetValue(LineNormalProperty, value); }
        }

        /// <summary>
        /// Gets or sets the LineOffset variable within the shader.
        /// </summary>
        public Point LineOffset
        {
            get { return (Point)GetValue(LineOffsetProperty); }
            set { SetValue(LineOffsetProperty, value); }
        }

        /// <summary>
        /// Gets or sets the FuzzyAmount variable within the shader.
        /// </summary>
        public double FuzzyAmount
        {
            get { return (double)GetValue(FuzzyAmountProperty); }
            set { SetValue(FuzzyAmountProperty, value); }
        }
    }
}
