﻿using System;
using System.Linq;
using CrowEngineBase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense
{
    public static class TextureCreation
    {
        public static GraphicsDevice device;

        public static Texture2D CreateTexture(int greenWidth, int width, int height, Func<int, Color> paint, Func<int, Color> basePaint)
        {
            if (device == null)
            {
                throw new Exception("You haven't initialized the graphics device for texture creation");
            }

            Texture2D texture = new Texture2D(device, width, height);

            //the array holds the color for each pixel in the texture
            Color[] data = new Color[width * height];
            int greenData = greenWidth * height;


            for (int pixel = 0; pixel < data.Count(); pixel++)
            {
                //the function applies the color according to the specified pixel
                data[pixel] = basePaint(pixel);
            }

            int greenPixel = 0;
            while (greenData > 0)
            {
                data[greenPixel] = paint(greenPixel);

                greenPixel += width;
                greenData -= 1;

                if (greenPixel >= width * height)
                {
                    greenPixel -= width * height;
                    greenPixel += 1;
                }

            }

            //set the color
            texture.SetData(data);

            return texture;
        }
    }
}
