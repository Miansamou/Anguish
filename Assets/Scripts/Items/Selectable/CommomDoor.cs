using UnityEngine;

public class CommomDoor : IInteractable
{
    #region variables

    [SerializeField]
    private Animator lockedMessage;

    [Header("Open Door")]
    [SerializeField]
    private PlayerItems inventory;
    [SerializeField]
    private string animationName;
    [SerializeField]
    private string soundName;
    [SerializeField]
    private ItemObject keyToOpen;
    [SerializeField]
    private bool locked;
    [SerializeField]
    private string key;

    private LocalizedText localized;
    private Animator animator;

    #endregion

    #region initialization

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    #endregion

    #region actions

    public override void Act()
    {
        LocalizedText localization = textMessage.GetComponent<LocalizedText>();
        localization.SetNewKey(key);

        textMessage.SetActive(true);

        if (player.GetInteractTrigger())
        {
            Acting();
        }
    }

    public override void Acting()
    {
        if (locked)
        {
            if (inventory.FindItemInInventory(keyToOpen.nameItem))
            {
                inventory.ShowUseItemsScreen(LocalizedText.GetTextDeterminatedKey(keyToOpen.nameItem));
                return;
            }
            
            localized = lockedMessage.gameObject.GetComponentInChildren<LocalizedText>();
            localized.SetNewKey("locked");
            localized.UpdateText();
            lockedMessage.SetTrigger("ShowBox");
            return;
        }
        AudioManager.instance.Play(soundName);
        animator.SetTrigger(animationName);
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public override void CancelAct()
    {
        textMessage.SetActive(false);
    }

    public override void UseItem()
    {
        locked = false;
        Acting();
    }

    #endregion
}
