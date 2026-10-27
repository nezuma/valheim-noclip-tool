using UnityEngine;

namespace NoclipTool
{
    public class NoclipMenu : MonoBehaviour
    {
        private Rect _windowRect = new Rect(20, 20, 320, 180);
        private GUIStyle _windowStyle;
        private GUIStyle _labelStyle;
        private bool _stylesInitialized;

        private void OnGUI()
        {
            if (!NoclipController.MenuVisible) return;

            InitializeStyles();

            _windowRect = GUILayout.Window(
                0,
                _windowRect,
                DrawWindow,
                "Noclip Tool",
                _windowStyle
            );
        }

        private void DrawWindow(int windowId)
        {
            GUILayout.Space(5);

            // Чекбокс включения/выключения noclip
            bool newState = GUILayout.Toggle(
                NoclipController.IsNoclipActive,
                "  Режим Noclip (пролёт сквозь стены)",
                GUILayout.Height(30)
            );

            if (newState != NoclipController.IsNoclipActive)
                NoclipController.Toggle();

            GUILayout.Space(10);

            // Ползунок скорости
            GUILayout.Label($"Скорость полёта: x{NoclipController.GetSpeed():F1}", _labelStyle);

            float newSpeed = GUILayout.HorizontalSlider(
                NoclipController.GetSpeed(),
                0.5f,
                10f,
                GUILayout.Height(20)
            );

            if (Mathf.Abs(newSpeed - NoclipController.GetSpeed()) > 0.01f)
                NoclipController.SetSpeed(newSpeed);

            GUILayout.Space(10);

            // Подсказки
            GUILayout.Label("— Управление —", _labelStyle);
            GUILayout.Label("WASD — движение по направлению камеры", _labelStyle);
            GUILayout.Label("Space / Ctrl — вверх / вниз", _labelStyle);
            GUILayout.Label("Колесо мыши — изменить скорость", _labelStyle);

            GUILayout.Space(5);

            if (GUILayout.Button("Закрыть", GUILayout.Height(25)))
                NoclipController.MenuVisible = false;

            GUI.DragWindow(new Rect(0, 0, 10000, 20));
        }

        private void InitializeStyles()
        {
            if (_stylesInitialized) return;

            _windowStyle = new GUIStyle(GUI.skin.window)
            {
                fontSize = 14,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.UpperCenter
            };

            _labelStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 12
            };

            _stylesInitialized = true;
        }
    }
}