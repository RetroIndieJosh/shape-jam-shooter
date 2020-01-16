using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EightBitsToInfinity
{
    [RequireComponent(typeof(AnimatedSpriteRenderer))]
    public class AnimatedSpriteLoader : MonoBehaviour
    {
        enum ScaleMode
        {
            ScaleToLoadedImage,
            ScaleToSprite
        }

        [SerializeField, Tooltip("Keys for each animation which also serve as directory names.")]
        private List<string> m_animationKeyList = new List<string>();

        // TODO set this per animation
        [SerializeField]
        private int m_framesPerSecond = 20;

        [Header("Paths")]

        [SerializeField, Tooltip("Top-level path for all animations, relative to build's PROJECT_Data/ folder")]
        private string m_relativeTopPath = "";

        [Header("Options")]

        // TODO allow setting this per animation/frame
        [SerializeField,
            Tooltip("Loaded Image: Keep image the same size\n"
            + "Sprite: Scale to match the size set in the editor")]
        private ScaleMode m_scaleMode = ScaleMode.ScaleToLoadedImage;

        private AnimatedSpriteRenderer m_animatedSpriteRenderer = null;

        private void Awake() {
            m_animatedSpriteRenderer = GetComponent<AnimatedSpriteRenderer>();

            foreach (var animKey in m_animationKeyList) {
                var anim = LoadAnimation(animKey);
                m_animatedSpriteRenderer.AddAnimation(animKey, anim);
            }
        }

        private string ConstructPath(string a_key, int a_index) {
            var appDataPath = Application.dataPath;
            return $"{appDataPath}/{m_relativeTopPath}/{a_key}/{a_index}.png";
        }

        private SpriteAnimation LoadAnimation(string a_key) {
            var animation = new SpriteAnimation {
                framesPerSecond = m_framesPerSecond
            };

            // load until we find an index that doesn't have a matching file
            Sprite sprite = null;
            var index = 0;
            do {
                sprite = LoadFrame(a_key, index);
                if (sprite == null)
                    break;
                animation.AddFrame(sprite);
                ++index;
            } while (true);

            Debug.Log($"Loaded {index} sprites for {name} animation {a_key}");
            return animation;
        }

        private Sprite LoadFrame(string a_key, int a_index) {
            var tex = LoadTexture(a_key, a_index);
            if (tex == null)
                return null;
            tex.filterMode = FilterMode.Point;

            var rect = new Rect(0, 0, tex.width, tex.height);
            var sprite = Sprite.Create(tex, rect, Vector2.one * 0.5f);

            // TODO only do this once
            // scale if desired (sprites default to image size)
            if (m_scaleMode == ScaleMode.ScaleToSprite)
                transform.localScale = Vector2.one * 100f / sprite.rect.size;

            return sprite;
        }

        private Texture2D LoadTexture(string a_key, int a_index) {
            var filePath = ConstructPath(a_key, a_index);
            if (File.Exists(filePath) == false)
                return null;

            var data = File.ReadAllBytes(filePath);
            var tex = new Texture2D(2, 2);
            var wasLoaded = tex.LoadImage(data);

            if (wasLoaded == false) {
                Debug.LogError($"Failed to load data for {name} at [{filePath}] (but file exists)");
                return null;
            }

            return tex;
        }
    }
}
