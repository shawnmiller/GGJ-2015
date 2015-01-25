using UnityEngine;

public static class PickColor
{

	public static Color Get(ColorType type)
	{
		switch (type)
		{
			case ColorType.Red:
				return new Color(160f / 255f, 0, 30f / 255f, 1);
			case ColorType.Green:
				return new Color(0, 130f / 255f, 40f / 255f, 1);
			case ColorType.Blue:
				return new Color(20f/ 255f, 0, 170f / 255f, 1);
			case ColorType.Orange:
				return new Color(215f / 255f, 125f / 255f, 0, 1);
			case ColorType.Purple:
				return new Color(130f / 255f, 0, 170f / 255f, 1);
			case ColorType.Black:
				return new Color(0, 0, 0, 1);
		}

		return new Color();
	}
}
