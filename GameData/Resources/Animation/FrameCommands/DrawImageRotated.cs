namespace GameData.Resources.Animation.FrameCommands;

// This draws images rotated at a free angle, and scaled.
public class DrawImageRotated : FrameCommand {
    private int _angle;
    public int X { get; set; }
    public int Y { get; set; }
    public int ImageNumber { get; set; }
    public int ImageSlot { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public int Angle {
        get => _angle;
        set {
            _angle = value;
            float ax = (short)(-(value >> 4) + 4096) / 15f;
            Console.WriteLine($"{value:X2} => {ax:N}");
        }
    }

    public override string ToString() {
        return $"{nameof(DrawImageRotated)}({X}, {Y}, {ImageNumber}, {ImageSlot}, {Width}, {Height}, {Angle});";
    }
}