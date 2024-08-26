namespace Features.PlayButton
{
    using Features.Extensions.View;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// PlayButton
    /// </summary>
    public class PlayButton : AbstractButtonView
    {
        protected const string SCENE_NAME = "Loading";

        protected override void OnButtonClick() => SceneManager.LoadScene(SCENE_NAME);
    }
}