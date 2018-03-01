
/// <summary>
/// Returns Ai Difficulity. None == non ai player
/// </summary>
public enum AiDifficulity {
	None,
	Easy,
	Normal,
	Hard
};

public static class AiDifficulityExtension {

	public static float SpeedModifier(AiDifficulity ai) {
		switch (ai) {
			case AiDifficulity.Easy:
				return .5f;
			case AiDifficulity.Normal:
				return 1f;
			case AiDifficulity.Hard:
				return 1.5f;
			default:
				return 0f;
		}
	}
}