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

    /// <summary>
    /// This is the implementation of an extensible framework ShaderEffect which loads
    /// a shader model 2 pixel shader. Dependecy properties declared in this class are mapped
    /// to registers as defined in the *.ps file being loaded below.
    /// </summary>
    public class CloudRevealTransitionEffect : CloudyTransitionEffect
    {
        #region Constructors

        /// <summary>
        /// Creates an instance of the shader.
        /// </summary>
        public CloudRevealTransitionEffect()
        {
            PixelShader shader = new PixelShader();
            shader.UriSource = Global.MakePackUri("ShaderSource/CloudRevealTransitionEffect.ps");
            PixelShader = shader;
        }

        #endregion
    }
}
