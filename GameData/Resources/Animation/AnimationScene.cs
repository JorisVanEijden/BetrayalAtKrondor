namespace GameData.Resources.Animation;

public record AnimationScene {
    public SceneTag SceneTag { get; set; }
    public List<Frame> Frames { get; set; } = [];
}