# Vault Libray for Unity3D (Native Plugin)

**DOWNLOAD:** [Latest build (Unity package)](https://github.com/dasannikov/Vault/releases)

Collection of C11 files and prebuild dynamic libraries for Unity3D with common data structures. It use highly optimized C11 that even faster than STL C++ containers (apporix. 10% faster).
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

### Performance tests
 Creation of 1 million Vector2 structs (Dynamic resize and creation one by one. Bad situation for generic C# collections and GC) and remove first 100 elements after that. Very CPU intensive operation because we need to move all data to the front of array. Most similar realization with generic C# `List<T>` is approx. 20x slower in speed.


| Platform | Vault.Array (Mono build) Time(sec) | Vault.Array (IL2CPP build) Time (sec) |
|----------|----------------|-------------------|
| AMD Phenom(tm) II x4 (Win7. 64Bit) | 0.257 (1) | 1.023 (2) vs 0.243 (3) |
| AMD Phenom(tm) II x4 (Win7. 32Bit) | 0.388 (4) |  |
| MBP 2013. MacOS Catalina 10.15.3 |  | 0.192 |
| iPhone6 iOS 12.4.5 |  | 0.195 |
| Android |  |  |

1. Prebuild DLL. GCC 64Bit with SSE optimized `memmove`.
2. Built-in Unity3D compiler (VS). Looks like no SSE optimization
3. Prebuild DLL in IL2CPP build. GCC 64Bit with SSE optimized `memmove`
4. Prebuild DLL. Clang 32Bit without SSE optimized `memmove`
