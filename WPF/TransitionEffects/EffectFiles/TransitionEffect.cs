// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.


namespace TransitionEffects
{
    using System;
    using System.Windows;
    using System.Windows.Media.Effects;
    using System.Windows.Media.Imaging;
    using System.Windows.Media;
#if SILVERLIGHT
    using UIPropertyMetadata = System.Windows.PropertyMetadata;
#endif 

    /// <summary>
    /// This is the implementation of an extensible framework ShaderEffect which loads
    /// a shader model 2 pixel shader. Dependecy properties declared in this class are mapped
    /// to registers as defined in the *.ps file being loaded below.
    /// </summary>
    public abstract class TransitionEffect : ShaderEffect
    {
        #region Dependency Properties

        /// <summary>
        /// Brush-valued properties turn into sampler-property in the shader.
        /// Represents the image present in the final state of the transition
        /// </summary>
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(TransitionEffect), 0, SamplingMode.NearestNeighbor);
        
        /// <summary>
        /// Brush-valued properties turn into sampler-property in the shader.
        /// Represents the image present in the initial state of the transition
        /// </summary>
        public static readonly DependencyProperty OldImageProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("OldImage", typeof(TransitionEffect), 1, SamplingMode.NearestNeighbor);
        
        /// <summary>
        /// Using a DependencyProperty as the backing store for Progress.  This enables animation, styling, binding, etc...
        /// Double used to represent state of Transition from start to finish
        /// </summary>
        public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register("Progress", typeof(double), typeof(TransitionEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)));

        #endregion

        #region Constructors

        /// <summary>
        /// Updates the shader's variables to the default values.
        /// </summary>
        internal TransitionEffect()
        {
            // Update each DependencyProperty that's registered with a shader register.  This
            // is needed to ensure the shader gets sent the proper default value.
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(OldImageProperty);
            UpdateShaderValue(ProgressProperty);
        }

        #endregion

        /// <summary>
        /// Gets or sets the Input variable within the shader.
        /// </summary>
        [System.ComponentModel.BrowsableAttribute(false)]
        public Brush Input
        {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        /// <summary>
        /// Gets or sets the OldImage variable within the shader.
        /// </summary>
        public Brush OldImage
        {
            get { return (Brush)GetValue(OldImageProperty); }
            set { SetValue(OldImageProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Progress variable within the shader.
        /// </summary>
        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }       
    }
}
