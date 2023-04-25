using Godot.Collections;

namespace Godot.DependencyInjection.Services.Input
{
    public interface IInputService
    {        /// <inheritdoc cref="Godot.Input.UseAccumulatedInput"/>
        public bool UseAccumulatedInput
        {
            get;
            set;
        }

        /// <inheritdoc cref="Godot.Input.Singleton"/>
        public GodotObject Singleton
        {
            get;
        }

        /// <inheritdoc cref="Godot.Input.JoyConnectionChanged"/>
        public event Godot.Input.JoyConnectionChangedEventHandler JoyConnectionChanged;

        /// <inheritdoc cref="Godot.Input.IsAnythingPressed"/>
        public bool IsAnythingPressed();

        /// <inheritdoc cref="Godot.Input.IsKeyPressed"/>
        public bool IsKeyPressed(Key keycode);

        /// <inheritdoc cref="Godot.Input.IsPhysicalKeyPressed"/>
        public bool IsPhysicalKeyPressed(Key keycode);

        /// <inheritdoc cref="Godot.Input.IsKeyLabelPressed"/>
        public bool IsKeyLabelPressed(Key keycode);

        /// <inheritdoc cref="Godot.Input.IsMouseButtonPressed"/>
        public bool IsMouseButtonPressed(MouseButton button);

        /// <inheritdoc cref="Godot.Input.IsJoyButtonPressed"/>
        public bool IsJoyButtonPressed(int device, JoyButton button);

        /// <inheritdoc cref="Godot.Input.IsActionPressed"/>
        public bool IsActionPressed(StringName action, bool exactMatch = false);

        /// <inheritdoc cref="Godot.Input.IsActionJustPressed"/>
        public bool IsActionJustPressed(StringName action, bool exactMatch = false);

        /// <inheritdoc cref="Godot.Input.IsActionJustReleased"/>
        public bool IsActionJustReleased(StringName action, bool exactMatch = false);

        /// <inheritdoc cref="Godot.Input.GetActionStrength"/>
        public float GetActionStrength(StringName action, bool exactMatch = false);

        /// <inheritdoc cref="Godot.Input.GetActionRawStrength"/>
        public float GetActionRawStrength(StringName action, bool exactMatch = false);

        /// <inheritdoc cref="Godot.Input.GetAxis"/>
        public float GetAxis(StringName negativeAction, StringName positiveAction);

        /// <inheritdoc cref="Godot.Input.GetVector"/>
        public Vector2 GetVector(StringName negativeX, StringName positiveX, StringName negativeY, StringName positiveY, float deadzone = -1f);

        /// <inheritdoc cref="Godot.Input.AddJoyMapping"/>
        public void AddJoyMapping(string mapping, bool updateExisting = false);

        /// <inheritdoc cref="Godot.Input.RemoveJoyMapping"/>
        public void RemoveJoyMapping(string guid);

        /// <inheritdoc cref="Godot.Input.IsJoyKnown"/>
        public bool IsJoyKnown(int device);

        /// <inheritdoc cref="Godot.Input.GetJoyAxis"/>
        public float GetJoyAxis(int device, JoyAxis axis);

        /// <inheritdoc cref="Godot.Input.GetJoyName"/>
        public string GetJoyName(int device);

        /// <inheritdoc cref="Godot.Input.GetJoyGuid"/>
        public string GetJoyGuid(int device);

        /// <inheritdoc cref="Godot.Input.GetConnectedJoypads"/>
        public Array<int> GetConnectedJoypads();

        /// <inheritdoc cref="Godot.Input.GetJoyVibrationStrength"/>
        public Vector2 GetJoyVibrationStrength(int device);

        /// <inheritdoc cref="Godot.Input.GetJoyVibrationDuration"/>
        public float GetJoyVibrationDuration(int device);

        /// <inheritdoc cref="Godot.Input.StartJoyVibration"/>
        public void StartJoyVibration(int device, float weakMagnitude, float strongMagnitude, float duration = 0f);

        /// <inheritdoc cref="Godot.Input.StopJoyVibration"/>
        public void StopJoyVibration(int device);

        /// <inheritdoc cref="Godot.Input.VibrateHandheld"/>
        public void VibrateHandheld(int durationMs = 500);

        /// <inheritdoc cref="Godot.Input.GetGravity"/>
        public Vector3 GetGravity();

        /// <inheritdoc cref="Godot.Input.GetAccelerometer"/>
        public Vector3 GetAccelerometer();

        /// <inheritdoc cref="Godot.Input.GetMagnetometer"/>
        public Vector3 GetMagnetometer();

        /// <inheritdoc cref="Godot.Input.GetGyroscope"/>
        public Vector3 GetGyroscope();

        /// <inheritdoc cref="Godot.Input.SetGravity"/>
        public void SetGravity(Vector3 value);

        /// <inheritdoc cref="Godot.Input.SetAccelerometer"/>
        public void SetAccelerometer(Vector3 value);

        /// <inheritdoc cref="Godot.Input.SetMagnetometer"/>
        public void SetMagnetometer(Vector3 value);

        /// <inheritdoc cref="Godot.Input.SetGyroscope"/>
        public void SetGyroscope(Vector3 value);

        /// <inheritdoc cref="Godot.Input.GetLastMouseVelocity"/>
        public Vector2 GetLastMouseVelocity();

        /// <inheritdoc cref="Godot.Input.GetMouseButtonMask"/>
        public MouseButtonMask GetMouseButtonMask();

        /// <inheritdoc cref="Godot.Input.WarpMouse"/>
        public void WarpMouse(Vector2 position);

        /// <inheritdoc cref="Godot.Input.ActionPress"/>
        public void ActionPress(StringName action, float strength = 1f);

        /// <inheritdoc cref="Godot.Input.ActionRelease"/>
        public void ActionRelease(StringName action);

        /// <inheritdoc cref="Godot.Input.SetDefaultCursorShape"/>
        public void SetDefaultCursorShape(Godot.Input.CursorShape shape = Godot.Input.CursorShape.Arrow);

        /// <inheritdoc cref="Godot.Input.GetCurrentCursorShape"/>
        public Godot.Input.CursorShape GetCurrentCursorShape();

        /// <inheritdoc cref="Godot.Input.SetCustomMouseCursor"/>
        public void SetCustomMouseCursor(Resource image, Godot.Input.CursorShape shape = Godot.Input.CursorShape.Arrow, Vector2? hotspot = null);

        /// <inheritdoc cref="Godot.Input.ParseInputEvent"/>
        public void ParseInputEvent(InputEvent @event);

        /// <inheritdoc cref="Godot.Input.FlushBufferedEvents"/>
        public void FlushBufferedEvents();
    }
}