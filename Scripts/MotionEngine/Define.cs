
namespace System.Numerics
{
}

namespace UnityEngine
{
}

namespace MotionEngine
{
	public struct Vector3
	{
		public float x;
		public float y;
		public float z;
		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
	}

	public struct Vector2
	{
		public float x;
		public float y;
		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}
	}
}