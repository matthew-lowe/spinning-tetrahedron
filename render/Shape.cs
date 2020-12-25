using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Primatives
{
    public abstract class Shape
    {
        protected BasicEffect _basicEffect;
        
        protected short _numVertices;
        protected short _numPrimatives;
        
        public short NumVertices => _numVertices;
        public short NumPrimatives => _numPrimatives;

        public VertexBuffer VertexBuffer { get; set; }
        public IndexBuffer IndexBuffer { get; set; }
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

            World = world;
            View = view;
            Projection = projection;
        }
    }
}