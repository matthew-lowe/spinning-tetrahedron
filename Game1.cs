using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector3 = Microsoft.Xna.Framework.Vector3;

// ReSharper disable PossibleLossOfFraction

namespace Primatives
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        private VertexBuffer _vertexBuffer;
        private IndexBuffer _indexBuffer;
        private BasicEffect _basicEffect;
        
        private Matrix _world;
        private Matrix _view;
        private Matrix _projection;

        private VertexPositionColor[] _vertices;
        private short[] _indices;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            this.Window.Title = "spin boi";
            
            _world = Matrix.CreateTranslation(Vector3.Zero);
            _view = Matrix.CreateLookAt(new Vector3(1, 1, 3), Vector3.Zero, Vector3.Up);
            _projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45), 
                _graphics.PreferredBackBufferWidth / _graphics.PreferredBackBufferHeight,
                0.01f,
                100f);

            _graphics.PreferMultiSampling = true;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _basicEffect = new BasicEffect(GraphicsDevice);
            
            // Vertex positions and colours
            _vertices = new VertexPositionColor[5];
            _vertices[0] = new VertexPositionColor(new Vector3(0, 1, 0), Color.Red);
            _vertices[1] = new VertexPositionColor(new Vector3(-0.816f, -0.333f, -0.471f), Color.Blue);
            _vertices[2] = new VertexPositionColor(new Vector3(0, -0.333f, 0.943f), Color.Yellow);
            _vertices[3] = new VertexPositionColor(new Vector3(0.816f, -0.333f, -0.471f), Color.Green);

            // Initialising the vertex buffer and loading in the vertices
            _vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 5, BufferUsage.WriteOnly);
            _vertexBuffer.SetData<VertexPositionColor>(_vertices);
            
            // How each vertex is connected to every other vertex
            _indices = new short[12];
            _indices[0] = 0; _indices[1] = 1; 
            _indices[2] = 0; _indices[3] = 2;
            _indices[4] = 0; _indices[5] = 3;
            _indices[6] = 1; _indices[7] = 2; 
            _indices[8] = 1; _indices[9] = 3;
            _indices[10] = 3; _indices[11] = 2;
            
            // Initialise the index buffer and set the indices
            _indexBuffer = new IndexBuffer(_graphics.GraphicsDevice, typeof(short), _indices.Length, BufferUsage.WriteOnly);
            _indexBuffer.SetData(_indices);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float rot = 0.01f;
            _world = _world * Matrix.CreateRotationY(rot);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _basicEffect.World = _world;
            _basicEffect.View = _view;
            _basicEffect.Projection = _projection;
            _basicEffect.VertexColorEnabled = true;
            
            GraphicsDevice.SetVertexBuffer(_vertexBuffer);
            GraphicsDevice.Indices = _indexBuffer;

            RasterizerState rasterizerState = new RasterizerState {CullMode = CullMode.None, MultiSampleAntiAlias = true};
            GraphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in _basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.LineList, _vertices, 0, 4, _indices, 0, 6);
            }

            base.Draw(gameTime);
        }
    }
}
