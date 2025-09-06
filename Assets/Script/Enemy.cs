using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Enemy : MonoBehaviour
{
    public Enemys enemys;
    private Animator animator;
    private SpriteAnchor anchor;
    public Slider sl;
    public TextMeshProUGUI ugui;
    public TextMeshProUGUI uguiMove;
    public int hp;
    public int attack;
    public System.Action onKill;
    public int move;
    public You you;
    public BattleChecker bc;
    public Rules rules;
    public bool isKilled;
    void Start()
    {
        animator = GetComponent<Animator>();
        anchor = GetComponent<SpriteAnchor>();
        EnemyChanger();
        UIChanger();
        sl.maxValue = hp;
        sl.value = sl.maxValue;
        bc.onEnemyStartAttack += TakeDamage;
        i = move;
        rules.onStartOfBattle += OnStart;
    }
    public void OnStart()
    {
        isKilled = false;
    }
    void UIChanger()
    {
        sl.value = hp;
        ugui.text = hp.ToString();
        uguiMove.text = i.ToString();
    }
    [HideInInspector]
   public int i;
    public void TakeDamage(int damage)
    {
        sl.value -= damage;
        int n = hp - damage;
        ugui.text = n.ToString();
        hp -= damage;
        UIChanger();
        if (hp <= 0)
        {
            sl.value = 0;
            ugui.text  = "0";
            onKill?.Invoke();
            Death();
        }
    }
    public MapGenerator map;
    int money;
    public Rules rule;
    void Death()
    {
        StartCoroutine(N());
    }
    IEnumerator N()
    {
        animator.SetTrigger("death");
        isKilled = true;
        yield return new WaitForSeconds(1);
        map.gameObject.SetActive(true);
        rule.coins += money;
    }
    IEnumerator AmimateMove()
    {
        animator.SetTrigger("atk");
        yield return new WaitForSeconds(1);
        you.TakeDamage(attack);
        i = move;
    }
    public System.Action onAttackEnd;
    public bool freeze;
    public void Attack()
    {
        if (freeze) return;
        i--;
        UIChanger();
        if (i == 0)
        {
            StartCoroutine(AmimateMove());
        }
        onAttackEnd?.Invoke();
    }
    public void EnemyChanger()
    {
        string str = enemys.ToString();
        anchor.sr.color = new(255, 255, 255);
        anchor.useAutoFit = false;
        RuntimeAnimatorController run = Resources.Load<RuntimeAnimatorController>(str);
        animator.runtimeAnimatorController = run;
        float yPos = default;
        float xScale = default;
        float yScale = default;
        float yRot = default;
        switch (str)
        {
            case "Elf":
                yPos = 2.29f;
                yRot = 180;
                xScale = 5;
                yScale = 5;
                attack = 25;
                hp = 390;
                move = 2;
                break;
            case "FireKnight":
                yPos = 2.29f;
                yRot = 180;
                xScale = 5;
                yScale = 5;
                hp = 450;
                attack = 40;
                move = 2;
                break;
            case "Goblin":
                yPos = 0;
                yRot = 180;
                xScale = 5;
                yScale = 5;
                hp = 80;
                attack = 15;
                move = 2;
                break;
            case "Golem":
                yPos = 0.32f;
                yRot = 180;
                xScale = 5;
                yScale = 5; hp = 200;
                attack = 50;
                move = 4;
                break;
            case "Hero":
                yPos = 0;
                yRot = 180;
                xScale = 5;
                yScale = 5;
                attack = 35;
                move=1;
                break;
            case "Knight":
                yPos = 0;
                yRot = 180;
                xScale = 5;
                yScale = 5; hp = 180;
                attack = 35;
                move = 2;
                break;
            case "Last":
                yPos = 0.69f;
                yRot = 180;
                xScale = 0.65f;
                yScale = 0.65f;
                hp = 5000;
                attack = 100;
                move = 2;
                break;
            case "Mashroom":
                yPos = -0.07f;
                yRot = 0;
                xScale = 6.5f;
                yScale = 5f; hp = 150;
                attack = 30;
                move = 3;
                break;
            case "Necr":
                yPos = 0.48f;
                yRot = 0;
                xScale = 3;
                yScale = 3;
                hp = 400;
                attack = 45;
                move = 4;
                break;
            case "Prince":
                yPos = -0.15f;
                yRot = 180;
                xScale = 5;
                yScale = 5;
                hp = 150;
                attack = 28;
                move = 2;
                break;
            case "POP":
                yPos = 2.25f;
                yRot = 180;
                xScale = 5;
                yScale = 5; hp = 380;
                attack = 50;
                move = 3;
                break;
            case "Samurai":
                yPos = -0.09f;
                yRot = 180;
                xScale = 5;
                yScale = 5; hp = 170;
                attack = 33;
                move = 1;
                break;
            case "Skelet":
                yPos = -0.12f;
                yRot = 180;
                xScale = 5;
                yScale = 5;
                hp = 200;
                attack = 40;
                move = 2;
                break;
            case "Stand":
                yPos = 0.17f;
                yRot = 0;
                xScale = 5;
                yScale = 5;
                hp = 490;
                attack = 69;
                move = 3;
                break;
            case "Valkyrie":
                yPos = 0.13f;
                yRot = 180;
                xScale = 5;
                yScale = 5;
                hp = 470;
                attack = 24;
                move = 2;
                break;
        }
        transform.position = new(transform.position.x, yPos, transform.position.z);
        transform.localScale = new(xScale, yScale, 1);
        transform.eulerAngles = new(0, yRot, 0);
        i = move;
        UIChanger();
        StartCoroutine(Next());
    }
    IEnumerator Next()
    {
        yield return new WaitForSeconds(0.1f);
        anchor.useAutoFit = true;
    }
    public void NeNorm()
    {
        Enemys[] allowed = {
        Enemys.Elf,
        Enemys.FireKnight,
        Enemys.Necr,
        Enemys.POP,
        Enemys.Stand,
        Enemys.Valkyrie
    };
        System.Random rnd = new();
        Enemys enemy = allowed[rnd.Next(allowed.Length)];
        enemys = enemy;
        money = 50;
        EnemyChanger();
    }
    public void Norm()
    {
        Enemys[] allowed = {
        Enemys.Golem,
        Enemys.Goblin,
        Enemys.Hero,
        Enemys.Prince,
        Enemys.Samurai,
        Enemys.Skelet,
        Enemys.Knight,
        Enemys.Mashroom
    };
        System.Random rnd = new();
        Enemys enemy = allowed[rnd.Next(allowed.Length)];
        enemys = enemy;
        money = 25;
        EnemyChanger();
    }

    public void Boss()
    {
        enemys = Enemys.Last;
        EnemyChanger();
    }
}
public enum Enemys
{
    Elf,
    FireKnight,
    Goblin,
    Golem,
    Hero,
    Knight,
    Last,
    Mashroom,
    Necr,
    Prince,
    POP,
    Samurai,
    Skelet,
    Stand,
    Valkyrie
}