using UnityEngine;
using DG.Tweening;
using Runtime.Extensions;
using UnityEngine.UI;

namespace Runtime.Managers
{
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {
        [Header("Object UI Settings")]
        [SerializeField] private GameObject objectUIPanel;
        [SerializeField] private Button objectCloseButton; 
        [SerializeField] private float scaleDuration = 0.3f;

        private bool isUIActive;

        private void Awake()
        {
            if (objectUIPanel != null)
            {
                objectUIPanel.transform.localScale = Vector3.zero;
                objectUIPanel.SetActive(false);
            }

            if (objectCloseButton != null)
            {
                objectCloseButton.onClick.AddListener(HideUI);
            }
        }

        public void OpenUI()
        {
            if (objectUIPanel != null)
            {
                objectUIPanel.SetActive(true);
                objectUIPanel.transform.DOScale(Vector3.one, scaleDuration).SetEase(Ease.OutBack);
                isUIActive = true;
            }
        }

        public void HideUI()
        {
            if (objectUIPanel != null)
            {
                objectUIPanel.transform.DOScale(Vector3.zero, scaleDuration).SetEase(Ease.InBack).OnComplete(() =>
                {
                    objectUIPanel.SetActive(false);
                });
                isUIActive = false;
            }
        }

        public bool IsUIActive()
        {
            return isUIActive;
        }
    }
}