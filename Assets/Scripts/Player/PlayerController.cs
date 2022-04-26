using DG.Tweening;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Singleton;

    public ObservableCollection<Runes> currentSpell = new ObservableCollection<Runes>();

    public PlayerData data;

    [SerializeField] Button castBtn;
    [Space()]
    [Header("Mana")]
    [SerializeField] Image manaFill;
    [SerializeField] Image manaUsage;
    [Header("HP")]
    [SerializeField] Image hpBar;

    [Header("Target system")]
    public GameObject[] targets;

    public Color usingColor = new Color(29, 80, 168, 1f);
    public Color redColor = new Color(168, 29, 39, 1f);

    int mana;
    int hp;

    int target;

    //----------------------------------
    private void Start()
    {
        Singleton = this;
        data = SaveSystem.LoadData();
        if (data == null)
        {
            data = new PlayerData();
        }

        hp = data.maxHP;
        mana = data.maxMana;

        currentSpell.CollectionChanged += SpellChanged;
    }

    public void SpellChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        manaUsage.fillAmount = 0;
        foreach (Runes rune in currentSpell)
        {
            manaUsage.fillAmount += (float)Rune.GetRune(rune).cost / (float)data.maxMana;
            if (!EnoughtMana())
                manaUsage.color = redColor;
            else
                manaUsage.color = usingColor;
        }
    }

    public void Cast()
    {
        //Safety mode
        if (currentSpell.Count == 0)
            return;

        if (!TrySpendMana())
            return;


        foreach (Spell spell in Spell.Spells.Values)
        {
            if (currentSpell.SequenceEqual(spell.spellRunes))
            {
                if (spell is SpellSummon)
                    (spell as SpellSummon).Cast(true);
                else if (spell is SpellTarget)
                {
                    target = -1;
                    StartCoroutine(SelectTarget(spell));
                }
                Casted();
                return;
            }
        }
        Casted();
        Debug.Log("No such spell");
    }

    private IEnumerator SelectTarget(Spell spell)
    {
        foreach (GameObject go in targets)
            go.SetActive(true);

        while (target<0)
            yield return null;

        (spell as SpellTarget).Cast(true, target);

        foreach (GameObject go in targets)
            go.SetActive(false);
    }

    public void TargetSelected(int number)
    {
        target = number;
    }

    public void Casted()
    {
        currentSpell.Clear();
        RuneBook.Singleton.ClearRunes();
        castBtn.interactable = false;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        hpBar.DOFillAmount((100f / data.maxHP * hp) / 100f, 1f);

        if (hp <= 0)
        {
            hp = 0;
            Gameplay.Singleton.Lose();
        }
        Debug.Log($"Now hp is {hp}");
    }

    public void CanCast(bool active = true)
    {
        castBtn.interactable = active;
    }

    //Mana
    public void RegenMana()
    {
        mana += data.manaRegen;
        UpdateManaUI();
    }
    
    private bool TrySpendMana()
    {
        int manacost = 0;
        foreach (Runes rune in currentSpell)
        {
            manacost += Rune.GetRune(rune).cost;
        }
        if (manacost > mana)
            return false; // not enought mana
        else
        {
            mana -= manacost;
            UpdateManaUI();
            return true;
        }
    }

    private bool EnoughtMana()
    {
        int manacost = 0;
        foreach (Runes rune in currentSpell)
        {
            manacost += Rune.GetRune(rune).cost;
        }
        if (manacost > mana)
            return false; // not enought mana
        else
        {
            return true;
        }
    }

    private void UpdateManaUI()
    {
        manaFill.fillAmount = (float)mana / (float)data.maxMana;
    }
}
