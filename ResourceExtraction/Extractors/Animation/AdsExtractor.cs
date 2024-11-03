namespace ResourceExtraction.Extractors.Animation;

using GameData.Resources.Animation;
using ResourceExtraction.Extensions;
using ResourceExtraction.Extractors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class AdsExtractor : ExtractorBase<AnimatorResource> {
    public override AnimatorResource Extract(string id, Stream resourceStream) {
        using var resourceReader = new BinaryReader(resourceStream, Encoding.GetEncoding(DosCodePage));
        var animation = new AnimatorResource(id);
        string tag = ReadTag(resourceReader);
        if (tag != "VER") {
            throw new InvalidDataException($"Expected VER tag, got {tag}");
        }
        int verSize = resourceReader.ReadUInt16();
        if (resourceReader.ReadUInt16() != 0x0000) {
            throw new InterestingDataException("UInt16 after size is not 0x0000 in VER tag");
        }
        animation.Version = resourceReader.ReadZeroTerminatedString();
        tag = ReadTag(resourceReader);
        if (tag != "ADS") {
            throw new InvalidDataException($"Expected ADS tag, got {tag}");
        }
        uint adsSize = resourceReader.ReadUInt32();
        if ((adsSize | 0x8000) == 0) {
            throw new InterestingDataException("Ads size does not have 0x8000 in ADS tag");
        }
        tag = ReadTag(resourceReader);
        if (tag != "RES") {
            throw new InvalidDataException($"Expected RES tag, got {tag}");
        }
        uint resSize = resourceReader.ReadUInt32();
        int nrOfResEntries = resourceReader.ReadUInt16();
        animation.ResourceFiles = new Dictionary<int, string>(nrOfResEntries);
        for (var i = 0; i < nrOfResEntries; i++) {
            int resourceId = resourceReader.ReadUInt16();
            string fileName = resourceReader.ReadZeroTerminatedString();
            animation.ResourceFiles.Add(resourceId, fileName);
        }

        tag = ReadTag(resourceReader);
        if (tag != "SCR") {
            throw new InvalidDataException($"Expected SCR tag, got {tag}");
        }
        uint scrSize = resourceReader.ReadUInt32();
        byte[] scriptBytes = DecompressToByteArray(resourceReader, scrSize);
        Dictionary<int, string> scripts = AdsScriptBuilder.CreateFrom(scriptBytes);
        Dictionary<int, List<AdsScriptCall>> commandsDebug = AdsScriptBuilder.CreateDebug(scriptBytes);

        tag = ReadTag(resourceReader);
        if (tag != "TAG") {
            throw new InvalidDataException($"Expected TAG tag, got {tag}");
        }
        Dictionary<int, string> tags = ReadTags(resourceReader);

        foreach (int sceneNr in scripts.Keys) {
            var scene = new AnimatorScript {
                Id = sceneNr,
                Tag = tags[sceneNr],
                Script = scripts[sceneNr],
                CommandsDebug = commandsDebug[sceneNr]
            };
            animation.Animations.Add(scene);
        }

        return animation;
    }
}

internal class InterestingDataException : Exception {
    public InterestingDataException(string message) {
        throw new Exception(message);
    }
}