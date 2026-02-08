using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace SelectAid.Services;

public class InputSender
{
    public void LeftClick() => SendMouseClick(MOUSEEVENTF_LEFTDOWN, MOUSEEVENTF_LEFTUP);
    public void RightClick() => SendMouseClick(MOUSEEVENTF_RIGHTDOWN, MOUSEEVENTF_RIGHTUP);
    public void LeftDown() => SendMouse(MOUSEEVENTF_LEFTDOWN);
    public void LeftUp() => SendMouse(MOUSEEVENTF_LEFTUP);
    public void DoubleClick()
    {
        LeftClick();
        LeftClick();
    }

    public void Scroll(int delta)
    {
        var input = new INPUT
        {
            type = INPUT_MOUSE,
            U = new InputUnion { mi = new MOUSEINPUT { mouseData = delta, dwFlags = MOUSEEVENTF_WHEEL } }
        };
        SendInput(1, new[] { input }, Marshal.SizeOf<INPUT>());
    }

    public void KeyPress(ushort key)
    {
        var down = new INPUT { type = INPUT_KEYBOARD, U = new InputUnion { ki = new KEYBDINPUT { wVk = key } } };
        var up = new INPUT { type = INPUT_KEYBOARD, U = new InputUnion { ki = new KEYBDINPUT { wVk = key, dwFlags = KEYEVENTF_KEYUP } } };
        SendInput(2, new[] { down, up }, Marshal.SizeOf<INPUT>());
    }

    public void MovePointer(Point absolute)
    {
        var x = (int)(absolute.X * 65535 / SystemParameters.PrimaryScreenWidth);
        var y = (int)(absolute.Y * 65535 / SystemParameters.PrimaryScreenHeight);
        var input = new INPUT
        {
            type = INPUT_MOUSE,
            U = new InputUnion
            {
                mi = new MOUSEINPUT { dx = x, dy = y, dwFlags = MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE }
            }
        };
        SendInput(1, new[] { input }, Marshal.SizeOf<INPUT>());
    }

    private void SendMouseClick(uint down, uint up)
    {
        var inputs = new[]
        {
            new INPUT { type = INPUT_MOUSE, U = new InputUnion { mi = new MOUSEINPUT { dwFlags = down } } },
            new INPUT { type = INPUT_MOUSE, U = new InputUnion { mi = new MOUSEINPUT { dwFlags = up } } }
        };
        SendInput(2, inputs, Marshal.SizeOf<INPUT>());
    }

    private void SendMouse(uint flags)
    {
        var inputs = new[]
        {
            new INPUT { type = INPUT_MOUSE, U = new InputUnion { mi = new MOUSEINPUT { dwFlags = flags } } }
        };
        SendInput(1, inputs, Marshal.SizeOf<INPUT>());
    }

    private const int INPUT_MOUSE = 0;
    private const int INPUT_KEYBOARD = 1;
    private const uint MOUSEEVENTF_MOVE = 0x0001;
    private const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
    private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    private const uint MOUSEEVENTF_LEFTUP = 0x0004;
    private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
    private const uint MOUSEEVENTF_RIGHTUP = 0x0010;
    private const uint MOUSEEVENTF_WHEEL = 0x0800;
    private const uint KEYEVENTF_KEYUP = 0x0002;

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

    [StructLayout(LayoutKind.Sequential)]
    private struct INPUT
    {
        public int type;
        public InputUnion U;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct InputUnion
    {
        [FieldOffset(0)]
        public MOUSEINPUT mi;
        [FieldOffset(0)]
        public KEYBDINPUT ki;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }
}
