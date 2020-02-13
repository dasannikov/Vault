# Vault Libray for Unity3D (Native Plugin)

**DOWNLOAD:** [Latest build (Unity package)](https://github.com/dasannikov/Vault/releases)

Tests and proof of concept. Collection of C files and prebuild dynamic libraries for Unity3D with common data structures. It use optimized cross platform C containers that even faster than STL C++ containers (apporix. 10% faster). Zero allocations.

- [x] `Vault.List` - Continuous data similar to C++ `std::vector` and C# `List<T>`
- [ ] Doubly linked list
- [ ] Hash map

### Supported platforms:
- Standalone Mono. Winows/MacOS/Linux
- Standalone IL2CPP. Winows/MacOS/Linux
- Mobile. iOS/Android
- Other through IL2CPP or specific shared libraries

### Advantages:
- Fast (Native code)
- No C# allocations and GC
- You manage your memory (`Free` means delete container and release memory)
- Doesn't use any Unity3D features. **Universal C# code**. Can work with any .NET code

## Vault.List
`Vault.List` is continius container of unmanaged types with dynamic size and ability to remove element. Similar to C++ `std::vector` container and C# `List<T>`. Main porpose of `Vault.List` is dynamic and fast creation of arrays with abillity to free memory without GC.

```csharp

// New list with default initializer
// Vault.List is a struct. Zero allocations in C# memory
var vecArr = new Vault.List<Vector2>(10, new Vector2(1.0f, 1.0f));

// Set element value
vecArr[9] = new Vector2(101f, 102f);
vecArr[0] = new Vector2(101f, 102f);

// Resize list
vecArr.Resize(14);

// Add element to list.
vecArr.Add(new Vector2(21f, 22f));

// Fast remove element by swap with last element
vecArr.RemoveBySwap(1);

// Slow remove element by memmove. Relatively slow :)
vecArr.Remove(1);

// Swap elements
vecArr.Swap(0, 1);

// Clear list
vecArr.Clear();

// Free list after use
// Completely release unmanaged memory
vecArr.Free();

```

### Performance
Performance (IL2CPP Builds) is similar (±10%) to generic C# `List<T>` and Unit3D `NativeList<T>`

