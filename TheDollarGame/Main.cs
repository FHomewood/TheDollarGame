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
            Random rand = new Random();
            oldMState = Mouse.GetState();
            for (int i = 0; i < vertexNo;i++)
            {
                vertexList.Add(new Vertex((byte)i, new Vector2(300,300) + 200 * new Vector2((float)Math.Cos(i * 2*Math.PI/vertexNo - Math.PI/2), (float)Math.Sin(i * 2 * Math.PI / vertexNo - Math.PI / 2))
                    , rand.Next(-3, 3), connectVertices(rand,i)));
            }
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
            foreach (Vertex vertex in vertexList) vertex.Update(newMState,oldMState,vertexList);

            oldMState = newMState;
            base.Update(gameTime);
        }
        
        private List<byte> connectVertices(Random rand, int vertexid)
        {
            List<byte> connectedverts = new List<byte>();
            int connections = rand.Next(1, vertexNo - 2);
            while (connections > 0)
            {
                int randvert = rand.Next(0, vertexNo);
                if (randvert != vertexid) connectedverts.Add((byte)randvert);
                connections--;
            }
            return connectedverts;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            sB.Begin();
            foreach(Vertex vertex in vertexList)
            {
                vertex.Draw(sB,vertTex,font,vertexList);
            }
            sB.End();
            base.Draw(gameTime);
        }
    }
}