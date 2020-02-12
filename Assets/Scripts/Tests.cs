using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
// using Unity.Collections;
using UnityEngine.UI;

[StructLayout(LayoutKind.Sequential)]
struct TestStruct {
    public int a;
    public int b;

    public TestStruct(int a_, int b_) {
        a = a_;
        b = b_;
    }
}

public class Tests : MonoBehaviour {

    public Text Label;
    
    const int _testResizeSize = 10_000_000;
    const int _testRemoveSize = 20;

    List<TestStruct> testList;
    Vault.List<TestStruct> testVault;
    
    // NativeList<TestStruct> testNativeList;

    /*
    void TestNativeList_ResizeAdd() {
        for(var i = 1; i <= _testResizeSize; ++i) {
            testNativeList.Add(new TestStruct(i, i * 2));
        }
    }
    */

    void TestCSharpList_ResizeAdd() {
        for(var i = 1; i <= _testResizeSize; ++i) {
            testList.Add(new TestStruct(i, i * 2));
        }
    }

    void TestVaultArray_ResizeAdd() {

        for(var i = 1; i <= _testResizeSize; ++i)
            testVault.Add(new TestStruct(i, i * 2));
    }

    void TestCSharpList_Remove() {
        for(var i = 1; i <= _testRemoveSize; ++i) {
            testList.RemoveAt(1);
        }
    }

    void TestVaultArray_Remove() {
        for(var i = 1; i <= _testRemoveSize; ++i) {
            testVault.Remove(1);
        }
    }

    void Test(string description, Action methodName) {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        methodName();
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        Debug.Log($"{description}. Time: {(float) elapsedMs / 1000f}");
    }

    void TestRound() {
        try {
            testList = new List<TestStruct>(0);
            // testNativeList = new NativeList<TestStruct>(0, Allocator.Temp);
            testVault = new Vault.List<TestStruct>(0);
            
            Debug.Log("---");
            Debug.Log($"Resize and Add new element ony by one. {_testResizeSize} elements.");
            Test("C#.List ResizeAdd", TestCSharpList_ResizeAdd);
            // Test("NativeList. ResizeAdd", TestNativeList_ResizeAdd);
            Test("Vault.Array. ResizeAdd", TestVaultArray_ResizeAdd);

            Debug.Log($"Remove element ony by one. {_testRemoveSize} elements.");
            Test("C#.List Remove", TestCSharpList_Remove);
            Test("Vault.Array. Remove", TestVaultArray_Remove);
        } catch(Exception ex) {
            Debug.Log(ex);
        } finally {
            testVault.Free();
            // testNativeList.Dispose();
            testList = null;
        }
    }

    IEnumerator Start() {

        yield return null;
        
        TestRound();
        yield return null;
        
        TestRound();
        yield return null;

        TestRound();
        yield return null;

        TestRound();
        yield return null;

        TestRound();
        yield return null;

        Debug.Log("---");
        Debug.Log("DONE");
        Label.text = "DONE";

    }
}
