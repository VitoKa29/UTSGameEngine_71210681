using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public int force;
    Rigidbody2D rigid;

    int scoreP1;
    int scoreP2;
    int scoreP3;
    int scoreP4;

    Text scoreUIP1;
    Text scoreUIP2;
    Text scoreUIP3;
    Text scoreUIP4;

    GameObject panelSelesai;
    Text txPemenang;

    AudioSource audio;
    public AudioClip hitSound;
    public AudioClip victoryMusic;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Vector2 arah = new Vector2(0, 2).normalized;
        rigid.AddForce(arah * force);
                                      

        scoreP1 = 0;
        scoreP2 = 0;
        scoreP3 = 0;
        scoreP4 = 0;

        scoreUIP1 = GameObject.Find("Score1").GetComponent<Text>();
        scoreUIP2 = GameObject.Find("Score2").GetComponent<Text>();
        scoreUIP3 = GameObject.Find("Score3").GetComponent<Text>();
        scoreUIP4 = GameObject.Find("Score4").GetComponent<Text>();

        panelSelesai = GameObject.Find("PanelSelesai");
        panelSelesai.SetActive(false);

        audio = GetComponent<AudioSource>();

        GameObject mainCamera = GameObject.FindWithTag("MainCamera");
        if (mainCamera != null)
        {
            AudioSource backgroundMusic = mainCamera.GetComponent<AudioSource>();
            if (backgroundMusic != null)
            {
                audio = backgroundMusic;
            }
            else
            {
                Debug.LogWarning("AudioSource tidak ditemukan di objek Main Camera.");
            }
        }
        else
        {
            Debug.LogWarning("Objek Main Camera tidak ditemukan.");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        audio.PlayOneShot(hitSound);

        if (coll.gameObject.name == "HitBoxBawah")
        {
            scoreP2 += 1;
            scoreP3 += 1;
            TampilkanScore();
            if (scoreP2 == 11)
            {
                panelSelesai.SetActive(true);
                txPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
                txPemenang.text = "Player Merah Pemenang!";
                Color merah = new Color();
                ColorUtility.TryParseHtmlString("#EE0000", out merah);
                txPemenang.color = merah;
                audio.Stop();
                AudioSource.PlayClipAtPoint(victoryMusic, transform.position);
                Destroy(gameObject);
                return;
            }
            ResetBall();
            Vector2 arah = new Vector2(0, 2).normalized;
            rigid.AddForce(arah * force);
        }
        if (coll.gameObject.name == "HitBoxAtas")
        {
            scoreP1 += 1;
            scoreP4 += 1;
            TampilkanScore();
            if (scoreP1 == 11)
            {
                panelSelesai.SetActive(true);
                txPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
                txPemenang.text = "Player Biru Pemenang!";
                Color biru = new Color();
                ColorUtility.TryParseHtmlString("#0004FB", out biru);
                txPemenang.color = biru;
                audio.Stop();
                AudioSource.PlayClipAtPoint(victoryMusic, transform.position);
                Destroy(gameObject);
                return;
            }
            ResetBall();
            Vector2 arah = new Vector2(0, -2).normalized;
            rigid.AddForce(arah * force);
        }
        if (coll.gameObject.name == "Pemukul1" || coll.gameObject.name == "Pemukul2")
        {
            float sudut = (transform.position.y - coll.transform.position.y) * 5f;
            Vector2 arah = new Vector2(rigid.velocity.x, sudut).normalized;
            rigid.velocity = new Vector2(0, 0);
            rigid.AddForce(arah * force * 2);
        }
    }
    void ResetBall()
    {
        transform.localPosition = Vector2.zero;
        rigid.velocity = Vector2.zero;
    }



    void TampilkanScore()
    {
        Debug.Log("Score P1: " + scoreP1 + " Score P2: " + scoreP2);
        scoreUIP1.text = scoreP1 + "";
        scoreUIP2.text = scoreP2 + "";
        scoreUIP3.text = scoreP3 + "";
        scoreUIP4.text = scoreP4 + "";
    }

}
