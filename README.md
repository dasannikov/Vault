# Vault Libray for Unity3D

## Vault.Array
`Vault.Array` is continius array of unmanaged types with dynamic array size and ability to remove element. Similar to C++ std::vactor container. C# don't have collection like `Vault.Array`. Most similar realization with `List<T>` is approx. 20x slower in speed.

Main porpose of `Vault.Array` is dynamic creation of array with abillity to free memory without GC.

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

// slowe remove element by memmove
vecArr.RemoveBySwap(1);

// Swap elements
vecArr.Swap(0, 1);

// Clear array
vecArr.Clear();

// Free array after use
vecArr.Free();

```

Performance test of creation 1 million object (dynamically one by one. Very bad alghorithm for C# GC) and remove 100 first elements after that. Very CPU intensive operation because we need to move all data to the front of array. Most similar realization with general C# `List<T>` is approx. 20x slower in speed.

| Platform | Mono Time(sec) | IL2CPP Time (sec) |
|----------|----------------|-------------------|
| AMD Phenom(tm) II x4 (Windows 7. 64Bit) | 0.257[^1] | 1.023[^2] |
| AMD Phenom(tm) II x4 (Windows 7. 32Bit) | 0.388[^3] | TODO |
| MBP 2013. MacOS Catalina 10.15.3 | TODO | 0.192 |
| iPhone6 iOS 12.4.5 | x | 0.195 |
| Android | x | TODO |

[^1] Prebuild DLL. GCC 64Bit with SSE optimized memmove.
[^2] Built-in Unity3D compiler (VS). Looks like no SSE optimization.
[^3] Prebuild DLL. Clang 32Bit withot SSE optimized memmove.
