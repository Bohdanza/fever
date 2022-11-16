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

namespace fever
{
    public class World
    {
        public const int UnitX = 100, UnitY = 60;

        public float crod = 0f;

        public int Begin { get; private set; } = 0;
        public int CurrentChunkBegin { get; private set; } = -20;

        public List<Object> objects { get; private set; } = new List<Object>();

        public World(ContentManager contentManager)
        {
            AddChunk(contentManager, 0, Begin);

            AddChunk(contentManager, 0, Begin + 20);
        }

        public void Update(ContentManager contentManager)
        {
            for(int i= 0; i<objects.Count(); i++)
            {
                objects[i].Update(contentManager, this);
            }

            var ks = Keyboard.GetState();

            if(ks.IsKeyDown(Keys.D))
            {
                crod += 0.1f;

                Begin = (int)Math.Floor(crod);
            }

            if(Begin>=CurrentChunkBegin+40)
            {
                DeleteChunk(CurrentChunkBegin);

                CurrentChunkBegin += 20;

                AddChunk(contentManager, 0, CurrentChunkBegin+40);
            }
        }

        public void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            for(int i=0; i<objects.Count; i++)
            {
                objects[i].Draw(spriteBatch, 
                    (int)(x + objects[i].X * UnitX - crod * UnitX), (int)(y + objects[i].Y * UnitY));
            }
        }

        private void AddChunk(ContentManager contentManager, int biome, int position)
        {
            var rnd = new Random();

            if(biome==0)
            {
                int mushroomCount = rnd.Next(7, 15);

                for(int i=0; i<mushroomCount; i++)
                {
                    objects.Add(new Mushroom(contentManager, rnd.NextDouble() * 20 + position, rnd.NextDouble()*20, 0));
                }
            }
        }

        private void DeleteChunk(int position)
        {
            while(objects.Count>0&&objects[0].X<position+20)
            {
                objects.RemoveAt(0);
            }
        }
    }
}