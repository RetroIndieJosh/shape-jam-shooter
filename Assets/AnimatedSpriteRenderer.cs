using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightBitsToInfinity
{
    [System.Serializable]
    public class SpriteAnimation
    {
        [SerializeField] private bool m_loop = true;
        [SerializeField] private List<Sprite> m_frameList = new List<Sprite>();

        public Sprite CurrentFrame => m_frameList[m_frameIndex];

        public int m_fps = 30;

        private float SecPerFrame => 1f / m_fps;

        private int m_frameIndex = 0;
        private float m_timeSinceLastStep = 0f;

        public void Step(float a_delta) {
            m_timeSinceLastStep += a_delta;
            if (m_timeSinceLastStep > SecPerFrame) {
                ++m_frameIndex;
                if (m_frameIndex >= m_frameList.Count && m_loop)
                    m_frameIndex = 0;
                m_timeSinceLastStep = 0f;
            }
        }
    }


    [System.Serializable]
    public class AnimationEntry
    {
        public string key;
        public SpriteAnimation animation;
    }


    [RequireComponent(typeof(SpriteRenderer))]
    public class AnimatedSpriteRenderer : MonoBehaviour
    {
        [Header("Animations")]
        [SerializeField] private List<AnimationEntry> m_animationList = new List<AnimationEntry>();
        [SerializeField] private string m_initialAnimation = "";

        [Header("Options")]
        [SerializeField] private bool m_caseSensitive = true;

        private Sprite CurrentSprite {
            get => m_spriteRenderer.sprite;
            set => m_spriteRenderer.sprite = value;
        }

        private SpriteAnimation m_activeAnimation = null;
        private SpriteRenderer m_spriteRenderer = null;

        public void SetAnimation(string a_key) {
            var anim = FindAnimation(a_key);
            if (anim == null)
                return;
            m_activeAnimation = anim;
        }

        private void Awake() {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start() {
            if (string.IsNullOrEmpty(m_initialAnimation) == false)
                SetAnimation(m_initialAnimation);
        }

        private void Update() {
            if (m_activeAnimation == null)
                return;
            m_activeAnimation.Step(Time.deltaTime);
            m_spriteRenderer.sprite = m_activeAnimation.CurrentFrame;
        }

        private SpriteAnimation FindAnimation(string a_key) {
            foreach (var anim in m_animationList)
                if (KeyMatch(a_key, anim.key))
                    return anim.animation;
            return null;
        }

        private bool KeyMatch(string a_one, string a_two) {
            return m_caseSensitive ? a_one == a_two : a_one.ToLower() == a_two.ToLower();
        }
    }
}
