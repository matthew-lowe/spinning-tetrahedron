using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector3 = Microsoft.Xna.Framework.Vector3;

// ReSharper disable PossibleLossOfFraction

namespace Primatives
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;

        private Cube testShape;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            this.Window.Title = "spin boi";
            
            Matrix world = Matrix.CreateTranslation(Vector3.Zero);
            Matrix view = Matrix.CreateLookAt(new Vector3(1, 1, 3), Vector3.Zero, Vector3.Up);
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45), 
                _graphics.PreferredBackBufferWidth / _graphics.PreferredBackBufferHeight,
                0.01f,
                100f);

            testShape = new Cube(GraphicsDevice, world, view, projection);
            
            _graphics.PreferMultiSampling = true;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float rot = 0.01f;
            testShape.World = testShape.World * Matrix.CreateRotationY(rot);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            GraphicsDevice.SetVertexBuffer(testShape.VertexBuffer);
            GraphicsDevice.Indices = testShape.IndexBuffer;

            RasterizerState rasterizerState = new RasterizerState {CullMode = CullMode.CullClockwiseFace, MultiSampleAntiAlias = true};
            GraphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in testShape.BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.LineList, testShape.Vertices, 0, 8, testShape.Indices, 0, 12);
            }

            base.Draw(gameTime);
        }
    }
}
