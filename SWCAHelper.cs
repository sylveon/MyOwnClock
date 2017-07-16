﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace MyOwnClock
{
    static class SWCAHelper
    {
        private enum WindowCompositionAttribute
        {
            WCA_ACCENT_POLICY = 19
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct WindowCompositionAttributeData
        {
            internal WindowCompositionAttribute Attribute;
            internal IntPtr Data;
            internal int SizeOfData;
        }

        [DllImport("user32.dll")]
        private static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        public enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_INVALID_STATE = 4
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct AccentPolicy
        {
            internal AccentState AccentState;
            internal int AccentFlags;
            internal uint GradientColor;
            internal int AnimationId;
        }

        public static void SetWindowCompositionAttribute(IntPtr hWnd, AccentState state = AccentState.ACCENT_ENABLE_BLURBEHIND, uint color = 0x00000000)
        {
            var accent = new AccentPolicy()
            {
                AccentState = state,
                AccentFlags = 2,
                GradientColor = color
            };
            var accentStructSize = Marshal.SizeOf(accent);
            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData()
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                Data = accentPtr,
                SizeOfData = accentStructSize
            };

            SetWindowCompositionAttribute(hWnd, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }

        public static void SetWindowCompositionAttribute(this WindowInteropHelper window, AccentState state = AccentState.ACCENT_ENABLE_BLURBEHIND, uint color = 0x00000000) => SetWindowCompositionAttribute(window.Handle, state, color);
    }
}