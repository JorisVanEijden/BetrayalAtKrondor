namespace ResourceExtraction.Extractors.Animation;

public class CutsceneCommand {
    public ushort Token { get; }
    public ushort[] Arguments { get; }

    public CutsceneCommand(ushort token, ushort[] arguments) {
        Token = token;
        Arguments = arguments;
    }

    public override string ToString() {
        switch (Token) {
            case 0x1510: // END_IF
                return "END IF";
            case 0x1500: // ELSE
                return "ELSE";
            case 0x1520: // END_IF
                return "END IF";
            case 0x1030: // IF_SCENE_NOT_PLAYED
                return $"IF NOT PLAYED scene_{Arguments[1]}";
            case 0x1420: // AND
                return "AND"; // The AND will be handled specially in ToHumanReadableScript
            case 0x1330: // IF_SCENE_NOT_PLAYED
                return $"IF NOT PLAYED scene_{Arguments[1]}";
            case 0x1350: // IF_SCENE_PLAYED
                return $"IF PLAYED scene_{Arguments[1]}";
            case 0x13A0: // IF_CHAPTER_LESS_EQUAL
                return $"IF CHAPTER <= {Arguments[0]}";
            case 0x13B0: // IF_CHAPTER_GREATER_EQUAL
                return $"IF CHAPTER >= {Arguments[0]}";
            case 0x2000: // CONTINUE_SCENE
                return $"CONTINUE scene_{Arguments[1]}";
            case 0x2005: // START_SCENE
                return $"START scene_{Arguments[1]}";
            case 0x2010: // STOP_SCENE
                return $"STOP scene_{Arguments[1]}";
            case 0xFFFF: // END_OF_SCRIPT
                return "END OF SCRIPT";
            default:
                return $"UNKNOWN_COMMAND 0x{Token:X4}, {string.Join(", ", Arguments)}";
        }
    }

    public static int GetCommandArgCount(ushort cmd) {
        return cmd switch {
            0x2000 or 0x2005 => 4, // Scene management commands (only second argument used)
            0x2010 => 3, // Scene management commands (only second argument used)
            0x1030 or 0x1330 or 0x1350 => 2, // Scene condition commands (first argument ignored)
            0x13A0 or 0x13B0 => 1, // Chapter comparison commands
            0x1420 or 0x1500 or 0x1520 or 0x1510 or 0xFFFF => 0, // Logical AND, ELSE, block ends, and end of script have no arguments
            _ => 0
        };
    }
}