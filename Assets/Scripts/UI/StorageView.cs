using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;


namespace SIS.UI
{
    public abstract class StorageView : MonoBehaviour
    {
        public Slot[] Slots;

        [SerializeField]
        protected UIDocument _document;

        [SerializeField]
        protected StyleSheet _styleSheet;

        protected static VisualElement _ghostIcon;

        protected VisualElement _root;
        protected VisualElement _container;

        IEnumerator Start()
        {
            yield return StartCoroutine(InitializeView());
        }

        public abstract IEnumerator InitializeView(int size = 20);
    }

}
