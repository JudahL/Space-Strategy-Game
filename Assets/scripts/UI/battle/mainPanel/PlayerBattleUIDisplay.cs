using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleUIDisplay : MonoBehaviour
{
    public string DamageDisplayFormat;
    public string CritDisplayFormat;
    public string EvadeDisplayFormat;
    public string ArmorDisplayFormat;
    public Text DamageText;
    public Text CritText;
    public Text EvadeText;
    public Text ArmorText;

    private IDefensiveStats _defensiveStats;
    private IOffensiveStats _offensiveStats;

    public void OnSelection(GameObject target)
    {
        _defensiveStats = target.GetComponent<IDefensiveStats>();
        _offensiveStats = target.GetComponent<IOffensiveStats>();

        UpdateText();
    }

    public void UpdateText()
    {
        if (_offensiveStats != null)
        {
            DamageText.text = DamageDisplayFormat.Replace("@", _offensiveStats.Damage);
            CritText.text = CritDisplayFormat.Replace("@", _offensiveStats.Crit);
        } 
        else
        {
            DamageText.text = DEFAULT_STRING;
            CritText.text = DEFAULT_STRING;
        }

        if (_defensiveStats != null)
        {
            EvadeText.text = EvadeDisplayFormat.Replace("@", _defensiveStats.Evade);
            ArmorText.text = ArmorDisplayFormat.Replace("@", _defensiveStats.Armor);
        }
        else
        {
            EvadeText.text = DEFAULT_STRING;
            ArmorText.text = DEFAULT_STRING;
        }
    }

    private static readonly string DEFAULT_STRING = "";
}
