using UnityEngine;
using UnityEngine.Audio;
public class SoundManager : MonoBehaviour
{
    public AudioClip bgmClip;   // BGM�̉����f�[�^
    public AudioClip[] seClips; // SE�̉����f�[�^
    public static SoundManager instance; // �V���O���g���̎���
    private AudioSource bgmAudio; // BGM�p�̃v���C���[
    private AudioSource[] seAudios;// SE�p�̃v���C���[
    public int maxSeAudio = 10;  // SE�p�̃v���C���[�̍ő吔
    void Start()
    {
        instance = this;
        bgmAudio = gameObject.AddComponent<AudioSource>();
        bgmAudio.clip = bgmClip;// BGM�̉�����ݒ�
        bgmAudio.loop = true;// ���[�v�Đ���L����
        bgmAudio.Play();// �����̍Đ��J�n

        seAudios = new AudioSource[maxSeAudio];
        for (int i = 0; i < maxSeAudio; i++)
        {
            seAudios[i] = gameObject.AddComponent<AudioSource>();
            seAudios[i].loop = false; // ���[�v�Đ��𖳌���
            seAudios[i].playOnAwake = false;// �����Đ�������
        }
    }
    public void PlaySE(int index)
    {
        if (index < 0) return; // index��0�����Ȃ牽�����Ȃ�
        if(index >= seClips.Length) return;// index���͈͊O�Ȃ牽�����Ȃ�
        for (int i = 0; i < maxSeAudio; i++)
        {  // �Đ����ł͂Ȃ��v���C���[��T��
            if (seAudios[i].isPlaying) continue;// �Đ����Ȃ玟��
            seAudios[i].PlayOneShot(seClips[index]);// SE���Đ�
            break;// SE��炵����for���𔲂���
        }
    }
}
