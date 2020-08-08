﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animTestScript : MonoBehaviour {
    Animator m_Animator;


    void Start() {
        m_Animator = GetComponent<Animator>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.J)) {
            m_Animator.Play("Spooked");
            print("Eek!");
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            m_Animator.Play("Laugh");
            print("Hihi!");
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            m_Animator.Play("Hungry");
            print("I'm running out of juice!");
        }
        if (Input.GetKeyDown(KeyCode.H)) {
            m_Animator.Play("Hello");
            print("hey there!");
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            m_Animator.Play("Charged");
            print("Fully charged! :-)");
        }
    }
}
