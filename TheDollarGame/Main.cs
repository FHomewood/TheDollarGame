using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace TheDollarGame
{
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch sB;
        Texture2D vertTex;
        SpriteFont font;
        List<Vertex> vertexList = new List<Vertex>();
        Random rand = new Random();

        MouseState oldMState, newMState;
        int vertexNo = 5;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 600;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            base.Initialize();
            oldMState = Mouse.GetState();
            vertexNo = rand.Next(4, 9);
            for (int i = 0; i < vertexNo;i++)
            {
                int value = rand.Next(-3, 5);
                vertexList.Add(new Vertex((byte)i, new Vector2(300,300) + 200 * new Vector2((float)Math.Cos(i * 2*Math.PI/vertexNo - Math.PI/2), (float)Math.Sin(i * 2 * Math.PI / vertexNo - Math.PI / 2))
                    , rand.Next(-3, 5)));
            }
            connectVertices();
        }
        
        protected override void LoadContent()
        {
            sB = new SpriteBatch(GraphicsDevice);
            vertTex = Content.Load<Texture2D>("White Button");
            font = Content.Load<SpriteFont>("font");
        }
        
        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            newMState = Mouse.GetState();
            foreach (Vertex vertex in vertexList)
            {
                vertex.Update(newMState, oldMState, vertexList);
            }

            oldMState = newMState;
            base.Update(gameTime);
        }
        
        private void connectVertices()
        {
            int connections = rand.Next(vertexNo-1, vertexNo * (vertexNo - 1) / 2);
            int overflow = connections - vertexNo + 1;
            int i = 0;
            while (i <= vertexNo-1)
            {
                int connectTo = rand.Next(0, vertexNo - 1);
                if (i != connectTo &&
                    !vertexList[i].connections.Contains((byte)connectTo) &&
                    !vertexList[connectTo].connections.Contains((byte)i))
                {
                    vertexList[i].connections.Add((byte)connectTo);
                    vertexList[connectTo].connections.Add((byte)i);
                    i++;
                }
            }

            //List<byte> connectedverts = new List<byte>();
            //int connections = rand.Next(1, vertexNo - 2);
            //while (connections > 0)
            //{
            //    int randvert = rand.Next(0, vertexNo);
            //    if (randvert != vertexid)
            //    {
            //        connectedverts.Add((byte)randvert);
            //        vertexList[0].findVert(vertexList, (byte)randvert).Add();
            //    }
            //    connections--;
            //}
            //return connectedverts;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            sB.Begin();
            foreach(Vertex vertex in vertexList) vertex.Draw(sB,vertTex,font,vertexList);
            sB.End();
            base.Draw(gameTime);
        }
    }
}