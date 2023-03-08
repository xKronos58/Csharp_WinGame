using System;
using System.Drawing;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
class Program
{
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
    static void Main()
    {
        

        // Define the window class
        var wc = new WNDCLASSEX
        {
            cbSize = 0,
            style = 0,
            lpfnWndProc = default,
            cbClsExtra = 0,
            cbWndExtra = 0,
            hInstance = default,
            hIcon = default,
            hCursor = default,
            hbrBackground = default,
            lpszMenuName = null,
            lpszClassName = null,
            hIconSm = default
        };
        wc.cbSize = (uint)Marshal.SizeOf(wc);
        wc.style = CS_HREDRAW | CS_VREDRAW;
        wc.lpfnWndProc = Marshal.GetFunctionPointerForDelegate<WndProcDelegate>(WndProc);
        wc.hInstance = Marshal.GetHINSTANCE(typeof(Program).Module);
        wc.hCursor = LoadCursor(IntPtr.Zero, IDC_ARROW);
        wc.lpszClassName = "MyWindowClass";
        RegisterClassEx(ref wc);

        // Define the WndProc function
        static IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            switch (msg)
            {
                case WM_PAINT:
                {
                    // Handle the WM_PAINT message here
                    // ...
                    break;
                }

                case WM_DESTROY:
                    PostQuitMessage(0);
                    break;

                default:
                    return DefWindowProc(hWnd, msg, wParam, lParam);
            }

            return IntPtr.Zero;
        }

        // Create the window
        IntPtr hWnd = CreateWindowEx(0, wc.lpszClassName, "My Window",
            WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT,
            CW_USEDEFAULT, CW_USEDEFAULT, IntPtr.Zero, IntPtr.Zero,
            Marshal.GetHINSTANCE(typeof(Program).Module), IntPtr.Zero);

        // Show the window
        ShowWindow(hWnd, SW_SHOWDEFAULT);
        UpdateWindow(hWnd);

        // Run the message loop
        MSG msg;
        while (GetMessage(out msg, IntPtr.Zero, 0, 0))
        {
            TranslateMessage(ref msg);
            DispatchMessage(ref msg);
        }
    }
    
    static IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
    {
        switch (msg)
        {
            case WM_PAINT:
                {
                    // Handle the WM_PAINT message here
                    // ...
                    break;
                }

            case WM_DESTROY:
                PostQuitMessage(0);
                break;

            default:
                return DefWindowProc(hWnd, msg, wParam, lParam);
        }

        return IntPtr.Zero;
    }

    const uint CS_HREDRAW = 0x0002;
    const uint CS_VREDRAW = 0x0001;
    const uint IDC_ARROW = 32512;
    const uint WM_PAINT = 0x000F;
    const uint WM_DESTROY = 0x0002;
    const uint WS_OVERLAPPEDWINDOW = 0x00CF0000;
    const uint CW_USEDEFAULT = 0x80000000u;
    const int SW_SHOWDEFAULT = 10;

    [StructLayout(LayoutKind.Sequential)]
    struct WNDCLASSEX
    {
        public uint cbSize;
        public uint style;
        public IntPtr lpfnWndProc;
        public int cbClsExtra;
        public int cbWndExtra;
        public IntPtr hInstance;
        public IntPtr hIcon;
        public IntPtr hCursor;
        public IntPtr hbrBackground;
        public string lpszMenuName;
        public string lpszClassName;
        public IntPtr hIconSm;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct MSG
    {
        public IntPtr hwnd;
        public uint message;
        public IntPtr wParam;
        public IntPtr lParam;
        public uint time;
        public POINT pt;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct POINT
    {
        public int x;
        public int y;
    }
    
    
    
    [DllImport("user32.dll")]
    static extern bool RegisterClassEx(ref WNDCLASSEX lpwcx);

    [DllImport("user32.dll")]
    static extern IntPtr CreateWindowEx(uint dwExStyle, string lpClassName,
        string lpWindowName, uint dwStyle, uint x, uint y, uint nWidth, uint nHeight,
        IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    static extern bool UpdateWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    static extern bool GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin,
        uint wMsgFilterMax);

    [DllImport("user32.dll")]
    static extern bool TranslateMessage(ref MSG lpMsg);

    [DllImport("user32.dll")]
    static extern IntPtr DispatchMessage(ref MSG lpMsg);

    [DllImport("user32.dll")]
    static extern IntPtr DefWindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll")]
    static extern void PostQuitMessage(int nExitCode);

    [DllImport("user32.dll")]
    static extern IntPtr LoadCursor(IntPtr hInstance, uint lpCursorName);
}

