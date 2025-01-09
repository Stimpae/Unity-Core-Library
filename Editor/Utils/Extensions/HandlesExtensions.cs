using UnityEditor;
using UnityEngine;

namespace Pastime.Core.Editor {
    /// <summary>
    /// Provides extension methods Unity's Handles class.
    /// </summary>
    public static class HandlesExtensions {
        /// <summary>
        /// Draws a label at the specified position with the given text and style options.
        /// </summary>
        /// <param name="position">The position to draw the label.</param>
        /// <param name="text">The text to display in the label.</param>
        /// <param name="textColor"></param>
        /// <param name="backgroundColor"></param>
        /// <param name="anchor">The text anchor alignment. Default is MiddleCenter.</param>
        /// <param name="fontSize">The font size of the text. Default is 12.</param>
        /// <param name="padding">The padding around the text. Default is 0.</param>
        public static void DrawLabel(Vector3 position, string text, Color textColor, Color backgroundColor, 
            TextAnchor anchor = TextAnchor.MiddleCenter, int fontSize = 12, int padding = 0) {
            var style = new GUIStyle {
                normal = {
                    textColor = textColor,
                    background = CreateTexture2D(backgroundColor),
                },
                alignment = anchor,
                padding = new RectOffset(padding, padding, padding, padding),
                fontSize = fontSize,
            };

            var content = new GUIContent {
                text = text,
            };
            
            Handles.Label(position, content, style);
        }
        
        /// <summary>
        /// Creates a 1x1 texture with the specified color.
        /// </summary>
        /// <param name="color">The color of the texture.</param>
        /// <returns>A Texture2D object with the specified color.</returns>
        private static Texture2D CreateTexture2D(Color color) {
            var texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, color);
            texture.Apply();
            return texture;
        }
    }
}