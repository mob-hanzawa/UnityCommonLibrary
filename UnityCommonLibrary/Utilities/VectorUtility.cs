﻿using UnityEngine;

namespace UnityCommonLibrary.Utilities
{
	public static class VectorUtility
	{
		public static Vector3 Round(Vector3 vec)
		{
			return new Vector3()
			{
				x = Mathf.RoundToInt(vec.x),
				y = Mathf.RoundToInt(vec.y),
				z = Mathf.RoundToInt(vec.z),
			};
		}
		public static Vector2 Round(Vector2 vec)
		{
			return new Vector2()
			{
				x = Mathf.RoundToInt(vec.x),
				y = Mathf.RoundToInt(vec.y)
			};
		}
		public static Vector3 RoundTo(Vector3 vec, float nearest)
		{
			return new Vector3()
			{
				x = MathUtility.RoundTo(vec.x, nearest),
				y = MathUtility.RoundTo(vec.y, nearest),
				z = MathUtility.RoundTo(vec.z, nearest)
			};
		}
		public static Vector3 RoundTo(Vector2 vec, float nearest)
		{
			return new Vector2()
			{
				x = MathUtility.RoundTo(vec.x, nearest),
				y = MathUtility.RoundTo(vec.y, nearest)
			};
		}
		public static Vector2 SmoothStep(Vector2 from, Vector2 to, float t)
		{
			return new Vector2()
			{
				x = Mathf.SmoothStep(from.x, to.x, t),
				y = Mathf.SmoothStep(from.y, to.y, t),
			};
		}
		public static void SetXYZ(this Vector3 v3, float? x = null, float? y = null, float? z = null)
		{
			if (x.HasValue)
			{
				v3.x = x.Value;
			}
			if (y.HasValue)
			{
				v3.y = y.Value;
			}
			if (z.HasValue)
			{
				v3.z = z.Value;
			}
		}
	}
}