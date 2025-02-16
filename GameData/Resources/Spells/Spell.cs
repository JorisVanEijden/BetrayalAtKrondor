namespace GameData.Resources.Spells;

using GameData;

public class Spell : IResource {
    public Spell(string id) {
        Id = id;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public int MinimumCost { get; set; }
    public int MaximumCost { get; set; }
    public bool Field6 { get; set; }
    public int Field8 { get; set; }
    public int FieldA { get; set; }
    public int FieldC { get; set; }
    public int ObjectId { get; set; }
    public SpellCalculation Calculation { get; set; }
    public int Damage { get; set; }
    public int Duration { get; set; }
    public SpellInfo Info { get; set; }
    public ResourceType Type { get => ResourceType.DAT; }

    public string ToCsv() {
        return $"{Id},{Name},{MinimumCost},{MaximumCost},{Field6},{Field8},{FieldA},{FieldC},{ObjectId},{Calculation},{Damage},{Duration}";
    }
}