namespace GameData;

public static class PaletteMapping {

    public static string? GetPaletteFor(string image) {
        image = StripThreeLetterExtension(image).ToUpper();

        return image switch {
            "BLANK" => "CREDITS.PAL",
            "BOOK" => "BOOK.PAL",
            "C11" => "C11B.PAL",
            "C11A1" => "C11A.PAL",
            "C11A2" => "C11A.PAL",
            "C11B" => "C11B.PAL",
            "C12A" => "C12A.PAL",
            "C12A_BAK" => "C12A.PAL",
            "C12A_MAG" => "C12A.PAL",
            "C12A_PUG" => "C12A.PAL",
            "C12B_ARC" => "C12B.PAL",
            "C12B_GOR" => "C12B.PAL",
            "C12B_SRL" => "C12A.PAL",
            "C42" => "C42.PAL",
            "C61A_TLK" => "C61B.PAL",
            "C61B_BAK" => "C61B.PAL",
            "CAST" => "OPTIONS.PAL",
            "CFRAME" => "OPTIONS.PAL",
            "CHAPTER" => "CHAPTER.PAL",
            "CONT2" => "CONTENTS.PAL",
            "CONTENTS" => "CONTENTS.PAL",
            "CREDITS" => "CREDITS.PAL",
            "DIALOG" => "OPTIONS.PAL",
            "ENCAMP" => "OPTIONS.PAL",
            "FCOMBAT" => "OPTIONS.PAL",
            "FRAME" => "OPTIONS.PAL",
            "FULLMAP" => "FULLMAP.PAL",
            "INT_BORD" => "INT_DYN.PAL",
            "INT_BOOK" => "INT_TITL.PAL",
            "INT_BUNT" => "INT_DYN.PAL",
            "INT_LGHT" => "INT_DYN.PAL",
            "INT_MENU" => "INT_MENU.PAL",
            "INVENTOR" => "INVENTOR.PAL",
            "OPTIONS0" => "OPTIONS.PAL",
            "OPTIONS1" => "OPTIONS.PAL",
            "OPTIONS2" => "OPTIONS.PAL",
            "PUZZLE" => "PUZZLE.PAL",
            "RIFTMAP" => "OPTIONS.PAL",
            "Z01L" => "Z01.PAL",
            "Z02L" => "Z02.PAL",
            "Z03L" => "Z03.PAL",
            "Z04L" => "Z04.PAL",
            "Z05L" => "Z05.PAL",
            "Z06L" => "Z06.PAL",
            "Z07L" => "Z07.PAL",
            "Z08L" => "Z08.PAL",
            "Z09L" => "Z09.PAL",
            "Z10L" => "Z10.PAL",
            "Z11L" => "Z11.PAL",
            "Z12L" => "Z12.PAL",
            _ => null
        };
    }

    private static string StripThreeLetterExtension(string fileName) {
        if (fileName.Length > 4 && fileName[^4] == '.') {
            return fileName[..^4];
        }
        return fileName;
    }

}