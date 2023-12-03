using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomVariables : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int Uniform(int v1, int v2)
    {
        return Random.Range(v1, v2);
    }

    public double Uniform(float v1, float v2)
    {
        return Random.Range(v1, v2);
    }




    public double Normal(double mean, double standardDeviation)
    {
        if (standardDeviation <= 0)
        {
            throw new ArgumentOutOfRangeException("Standard deviation must be positive.");
        }
        double p, p1, p2;
        do
        {
            p1 = Uniform(-1f, 1f);
            p2 = Uniform(-1f, 1f);
            p = p1 * p1 + p2 * p2;
        }
        while (p >= 1f);
        return mean + standardDeviation * p1 * Math.Sqrt(-2f * Math.Log(p) / p);
    }

    public double Arcsine(double xMin, double xMax)
    {
        if (xMax < xMin)
        {
            throw new ArgumentOutOfRangeException("xMin should be smaller than xMax. xMin=" + xMin + " ; xMax=" + xMax);
        }
        double q = Math.Sin(Math.PI / 2 * Uniform(0f, 1f));
        return xMin + (xMax - xMin) * q * q;
    }

    public double CustomDiscrete(double[] options, double[] weights)
    {
        if (options.Length != weights.Length)
        {
            throw new ArgumentException("Options and Weights should have the same dimension.");
        }
        double sum = 0;
        foreach (double weight in weights)
        {
            sum += weight;
        }
        if (sum != 1)
        {
            throw new ArgumentException("The sum of the weights should be 1.");
        }

        double chosenOption = Uniform(0f, 1f);

        sum = 0;
        for (int i = 0; i != options.Length; i++)
        {
            sum += weights[i];
            if (sum > chosenOption)
            {
                return options[i];
            }
        }
        return options[options.Length];
    }

    public bool Bernoulli(double p)
    {
        if (p < 0 || p > 1)
        {
            throw new ArgumentOutOfRangeException("p should be between 0 and 1.");
        }
        return Uniform(0f, 1f) < p;
    }

    public int Binomial(int n, double p)
    {
        if (n < 1 || p < 0 || p > 1)
        {
            throw new ArgumentOutOfRangeException("n should be grrater or equal to 1 and p should be between 0 and 1.");
        }
        int sum = 0;
        for (int i = 0; i < n; i++)
        {
            sum += Bernoulli(p) ? 1 : 0;
        }
        return sum;
    }

}
