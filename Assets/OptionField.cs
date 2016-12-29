using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets
{
    public class OptionField : InputField
    {

        public MyType Type;

        private bool _selected;

        void Start()
        {
            if (transform.GetChild(1).name == "Name")
            {
                Type = MyType.String;
            }
            else
            {
                Type = MyType.KeyCode;
            }

        }

        void OnFocus()
        {

        }

        void Update()
        {
            if (_selected)
            {
                if (Input.anyKeyDown)
                {
                    KeyCode key = KeyCode.A;
                    foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
                    {
                        if (Input.GetKeyDown(kcode))
                        {
                            key = kcode;
                            Debug.Log("KeyCode down: " + kcode);
                            break;
                        }
                    }

                    text = key.ToString();
                    caretPosition = 0;
                    interactable = false;
                    _selected = false;
                }
            }
        }
        /// <summary>
        ///   <para>What to do w    hen the event system sends a pointer click Event.</para>
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnPointerClick(PointerEventData eventData)
        {
            interactable = true;

            base.OnPointerClick(eventData);
            Debug.Log("pressed me!");
            _selected = true;

        }


        public enum MyType
        {
            String,
            KeyCode
        }



    }
}
