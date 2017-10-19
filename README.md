# WPFFx
Backup of Windows Presentation Foundation Pixel Shader Effects Library http://wpffx.codeplex.com

## Project Description
Library with sample HLSL effects for WPF and Silverlight applications.

Initial seed includes:
Effects:
BandedSwirl, Bloom, BrightExtract, ColorKeyAlpha, ColorTone, ContrastAdjust, DirectionalBlur, Embossed, Gloom, GrowablePoissonDiskEffect, InvertColor, LightStreak, Magnify, Monochrome, Pinch, Pixelate, Ripple, Sharpen, SmoothMagnify, Swirl, Tone, Toon, and ZoomBlur

Transition Effects:
BandedSwirl, Blings, Blood, CircleReveal, CircleStretch, CircularBlur, CloudReveral, Cloudy, Crumble, Dissolve, DropFade, Fade, LeastBright, LineReveal, MostBright, PixelateIn, PixelateOut, Pixelate, RadialBlur, RadialWiggle, RandomCircleReveal, Ripple, Rotate, Saturate, Shrink, SlideIn, SmoothSwirl, Swirl, Water, Wave.

Video demonstrating the effects http://channel9.msdn.com/shows/Continuum/WPFFXDemo/
Tutorial on writing pixel shader effects http://cid-123ec1ed6c72a14a.skydrive.live.com/browse.aspx/Public/HLSL
Blog post announcing the library with useful links http://blogs.msdn.com/jaimer/archive/2008/10/03/announcement-wpf-shader-effects-library-on-codeplex.aspx

## Requirements:
This requires .NET framework 3.5 SP1 and requires Direct X SDK at compile time.. (to build the PS files).
This also uses the Shader Effects BuildTask and Templates from the WPF futures samples at http://www.codeplex.com/wpf 
For silverlight, Silverlight 3 Preview ( released at MIX09) and the corresponding tools are needed.

Last edited Mar 27, 2009 at 2:01 AM by jaimer, version 5