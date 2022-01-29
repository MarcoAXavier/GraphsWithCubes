using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;

public class FrameRateCounter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _frameRateText;
    private TextMeshProUGUI FrameRateText => _frameRateText;

    [SerializeField, Range(0.1f, 2f)]
    float _sampleDuration = 1f;
    float SampleDuration => _sampleDuration;

    private float Duration { get; set; } = 0f;
    private float BestDuration { get; set; } = 1000f;
    private float WorstDuration { get; set; } = 0;
    private int Frames { get; set; }

    private void Update()
    {
        var frameDuration = Time.unscaledDeltaTime;
        Frames++;
        Duration += frameDuration;

        if (Duration < BestDuration) BestDuration = frameDuration;
        
        if (Duration > WorstDuration) WorstDuration = frameDuration;

        if (Duration >= SampleDuration)
        {
            FrameRateText.SetText("FPS\n{0:0}\n{1:0}\n{2:0}", 1f / BestDuration, Frames / Duration, 1f / WorstDuration );
            Duration = 0f;
            Frames = 0;
            BestDuration = 1000f;
            WorstDuration = 0f;
        }
    }
}
