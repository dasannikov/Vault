using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

sealed class VaultInterop {
#if ENABLE_IL2CPP
    [DllImport ("__Internal")] internal static extern IntPtr vault_init(int sizeOfElement, int count);
    [DllImport ("__Internal")] internal static extern IntPtr vault_init_with(int sizeOfElement, int count, IntPtr defaultElement);
    [DllImport ("__Internal")] internal static extern void vault_delete(IntPtr vault);
    
    [DllImport ("__Internal")] internal static extern int vault_get_count(IntPtr vault);
    [DllImport ("__Internal")] internal static extern int vault_get_count_max(IntPtr vault);
    
    [DllImport ("__Internal")] internal static extern IntPtr vault_resize(IntPtr vault, int new_count);
    [DllImport ("__Internal")] internal static extern IntPtr vault_add(IntPtr vault, IntPtr element);
    [DllImport ("__Internal")] internal static extern void vault_remove(IntPtr vault, int index);
    [DllImport ("__Internal")] internal static extern void vault_remove_swap(IntPtr vault, int index);
#else
    [DllImport("Vault")] internal static extern IntPtr vault_init(int sizeOfElement, int count);
    [DllImport("Vault")] internal static extern IntPtr vault_init_with(int sizeOfElement, int count, IntPtr defaultElement);
    [DllImport("Vault")] internal static extern void vault_delete(IntPtr vault);
    
    [DllImport("Vault")] internal static extern int vault_get_count(IntPtr vault);
    [DllImport("Vault")] internal static extern int vault_get_count_max(IntPtr vault);
    
    [DllImport("Vault")] internal static extern IntPtr vault_resize(IntPtr vault, int new_count);
    [DllImport("Vault")] internal static extern IntPtr vault_add(IntPtr vault, IntPtr element);
    [DllImport("Vault")] internal static extern void vault_remove(IntPtr vault, int index);
    [DllImport("Vault")] internal static extern void vault_remove_swap(IntPtr vault, int index);
#endif
}

public struct Vault<T> where T : unmanaged {

    unsafe T* _data;

    public T this[int i] {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get {
            unsafe {
                return _data[i];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
            unsafe {
                _data[i] = value;
            }
        }
    }

    public int Count {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get {
            unsafe {
                return VaultInterop.vault_get_count((IntPtr) _data);
            }
        }
    }

    public int Capacity {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get {
            unsafe {
                return VaultInterop.vault_get_count_max((IntPtr) _data);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe Vault(int count) {
        _data = (T*) VaultInterop.vault_init(sizeof(T), count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe Vault(int count, ref T defaultElement) {
        fixed(T* e = &defaultElement) {
            _data = (T*) VaultInterop.vault_init_with(sizeof(T), count, (IntPtr) e);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe Vault(int count, T defaultElement) {
        var e = &defaultElement;
        _data = (T*) VaultInterop.vault_init_with(sizeof(T), count, (IntPtr) e);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe void Free() {
        VaultInterop.vault_delete((IntPtr) _data);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe void Resize(int newCount) {
        _data = (T*) VaultInterop.vault_resize((IntPtr) _data, newCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe void Add(ref T element) {
        fixed(T* e = &element) {
            _data = (T*) VaultInterop.vault_add((IntPtr) _data, (IntPtr) e);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe void Add(T element) {
        var e = &element;
        _data = (T*) VaultInterop.vault_add((IntPtr) _data, (IntPtr) e);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe void Remove(int index) {
        VaultInterop.vault_remove((IntPtr) _data, index);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe void RemoveBySwap(int index) {
        VaultInterop.vault_remove_swap((IntPtr) _data, index);
    }
    
}
