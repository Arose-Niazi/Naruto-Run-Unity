using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioHandler : Reset
{
    public AudioClip[] OrochimaruClips = new AudioClip[6];
    public AudioSource OrochimaruAudioSource;

    private readonly ArrayList _toPlayOrochimaru = new ArrayList();
    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating(nameof(PlayOrochimaruSound), 15f, 15f);
        ResetSettings();
    }
    
    void PlayOrochimaruSound()
    {
        if (Settings.GameOver) return;
        if (_toPlayOrochimaru.Count > 0)
        {
            var x = Random.Range(0, _toPlayOrochimaru.Count - 1);
            var i = (int) _toPlayOrochimaru[x];
            OrochimaruAudioSource.clip = OrochimaruClips[i];
            _toPlayOrochimaru.RemoveAt(x);
            OrochimaruAudioSource.Play();
        }
    }

    public override void ResetSettings()
    {
        _toPlayOrochimaru.Clear();
        for (var x = 0; x < OrochimaruClips.Length; x++)
            _toPlayOrochimaru.Add(x);
        
        OrochimaruAudioSource.Stop();
    }
    
}
