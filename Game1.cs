using System;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Primatives.render;
using Vector3 = Microsoft.Xna.Framework.Vector3;

// ReSharper disable PossibleLossOfFraction

namespace Primatives
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;

        public Matrix GlobalWorld;
        public Matrix GlobalView;
        public Matrix GlobalProjection;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            this.Window.Title = "spin boi";
            
            GlobalWorld = Matrix.CreateTranslation(Vector3.Zero);
            GlobalView = Matrix.CreateLookAt(new Vector3(1, 1, 3), Vector3.Zero, Vector3.Up);
            GlobalProjection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45), 
                _graphics.PreferredBackBufferWidth / _graphics.PreferredBackBufferHeight,
                0.01f,
                100f);

            _graphics.PreferMultiSampling = true;

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    var cube = new Cube(GraphicsDevice, GlobalWorld, GlobalView, GlobalProjection);
                    cube.World *= Matrix.CreateTranslation(new Vector3(row + 0.3f * row, col + 0.3f * col, 0) + new Vector3(-5, -5, -5));
                    ShapeManager.AddShape(cube);
                }
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ShapeManager.CalculateVertices();

            foreach (var shape in ShapeManager.Shapes)
            {
                shape.World = Matrix.CreateRotationY(0.01f) * shape.World;
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            ShapeManager.RenderShapes();

            base.Draw(gameTime);
        }
    }
}
