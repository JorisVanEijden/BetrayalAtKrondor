namespace GameData.Resources.Animation.FrameCommands;

public class TagFrame : FrameCommand {
    public int SceneNumber { get; set; }
    public string? SceneTag { get; set; }
    public bool UnknownBool { get; set; }

    public override string ToString() {
        return $"{nameof(TagFrame)}({SceneNumber}, {UnknownBool}) [{SceneTag}] ;";
    }
}