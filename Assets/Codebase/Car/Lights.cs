using UnityEngine;

public class Lights : MonoBehaviour
{
   [SerializeField] private GameObject _lightsObject;
   [SerializeField] private Material _lightsMaterial;

   public void FireLights(bool isActive)
   {
      
      _lightsObject.SetActive(isActive);
      if (isActive)
      {
         _lightsMaterial.EnableKeyword("_EMISSION");
      }
      else
      {
         _lightsMaterial.DisableKeyword("_EMISSION");
      }
   }
}
