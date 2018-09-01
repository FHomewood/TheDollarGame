using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDollarGame
{
    class Vertex
    {
        private byte id;
        private Vector2 location;
        private int value;
        private List<byte> connections;
        public Vertex(byte id,Vector2 location, int value, List<byte> connections)
        {
            this.id = id;
            this.location = location;
            this.value = value;
            this.connections = connections;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch sB, Texture2D vertTex, SpriteFont font, List<Vertex> vertList)
        {
            sB.Draw(vertTex, location, null, Color.FromNonPremultiplied(255, 100, 100, 255), 0f, vertTex.Bounds.Center.ToVector2(), 0.15f, SpriteEffects.None, 0f);
            sB.DrawString(font, value.ToString(), location, Color.Black, 0f, font.MeasureString(value.ToString()) / 2, 1f, SpriteEffects.None, 0f);
            foreach(byte findid in connections)
            {
                for(int i = 1; i < 10; i++)
                {
                    sB.Draw(vertTex, i / 10 * findVert(vertList, findid).location - location,null,Color.White,0f,vertTex.Bounds.Center.ToVector2(),0.06f,SpriteEffects.None,0f);
                }
            }
        }

        public Vertex findVert(List<Vertex> list, byte findid)
        {
            Vertex output = null;
            foreach (Vertex vertex in list) if (id == vertex.id) output = vertex;
            return output;
        }
    }
}
