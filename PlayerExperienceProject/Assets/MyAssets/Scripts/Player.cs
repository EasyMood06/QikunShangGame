using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // falling parameters
    float gravity = 3f;
    float maxFallSpeed = 10f;
    float fallingSpeed = 0f;
    // horizontal parameters
    float hMoveSpeed = 5f;

    public GameObject level;
    float yInstantiatePosition;
    float xmin = 8f;
    float xmax = 25f;


    int coin_nums = 0;

    bool isPause = true;
    public GameObject startUI;

    int life = 3;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    bool isEnd;
    public GameObject deadUI;

    public Text fallingText;

    AudioSource audioSource;
    public AudioClip getCoin;
    public AudioClip getHit;



    // Start is called before the first frame update
    void Start()
    {
        // need space to start
        isPause = true;
        coin_nums = 0;
        startUI.SetActive(true);
        life = 3;
        heart1.SetActive(true); heart2.SetActive(true); heart3.SetActive(true);
        deadUI.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnd)
            return;
        ProcessHorizontalMovement();

        if (Input.GetKey(KeyCode.Space))
        {
            isPause = false;
            startUI.SetActive(false);
        }

        if(isPause)
        {
            return;
        }

        ProcessFalling();

        
        ProcessFallingText();
    }

    void ProcessFalling()
    {

        fallingSpeed += gravity * Time.deltaTime;

        float coinXfallspeed = maxFallSpeed + maxFallSpeed * (coin_nums) * 0.25f;

        if (fallingSpeed >= coinXfallspeed)
            fallingSpeed = coinXfallspeed;


        gameObject.transform.Translate(Vector3.down * fallingSpeed * Time.deltaTime, Space.World);
    }

    void ProcessFallingText()
    {
        print("speed" + fallingSpeed);
        fallingText.text = fallingSpeed.ToString();
        float GBcolor;
        if(fallingSpeed >= 30)
        {
            GBcolor = 0;
        }
        else
        {
            GBcolor = 1 - fallingSpeed / 30.0f;
        }
        fallingText.color = new Color(1, GBcolor, GBcolor);
    }

    void ProcessHorizontalMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal") * hMoveSpeed * Time.deltaTime;
        transform.Translate(Vector3.right * horizontalInput);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xmin, xmax), transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            coin_nums++;
            audioSource.PlayOneShot(getCoin);
        }
        if(other.gameObject.tag == "Finish")
        {
            yInstantiatePosition = other.gameObject.GetComponent<Transform>().position.y;
            yInstantiatePosition -= 300;
            Instantiate(level, new Vector3(0, yInstantiatePosition, 0),Quaternion.identity);
        }
    }

    public int GetCoinNum()
    {
        return coin_nums;
    }

    private void OnCollisionEnter(Collision collision)
    {
        DeleteLife();
        fallingSpeed = 0f;
        isPause = true;
        startUI.SetActive(true);
        audioSource.PlayOneShot(getHit);
    }

    private void DeleteLife()
    {
        life--;
        switch(life)
        {
            case 2:
                heart3.SetActive(false);
                break;
            case 1:
                heart3.SetActive(false);
                heart2.SetActive(false);
                break;
            case 0:
                heart3.SetActive(false);
                heart2.SetActive(false);
                heart1.SetActive(false);
                GameEnd();
                break;
        }
    }

    private void GameEnd()
    {
        isEnd = true;
        deadUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

}
