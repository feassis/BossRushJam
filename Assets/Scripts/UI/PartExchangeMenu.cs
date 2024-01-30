using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PartExchangeMenu : MonoBehaviour
{
    [SerializeField] private MechaPartSlot playerTorsoPart;
    [SerializeField] private MechaPartSlot playerLeftArmPart;
    [SerializeField] private MechaPartSlot playerRightArmPart;
    [SerializeField] private MechaPartSlot playerLegPart;

    [SerializeField] private MechaPartSlot bossTorsoPart;
    [SerializeField] private MechaPartSlot bossLeftArmPart;
    [SerializeField] private MechaPartSlot bossRightArmPart;
    [SerializeField] private MechaPartSlot bossMiddleArmPart;
    [SerializeField] private MechaPartSlot bossLegPart;
    [SerializeField] private Image selectedImage;

    [SerializeField] private Image descriptionImage;
    [SerializeField] private TextMeshProUGUI descriptionName;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI mpText;
    [SerializeField] private TextMeshProUGUI mpresText;
    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI defText;
    [SerializeField] private TextMeshProUGUI intText;
    [SerializeField] private TextMeshProUGUI wisText;
    [SerializeField] private TextMeshProUGUI spdText;

    [SerializeField] private string nextScene = "TestScene";

    private MechaPartSlot selectedSlot;

    private static MechaTorso bossTorso;
    private static List<MechaArms> bossArms = new List<MechaArms>();
    private static MechaLegs playerLegs;
    private static MechaLegs bossLegs;

    

    public static void OpenPartExchangeMenu( MechaTorso bossTorso,
        List<MechaArms> bossArms, MechaLegs bossLegs)
    {
        PartExchangeMenu.bossTorso = bossTorso;
        PartExchangeMenu.bossArms.AddRange(bossArms);
        PartExchangeMenu.bossLegs = bossLegs;

        SceneManager.LoadScene("BossTradeParts");
    }

    private void ClearStatText()
    {
        hpText.text = "";
        mpText.text = "";
        atkText.text = "";
        defText.text = "";
        intText.text = "";
        wisText.text = "";
        spdText.text = "";
        mpresText.text = "";
    }

    private void SetupText(Stat stat, float amount)
    {
        switch (stat)
        {
            case Stat.HP:
                hpText.text = amount.ToString();
                break;
            case Stat.MP:
                mpText.text = amount.ToString();
                break;
            case Stat.ATK:
                atkText.text = amount.ToString();
                break;
            case Stat.DEF:
                defText.text = amount.ToString();
                break;
            case Stat.INT:
                intText.text = amount.ToString();
                break;
            case Stat.WIS:
                wisText.text = amount.ToString();
                break;
            case Stat.SPD:
                spdText.text = amount.ToString();
                break;
            case Stat.MPRES:
                mpresText.text = amount.ToString();
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        SetupPlayerSlots();

        SetupBossSlots();

        ClearStatText();
    }

    private void SetupBossSlots()
    {
        MechaBodyPart bossTPart = MechaPartService.Instance.GetMechaBody(bossTorso);
        bossTorsoPart.OnPointerDownAction += OnSlotClicked;
        bossTorsoPart.OnDragEndAction += OnSlotReleased;
        bossTorsoPart.Icon.sprite = bossTPart.GetSprite();
        bossTorsoPart.torso = bossTorso;

        var bossLArm = MechaPartService.Instance.GetArmPart(bossArms[0]);
        bossLeftArmPart.OnPointerDownAction += OnSlotClicked;
        bossLeftArmPart.OnDragEndAction += OnSlotReleased;
        bossLeftArmPart.Icon.sprite = bossLArm.GetSprite();
        bossLeftArmPart.arm = bossArms[0];

        var bossRArm = MechaPartService.Instance.GetArmPart(bossArms[1]);
        bossRightArmPart.OnPointerDownAction += OnSlotClicked;
        bossRightArmPart.OnDragEndAction += OnSlotReleased;
        bossRightArmPart.Icon.sprite = bossRArm.GetSprite();
        bossRightArmPart.arm = bossArms[1];

        if (bossArms.Count > 2)
        {
            var bossMArm = MechaPartService.Instance.GetArmPart(bossArms[2]);
            bossMiddleArmPart.OnPointerDownAction += OnSlotClicked;
            bossMiddleArmPart.OnDragEndAction += OnSlotReleased;
            bossMiddleArmPart.Icon.sprite = bossMArm.GetSprite();
            bossMiddleArmPart.arm = bossArms[2];
        }
        else
        {
            bossMiddleArmPart.gameObject.SetActive(false);
        }

        var bossL = MechaPartService.Instance.GetMechaLeg(bossLegs);
        bossLegPart.OnPointerDownAction += OnSlotClicked;
        bossLegPart.OnDragEndAction += OnSlotReleased;
        bossLegPart.Icon.sprite = bossL.GetSprite();
        bossLegPart.leg = bossLegs;
    }

    private void SetupPlayerSlots()
    {
        var playerProfile = PlayerProfile.Instance;

        MechaBodyPart playerTorso = MechaPartService.Instance.GetMechaBody(playerProfile.torso);
        playerTorsoPart.OnPointerDownAction += OnSlotClicked;
        playerTorsoPart.OnDragEndAction += OnSlotReleased;
        playerTorsoPart.Icon.sprite = playerTorso.GetSprite();
        playerTorsoPart.torso = playerProfile.torso;
        playerTorsoPart.isPlayer = true;

        var playerLeftArm = MechaPartService.Instance.GetArmPart(playerProfile.leftArm);
        playerLeftArmPart.OnPointerDownAction += OnSlotClicked;
        playerLeftArmPart.OnDragEndAction += OnSlotReleased;
        playerLeftArmPart.Icon.sprite = playerLeftArm.GetSprite();
        playerLeftArmPart.arm = playerProfile.leftArm;
        playerLeftArmPart.isPlayer = true;

        var playerRightArm = MechaPartService.Instance.GetArmPart(playerProfile.rightArm);
        playerRightArmPart.OnPointerDownAction += OnSlotClicked;
        playerRightArmPart.OnDragEndAction += OnSlotReleased;
        playerRightArmPart.Icon.sprite = playerRightArm.GetSprite();
        playerRightArmPart.arm = playerProfile.rightArm;
        playerRightArmPart.isPlayer = true;

        var playerLeg = MechaPartService.Instance.GetMechaLeg(playerProfile.leg);
        playerLegPart.OnPointerDownAction += OnSlotClicked;
        playerLegPart.OnDragEndAction += OnSlotReleased;
        playerLegPart.Icon.sprite = playerLeg.GetSprite();
        playerLegPart.leg = playerProfile.leg;
        playerLegPart.isPlayer = true;
    }

    public void OnSlotClicked(MechaPartSlot slot)
    {
        selectedSlot = slot;
        selectedImage.gameObject.SetActive(true);
        selectedImage.sprite = slot.Icon.sprite;

        MechaPart part = MechaPartService.Instance.GetMechaLeg(PlayerProfile.Instance.leg);

        switch (slot.type)
        {
            case MechaSlotType.Torso:
                part = MechaPartService.Instance.GetMechaBody(slot.torso);
                break;
            case MechaSlotType.LeftArm:
                part = MechaPartService.Instance.GetArmPart(slot.arm);
                break;
            case MechaSlotType.RightArm:
                part = MechaPartService.Instance.GetArmPart(slot.arm);
                break;
            case MechaSlotType.MiddleArm:
                part = MechaPartService.Instance.GetArmPart(slot.arm);
                break;
            case MechaSlotType.Leg:
                part = MechaPartService.Instance.GetMechaLeg(slot.leg);
                break;
            default:
                break;
        }
        
        ClearStatText();

        descriptionImage.sprite = part.GetSprite();
        descriptionName.text = part.GetName();

        foreach (var s in part.GetPartStatus())
        {
            SetupText(s.Type, s.Amount);
        }
    }

    public void OnSlotReleased()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Get the pointer event data
            PointerEventData eventData = new PointerEventData(EventSystem.current);

            // Set the position of the pointer event data to the current mouse position
            eventData.position = Input.mousePosition;

            // Raycast to determine which UI element is being hovered over
            List<RaycastResult> raycastResult = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResult);

            foreach (var ray in raycastResult)
            {
                if (ray.gameObject.TryGetComponent<MechaPartSlot>(out MechaPartSlot mecha))
                {
                    if(mecha == selectedSlot)
                    {
                        continue;
                    }

                    if (mecha.isPlayer == selectedSlot.isPlayer)
                    {
                        continue;
                    }

                    if (!mecha.isPlayer)
                    {
                        continue;
                    }

                    if (mecha.type == selectedSlot.type)
                    {
                        if(mecha.type == MechaSlotType.Torso)
                        {
                            mecha.torso = selectedSlot.torso;
                            mecha.Icon.sprite = MechaPartService.Instance.GetMechaBody(mecha.torso).GetSprite();
                            PlayerProfile.Instance.torso = mecha.torso;
                            GoToNextScene();
                        }

                        if (mecha.type == MechaSlotType.LeftArm ||
                            mecha.type == MechaSlotType.RightArm)
                        {
                            mecha.arm = selectedSlot.arm;
                            mecha.Icon.sprite = MechaPartService.Instance.GetArmPart(mecha.arm).GetSprite();
                            
                            if(mecha.type == MechaSlotType.RightArm)
                            {
                                PlayerProfile.Instance.rightArm = mecha.arm;
                            }
                            else
                            {
                                PlayerProfile.Instance.leftArm = mecha.arm;
                            }
                            GoToNextScene();
                        }

                        if (mecha.type == MechaSlotType.Leg)
                        {
                            mecha.leg = selectedSlot.leg;
                            mecha.Icon.sprite = MechaPartService.Instance.GetMechaLeg(mecha.leg).GetSprite();
                            PlayerProfile.Instance.leg = mecha.leg;
                            GoToNextScene();
                        }
                    }

                    if ((   mecha.type == MechaSlotType.LeftArm ||
                            mecha.type == MechaSlotType.RightArm)
                            &&
                            (selectedSlot.type == MechaSlotType.MiddleArm ||
                            selectedSlot.type == MechaSlotType.LeftArm ||
                            selectedSlot.type == MechaSlotType.RightArm))
                    {
                        mecha.arm = selectedSlot.arm;
                        mecha.Icon.sprite = MechaPartService.Instance.GetArmPart(mecha.arm).GetSprite();

                        if (mecha.type == MechaSlotType.RightArm)
                        {
                            PlayerProfile.Instance.rightArm = mecha.arm;
                        }
                        else
                        {
                            PlayerProfile.Instance.leftArm = mecha.arm;
                        }
                        GoToNextScene();
                    }
                }
            }

            selectedSlot = null;
            selectedImage.gameObject.SetActive(false);
        }
    }

    private void GoToNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    private void Update()
    {
        selectedImage.rectTransform.position = Mouse.current.position.ReadValue();
    }
}
