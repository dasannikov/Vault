using System;
using System.Runtime.CompilerServices;

namespace Vault {

    public struct List<T> where T : unmanaged {

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
                    return Interop.vault_array_get_count((IntPtr) _data);
                }
            }
        }

        public int Capacity {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get {
                unsafe {
                    return Interop.vault_array_get_count_max((IntPtr) _data);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe List(int count) {
            _data = (T*) Interop.vault_array_init(sizeof(T), count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe List(int count, ref T defaultElement) {
            fixed(T* e = &defaultElement) {
                _data = (T*) Interop.vault_array_init_with(sizeof(T), count, (IntPtr) e);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe List(int count, T defaultElement) {
            var e = &defaultElement;
            _data = (T*) Interop.vault_array_init_with(sizeof(T), count, (IntPtr) e);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Free() {
            if(_data != null)
                Interop.vault_array_delete((IntPtr) _data);
            _data = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Resize(int newCount) {
            _data = (T*) Interop.vault_array_resize((IntPtr) _data, newCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Add(ref T element) {
            fixed(T* e = &element) {
                _data = (T*) Interop.vault_array_add((IntPtr) _data, (IntPtr) e);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Add(T element) {
            var e = &element;
            _data = (T*) Interop.vault_array_add((IntPtr) _data, (IntPtr) e);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Remove(int index) {
            Interop.vault_array_remove((IntPtr) _data, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void RemoveBySwap(int index) {
            Interop.vault_array_remove_swap((IntPtr) _data, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Clear() {
            _data = (T*) Interop.vault_array_resize((IntPtr) _data, 0);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Swap(int index1, int index2) {
            Interop.vault_array_swap((IntPtr) _data, index1, index2);
        }
        
    }
}