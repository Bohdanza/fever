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
        public List<Object> objectsDrawing { get; private set; } = new List<Object>();

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
            for(int i=0; i<objectsDrawing.Count; i++)
            {
                objectsDrawing[i].Draw(spriteBatch, 
                    (int)(x + objectsDrawing[i].X * UnitX - crod * UnitX), (int)(y + objectsDrawing[i].Y * UnitY));
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
                    AddObject(new Mushroom(contentManager, rnd.NextDouble() * 20 + position, rnd.NextDouble()*20, 0));
                }
            }
        }

        private void DeleteChunk(int position)
        {
            while(objects.Count>0&&objects[0].X<position+20)
            {
                DeleteObject(objects[0]);
            }
        }

        public void DeleteObject(Object @object)
        {
            objects.Remove(@object);
            objectsDrawing.Remove(@object);
        }

        public void AddObject(Object @object)
        {
            if(objects.Count==0)
            {
                objectsDrawing.Add(@object);
                objects.Add(@object);

                return;
            }

            int l = 0, r = objectsDrawing.Count-1; 

            while(l<r-1)
            {
                int mid = (l + r) / 2;

                if(objectsDrawing[mid].Y<@object.Y)
                    l = mid;
                else
                    r = mid;
            }

            if (objectsDrawing[r].Y <= @object.Y)
                objectsDrawing.Insert(r + 1, @object);
            else
                objectsDrawing.Insert(l + 1, @object);


            l = 0; r = objects.Count-1;

            while (l < r - 1)
            {
                int mid = (l + r) / 2;

                if (objects[mid].X < @object.X)
                    l = mid;
                else
                    r = mid;
            }

            if (objectsDrawing[r].X <= @object.X)
            {
                objects.Insert(r + 1, @object);
            }
            else
                objects.Insert(l + 1, @object);
        }
    }
}