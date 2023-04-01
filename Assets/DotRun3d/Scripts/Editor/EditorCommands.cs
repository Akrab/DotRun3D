using UnityEditor;
using UnityEditor.SceneManagement;

namespace DotRun3D.EditorExt
{

    public class EditorCommands : Editor
    {
        const string PATH_SCENES = "Assets\\DotRun3d\\Scenes\\{0}.unity";
        const string LOBBY_SCENE = "Lobby";
        const string UI_SCENE = "UI";
        const string GAME_SCENE = "Game";

        [MenuItem("Game/Play")]
        static void Play()
        {
            EditorSceneManager.OpenScene(string.Format(PATH_SCENES, LOBBY_SCENE));
            EditorApplication.isPlaying = true;
        }

        [MenuItem("Game/Lobby")]
        static void Lobby()
        {
            EditorSceneManager.OpenScene(string.Format(PATH_SCENES, LOBBY_SCENE));
            EditorApplication.isPlaying = false;
        }

        [MenuItem("Game/UI")]
        static void UI()
        {
            EditorSceneManager.OpenScene(string.Format(PATH_SCENES, UI_SCENE));
            EditorApplication.isPlaying = false;
        }

        [MenuItem("Game/Game")]
        static void Game()
        {
            EditorSceneManager.OpenScene(string.Format(PATH_SCENES, GAME_SCENE));
            EditorApplication.isPlaying = false;
        }
    }

}