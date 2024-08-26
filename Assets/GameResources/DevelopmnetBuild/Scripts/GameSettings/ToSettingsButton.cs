namespace Features.ToSettingsButton
{
    using Features.Extensions.View;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// ToSettingsButton
    /// </summary>
    public class ToSettingsButton : AbstractButtonView
    {
        protected const string SCENE_NAME = "Settings";

        protected override void OnButtonClick() => SceneManager.LoadScene(SCENE_NAME);
    }
}