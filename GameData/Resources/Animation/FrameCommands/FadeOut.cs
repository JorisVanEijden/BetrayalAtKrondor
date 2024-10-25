namespace GameData.Resources.Animation.FrameCommands;

/// <summary>
/// Fade out the current buffer.
/// </summary>
public class FadeOut : FrameCommand {
    public int Start { get; set; }

    public int Length { get; set; }

    /// Palette index of the color to fade to
    public int Color { get; set; }

    /// Speed ranging from 0 to 6, determines the speed of the fade
    public int Speed { get; set; }

    public override string ToString() {
        return $"{nameof(FadeOut)}({Start}, {Length}, {Color}, {Speed});";
    }
}