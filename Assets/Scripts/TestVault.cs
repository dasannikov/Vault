using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class TestVault : MonoBehaviour {

    public Text Label;

    [StructLayout(LayoutKind.Sequential)]
    struct Vec2f {
        public float x;
        public float y;

        public Vec2f(float a, float b) {
            x = a;
            y = b;
        }
    }

    static void TestDev() {

        // New array with default initializer
        var vecArr = new Vault.Array<Vec2f>(10, new Vec2f(1.0f, 1.0f));
        for(var i = 0; i < vecArr.Count; i++) {
            Debug.Log($"{i}: {vecArr[i].x}, {vecArr[i].y}");
        }

        // Set element value
        vecArr[9] = new Vec2f(101f, 102f);
        vecArr[0] = new Vec2f(101f, 102f);

        // Resize array
        vecArr.Resize(14);
        
        // Add element to array.
        vecArr.Add(new Vec2f(21f, 22f));
        
        // Fast remove element by swap with last
        vecArr.RemoveBySwap(1);
        
        // Swap elements
        vecArr.Swap(0, 1);

        // Show elements
        for(var i = 0; i < vecArr.Count; i++) {
            Debug.Log($"{i}: {vecArr[i].x}, {vecArr[i].y}");
        }

        // Clear array
        vecArr.Clear();

        // Check array capacity
        Debug.Log(vecArr.Capacity);
        
        // Free array after use
        vecArr.Free();
    }

    float Test() {
        var vecArr = new Vault.Array<Vec2f>(0);

        // Add element
        var eNew = new Vec2f(0f, 0f);
        for(float i = 0; i < 1000000.0f; i += 1.0f) {
            eNew.x = i;
            eNew.y = i;
            vecArr.Add(eNew);
        }

        // Remove first element
        for(var i = 0; i < 100; ++i)
            vecArr.Remove(0);

        // Remove first element by swap
        for(var i = 0; i < 100; ++i)
            vecArr.RemoveBySwap(0);

        // Count summ
        var count = vecArr.Count;
        var summ = 0.0f;
        for(var i = 0; i < count; ++i) {
            var e = vecArr[i];
            summ += e.x + e.y;
        }

        vecArr.Free();
        return summ;
    }

    void Start() {
        try {
            
            // Test API
            TestDev();

            // Test speed
            var watch = System.Diagnostics.Stopwatch.StartNew();
            float summ = 0;
            summ = Test();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Label.text = $"Summ:  {summ.ToString("F")}\nTime: {(float) elapsedMs / 1000f}";

        } catch(Exception ex) {
            Debug.Log(ex);
        }
    }
}
