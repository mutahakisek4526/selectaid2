using System;
using System.Speech.Synthesis;
using SelectAid.Models;

namespace SelectAid.Services;

public class SpeechService : IDisposable
{
    private readonly SpeechSynthesizer _synth = new();

    public void ApplySettings(SpeechSettings settings)
    {
        _synth.Rate = settings.Rate;
        _synth.Volume = settings.Volume;
        if (!string.IsNullOrWhiteSpace(settings.Voice))
        {
            _synth.SelectVoice(settings.Voice);
        }
    }

    public void Speak(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return;
        }

        _synth.SpeakAsyncCancelAll();
        _synth.SpeakAsync(text);
    }

    public void Dispose()
    {
        _synth.Dispose();
    }
}
