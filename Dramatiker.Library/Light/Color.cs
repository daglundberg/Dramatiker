namespace Dramatiker.Library
{
	public struct Color
	{
		public Color(byte red, byte green, byte blue, byte alpha = 255)
		{
			R = red;
			G = green;
			B = blue;
			A = alpha;
		}

		public Color(Color color, byte alpha)
		{
			R = color.R;
			G = color.G;
			B = color.B;
			A = alpha;
		}

		public byte R;
		public byte G;
		public byte B;
		public byte A;

		public static Color Crimson		{ get => new Color(220,	020, 060); }
		public static Color MagentaHaze { get => new Color(159, 069, 118); }
		public static Color Blue		{ get => new Color(000,	000, 255); }
		public static Color Red			{ get => new Color(255, 000, 000); }
		public static Color Green		{ get => new Color(000,	255, 000); }
		public static Color Black		{ get => new Color(000,	000, 000); }
		public static Color White		{ get => new Color(255,	255, 255); }
		public static Color Purple		{ get => new Color(128,	000, 128); }
		public static Color Transparent	{ get => new Color(000,	000, 000, 000); }

		public static Color Lerp(Color s, Color t, float k)
		{
			k = Clamp(k, 0, 1);
			var bk = (1 - k);
			var a = s.A * bk + t.A * k;
			var r = s.R * bk + t.R * k;
			var g = s.G * bk + t.G * k;
			var b = s.B * bk + t.B * k;
			return new Color((byte)r, (byte)g, (byte)b, (byte)a);
		}

		public static float Clamp(float value, float min, float max)
		{
			return (value < min) ? min : (value > max) ? max : value;
		}
	}
}
