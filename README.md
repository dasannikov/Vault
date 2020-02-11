# Vault Libray for Unity3D (Native Plugin)

**DOWNLOAD:** [Latest build (Unity package)](https://github.com/dasannikov/Vault/releases)

Collection of C11 files and prebuild dynamic libraries for Unity3D with common data structures.
- [x] `Vault.Array` - Continuous array similar to C++ `std::vector`
- [ ] `Vault.List` - TODO. Doubly linked list
- [ ] `Vault.Map` - TODO. Hash map

Supported platforms:
- Standalone Mono. Winows/MacOS/Linux
- Standalone IL2CPP. Winows/MacOS/Linux
- Mobile. iOS/Android
- Other through IL2CPP or specific shared libraries

Advantages:
- Fast (Native code)
- No C# allocations and GC
- You manage your memory (`Free` means delete container and release memory)
- Don't use any Unity3D features. Universal C# code. Can works with any .net code.

## Vault.Array
`Vault.Array` is continius array of unmanaged types with dynamic array size and ability to remove element. Similar to C++ `std::vector` container. C# don't have collection like `Vault.Array`. Most similar realization with `List<T>` is approx. 20x slower in speed. Main porpose of `Vault.Array` is dynamic and fast creation of arrays with abillity to free memory without GC.

```csharp

// New array with default initializer
var vecArr = new Vault.Array<Vector2>(10, new Vector2(1.0f, 1.0f));

// Set element value
vecArr[9] = new Vector2(101f, 102f);
vecArr[0] = new Vector2(101f, 102f);

// Resize array
vecArr.Resize(14);

// Add element to array.
vecArr.Add(new Vector2(21f, 22f));

// Fast remove element by swap with last element
vecArr.RemoveBySwap(1);

// Slow remove element by memmove. Relatively slow :)
vecArr.Remove(1);

// Swap elements
vecArr.Swap(0, 1);

// Clear array
vecArr.Clear();

// Free array after use
vecArr.Free();

```

### Performance test
 Creation of 1 million structs (Dynamic creation one by one. Very bad situation for generic C# collections and GC) and remove first 100 elements after that. Very CPU intensive operation because we need to move all data to the front of array. Most similar realization with general C# `List<T>` is approx. 20x slower in speed.

| Platform | Mono Time(sec) | IL2CPP Time (sec) |
|----------|----------------|-------------------|
| AMD Phenom(tm) II x4 (Windows 7. 64Bit) | 0.257 (1) | 1.023 (2) |
| AMD Phenom(tm) II x4 (Windows 7. 32Bit) | 0.388 (3) |  |
| MBP 2013. MacOS Catalina 10.15.3 |  | 0.192 |
| iPhone6 iOS 12.4.5 |  | 0.195 |
| Android |  |  |

1. Prebuild DLL. GCC 64Bit with SSE optimized memmove.
2. Built-in Unity3D compiler (VS). Looks like no SSE optimization.
3. Prebuild DLL. Clang 32Bit withot SSE optimized memmove.
