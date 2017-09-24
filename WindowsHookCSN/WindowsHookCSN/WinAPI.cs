using System;
using System.Runtime.InteropServices;

namespace WindowsHookCSN
{
    public class Win32API
    {
        #region DLL导入

        /// <summary>
        /// 用于设置窗口,以及调整窗口在z序列中的位置
        /// </summary>
        /// <param name="hWnd">在z序中的位于被置位的窗口前的窗口句柄</param>
        /// <param name="hWndInsertAfter">用于标识在z-顺序的此 CWnd 对象之前的 CWnd 对象。
        /// 如果uFlags参数中设置了SWP_NOZORDER标记则本参数将被忽略。可为下列值之一：
        /// HWND_BOTTOM：值为1，将窗口置于Z序的底部。
        /// 如果参数hWnd标识了一个顶层窗口，则窗口失去顶级位置，并且被置在其他窗口的底部。
        /// HWND_NOTOPMOST：值为-2，将窗口置于所有非顶层窗口之上（即在所有顶层窗口之后）。
        /// 如果窗口已经是非顶层窗口则该标志不起作用。
        /// HWND_TOP：值为0，将窗口置于Z序的顶部。
        /// HWND_TOPMOST：值为-1，将窗口置于所有非顶层窗口之上。
        /// 即使窗口未被激活窗口也将保持顶级位置。</param>
        /// <param name="X">以客户坐标指定窗口新位置的左边界。</param>
        /// <param name="Y">以客户坐标指定窗口新位置的顶边界。</param>
        /// <param name="cx">以像素指定窗口的新的宽度。</param>
        /// <param name="cy">以像素指定窗口的新的高度。</param>
        /// <param name="uFlags">窗口尺寸和定位的标志。该参数可以是下列值的组合：
        /// SWP_ASYNCWINDOWPOS：如果调用进程不拥有窗口，系统会向拥有窗口的线程发出需求。这就防止调用线程在其他线程处理需求的时候发生死锁。
        /// SWP_DEFERERASE：防止产生WM_SYNCPAINT消息。
        /// SWP_DRAWFRAME：在窗口周围画一个边框（定义在窗口类描述中）。
        /// SWP_FRAMECHANGED：给窗口发送WM_NCCALCSIZE消息，即使窗口尺寸没有改变也会发送该消息。如果未指定这个标志，只有在改变了窗口尺寸时才发送WM_NCCALCSIZE。
        /// SWP_HIDEWINDOW;隐藏窗口。
        /// SWP_NOACTIVATE：不激活窗口。如果未设置标志，则窗口被激活，并被设置到其他最高级窗口或非最高级组的顶部（根据参数hWndlnsertAfter设置）。
        /// SWP_NOCOPYBITS：清除客户区的所有内容。如果未设置该标志，客户区的有效内容被保存并且在窗口尺寸更新和重定位后拷贝回客户区。
        /// SWP_NOMOVE：维持当前位置（忽略X和Y参数）。
        /// SWP_NOOWNERZORDER：不改变z序中的所有者窗口的位置。
        /// SWP_NOREDRAW:不重画改变的内容。如果设置了这个标志，则不发生任何重画动作。
        /// 适用于客户区和非客户区（包括标题栏和滚动条）和任何由于窗回移动而露出的父窗口的所有部分。
        /// 如果设置了这个标志，应用程序必须明确地使窗口无效并区重画窗口的任何部分和父窗口需要重画的部分。
        /// SWP_NOREPOSITION：与SWP_NOOWNERZORDER标志相同。
        /// SWP_NOSENDCHANGING：防止窗口接收WM_WINDOWPOSCHANGING消息。
        /// SWP_NOSIZE：维持当前尺寸（忽略cx和Cy参数）。
        /// SWP_NOZORDER：维持当前Z序（忽略hWndlnsertAfter参数）。
        /// SWP_SHOWWINDOW：显示窗口。</param>
        /// <returns>如果函数成功，返回值为非零；如果函数失败，返回值为零。若想获得更多错误消息，请调用GetLastError函数。</returns>
        /// 
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter,
            int X, int Y, int cx, int cy, int uFlags);

        /// <summary>
        /// 安装钩子。使用API函数SetWindowsHookEx()把一个应用程序定义的钩子子程安装到钩子链表中。
        /// SetWindowsHookEx函数总是在Hook链的开头安装Hook子程。
        /// 当指定类型的Hook监视的事件发生时，
        /// 系统就调用与这个Hook关联的 Hook链的开头的Hook子程。
        /// 每一个Hook链中的Hook子程都决定是否把这个事件传递到下一个Hook子程。
        /// Hook子程传递事件到下一个 Hook子程需要调用CallNextHookEx函数。
        /// </summary>
        /// <param name="idHook">钩子的类型，即它处理的消息类型</param>
        /// <param name="lpfn">钩子函数的入口地址（回调函数），当钩子钩到任何消息后便调用这个函数。</param>
        /// <param name="hInstance">应用程序实例的句柄</param>
        /// <param name="threadId">与安装的钩子子程相关联的线程的标识符。
        /// 如果为0，钩子子程与所有的线程关联，即为全局钩子。</param>
        /// <returns>若此函数执行成功,则返回值就是该钩子的句柄;若此函数执行失败,
        /// 则返回值为NULL</returns>
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr SetWindowsHookEx(WH_Codes idHook, HookProc lpfn,
            IntPtr pInstance, int threadId);

