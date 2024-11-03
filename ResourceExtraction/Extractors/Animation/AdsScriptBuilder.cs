namespace ResourceExtraction.Extractors.Animation;

using GameData.Resources.Animation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class AdsScriptBuilder {
    public static HashSet<ushort> SeenCommands = [];

    public static Dictionary<int, List<AdsScriptCall>> CreateDebug(byte[] scriptBytes) {
        var script = new Dictionary<int, List<AdsScriptCall>>();
        using var scriptReader = new BinaryReader(new MemoryStream(scriptBytes));
        while (scriptReader.BaseStream.Position < scriptReader.BaseStream.Length) {
            int index = scriptReader.ReadUInt16();
            ushort cmd = scriptReader.ReadUInt16();
            SeenCommands.Add(cmd);
            var commands = new List<AdsScriptCall>();
            while (cmd != 0xFFFF) {
                int argCount = GetCommandArgCount(cmd);
                List<string> arguments = new(argCount);
                for (var i = 0; i < argCount; i++) {
                    arguments.Add($"{scriptReader.ReadUInt16():X4}");
                }
                commands.Add(new AdsScriptCall {
                    Function = $"{cmd:X4}",
                    Arguments = arguments
                });
                cmd = scriptReader.ReadUInt16();
                SeenCommands.Add(cmd);
            }
            script[index] = commands;
        }

        return script;
    }

    public static Dictionary<int, string> CreateFrom(byte[] scriptBytes) {
        using var scriptReader = new BinaryReader(new MemoryStream(scriptBytes));

        return ParseScenes(scriptReader);
    }

    private static Dictionary<int, string> ParseScenes(BinaryReader reader) {
        var scenes = new Dictionary<int, string>();

        while (reader.BaseStream.Position < reader.BaseStream.Length) {
            ushort sceneNumber = reader.ReadUInt16();
            string sceneScript = ParseScript(reader);
            scenes[sceneNumber] = sceneScript;
        }

        return scenes;
    }

    private static string ParseScript(BinaryReader reader) {
        var script = new System.Text.StringBuilder();
        int indentLevel = 0;
        bool buildingCondition = false;
        bool combiningCondition = false;
        bool inElse = false;
        var conditionBuilder = new System.Text.StringBuilder();

        const int indentSize = 4;
        while (reader.BaseStream.Position < reader.BaseStream.Length) {
            ushort cmd = reader.ReadUInt16();
            SeenCommands.Add(cmd);

            if (cmd == 0xFFFF)
                break; // End of scene

            string line = ParseCommand(cmd, reader);

            if (cmd is 0x1510 or 0x1520) {
                if (indentLevel > 0) {
                    indentLevel--;
                    if (inElse) {
                        indentLevel--;
                    }
                }
                inElse = false;
            }

            if (line.Length == 0) {
                continue;
            }

            if (line is "and") {
                conditionBuilder.Append($" {line} ");
                combiningCondition = true;
            } else if ((cmd & 0xFF00) == 0x1300 || (cmd & 0xFF00) == 0x1000) {
                if (combiningCondition) {
                    conditionBuilder.Append(line);
                    combiningCondition = false;
                } else {
                    if (buildingCondition) {
                        script.AppendLine(new string(' ', indentLevel * indentSize) + conditionBuilder);
                        conditionBuilder.Clear();
                        indentLevel++;
                    }
                    buildingCondition = true;
                    conditionBuilder.Append((inElse ? "else " : string.Empty) + $"if {line}");
                }
            } else if (line is "else") {
                inElse = true;
            } else {
                if (buildingCondition) {
                    script.AppendLine(new string(' ', indentLevel * indentSize) + conditionBuilder);
                    conditionBuilder.Clear();
                    indentLevel++;
                    buildingCondition = false;
                }
                script.AppendLine(new string(' ', indentLevel * indentSize) + line);
                combiningCondition = false;
            }
        }

        return script.ToString();
    }

    private static string ParseCommand(ushort cmd, BinaryReader reader) {
        var args = ReadRemainingArguments(cmd, reader).ToArray();
        return cmd switch {
            0x1030 => $"not played scene {args[1]}",
            0x1330 => $">not played scene {args[1]}<",
            0x1350 =>  $"played scene {args[1]}",
            // 0x1380 => $"var1 <= {ReadArgument(reader)}",
            // 0x1390 => $"var1 >= {ReadArgument(reader)}",
            0x13A0 => $"$chapter <= {args[0]}",
            // 0x13A1 => $"var2 <= {ReadArgument(reader)}",
            0x13B0 => $"$chapter >= {args[0]}",
            // 0x13B1 => $"var2 >= {ReadArgument(reader)}",
            // 0x13C0 => $"Chapter == {ReadArgument(reader)}",
            // 0x13C1 => $"var2 == {ReadArgument(reader)}",
            0x1420 => "and",
            // 0x1430 => "||",
            0x1500 => "else",
            0x1510 => "",
            0x1520 => "",
            0x2000 => $"Continue scene {args[1]}",
            0x2005 => $"Play scene {args[1]}",
            0x2010 => $"Stop scene {args[1]}",
            // 0xF010 => $"FadeOut({ReadArgument(reader)});",
            // 0xFFF0 => "}",
            _ => throw new NotImplementedException($"Unknown command {cmd:X4} at position {reader.BaseStream.Position - 2}"),
        };
    }

    private static string ReadArgument(BinaryReader reader) {
        return $"{reader.ReadUInt16():D}";
    }

    private static IEnumerable<string> ReadRemainingArguments(ushort cmd, BinaryReader reader) {
        int argCount = GetCommandArgCount(cmd);

        return Enumerable.Range(0, argCount).Select(_ => ReadArgument(reader));
    }

    private static int GetCommandArgCount(ushort cmd) {
        return cmd switch {
            0x2000 or 0x2005 => 4,
            0x2010 or 0x2015 or 0x2020 or 0x4000 or 0x4010 => 3,
            0x1010 or 0x1020 or 0x1030 or 0x1040 or 0x1050 or 0x1060 or 0x1070 or 0x1310 or 0x1320 or 0x1330 or 0x1340 or 0x1350 or 0x1360 or 0x1370 => 2,
            0xF010 or 0xF200 or 0xF210 or 0x1080 or 0x1380 or 0x1390 or 0x13A0 or 0x13A1 or 0x13B0 or 0x13B1 or 0x13C0 or 0x13C1 or 0x3020 => 1,
            _ => 0
        };
    }
}