using System.Collections;
using TMPro;
using UnityEngine;

//Script attached to the Canvas
namespace Test.Scripts
{
    public class Subtitle : MonoBehaviour
    {
        [Header("Set Text To Display")]
        [SerializeField] private string subtitleText;
        [SerializeField] private string authorText;
        
        [Header("Set Timer")]
        [SerializeField] private float timeOnScene;
        
        //Custom Classes References
        [SerializeField] DisplayText display = new DisplayText();
        TextFading _fading = new TextFading();
        
        private void Start()
        {
           SetSubtitle();
           SetSubtitleWithFade();
        }

        private void SetSubtitle() => display.SetTitles(subtitleText,authorText);

        private void SetSubtitleWithFade()
        {
            StartCoroutine(_fading.StartFade(display.subtitle, display.author, timeOnScene));
        }
    }

    [System.Serializable]
    class DisplayText
    {
        [SerializeField] internal TextMeshProUGUI subtitle;
        [SerializeField] internal TextMeshProUGUI author;
        
        internal void SetTitles(string subtitleText, string authorText)
        {
            subtitle.text = $"\"{subtitleText}\"";
            author.text = $"- {authorText}";
        } 
    }

    class TextFading
    {
        internal IEnumerator StartFade(TextMeshProUGUI subTitle,TextMeshProUGUI authorTitle,float timeOnScreen)
        {
            yield return new WaitForSeconds(timeOnScreen);
            subTitle.gameObject.SetActive(false);
            authorTitle.gameObject.SetActive(false);
        }
    }
}