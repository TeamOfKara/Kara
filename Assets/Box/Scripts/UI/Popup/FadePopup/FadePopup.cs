using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BW
{
    public class FadePopup : MonoBehaviour
    {
        [SerializeField] private Image maskImage;
        [SerializeField] private Image maskedImage;
        [SerializeField] private Color maskedImageColor = Color.black;
        [SerializeField, Range(0.1f, 3f)] private float fadeSpeed = 1f;
        private float maskImageMaxWidth;
        private float maskImageMaxHeight;

        private void Awake()
        {
            // 이미지 안가리도록 maskImage 마스크 설정 해 두기
            maskImage.TryGetComponent<RectTransform>(out RectTransform maskImageRect);
            maskImageMaxWidth = maskImageRect.rect.width;
            maskImageMaxHeight = maskImageRect.rect.height;

            // Set maskedImage Color
            maskedImage.color = maskedImageColor;
            
            // UnActive maskImage
            maskImage.gameObject.SetActive(false);
        }

        /// <summary>
        /// Call Fade_Out
        /// </summary>
        /// <param name="fadeAction">Action that Completes Fade Out</param>
        public void FadeOut(Action fadeAction = null) => StartCoroutine(FadeOutCoroutine(fadeAction));

        IEnumerator FadeOutCoroutine(Action fadeAction)
        {
            yield return null;
            maskImage.TryGetComponent<RectTransform>(out RectTransform maskImageRect);
            maskImageRect.sizeDelta = new Vector2(maskImageMaxWidth, maskImageMaxHeight);
            maskImage.gameObject.SetActive(true);

            float currentTime = 0;
            float currentWidth = maskImageMaxWidth;
            float currentHeight = maskImageMaxHeight;

            while(true) {
                yield return null;
                currentTime += Time.deltaTime;
                currentWidth = currentWidth <= 0f ? 0f : Mathf.Lerp(maskImageMaxWidth, 0f, currentTime / fadeSpeed);
                currentHeight = currentHeight <= 0f ? 0f : Mathf.Lerp(maskImageMaxHeight, 0f, currentTime / fadeSpeed);

                maskImageRect.sizeDelta = new Vector2(currentWidth, currentHeight);

                if (currentWidth <= 0f && currentHeight <= 0f) break;
            }
            fadeAction?.Invoke();
        }

        /// <summary>
        /// Call Fade_In
        /// </summary>
        public void FadeIn() => StartCoroutine(FadeInCoroutine());

        IEnumerator FadeInCoroutine()
        {
            yield return null;
            maskImage.TryGetComponent<RectTransform>(out RectTransform maskImageRect);
            maskImageRect.sizeDelta = new Vector2(0, 0);
            maskImage.gameObject.SetActive(true);

            float currentTime = 0;
            float currentWidth = 0;
            float currentHeight = 0;

            while(true) {
                yield return null;
                currentTime += Time.deltaTime;
                currentWidth = currentWidth >= maskImageMaxWidth ? maskImageMaxWidth : Mathf.Lerp(0f, maskImageMaxWidth, currentTime / fadeSpeed);
                currentHeight = currentHeight >= maskImageMaxHeight ? maskImageMaxHeight : Mathf.Lerp(0f, maskImageMaxHeight, currentTime / fadeSpeed);

                maskImageRect.sizeDelta = new Vector2(currentWidth, currentHeight);

                if (currentWidth >= maskImageMaxWidth && currentHeight >= maskImageMaxHeight) break;
            }
            maskImage.gameObject.SetActive(false);
        }
    }
}