        /// <summary>
        /// 卸载钩子
        /// </summary>
        /// <param name="pHookHandle">要删除的钩子的句柄。这个参数是上一个函数SetWindowsHookEx的返回值.</param>
        /// <returns>如果函数成功，返回值为非零值。</returns>
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(IntPtr pHookHandle);

        /// <summary>
        /// 传递钩子。将钩子信息传递到当前钩子链中的下一个子程，
        /// 一个钩子程序可以调用这个函数之前或之后处理钩子信息
        /// </summary>
        /// <param name="idHook">当前钩子的句柄</param>
        /// <param name="nCode">钩传递给当前Hook过程的代码。
        /// 下一个钩子程序使用此代码，以确定如何处理钩的信息</param>
        /// <param name="wParam">wParam参数值传递给当前Hook过程。此参数的含义取决于当前的钩链与钩的类型</param>
        /// <param name="lParam">lParam的值传递给当前Hook过程。此参数的含义取决于当前的钩链与钩的类型。</param>
        /// <returns></returns>
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(IntPtr pHookHandle, int nCode,
            Int32 wParam, IntPtr lParam);

        /// <summary>
        /// 转换当前按键信息。该函数将指定的虚拟键码和键盘状态翻译为相应的字符或字符串。
        /// 该函数使用由给定的键盘布局句柄标识的物理键盘布局和输入语言来翻译代码。
        /// </summary>
        /// <param name="uVirtKey">指定要翻译的虚拟键码</param>
        /// <param name="uScanCode">定义被翻译键的硬件扫描码。若该键处于up状态，则该值的最高位被设置</param>
        /// <param name="lpbKeyState">指向包含当前键盘状态的一个256字节数组。数组的每个成员包含一个键的状态。若某字节的最高位被设置，则该键处于down状态。若最低位被设置，则表明该键被触发。在此函数中，仅有capslock键的触发位是相关的。
        /// NumloCk和scroll loCk键的触发状态将被忽略。</param>
        /// <param name="lpwTransKey">指向接受翻译所得字符或字符串的缓冲区</param>
        /// <param name="fuState">定义一个菜单是否处于激活状态。若一菜单是活动的，则该参数为1，否则为0</param>
        /// <returns>0：对于当前键盘状态，所定义的虚拟键没有翻译。
        /// 1：一个字符被拷贝到缓冲区
        /// 2：两个字符被拷贝到缓冲区。
        /// 当一个存储在键盘布局中的死键（重音或双音字符）
        /// 无法与所定义的虚拟键形成一个单字符时，通常会返回该值</returns>
        [DllImport("user32.dll")]
        public static extern int ToAscii(UInt32 uVirtKey, UInt32 uScanCode,
            byte[] lpbKeyState, byte[] lpwTransKey, UInt32 fuState);

        /// <summary>
        /// 获取按键状态
        /// </summary>
        /// <param name="pbKeyState">指向一个256字节的数组，数组用于接收每个虚拟键的状态</param>
        /// <returns>非0表示成功</returns>
        [DllImport("user32.dll")]
        public static extern int GetKeyboardState(byte[] pbKeyState);

        /// <summary>
        /// 该函数检取指定虚拟键的状态。该状态指定此键是UP状态，DOWN状态，还是被触发的（开关每次按下此键时进行切换）
        /// </summary>
        /// <param name="vKey">定义一虚拟键。
        /// 若要求的虚拟键是字母或数字（A～Z，a～z或0～9），
        /// nVirtKey必须被置为相应字符的ASCII码值，对于其他的键，
        /// nVirtKey必须是一虚拟键码。若使用非英语键盘布局，
        /// 则取值在ASCIIa～z和0～9的虚拟键被用于定义绝大多数的字符键。
        /// 例如，对于德语键盘格式，值为ASCII0（OX4F）的虚拟键指的是"0"键，
        /// 而VK_OEM_1指"带变音的0键"</param>
        /// <returns>GetKeyState(VK_SHIFT) &gt; 0 没按下
        /// GetKeyState(VK_SHIFT) &lt; 0
        /// 被按下
        /// 返回值给出了给定虚拟键的状态，状态如下：
        /// 若高序位为1，则键处于DOWN状态，否则为UP状态。
        /// 若低序位为1，则键被触发。例如CAPS LOCK键，
        /// 被打开时将被触发。若低序位置为0，则键被关闭，
        /// 且不被触发。触发键在键盘上的指示灯，当键被触发时即亮，键不被触发时即灭。
        /// </returns>

        [DllImport("user32.dll")]
        public static extern short GetKeyStates(int vKey);

        /// <summary>
        /// 获取当前鼠标位置
        /// </summary>
        /// <param name="lpPoint">该结构接收光标的屏幕坐标</param>
        /// <returns>如果成功，返回值非零；如果失败，返回值为零</returns>
        [DllImport("user32.dll")]
        public extern static int GetCursorPos(ref POINT lpPoint);


        #endregion DLL导入

    }
}
