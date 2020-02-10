using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class TestVault : MonoBehaviour { 

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
        
        var vecArr = new Vault<Vec2f>(10, new Vec2f(1.0f, 1.0f));
        
        for(var i = 0; i < vecArr.Count; i++) {
            Debug.Log($"{i}: {vecArr[i].x}, {vecArr[i].y}");
        }

        vecArr[9] = new Vec2f(101f, 102f);
        vecArr.Resize(14);
        vecArr.Add(new Vec2f(21f, 22f));
        vecArr.RemoveBySwap(1);
        
        for(var i = 0; i < vecArr.Count; i++) {
            Debug.Log($"{i}: {vecArr[i].x}, {vecArr[i].y}");
        }
        
        Debug.Log(vecArr.Capacity);
        
        vecArr.Free();
    }
    
    float Test() {
        var vecArr = new Vault<Vec2f>(0);

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
        var watch = System.Diagnostics.Stopwatch.StartNew();

        float summ = 0;
        try {
            summ = Test();
            //TestDev();
        } catch(Exception ex) {
            Debug.Log(ex);
        }

        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        Debug.Log($"Summ: {summ.ToString("F")}");
        Debug.Log($"Elapsed time: {(float)elapsedMs / 1000f}");
    }
}
