﻿using System;
using UnityEngine;

public class FragmentManager : MonoBehaviour {

    #region Singleton

    public static FragmentManager instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    public enum FragmentEnum {
        Home,
        Matchmaking,
        Shop,
        Leaderboards
    }

    public enum PopupEnum {
        Settings
    }

    [SerializeField]
    private Camera _camera = null;

    [SerializeField]
    private Animator _transitionAnimator = null;

    [SerializeField]
    private Fragment[] _fragments = null;


    private Fragment _currentFragment;
    private Fragment _nextFragment;
    private string ANIM_IN_TRIGGER = "In";
    private string ANIM_OUT_TRIGGER = "Out";
    private bool _isBusy = false;

    private void Start() {
        _currentFragment = GetFragment(FragmentEnum.Home);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (_currentFragment.FragmentEnum == FragmentEnum.Matchmaking) {
                Open(FragmentEnum.Home);
            } else {
                Open(FragmentEnum.Matchmaking);
            }
        }
    }

    private Fragment GetFragment(FragmentEnum fragmentEnum) {
        for (int ii = 0; ii < _fragments.Length; ii++) {
            if (_fragments[ii].FragmentEnum == fragmentEnum) {
                return _fragments[ii];
            }
        }
        return null;
    }

    private void StartTransitionIn() {
        _transitionAnimator.SetTrigger(ANIM_IN_TRIGGER);
    }

    private void StartTransitionOut() {
        _transitionAnimator.SetTrigger(ANIM_OUT_TRIGGER);
    }

    private void SwitchCurrentFragment() {
        if (_nextFragment == null) {
            Debug.LogError("Next fragment is null!");
            return;
        }

        _currentFragment.Hide();
        _currentFragment = _nextFragment;
        _currentFragment.Show();

        _camera.backgroundColor = _currentFragment.BottomColor;
    }

    public void Open(FragmentEnum fragmentEnum) {
        if (_isBusy) {
            Debug.LogError("Transition is already active!");
            return;
        }
        _nextFragment = GetFragment(fragmentEnum);

        _isBusy = true;
        StartTransitionIn();
    }

    // Called from ScreenTransitionInBehaviour.cs
    public void OnTransitionInFinished() {
        SwitchCurrentFragment();

        StartTransitionOut();
    }

    // Called from ScreenTransitionOutBehaviour.cs
    public void OnTransitionOutFinished() {
        _isBusy = false;
    }

}
