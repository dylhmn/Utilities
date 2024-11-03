using System.Runtime.InteropServices;
// For DLL (Dynamic-link library)

// IMPORTS - Because C# doesn't have mouse events
[DllImport("user32.dll")] // User 32 module (Windows API)
static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, IntPtr dwExtraInfo); // click
[DllImport("user32.dll")]
static extern short GetAsyncKeyState(int vKey); // hotkey

// CLASS VARIABLES
const uint LEFTDOWN = 0x02; // LEFTDOWN = Left click
const uint LEFTUP = 0x04; // LEFTUP = Releasing left click
// The value is representing the virtual button press (from microsoft documents)

const int HOTKEY = 0x60;

bool enableClicker = false; // ON/OFF for hotkey
int clickInterval = 3; // Miliseconds between clicks


// AUTOCLICKER LOOP
while (true)
{
    if (GetAsyncKeyState(HOTKEY) < 0) // if hotkey is down
    {
        enableClicker = !enableClicker; // Enable / Disable, depends on the bool
        Thread.Sleep(300); // Stops spam
    }
    if (enableClicker)
    {
        MouseClick();
        
    }
    Thread.Sleep(clickInterval);
}

// CREATING MOUSE CLICK
void MouseClick()
{ 
    mouse_event(LEFTDOWN,0,0,0, IntPtr.Zero); // No more information is needed than the click (No mouse movements)
    mouse_event(LEFTUP, 0, 0, 0 ,IntPtr.Zero);
}
