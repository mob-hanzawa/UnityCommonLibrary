﻿using UnityCommonLibrary.Time;
using UnityCommonLibrary.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace UnityCommonLibrary {
    public class SimpleSpriteAnimation : MonoBehaviour {
        [SerializeField]
        private TimeMode timeMode;
        [SerializeField]
        private float fps = 30f;
        [SerializeField]
        private bool loop;
        [SerializeField]
        [HideInInspector]
        private Sprite[] frames;

        private UTimer timer = new UTimer();
        private SpriteRenderer spriteRenderer;
        private Image img;
        private int index;

        private void Awake() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            img = GetComponent<Image>();
            timer.Restart();
        }

        private void Update() {
            timer.duration = fps == 0f ? 0f : 1f / fps;
            if(!timer.Tick()) {
                return;
            }

            if(!GotoIndex(index)) {
                if(loop) {
                    index = 0;
                }
                return;
            }
            index++;
            timer.Restart();
        }

        public void PlayOneShot() {
            enabled = true;
            index = 1;
            timer.Restart();
            GotoIndex(0);
            loop = false;
        }

        public bool GotoIndex(int index) {
            if(index >= frames.Length || index < 0) {
                return false;
            }
            if(spriteRenderer != null) {
                spriteRenderer.sprite = frames[index];
            }
            if(img != null) {
                img.sprite = frames[index];
            }
            this.index = index;
            return true;
        }
    }
}