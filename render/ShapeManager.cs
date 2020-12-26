using System.Collections.Generic;

namespace Primatives.render
{
    public static class ShapeManager
    {
        private static List<Shape> _shapes = new List<Shape>();
        public static List<Shape> Shapes => _shapes;

        public static void AddShape(Shape shape)
        {
            _shapes.Add(shape);
        }

        public static void RenderShapes()
        {
            foreach (var shape in _shapes)
            {
                shape.Render();
            }
        }

        public static void CalculateVertices()
        {
            foreach (var shape in _shapes)
            {
                shape.CalculateVertices();
            }
        }
    }
}