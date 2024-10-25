namespace GameData.Resources.Animation.FrameCommands;

public class FillArea : FrameCommand, IArea {
    public int X { get; set; }

    public int Y { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public override string ToString() {
        return $"{nameof(FillArea)}({X}, {Y}, {Width}, {Height});";
    }
}