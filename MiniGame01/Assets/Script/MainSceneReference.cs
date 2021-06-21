using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainSceneReference : MonoBehaviour
{
    public static MainSceneReference reference;

    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI OpalText;

    public StateMachine stateMachine;

    private void Awake()
    {
        reference = this;
    }

    private void Start()
    {
        // stateMachine.Machine = this;
        // stateMachine.ChangeState("Main");
    }
}