namespace GameData.Resources.Animation.FrameCommands;

public class SetRange3 : FrameCommand {
    public int Start { get; set; }

    public int End { get; set; }

    public override string ToString() {
        return $"{nameof(SetRange3)}({Start}, {End});";
    }
}