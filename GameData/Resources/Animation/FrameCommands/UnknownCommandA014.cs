namespace GameData.Resources.Animation.FrameCommands;

// This appears to do absolutely nothing.
public class UnknownCommandA014 : FrameCommand {
    public int Arg1 { get; set; }

    public int Arg2 { get; set; }

    public int Arg3 { get; set; }

    public int Arg4 { get; set; }

    public override string ToString() {
        return $"{nameof(UnknownCommandA014)}({Arg1}, {Arg2}, {Arg3}, {Arg4});";
    }
}