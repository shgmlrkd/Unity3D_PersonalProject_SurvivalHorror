using UnityEngine;

public class PlayerControlLock : MonoBehaviour
{
    [SerializeField]
    private PlayerInteract playerInteract;

    [SerializeField]
    private PlayerMove playerMove;

    [SerializeField]
    private CameraMove cameraMove;

    private void Awake()
    {
        if (playerMove == null)
        {
            playerMove = GetComponent<PlayerMove>();
        }

        if(cameraMove == null)
        {
            cameraMove = Camera.main.GetComponent<CameraMove>();
        }

        if(playerInteract == null)
        {
            playerInteract = GetComponentInChildren<PlayerInteract>();
        }
    }

    public void Lock()
    {
        SetEnabled(false);
    }

    public void Unlock()
    {
        SetEnabled(true);
    }

    private void SetEnabled(bool enabled)
    {
        playerInteract.enabled = enabled;
        playerMove.enabled = enabled;
        cameraMove.enabled = enabled;
    }
}