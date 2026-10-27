using HarmonyLib;
using UnityEngine;

namespace NoclipTool
{
    public static class NoclipController
    {
        public static bool IsNoclipActive { get; private set; }
        public static bool MenuVisible { get; set; }

        private static float _speedMultiplier = 2f;
        private static Rigidbody _playerRigidbody;

        // Сохранённое состояние
        private static bool _savedUseGravity;
        private static bool _savedGodMode;
        private static bool _stateSaved;

        public static void SetSpeed(float value) => _speedMultiplier = value;
        public static float GetSpeed() => _speedMultiplier;

        public static void Toggle()
        {
            IsNoclipActive = !IsNoclipActive;

            var player = Player.m_localPlayer;
            if (player == null) return;

            if (IsNoclipActive)
            {
                if (!_stateSaved)
                {
                    _playerRigidbody = Traverse.Create(player)
                        .Field("m_body").GetValue<Rigidbody>();
                    
                    if (_playerRigidbody != null)
                    {
                        _savedUseGravity = _playerRigidbody.useGravity;
                        _playerRigidbody.useGravity = false;
                    }

                    // Запоминаем и включаем бессмертие
                    _savedGodMode = player.InGodMode();
                    player.SetGodMode(true);
                    _stateSaved = true;
                }
            }
            else
            {
                if (_stateSaved)
                {
                    // Восстанавливаем всё как было
                    if (_playerRigidbody != null)
                    {
                        _playerRigidbody.useGravity = _savedUseGravity;
                        _playerRigidbody.linearVelocity = Vector3.zero; // Сбрасываем скорость
                    }

                    player.SetGodMode(_savedGodMode); // Возвращаем бессмертие в исходное состояние
                    _stateSaved = false;
                }
            }

            NoclipToolPlugin.Log.LogInfo($"Noclip: {(IsNoclipActive ? "ON" : "OFF")}");
        }

        public static void UpdateNoclip(Player player)
        {
            if (player != Player.m_localPlayer) return;
            if (!IsNoclipActive) return;

            if (_playerRigidbody == null)
                _playerRigidbody = Traverse.Create(player)
                    .Field("m_body").GetValue<Rigidbody>();

            if (_playerRigidbody == null) return;

            Transform cam = GameCamera.instance != null
                ? GameCamera.instance.transform
                : null;
            if (cam == null) return;

            Vector3 moveDir = cam.forward * Input.GetAxis("Vertical") +
                              cam.right   * Input.GetAxis("Horizontal");

            if (moveDir.sqrMagnitude > 1f)
                moveDir.Normalize();

            if (Input.GetKey(KeyCode.Space))
                moveDir.y += 1f;
            else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                moveDir.y -= 1f;

            float speed = player.m_runSpeed * _speedMultiplier;
            _playerRigidbody.linearVelocity = moveDir * speed;

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f) _speedMultiplier += 0.5f;
            else if (scroll < 0f) _speedMultiplier -= 0.5f;
            _speedMultiplier = Mathf.Clamp(_speedMultiplier, 0.5f, 20f);
        }
    }
}