using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using HuggingFace.API;
using System.IO;
using UnityEngine.UI;


public class VoiceMovement : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private AudioSource bellRing;

    private AudioClip clip;
    private byte[] bytes;
    private bool recording;

    public static VoiceMovement Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (recording && Microphone.GetPosition(null) >= clip.samples)
        {
            StopRecording();
        }


    }

    public void StartRecording()
    {
        text.color = Color.white;
        text.text = "Recording...";
        clip = Microphone.Start(null, false, 1, 44100);
        recording = true;
        bellRing.Play();
    }

    private void StopRecording()
    {
        var position = Microphone.GetPosition(null);
        Microphone.End(null);
        var samples = new float[position * clip.channels];
        clip.GetData(samples, 0);
        bytes = EncodeAsWAV(samples, clip.frequency, clip.channels);
        recording = false;
        SendRecording();

    }

    #region Convert to Audio (WAV)
    private void SendRecording()
    {

        text.color = Color.yellow;
        text.text = "Sending...";
       
        HuggingFaceAPI.AutomaticSpeechRecognition(bytes, response => {
            text.color = Color.white;
            text.text = response;
            text.text = response;
            //call all necessary methods, based on Voice Inputs
            if(DistanceManager.InstanceDist != null)
            {
                DistanceManager.InstanceDist.SetMovement(response);
                
            }
            if(QuestsManager.Instance != null)
            {

                QuestsManager.Instance.GetTheAnswer(response);
            }
            if(GetTheVoiceInput.Instance != null)
            {
                GetTheVoiceInput.Instance.GetTheFirstAnswer(response);
            }
            
            
        }, error => {
            text.color = Color.red;
            text.text = error;
           
        });
    }

    private byte[] EncodeAsWAV(float[] samples, int frequency, int channels)
    {
        using (var memoryStream = new MemoryStream(44 + samples.Length * 2))
        {
            using (var writer = new BinaryWriter(memoryStream))
            {
                writer.Write("RIFF".ToCharArray());
                writer.Write(36 + samples.Length * 2);
                writer.Write("WAVE".ToCharArray());
                writer.Write("fmt ".ToCharArray());
                writer.Write(16);
                writer.Write((ushort)1);
                writer.Write((ushort)channels);
                writer.Write(frequency);
                writer.Write(frequency * channels * 2);
                writer.Write((ushort)(channels * 2));
                writer.Write((ushort)16);
                writer.Write("data".ToCharArray());
                writer.Write(samples.Length * 2);

                foreach (var sample in samples)
                {
                    writer.Write((short)(sample * short.MaxValue));
                }
            }
            return memoryStream.ToArray();
        }
    }
    #endregion
}
