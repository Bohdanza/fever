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
    public class Mushroom:Object
    {
        public Mushroom(ContentManager contentManager, double x, double y, int type):
            base(contentManager, x, y, "mushroom_"+type.ToString()+"_")
        {
            switch(type)
            {
                case 0:
                    Radius = 0.17;
                    break;
                case 1:
                    Radius = 0.25;
                    break;
            }
        }
    }
}