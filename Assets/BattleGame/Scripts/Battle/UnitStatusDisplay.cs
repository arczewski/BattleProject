using System.Collections;
using TMPro;
using UnityEngine;

namespace AFSInterview
{
    public class UnitStatusDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMesh;
        private Coroutine fadeTextCoroutine;
        
        public void OnHealthReduced(int health, int value)
        {
            if(fadeTextCoroutine != null)
                StopCoroutine(fadeTextCoroutine);

            ResetColor();
            textMesh.text = $"{value} HP";
            fadeTextCoroutine = StartCoroutine(FadeText());
        }

        private void ResetColor()
        {
            var color = textMesh.color;
            color.a = 1;
            textMesh.color = color;
        }

        private IEnumerator FadeText()
        {
            yield return new WaitForSeconds(1f);
            float elapsedTime = 0f;
            float fadeDuration = 1f;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                var color = textMesh.color;
                color.a = alpha;
                textMesh.color = color;
                yield return null;
            }
        }
    }
}