namespace GameData.Resources.Animation;

using GameData.Resources.Animation.FrameCommands;

public record Frame {
    public List<FrameCommand> Commands { get; set; } = [];
}