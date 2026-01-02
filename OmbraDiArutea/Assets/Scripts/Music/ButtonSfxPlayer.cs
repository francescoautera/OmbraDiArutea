using UnityEngine.UI;

namespace OmbreDiAretua
{
   
public class ButtonSfxPlayer : SfxPlayer
{
   private Button button;

   // private void Awake()
   // {
   //    button = GetComponentInChildren<Button>();
   //    if (button)
   //    {
   //       button.onClick.AddListener(PlayFx);
   //    }
   // }

   public void AddToListener() => button.onClick.AddListener(PlayFx);
}
}
