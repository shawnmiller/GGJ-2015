using UnityEngine;

public static class PickColor
{

	public static Color Get(ColorType type)
	{
		switch (type)
		{
			case ColorType.Red:
				return new Color(255f, 0, 0, 1);
			case ColorType.Green:
				return new Color(0, 255f, 0, 1);
			case ColorType.Blue:
				return new Color(0, 0, 255f, 1);
		}

		return new Color();
	}
}
