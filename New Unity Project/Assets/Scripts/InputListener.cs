using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputListener : MonoBehaviour
{
    #region Singleton
    private static InputListener instance;
    public static InputListener Instance { get { return instance; } }
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Debug.LogWarning("More than one instance of InputListener found!");
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    private KeyCode primaryAttackKey = KeyCode.Mouse0;
    private KeyCode secondaryAttackKey = KeyCode.Mouse1;
    private KeyCode interactKey = KeyCode.E;
    private KeyCode menuKey = KeyCode.Escape;
    private KeyCode mobilityKey = KeyCode.LeftShift;
    public float horizontal { get; private set; }
    public float vertical { get; private set; }
    public float horizonalMouse { get; private set; }
    public float verticalMouse { get; private set; }
    public bool primaryAttack { get; private set; }
    public bool secondaryAttack { get; private set; }
    public bool mobility { get; private set; }
    public bool menu { get; private set; }
    public bool interact { get; private set; }

    private void Update(){
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        horizonalMouse = Input.GetAxisRaw("Mouse X");
        verticalMouse = Input.GetAxisRaw("Mouse X");
        mobility = Input.GetKeyDown(mobilityKey);
        menu = Input.GetKeyDown(interactKey);
        interact = Input.GetKeyDown(interactKey);
    }
    
}
