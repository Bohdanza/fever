using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace fever
{
    public abstract class Object
    {
        public double X { get; protected set; }
        public double Y { get; protected set; }
        
        public double Radius { get; protected set; }

        public DynamicTexture Texture { get; protected set; } = null;
        private string baseName="";

        public Object(ContentManager contentManager, double x, double y, double radius, string textureName)
        {
            X = x;
            Y = y;

            Radius = radius;

            baseName = textureName;
        }

        public Object(ContentManager contentManager, double x, double y, string textureName)
        {
            X = x;
            Y = y;

            Radius = 0;

            baseName = textureName;
        }

        public virtual void Update(ContentManager contentManager, World world)
        {
            if(Texture==null)
            {
                Texture = new DynamicTexture(contentManager, baseName);

                var rnd = new Random();

                int q = rnd.Next(0, Texture.Textures.Count);

                for(int i=0; i<q; i++)
                {
                    Texture.Update(contentManager, true);
                }
            }

            Texture.Update(contentManager);
        }
        
        public virtual void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            if (Texture != null)
            {
                spriteBatch.Draw(Texture.GetCurrentFrame(), new Vector2(x, y), Color.White);
            }
        }
    }
}