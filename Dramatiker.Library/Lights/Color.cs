namespace Dramatiker.Library.Lights;

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

	public Color(string hex)
	{
		var bytes = Convert.FromHexString(hex);
		R = bytes[0];
		G = bytes[1];
		B = bytes[2];
		A = bytes[3];
	}

	public byte R;
	public byte G;
	public byte B;
	public byte A;

	public static Color Crimson => new(220, 020, 060);
	public static Color MagentaHaze => new(159, 069, 118);
	public static Color Blue => new(000, 000, 255);
	public static Color BlueWhite => new(080, 080, 190);
	public static Color Red => new(255, 000, 000);
	public static Color Green => new(000, 255, 000);
	public static Color Black => new(000, 000, 000);
	public static Color White => new(255, 255, 255);
	public static Color WarmWhite => new(140, 130, 120);
	public static Color PinkWhite => new(150, 090, 140);
	public static Color Pink => new(150, 000, 140);
	public static Color Yellow => new(200, 190, 000);
	public static Color Orange => new(200, 100, 000);
	public static Color OrangeWhite => new(250, 190, 090);
	public static Color Purple => new(128, 000, 128);
	public static Color Turquise => new(000, 150, 150);
	public static Color Transparent => new(000, 000, 000, 000);

	public static Color Lerp(Color s, Color t, float k)
	{
		k = Clamp(k, 0, 1);
		var bk = 1 - k;
		var a = s.A * bk + t.A * k;
		var r = s.R * bk + t.R * k;
		var g = s.G * bk + t.G * k;
		var b = s.B * bk + t.B * k;
		return new Color((byte) r, (byte) g, (byte) b, (byte) a);
	}

	public static float Clamp(float value, float min, float max)
	{
		return value < min ? min : value > max ? max : value;
	}

	public string ToHex()
	{
		return Convert.ToHexString(new[] {R, G, B, A});
	}
}