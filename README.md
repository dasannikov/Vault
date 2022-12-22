# Vault Libray for Unity3D

**DOWNLOAD:** [Latest build (Unity package)](https://github.com/dasannikov/Vault/releases)

`Vault.List` - Continuous data array with dynamic size similar to C++ `std::vector<T>` or C# `List<T>`. It uses an optimized native array implemented on `C` that is even faster than STL C++ containers (apporix. 10% faster) and has zero C# memory allocations.

## Supported platforms:
- Standalone Mono. Windows/MacOS/Linux
- Standalone IL2CPP. Windows/MacOS/Linux
- Mobile. iOS/Android
- Other through IL2CPP or specific shared libraries

## Advantages:
- Doesn't use any Unity3D features. Can work with any C# code
- Continuous data array with dynamic size
- Fast and small prebuild native libraries
- No C# memory allocations and C# GC
- You manage your memory (`Free` means deleting container and releasing memory)

## Usage
`Vault.List` is a continuous container with dynamic size. Similar to C++ `std::vector<T>` or C# `List<T>` or Unity's `NativeList<T>`

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

// Relatively slow remove element by memmove
vecArr.Remove(1);

// Swap elements
vecArr.Swap(0, 1);

// Clear list
vecArr.Clear();

// Free list after use
// Completely release unmanaged memory
vecArr.Free();

```

## Performance
Performance (IL2CPP Builds) is similar (±10%) to Unit's `NativeList<T>`

