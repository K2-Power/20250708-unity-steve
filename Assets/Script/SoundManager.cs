using UnityEngine;
using UnityEngine.Audio;
public class SoundManager : MonoBehaviour
{
    public AudioClip bgmClip;   // BGMの音源データ
    public AudioClip[] seClips; // SEの音源データ
    public static SoundManager instance; // シングルトンの実体
    private AudioSource bgmAudio; // BGM用のプレイヤー
    private AudioSource[] seAudios;// SE用のプレイヤー
    public int maxSeAudio = 10;  // SE用のプレイヤーの最大数
    void Start()
    {
        instance = this;
        bgmAudio = gameObject.AddComponent<AudioSource>();
        bgmAudio.clip = bgmClip;// BGMの音源を設定
        bgmAudio.loop = true;// ループ再生を有効化
        bgmAudio.Play();// 音源の再生開始

        seAudios = new AudioSource[maxSeAudio];
        for (int i = 0; i < maxSeAudio; i++)
        {
            seAudios[i] = gameObject.AddComponent<AudioSource>();
            seAudios[i].loop = false; // ループ再生を無効化
            seAudios[i].playOnAwake = false;// 自動再生無効化
        }
    }
    public void PlaySE(int index)
    {
        if (index < 0) return; // indexが0未満なら何もしない
        if(index >= seClips.Length) return;// indexが範囲外なら何もしない
        for (int i = 0; i < maxSeAudio; i++)
        {  // 再生中ではないプレイヤーを探す
            if (seAudios[i].isPlaying) continue;// 再生中なら次へ
            seAudios[i].PlayOneShot(seClips[index]);// SEを再生
            break;// SEを鳴らしたらfor文を抜ける
        }
    }
}
