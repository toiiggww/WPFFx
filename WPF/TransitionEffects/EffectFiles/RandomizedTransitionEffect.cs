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
    using System.Windows;
#if SILVERLIGHT
    using UIPropertyMetadata = System.Windows.PropertyMetadata;    
#endif

    /// <summary>
    /// This is the implementation of an extensible framework ShaderEffect which loads
    /// a shader model 2 pixel shader. Dependecy properties declared in this class are mapped
    /// to registers as defined in the *.ps file being loaded below.
    /// </summary>
    public abstract class RandomizedTransitionEffect : TransitionEffect
    {
        #region Dependency Properties

        /// <summary>
        /// Dependency property which modifies the RandomSeed variable within the pixel shader.
        /// </summary>
        public static readonly DependencyProperty RandomSeedProperty = DependencyProperty.Register("RandomSeed", typeof(double), typeof(RandomizedTransitionEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(1)));

        #endregion

        #region Constructors

        /// <summary>
        /// Updates the shader's variables to the default values.
        /// </summary>
        internal RandomizedTransitionEffect()
        {
            UpdateShaderValue(RandomSeedProperty);
        }

        #endregion

        /// <summary>
        /// Gets or sets the RandomSeed variable within the shader.
        /// </summary>
        public double RandomSeed
        {
            get { return (double)GetValue(RandomSeedProperty); }
            set { SetValue(RandomSeedProperty, value); }
        }
    }
}
