using System;
using System.Runtime.InteropServices;

namespace Vault {
    internal sealed class Interop {
        
#if ENABLE_MONO || UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        [DllImport("Vault")] internal static extern IntPtr vault_array_init(int sizeOfElement, int count);
        [DllImport("Vault")] internal static extern IntPtr vault_array_init_with(int sizeOfElement, int count, IntPtr defaultElement);
        [DllImport("Vault")] internal static extern void vault_array_delete(IntPtr vault);

        [DllImport("Vault")] internal static extern int vault_array_get_count(IntPtr vault);
        [DllImport("Vault")] internal static extern int vault_array_get_count_max(IntPtr vault);

        [DllImport("Vault")] internal static extern IntPtr vault_array_resize(IntPtr vault, int new_count);
        [DllImport("Vault")] internal static extern IntPtr vault_array_add(IntPtr vault, IntPtr element);
        [DllImport("Vault")] internal static extern void vault_array_remove(IntPtr vault, int index);
        [DllImport("Vault")] internal static extern void vault_array_remove_swap(IntPtr vault, int index);
#else
        [DllImport ("__Internal")] internal static extern IntPtr vault_array_init(int sizeOfElement, int count);
        [DllImport ("__Internal")] internal static extern IntPtr vault_array_init_with(int sizeOfElement, int count, IntPtr defaultElement);
        [DllImport ("__Internal")] internal static extern void vault_array_delete(IntPtr vault);

        [DllImport ("__Internal")] internal static extern int vault_array_get_count(IntPtr vault);
        [DllImport ("__Internal")] internal static extern int vault_array_get_count_max(IntPtr vault);

        [DllImport ("__Internal")] internal static extern IntPtr vault_array_resize(IntPtr vault, int new_count);
        [DllImport ("__Internal")] internal static extern IntPtr vault_array_add(IntPtr vault, IntPtr element);
        [DllImport ("__Internal")] internal static extern void vault_array_remove(IntPtr vault, int index);
        [DllImport ("__Internal")] internal static extern void vault_array_remove_swap(IntPtr vault, int index);
#endif


    }
}