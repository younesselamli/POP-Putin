using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Punch : MonoBehaviour
{
    public Animator text;
    [SerializeField] AudioClip[] punches;
    AudioSource punchSoundEF;
    public Login playFabManager;
    public Text score_Txt;
    public Text countryScore;
    public int score;
    public GameObject MainImg;
    public GameObject secondImg;
    

    // Start is called before the first frame update
    void Start()
    {
        punchSoundEF=GetComponent<AudioSource>();
        score = PlayerPrefs.GetInt("Sc");
    }
    private void Update()
    {
        PlayerPrefs.SetInt("Sc", score);
        score_Txt.text = score.ToString();


        if (Input.GetMouseButton(0))
        {
            MainImg.SetActive(false);
            secondImg.SetActive(true);

        }
        else
        {
            MainImg.SetActive(true);
            secondImg.SetActive(false);

        }
        if (Input.GetMouseButtonDown(0))
        {
            score += 1;
            text.SetBool("pun", true);
            playFabManager.SendLeaderBoard(score);
            soundEF();
        }
        if (Input.GetMouseButtonUp(0))
        {
            text.SetBool("pun", false);
        }

    }
    void soundEF()
    {
        AudioClip clips = punches[UnityEngine.Random.Range(0, punches.Length)];
        punchSoundEF.PlayOneShot(clips);

    }
}
