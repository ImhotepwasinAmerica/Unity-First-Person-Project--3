using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Detect : MonoBehaviour {




    public bool Q, W, E, R, T, Y, U, I, O, P,
        A, S, D, F, G, H, J, K, L, 
        Z, X, C, V, B, N, M;

    
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        Q = Input.GetKeyDown(KeyCode.Q);
        W = Input.GetKeyDown(KeyCode.W);
        E = Input.GetKeyDown(KeyCode.E);
        R = Input.GetKeyDown(KeyCode.R);
        T = Input.GetKeyDown(KeyCode.T);
        Y = Input.GetKeyDown(KeyCode.Y);
        U = Input.GetKeyDown(KeyCode.U);
        I = Input.GetKeyDown(KeyCode.I);
        O = Input.GetKeyDown(KeyCode.O);
        P = Input.GetKeyDown(KeyCode.P);
        A = Input.GetKeyDown(KeyCode.A);
        S = Input.GetKeyDown(KeyCode.S);
        D = Input.GetKeyDown(KeyCode.D);
        F = Input.GetKeyDown(KeyCode.F);
        G = Input.GetKeyDown(KeyCode.G);
        H = Input.GetKeyDown(KeyCode.H);
        J = Input.GetKeyDown(KeyCode.J);
        K = Input.GetKeyDown(KeyCode.K);
        L = Input.GetKeyDown(KeyCode.L);
        Z = Input.GetKeyDown(KeyCode.Z);
        X = Input.GetKeyDown(KeyCode.X);
        C = Input.GetKeyDown(KeyCode.C);
        V = Input.GetKeyDown(KeyCode.V);
        B = Input.GetKeyDown(KeyCode.B);
        N = Input.GetKeyDown(KeyCode.M);
        M = Input.GetKeyDown(KeyCode.Slash);
        
        

    }
}
