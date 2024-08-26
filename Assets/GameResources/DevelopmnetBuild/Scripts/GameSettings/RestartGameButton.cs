namespace Features.RestartGameButton
{
    using Features.Extensions.View;
    using System.IO;
    using UnityEngine;

    /// <summary>
    /// RestartGameButton
    /// </summary>
    public class RestartGameButton : AbstractButtonView
    {
        protected override void OnButtonClick()
        {
            string path = Path.Combine(Application.persistentDataPath, "ConveyorData", "TomatoFerm.json");
            if (File.Exists(path))
            {
                File.Delete(path);
            }    
        }
    }
}