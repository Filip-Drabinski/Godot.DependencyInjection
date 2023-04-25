using Godot.Collections;
using Godot.NativeInterop;

namespace Godot.DependencyInjection.Services.Input
{

    /// <summary>
    /// Injecteable wrapper for <see cref="Godot.Input"/>
    /// </summary>
    public class InputService : IInputService
    {
        /// <inheritdoc/>
        public bool UseAccumulatedInput
        {
            get => Godot.Input.UseAccumulatedInput;
            set => Godot.Input.UseAccumulatedInput = value;
        }

        /// <inheritdoc/>
        public GodotObject Singleton
        {
            get => Godot.Input.Singleton;
        }

        /// <inheritdoc/>
        public event Godot.Input.JoyConnectionChangedEventHandler JoyConnectionChanged
        {
            add => Godot.Input.JoyConnectionChanged += value;
            remove => Godot.Input.JoyConnectionChanged -= value;
        }

        /// <inheritdoc/>
        public bool IsAnythingPressed() => Godot.Input.IsAnythingPressed();

        /// <inheritdoc/>
        public bool IsKeyPressed(Key keycode) => Godot.Input.IsKeyPressed(keycode);

        /// <inheritdoc/>
        public bool IsPhysicalKeyPressed(Key keycode) => Godot.Input.IsPhysicalKeyPressed(keycode);

        /// <inheritdoc/>
        public bool IsKeyLabelPressed(Key keycode) => Godot.Input.IsKeyLabelPressed(keycode);

        /// <inheritdoc/>
        public bool IsMouseButtonPressed(MouseButton button) => Godot.Input.IsMouseButtonPressed(button);

        /// <inheritdoc/>
        public bool IsJoyButtonPressed(int device, JoyButton button) => Godot.Input.IsJoyButtonPressed(device, button);

        /// <inheritdoc/>
        public bool IsActionPressed(StringName action, bool exactMatch = false) => Godot.Input.IsActionPressed(action, exactMatch);

        /// <inheritdoc/>
        public bool IsActionJustPressed(StringName action, bool exactMatch = false) => Godot.Input.IsActionJustPressed(action, exactMatch);

        /// <inheritdoc/>
        public bool IsActionJustReleased(StringName action, bool exactMatch = false) => Godot.Input.IsActionJustReleased(action, exactMatch);

        /// <inheritdoc/>
        public float GetActionStrength(StringName action, bool exactMatch = false) => Godot.Input.GetActionStrength(action, exactMatch);

        /// <inheritdoc/>
        public float GetActionRawStrength(StringName action, bool exactMatch = false) => Godot.Input.GetActionRawStrength(action, exactMatch);

        /// <inheritdoc/>
        public float GetAxis(StringName negativeAction, StringName positiveAction) => Godot.Input.GetAxis(negativeAction, positiveAction);

        /// <inheritdoc/>
        public Vector2 GetVector(StringName negativeX, StringName positiveX, StringName negativeY, StringName positiveY, float deadzone = -1f) => Godot.Input.GetVector(negativeX, positiveX, negativeY, positiveY, deadzone);

        /// <inheritdoc/>
        public void AddJoyMapping(string mapping, bool updateExisting = false) => Godot.Input.AddJoyMapping(mapping, updateExisting);

        /// <inheritdoc/>
        public void RemoveJoyMapping(string guid) => Godot.Input.RemoveJoyMapping(guid);

        /// <inheritdoc/>
        public bool IsJoyKnown(int device) => Godot.Input.IsJoyKnown(device);

        /// <inheritdoc/>
        public float GetJoyAxis(int device, JoyAxis axis) => Godot.Input.GetJoyAxis(device, axis);

        /// <inheritdoc/>
        public string GetJoyName(int device) => Godot.Input.GetJoyName(device);

        /// <inheritdoc/>
        public string GetJoyGuid(int device) => Godot.Input.GetJoyGuid(device);

        /// <inheritdoc/>
        public Array<int> GetConnectedJoypads() => Godot.Input.GetConnectedJoypads();

        /// <inheritdoc/>
        public Vector2 GetJoyVibrationStrength(int device) => Godot.Input.GetJoyVibrationStrength(device);

        /// <inheritdoc/>
        public float GetJoyVibrationDuration(int device) => Godot.Input.GetJoyVibrationDuration(device);

        /// <inheritdoc/>
        public void StartJoyVibration(int device, float weakMagnitude, float strongMagnitude, float duration = 0f) => Godot.Input.StartJoyVibration(device, weakMagnitude, strongMagnitude, duration);

        /// <inheritdoc/>
        public void StopJoyVibration(int device) => Godot.Input.StopJoyVibration(device);

        /// <inheritdoc/>
        public void VibrateHandheld(int durationMs = 500) => Godot.Input.VibrateHandheld(durationMs);

        /// <inheritdoc/>
        public Vector3 GetGravity() => Godot.Input.GetGravity();

        /// <inheritdoc/>
        public Vector3 GetAccelerometer() => Godot.Input.GetAccelerometer();

        /// <inheritdoc/>
        public Vector3 GetMagnetometer() => Godot.Input.GetMagnetometer();

        /// <inheritdoc/>
        public Vector3 GetGyroscope() => Godot.Input.GetGyroscope();

        /// <inheritdoc/>
        public void SetGravity(Vector3 value) => Godot.Input.SetGravity(value);

        /// <inheritdoc/>
        public void SetAccelerometer(Vector3 value) => Godot.Input.SetAccelerometer(value);

        /// <inheritdoc/>
        public void SetMagnetometer(Vector3 value) => Godot.Input.SetMagnetometer(value);

        /// <inheritdoc/>
        public void SetGyroscope(Vector3 value) => Godot.Input.SetGyroscope(value);

        /// <inheritdoc/>
        public Vector2 GetLastMouseVelocity() => Godot.Input.GetLastMouseVelocity();

        /// <inheritdoc/>
        public MouseButtonMask GetMouseButtonMask() => Godot.Input.GetMouseButtonMask();

        /// <inheritdoc/>
        public void WarpMouse(Vector2 position) => Godot.Input.WarpMouse(position);

        /// <inheritdoc/>
        public void ActionPress(StringName action, float strength = 1f) => Godot.Input.ActionPress(action, strength);

        /// <inheritdoc/>
        public void ActionRelease(StringName action) => Godot.Input.ActionRelease(action);

        /// <inheritdoc/>
        public void SetDefaultCursorShape(Godot.Input.CursorShape shape = Godot.Input.CursorShape.Arrow) => Godot.Input.SetDefaultCursorShape(shape);

        /// <inheritdoc/>
        public Godot.Input.CursorShape GetCurrentCursorShape() => Godot.Input.GetCurrentCursorShape();

        /// <inheritdoc/>
        public void SetCustomMouseCursor(Resource image, Godot.Input.CursorShape shape = Godot.Input.CursorShape.Arrow, Vector2? hotspot = null) => Godot.Input.SetCustomMouseCursor(image, shape, hotspot);

        /// <inheritdoc/>
        public void ParseInputEvent(InputEvent @event) => Godot.Input.ParseInputEvent(@event);

        /// <inheritdoc/>
        public void FlushBufferedEvents() => Godot.Input.FlushBufferedEvents();

    }
}
