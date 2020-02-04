using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] m_onlyInGame;
    [SerializeField] GameObject[] m_onlyInTitle;
    [SerializeField] PlayableDirector m_director;
    Status m_status;

    void Start()
    {
        Init();
    }

    void Init()
    {
        m_status = Status.Title;
        SwitchObjects(m_onlyInGame, false);
    }

    void Update()
    {
        switch (m_status)
        {
            case Status.Title:
                if (Input.anyKeyDown)
                {
                    m_director.Play();
                    m_status = Status.OpeningCutscene;
                    SwitchObjects(m_onlyInTitle, false);
                    SwitchObjects(m_onlyInGame, true);
                }
                break;
            case Status.OpeningCutscene:
                if (m_director.state != PlayState.Playing)
                {
                    m_status = Status.InGame;
                }
                break;
            case Status.InGame:
                break;
            default:
                break;
        }
    }

    void SwitchObjects(GameObject[] gameObjects, bool active)
    {
        foreach (var go in gameObjects)
        {
            if (go)
            {
                go.SetActive(active);
            }
        }
    }

    public void ReloadScene()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        Debug.Log("Reload " + sceneName);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}

public enum Status
{
    Title,
    OpeningCutscene,
    InGame,
}