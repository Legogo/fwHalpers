using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fwp.halpers
{
    public class ColorSpacer : PropertyAttribute
    {
        public float spaceHeight;
        public float lineHeight;
        public float lineWidth;
        public Color lineColor = Color.red;

        /// <summary>
        /// color channel [0,1]
        /// </summary>
        public ColorSpacer(float r, float g, float b, float spaceHeight = 0f, float lineHeight = 0f, float lineWidth = 0f)
        {
            this.spaceHeight = spaceHeight;
            this.lineHeight = lineHeight;
            this.lineWidth = lineWidth;

            // unfortunately we can't pass a color through as a Color object
            // so we pass as 3 floats and make the object here
            this.lineColor = new Color(r, g, b);
        }
    }

}
