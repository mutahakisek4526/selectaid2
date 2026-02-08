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
                    new() { Label = "あ", Value = "あ" }, new() { Label = "い", Value = "い" }, new() { Label = "う", Value = "う" }, new() { Label = "え", Value = "え" }, new() { Label = "お", Value = "お" }, new() { Label = "わ", Value = "わ" },
                    new() { Label = "か", Value = "か" }, new() { Label = "き", Value = "き" }, new() { Label = "く", Value = "く" }, new() { Label = "け", Value = "け" }, new() { Label = "こ", Value = "こ" }, new() { Label = "ん", Value = "ん" },
                    new() { Label = "さ", Value = "さ" }, new() { Label = "し", Value = "し" }, new() { Label = "す", Value = "す" }, new() { Label = "せ", Value = "せ" }, new() { Label = "そ", Value = "そ" }, new() { Label = "ー", Value = "ー" },
                    new() { Label = "た", Value = "た" }, new() { Label = "ち", Value = "ち" }, new() { Label = "つ", Value = "つ" }, new() { Label = "て", Value = "て" }, new() { Label = "と", Value = "と" }, new() { Label = "。", Value = "。" },
                    new() { Label = "な", Value = "な" }, new() { Label = "に", Value = "に" }, new() { Label = "ぬ", Value = "ぬ" }, new() { Label = "ね", Value = "ね" }, new() { Label = "の", Value = "の" }, new() { Label = "、", Value = "、" },
                    new() { Label = "は", Value = "は" }, new() { Label = "ひ", Value = "ひ" }, new() { Label = "ふ", Value = "ふ" }, new() { Label = "へ", Value = "へ" }, new() { Label = "ほ", Value = "ほ" }, new() { Label = "?", Value = "?" },
                    new() { Label = "ま", Value = "ま" }, new() { Label = "み", Value = "み" }, new() { Label = "む", Value = "む" }, new() { Label = "め", Value = "め" }, new() { Label = "も", Value = "も" }, new() { Label = "!", Value = "!" },
                    new() { Label = "や", Value = "や" }, new() { Label = "ゆ", Value = "ゆ" }, new() { Label = "よ", Value = "よ" }, new() { Label = "ぁ", Value = "ぁ" }, new() { Label = "っ", Value = "っ" }, new() { Label = "゛", Value = "dakuten" },
                    new() { Label = "ら", Value = "ら" }, new() { Label = "り", Value = "り" }, new() { Label = "る", Value = "る" }, new() { Label = "れ", Value = "れ" }, new() { Label = "ろ", Value = "ろ" }, new() { Label = "゜", Value = "handakuten" }
                }
            },
            new()
            {
                Id = "numbers",
                Name = "数字/記号",
                Columns = 5,
                Keys = new List<KeyboardKey>
                {
                    new() { Label = "1", Value = "1" }, new() { Label = "2", Value = "2" }, new() { Label = "3", Value = "3" }, new() { Label = "4", Value = "4" }, new() { Label = "5", Value = "5" },
                    new() { Label = "6", Value = "6" }, new() { Label = "7", Value = "7" }, new() { Label = "8", Value = "8" }, new() { Label = "9", Value = "9" }, new() { Label = "0", Value = "0" },
                    new() { Label = "@", Value = "@" }, new() { Label = "#", Value = "#" }, new() { Label = "$", Value = "$" }, new() { Label = "%", Value = "%" }, new() { Label = "&", Value = "&" }
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
