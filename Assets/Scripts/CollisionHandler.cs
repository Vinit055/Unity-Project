using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float delayTime = 2f;
    [SerializeField]AudioClip crashSound;
    [SerializeField]AudioClip successSound;
    [SerializeField]ParticleSystem crashParticles;
    [SerializeField]ParticleSystem successParticles;

    AudioSource audio;

    bool isTransitioning = false;
    bool collisionDisabled = false;



    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheatDebug();
    }

      void CheatDebug()
    {
        if(Input.GetKey(KeyCode.L))
        {
            nextLevel();
        }
        else if(Input.GetKey(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //toggle collision
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || collisionDisabled){return;}

        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is okay!");
                break;
            
            case "Finish":
                nextLevelSeq();
                break;

            default:
                startCrashSeq();
                break;
        }
    }

    void startCrashSeq()
    {
        isTransitioning = true;
        audio.Stop();
        audio.PlayOneShot(crashSound);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("reloadLevel", delayTime);    
    }

    void nextLevelSeq()
    {
        isTransitioning = true;
        audio.Stop();
        audio.PlayOneShot(successSound);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("nextLevel", delayTime);
    }

    void reloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void nextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

  
}
