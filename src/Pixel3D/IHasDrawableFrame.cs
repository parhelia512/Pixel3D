﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixel3D
{
    // TODO BUG: Pretty much everything that is using this is disregarding the fact that an item may be in the draw list multiple times
    //           with different "tag" for drawing differently. Relates to following todo item...

    // TODO: This should probably be replaced with a mechanisim that actually captures what gets drawn to the screen
    //       (at least for in-engine debugging)
    public interface IHasDrawableFrame
    {
        /// <summary>Get a frame to draw for this item for debugging/editing purposes</summary>
        DrawableFrame GetDrawableFrame();
    }
}
