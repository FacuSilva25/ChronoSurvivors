using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeCardUI : MonoBehaviour
{
    [Header("Referencias Visuales")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public Image iconImage;
    public Button selectButton;

    // Almacena la referencia a lo que otorga la carta
    private WeaponData assignedWeapon;
    private PassiveItemData assignedPassive;
    private LevelUpUI levelUpManager;

    public void SetupWeapon(WeaponData weapon, LevelUpUI manager, bool isUpgrade, int targetLevel)
    {
        assignedWeapon = weapon;
        assignedPassive = null;
        levelUpManager = manager;

        titleText.text = isUpgrade ? $"{weapon.weaponName} (Nvl. {targetLevel})" : $"{weapon.weaponName} (NUEVA)";
        descriptionText.text = weapon.baseStats.description;

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(SelectThisOption);
    }

    public void SetupPassive(PassiveItemData passive, LevelUpUI manager)
    {
        assignedPassive = passive;
        assignedWeapon = null;
        levelUpManager = manager;

        titleText.text = $"{passive.itemName} (Pasiva)";
        descriptionText.text = passive.description;

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(SelectThisOption);
    }

    void SelectThisOption()
    {
        if (assignedWeapon != null)
        {
            levelUpManager.ApplyWeaponSelection(assignedWeapon);
        }
        else if (assignedPassive != null)
        {
            levelUpManager.ApplyPassiveSelection(assignedPassive);
        }
    }
}