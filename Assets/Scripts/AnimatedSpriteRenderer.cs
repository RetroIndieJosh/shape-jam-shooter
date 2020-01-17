using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// TODO handle colorization per frame/animation
namespace EightBitsToInfinity
{
    [System.Serializable]
    public class SpriteAnimation
    {
        [SerializeField] public bool loop = true;
        [SerializeField] private List<Sprite> m_frameList = new List<Sprite>();

        [Header("Events")]
        [SerializeField] private UnityEvent m_onFinish = new UnityEvent();
        [SerializeField] private UnityEvent m_onStart = new UnityEvent();

        public Sprite CurrentFrame => m_frameIndex < 0 || m_frameIndex >= m_frameList.Count 
            ? null : m_frameList[m_frameIndex];

        public int framesPerSecond = 30;

        public int FrameCount => m_frameList.Count;
        private float SecPerFrame => 1f / framesPerSecond;

        private int m_frameIndex = 0;
        private float m_timeSinceLastStep = 0f;

        public void AddFrame(Sprite a_frame) {
            m_frameList.Add(a_frame);
        }

        public void AddFinishEvent(UnityAction a_action) {
            m_onFinish.AddListener(a_action);
        }

        public void AddStartEvent(UnityAction a_action) {
            m_onStart.AddListener(a_action);
        }

        public void Step(float a_delta) {
            m_timeSinceLastStep += a_delta;
            if (m_timeSinceLastStep > SecPerFrame) {
                ++m_frameIndex;
                if (m_frameIndex >= m_frameList.Count) {
                    m_onFinish.Invoke();
                    if(loop)
                        m_frameIndex = 0;
                }
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

        public List<string> requiredAnimationList = new List<string>();

        private SpriteAnimation m_activeAnimation = null;
        private Color m_originalColor = Color.white;
        private Sprite m_originalSprite = null;
        private SpriteRenderer m_spriteRenderer = null;

        public void AddAnimation(string a_key, SpriteAnimation a_animation) {
            if (a_animation.FrameCount == 0)
                return;

            var entry = new AnimationEntry() {
                animation = a_animation,
                key = a_key
            };
            m_animationList.Add(entry);
        }

        public SpriteAnimation FindAnimation(string a_key) {
            foreach (var anim in m_animationList) {
                if (KeyMatch(a_key, anim.key))
                    return anim.animation;
            }

            return null;
        }
        private void Revert() {
            m_spriteRenderer.sprite = m_originalSprite;
            m_spriteRenderer.color = m_originalColor;
        }

        public void SetAnimation(string a_key) {
            var anim = FindAnimation(a_key);
            if (anim == null) {
                Revert();
                return;
            }
            m_activeAnimation = anim;
        }

        private void Awake() {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
            m_originalSprite = m_spriteRenderer.sprite;
            m_originalColor = m_spriteRenderer.color;
            m_spriteRenderer.sprite = null;
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

        private bool KeyMatch(string a_one, string a_two) {
            return m_caseSensitive ? a_one == a_two : a_one.ToLower() == a_two.ToLower();
        }
    }
}
