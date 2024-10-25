namespace GameData.Resources.Animation.FrameCommands;

// This appears to do absolutely nothing.
public class UnknownCommandA0B5 : FrameCommand {
    public int Arg1 { get; set; }

    public int Arg2 { get; set; }

    public int Arg3 { get; set; }

    public int Arg4 { get; set; }

    public int Arg5 { get; set; }

    public override string ToString() {
        return $"{nameof(UnknownCommandA0B5)}({Arg1}, {Arg2}, {Arg3}, {Arg4}, {Arg5});";
    }
}