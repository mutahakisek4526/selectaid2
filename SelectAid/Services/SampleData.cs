using System.Collections.Generic;
using SelectAid.Models;

namespace SelectAid.Services;

public static class SampleData
{
    public static List<KeyboardLayout> CreateDefaultLayouts()
    {
        return new List<KeyboardLayout>
        {
            new()
            {
                Id = "kana",
                Name = "標準50音",
                Columns = 6,
                Keys = new List<KeyboardKey>
                {
                    new() { Label = "あ", OutputText = "あ" }, new() { Label = "い", OutputText = "い" }, new() { Label = "う", OutputText = "う" }, new() { Label = "え", OutputText = "え" }, new() { Label = "お", OutputText = "お" }, new() { Label = "わ", OutputText = "わ" },
                    new() { Label = "か", OutputText = "か" }, new() { Label = "き", OutputText = "き" }, new() { Label = "く", OutputText = "く" }, new() { Label = "け", OutputText = "け" }, new() { Label = "こ", OutputText = "こ" }, new() { Label = "ん", OutputText = "ん" },
                    new() { Label = "さ", OutputText = "さ" }, new() { Label = "し", OutputText = "し" }, new() { Label = "す", OutputText = "す" }, new() { Label = "せ", OutputText = "せ" }, new() { Label = "そ", OutputText = "そ" }, new() { Label = "ー", OutputText = "ー" },
                    new() { Label = "た", OutputText = "た" }, new() { Label = "ち", OutputText = "ち" }, new() { Label = "つ", OutputText = "つ" }, new() { Label = "て", OutputText = "て" }, new() { Label = "と", OutputText = "と" }, new() { Label = "。", OutputText = "。" },
                    new() { Label = "な", OutputText = "な" }, new() { Label = "に", OutputText = "に" }, new() { Label = "ぬ", OutputText = "ぬ" }, new() { Label = "ね", OutputText = "ね" }, new() { Label = "の", OutputText = "の" }, new() { Label = "、", OutputText = "、" },
                    new() { Label = "は", OutputText = "は" }, new() { Label = "ひ", OutputText = "ひ" }, new() { Label = "ふ", OutputText = "ふ" }, new() { Label = "へ", OutputText = "へ" }, new() { Label = "ほ", OutputText = "ほ" }, new() { Label = "?", OutputText = "?" },
                    new() { Label = "ま", OutputText = "ま" }, new() { Label = "み", OutputText = "み" }, new() { Label = "む", OutputText = "む" }, new() { Label = "め", OutputText = "め" }, new() { Label = "も", OutputText = "も" }, new() { Label = "!", OutputText = "!" },
                    new() { Label = "や", OutputText = "や" }, new() { Label = "ゆ", OutputText = "ゆ" }, new() { Label = "よ", OutputText = "よ" }, new() { Label = "ぁ", OutputText = "ぁ" }, new() { Label = "っ", OutputText = "っ" }, new() { Label = "゛", OutputText = "゛", Action = "Dakuten" },
                    new() { Label = "ら", OutputText = "ら" }, new() { Label = "り", OutputText = "り" }, new() { Label = "る", OutputText = "る" }, new() { Label = "れ", OutputText = "れ" }, new() { Label = "ろ", OutputText = "ろ" }, new() { Label = "゜", OutputText = "゜", Action = "Handakuten" }
                }
            },
            new()
            {
                Id = "numbers",
                Name = "数字/記号",
                Columns = 5,
                Keys = new List<KeyboardKey>
                {
                    new() { Label = "1", OutputText = "1" }, new() { Label = "2", OutputText = "2" }, new() { Label = "3", OutputText = "3" }, new() { Label = "4", OutputText = "4" }, new() { Label = "5", OutputText = "5" },
                    new() { Label = "6", OutputText = "6" }, new() { Label = "7", OutputText = "7" }, new() { Label = "8", OutputText = "8" }, new() { Label = "9", OutputText = "9" }, new() { Label = "0", OutputText = "0" },
                    new() { Label = "@", OutputText = "@" }, new() { Label = "#", OutputText = "#" }, new() { Label = "$", OutputText = "$" }, new() { Label = "%", OutputText = "%" }, new() { Label = "&", OutputText = "&" }
                }
            }
        };
    }

    public static PhraseCatalog CreateDefaultPhrases()
    {
        return new PhraseCatalog
        {
            Scenes = new List<PhraseScene>
            {
                new()
                {
                    Name = "日常",
                    Categories = new List<PhraseCategory>
                    {
                        new()
                        {
                            Name = "あいさつ",
                            Items = new List<PhraseItem>
                            {
                                new() { Text = "こんにちは" },
                                new() { Text = "ありがとう" },
                                new() { Text = "よろしくお願いします" }
                            }
                        }
                    }
                }
            }
        };
    }
}
