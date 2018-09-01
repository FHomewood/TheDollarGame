using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private int value;
        private Color renderCol;
        private Vector2 location;
        private List<byte> connections;
        public Vertex(byte id,Vector2 location, int value, List<byte> connections)
        {
            renderCol = Color.Red;
            this.id = id;
            this.location = location;
            this.value = value;
            this.connections = connections;
        }

        public void Update(MouseState newMState, MouseState oldMState, List<Vertex> list)
        {
                // Clicked
                if ((newMState.Position.ToVector2() - location).Length() < 20 &&
                 newMState.LeftButton == ButtonState.Pressed && 
                 oldMState.LeftButton == ButtonState.Released)
            {
                renderCol = Color.DeepPink;
                value -= connections.Count;
                foreach (byte connect in connections) findVert(list, connect).value--;
            }
                //Hovered
                else if ((newMState.Position.ToVector2() - location).Length() < 20)
            {
                renderCol = Color.PaleVioletRed;
            }
                else
            {
                renderCol = Color.Red;
            }

        }

        public void Draw(SpriteBatch sB, Texture2D vertTex, SpriteFont font, List<Vertex> vertList)
        {
            foreach(byte findid in connections)
            {
                for(int i = 10; i < 60; i++)
                {
                    sB.Draw(vertTex,
                        location + (findVert(vertList, findid).location - location) * i / 70
                        ,null,Color.Gray,0f,vertTex.Bounds.Center.ToVector2(),0.01f,SpriteEffects.None,0f);
                }
            }
            sB.Draw(vertTex, location, null, renderCol, 0f, vertTex.Bounds.Center.ToVector2(), 0.15f, SpriteEffects.None, 1f);
            sB.DrawString(font, value.ToString(), location, Color.Black, 0f, font.MeasureString(value.ToString()) / 2, 1f, SpriteEffects.None, 0f);
        }

        public Vertex findVert(List<Vertex> list, byte findid)
        {
            Vertex output = null;
            foreach (Vertex vertex in list) if (findid == vertex.id) output = vertex;
            return output;
        }
    }
}
