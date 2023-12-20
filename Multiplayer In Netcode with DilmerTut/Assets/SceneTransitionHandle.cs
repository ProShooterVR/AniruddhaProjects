using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using Anirudha.Core.Singletons;

public class SceneTransitionHandler : NetworkBehaviour
{
    [SerializeField]
    public string DefaultMainMenu = "Main";
    private int m_numberOfClientLoaded;

    public static SceneTransitionHandler Instance { get; private set; }

    // Define the ClientLoadedSceneDelegateHandler delegate
    public delegate void ClientLoadedSceneDelegateHandler(ulong clientId);
    [HideInInspector]
    public event ClientLoadedSceneDelegateHandler OnClientLoadedScene;

    public enum SceneStates
    {
        Init,
        Start,
        Lobby,
        Ingame
    }

    private SceneStates m_SceneState;

    public bool InitializeAsHost { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Multiple SceneTransitionHandlers detected. Destroying the new one.");
            Destroy(this);
        }
        else
        {
            Instance = this;
            SetSceneState(SceneStates.Init);
        }
    }

    public void SetSceneState(SceneStates sceneState)
    {
        m_SceneState = sceneState;
    }

    public SceneStates GetCurrentSceneState()
    {
        return m_SceneState;
    }

    public void Initialize()
    {
        if (m_SceneState == SceneStates.Init)
        {
            SceneManager.LoadScene(DefaultMainMenu);
        }
    }

    public void SwitchScene(string scenename)
    {
        if (NetworkManager.Singleton && NetworkManager.Singleton.IsServer && NetworkManager.Singleton.IsListening)
        {
            NetworkManager.Singleton.SceneManager.OnSceneEvent += OnSceneEvent;
            NetworkManager.Singleton.SceneManager.LoadScene(scenename, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(scenename);
        }
    }

    private void OnSceneEvent(SceneEvent sceneEvent)
    {
        if (sceneEvent.SceneEventType != SceneEventType.LoadComplete) return;

        m_numberOfClientLoaded += 1;
        OnClientLoadedScene?.Invoke(sceneEvent.ClientId);
    }

    public void ExitAndLoadStartMenu()
    {
        SetSceneState(SceneStates.Start);
        SceneManager.LoadScene(1);
    }

    public static void OnButtonClick_SwitchScene(string scenename)
    {
        if (Instance != null)
        {
            Instance.SwitchScene(scenename);
        }
    }
}
