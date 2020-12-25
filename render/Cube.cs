using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Primatives
{
    public class Cube : Shape
    {
        public Cube(GraphicsDevice graphicsDevice, Matrix world, Matrix view, Matrix projection) : base(graphicsDevice, world, view, projection)
        {
            _vertices = new VertexPositionColor[8];
            _vertices[0] = new VertexPositionColor(new Vector3(0.5f, -0.5f, 0.5f), Color.Red);
            _vertices[1] = new VertexPositionColor(new Vector3(0.5f, -0.5f, -0.5f), Color.Blue);
            _vertices[2] = new VertexPositionColor(new Vector3(-0.5f, -0.5f, 0.5f), Color.Yellow);
            _vertices[3] = new VertexPositionColor(new Vector3(-0.5f, -0.5f, -0.5f), Color.Green);
            _vertices[4] = new VertexPositionColor(new Vector3(0.5f, 0.5f, 0.5f), Color.Red);
            _vertices[5] = new VertexPositionColor(new Vector3(-0.5f, 0.5f, 0.5f), Color.Blue);
            _vertices[6] = new VertexPositionColor(new Vector3(0.5f, 0.5f, -0.5f), Color.Yellow);
            _vertices[7] = new VertexPositionColor(new Vector3(-0.5f, 0.5f, -0.5f), Color.Green);
            
            VertexBuffer = new VertexBuffer(_graphicsDevice, typeof(VertexPositionColor), 8, BufferUsage.WriteOnly);
            VertexBuffer.SetData<VertexPositionColor>(_vertices);
            
            _indices = new short[24];
            _indices[0] = 0; _indices[1] = 1; 
            _indices[2] = 0; _indices[3] = 2;
            _indices[4] = 2; _indices[5] = 3;
            _indices[6] = 1; _indices[7] = 3; 
            
            _indices[8] = 4; _indices[9] = 5;
            _indices[10] = 4; _indices[11] = 6;
            _indices[12] = 6; _indices[13] = 7;
            _indices[14] = 5; _indices[15] = 7;
            
            _indices[16] = 4; _indices[17] = 0;
            _indices[18] = 5; _indices[19] = 2;
            _indices[20] = 6; _indices[21] = 1;
            _indices[22] = 3; _indices[23] = 7;
            
            // Initialise the index buffer and set the indices
            IndexBuffer = new IndexBuffer(_graphicsDevice, typeof(short), _indices.Length, BufferUsage.WriteOnly);
            IndexBuffer.SetData(_indices);
        }
    }
}