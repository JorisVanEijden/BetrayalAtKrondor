namespace GameData.Resources.Animation;

public record AnimationResource : IResource {
    public AnimationResource(string id) {
        Id = id;
    }

    public string Version { get; set; }
    public ushort NumberOfFrames { get; set; }
    public Dictionary<int, AnimationScene> Scenes { get; set; }

    public ResourceType Type { get => ResourceType.TTM; }
    public string Id { get; }
}