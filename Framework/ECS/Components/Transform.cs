using System.Numerics;
using Foster.Framework;

namespace Foster.Framework.Components
{
    public class Transform : IComponent
    {
        public Vector2 Position = Vector2.Zero;
        public float Rotation = 0f;
        public Vector2 Scale = Vector2.One;

        public Transform()
        {

        }

        public Transform(Vector2 position)
            : this(position, 0f)
        {
        }

        public Transform(Vector2 position, float rotation)
            : this(position, rotation, Vector2.One)
        {
        }

        public Transform(Vector2 position, float rotation, Vector2 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

    }
}
