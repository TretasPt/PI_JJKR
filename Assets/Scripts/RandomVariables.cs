using System;
using System.IO;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomVariables : MonoBehaviour
{

    public static int Uniform(int v1, int v2)
    {
        return Random.Range(v1, v2);
    }

    public static double Uniform(float v1, float v2)
    {
        return Random.Range(v1, v2);
    }




    public static double Normal(double mean, double standardDeviation)
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

    public static double NormalBounded(double mean, double standardDeviation, double min, double max, int maxAttempts = 1000)
    {
        if (min >= max)
        {
            throw new ArgumentOutOfRangeException("min should be lesser than max.");
        }
        while (maxAttempts > 0)
        {
            double output = Normal(mean, standardDeviation);
            if (output >= min && output <= max)
            {
                return output;
            }
            maxAttempts--;
        }
        throw new Exception("Exceeded max generation attempts.");
    }

    public static double Arcsine(double xMin, double xMax)
    {
        if (xMax < xMin)
        {
            throw new ArgumentOutOfRangeException("xMin should be smaller than xMax. xMin=" + xMin + " ; xMax=" + xMax);
        }
        double q = Math.Sin(Math.PI / 2 * Uniform(0f, 1f));
        return xMin + (xMax - xMin) * q * q;
    }

    public static double CustomDiscrete(double[] options, double[] weights)
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

    public static bool Bernoulli(double p)
    {
        if (p < 0 || p > 1)
        {
            throw new ArgumentOutOfRangeException("p should be between 0 and 1.");
        }
        return Uniform(0f, 1f) < p;
    }

    public static int Binomial(int n, double p)
    {
        if (n < 1 || p < 0 || p > 1)
        {
            throw new ArgumentOutOfRangeException("n should be greater or equal to 1 and p should be between 0 and 1.");
        }
        int sum = 0;
        for (int i = 0; i < n; i++)
        {
            sum += Bernoulli(p) ? 1 : 0;
        }
        return sum;
    }


    private class DistributionTesterStructure
    {
        public string Name { get; }
        public Func<string> Generator { get; }
        public int Iterations { get; }

        public DistributionTesterStructure(string name, Func<string> generator, int iterations = 1000)
        {
            this.Name = name;
            this.Generator = generator;
            this.Iterations = iterations;
        }

    }

    public static void TestDistributions()
    {
        Debug.Log("Started generating random variables to files.");


        DistributionTesterStructure[] generators = {
            // new("",()=>""),

            new("ChooseTreeType", ()=>Uniform(0, 10).ToString()),
            new("ChooseBushType", ()=>Uniform(0, 5).ToString()),


            new("GenerateNumberOfClusters", ()=>Uniform(5, 20).ToString()),
            new("ChoseXOrZToPositionCluster", ()=>Uniform(-256.5f, 256.5f).ToString()),
            new("GenerateClusterMean",()=>Uniform(5f, 50f).ToString()),
            new("PolarPropAngle",()=>Uniform(0f, 2 * Mathf.PI).ToString()),
            new("GenerateClusterStandardDeviation",()=>Uniform(1f, 50f).ToString()),
            new("ChosePerlinNoiseOffset", ()=>Uniform(0f, 100000f).ToString()),

            new("ResetPlayerPosition", ()=> NormalBounded(0,5,-10,10).ToString()),
            new("RandomizeShoot",()=>Normal(0f, 0.015f).ToString()),
            
            //new("PolarPropRadious", NormalBounded(mean, sDeviation, 0, 200).ToString()),
        };


        foreach (DistributionTesterStructure generator in generators)
        {
            using StreamWriter writer = new("./ThingsThatShouldBeOutside/" + generator.Name + ".csv");
            for (int i = 0; i < generator.Iterations; i++)
            {
                writer.Write(generator.Generator() + ";");
            }
        }

        Debug.Log("Finished generating random variables to files.");

    }


}
