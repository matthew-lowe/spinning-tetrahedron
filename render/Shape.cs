using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Primatives
{
    public abstract class Shape
    {
        protected BasicEffect _basicEffect;

        public short NumVertices => (short) _vertices.Length;
        public short NumPrimatives => (short) (_indices.Length / 2);

        public VertexBuffer VertexBuffer { get; set; }
        public IndexBuffer IndexBuffer { get; set; }

        public Vector3 Size { get; set; }
        public Vector3 Origin { get; set; }
        
        public BasicEffect BasicEffect => _basicEffect;

        protected GraphicsDevice _graphicsDevice;
        
        // Private backing fields shouldn't directly be set, else the effect won't change
        protected Matrix _world;
        public Matrix World
        {
            get { return _world; }
            set
            {
                _world = value;
                _basicEffect.World = _world;
            }
        }

        protected Matrix _view;
        public Matrix View
        {
            get { return _view;}
            set
            {
                _view = value;
                _basicEffect.View = _view;
            }
        }

        protected Matrix _projection;
        public Matrix Projection
        {
            get { return _projection;}
            set
            {
                _projection = value;
                _basicEffect.Projection = _projection;
            }
        }

        protected VertexPositionColor[] _vertices;
        protected short[] _indices;

        public VertexPositionColor[] Vertices => _vertices;
        public short[] Indices => _indices;

        // Initialised to white to start
        public Color[] VertexColors { get; set; }

        public Shape(GraphicsDevice graphicsDevice, Matrix world, Matrix view, Matrix projection)
        {
            _graphicsDevice = graphicsDevice;
            _basicEffect = new BasicEffect(_graphicsDevice) {VertexColorEnabled = true};

            Size = new Vector3(1, 1, 1);
            Origin = new Vector3(0, 0, 0);
            
            World = world;
            View = view;
            Projection = projection;
        }
        public void Render()
        {
            _graphicsDevice.SetVertexBuffer(VertexBuffer);
            _graphicsDevice.Indices = IndexBuffer;
            
            RasterizerState rasterizerState = new RasterizerState {CullMode = CullMode.CullClockwiseFace, MultiSampleAntiAlias = true};
            _graphicsDevice.RasterizerState = rasterizerState;
            
            foreach (EffectPass pass in BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _graphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.LineList, Vertices, 0, NumVertices, Indices, 0, NumPrimatives);
            }
        }

        public abstract void CalculateVertices();
    }
}