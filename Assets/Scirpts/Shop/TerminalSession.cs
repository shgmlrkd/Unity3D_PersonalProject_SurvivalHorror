using UnityEngine;
using UnityEngine.InputSystem;

public class TerminalSession : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    [SerializeField]
    private Terminal terminal;

    private CameraFocus cameraFocus;

    [SerializeField]
    private PlayerControlLock playerControlLock;

    [SerializeField]
    private GameObject terminalUI;

    private bool isUsing;
    private bool isTransitioning;

    private void Awake()
    {
        cameraFocus = Camera.main.GetComponent<CameraFocus>();

        if(playerControlLock == null)
        {
            playerControlLock = GameObject.FindGameObjectWithTag(PLAYER_TAG).GetComponent<PlayerControlLock>();
        }
    }

    private void OnEnable()
    {
        terminal.OnEntered += EnterTerminal;
    }

    private void OnDisable()
    {
        terminal.OnEntered -= EnterTerminal;
    }

    private void Update()
    {
        if (!isUsing || isTransitioning)
        {
            return;
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ExitTerminal();
        }
    }

    private void EnterTerminal()
    {
        if (isUsing || isTransitioning)
        {
            return;
        }

        isUsing = true;
        isTransitioning = true;

        cameraFocus.SaveCurrentTransform();

        playerControlLock.Lock();
        //terminalUI.SetActive(true);

        cameraFocus.MoveTo(terminal.ViewPoint, CompleteEnter);
    }

    private void CompleteEnter()
    {
        isTransitioning = false;
    }

    private void ExitTerminal()
    {
        if (!isUsing || isTransitioning)
        {
            return;
        }

        isTransitioning = true;

        terminal.Exit();
        //terminalUI.SetActive(false);

        cameraFocus.Restore(CompleteExit);
    }

    private void CompleteExit()
    {
        isUsing = false;
        isTransitioning = false;

        playerControlLock.Unlock();
    }
}