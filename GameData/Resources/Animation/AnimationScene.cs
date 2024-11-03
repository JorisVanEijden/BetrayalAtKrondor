namespace GameData.Resources.Animation;

using GameData.Resources.Animation.FrameCommands;

public record AnimationScene {
    public string SceneTag { get; set; }
    public bool UnknownBool { get; set; }
    public List<List<FrameCommand>> Frames { get; set; } = [];
}