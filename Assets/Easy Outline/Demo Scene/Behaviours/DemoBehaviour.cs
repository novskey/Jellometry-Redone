using Assets.Easy_Outline.Behaviours;
using UnityEngine;

namespace Assets.Easy_Outline.Demo_Scene.Behaviours
{
    public class DemoBehaviour : MonoBehaviour {

        public OutlineSystem OutlineSystem;
        public GameObject TargetGameobject;
	
        void Update () {
            //Rotate object
            TargetGameobject.transform.Rotate(0f, Time.deltaTime * 30f, 0f);
            //Bob object
            TargetGameobject.transform.position = new Vector3(0f, Mathf.Sin(Time.time) * 0.15f);
        }

        public void ChangeOutlineColourR(float val)
        {
            OutlineSystem.OutlineColor.r = val;
        }

        public void ChangeOutlineColourG(float val)
        {
            OutlineSystem.OutlineColor.g = val;
        }

        public void ChangeOutlineColourB(float val)
        {
            OutlineSystem.OutlineColor.b = val;
        }

        public void ChangeOutlineSize(float val)
        {
            OutlineSystem.OutlineSize = val;
        }

        public void ToggleMode(bool solid)
        {
            OutlineSystem.SolidOutline = solid;
        }
    }
}
