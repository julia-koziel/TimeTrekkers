using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;
using BayesOpt.Kernels;
using BayesOpt;
using MathNet.Numerics;

public class Optimiser : MonoBehaviour 
{
    public FloatVariable parameter;
    public float minValue;
    public float maxValue;
    public int nInitPoints;
    public bool equidistantInitPoints;
    public bool includeExtremes;
    BayesianOptimisation optimizer;
    Queue<float> initPoints = new Queue<float>();
    GraphDrawer _graphDrawer;
    GraphDrawer GraphDrawer
    {
        get
        {
            if (_graphDrawer == null) _graphDrawer = Camera.main.gameObject.AddComponent<GraphDrawer>();
            return _graphDrawer;
        }
    }
    // TODO allow customizable values, maybe kernels generally...
    Kernel kernel = new RBF(7, 3, 15, false) 
                  * new ConstantKernel(20, 10, 30, false) 
                  + new WhiteKernel(10, 5, 30, false);

    void OnEnable() 
    {
        optimizer = new BayesianOptimisation((minValue, maxValue), 800, optResolution: 20); 
        optimizer.setParams(kernel: kernel, alpha: 0, normalizeY: true);

        if (nInitPoints > 0)
        {
            Generate.LinearSpaced(includeExtremes ? nInitPoints : nInitPoints + 2, minValue, maxValue)
                    .SkipWhile((x, i) => !includeExtremes && i == 0)
                    .TakeWhile((x, i) => i < nInitPoints)
                    .ToArray()
                    .Shuffled()
                    .ForEach(x => initPoints.Enqueue((float) x));
        }
        else initPoints.Enqueue(UnityEngine.Random.Range(minValue, maxValue));
        
        UpdateParameters();
    }

    public void RegisterPoint(float y)
    {
        optimizer.register(parameter.Value, y);

        UpdateParameters();
    }

    void UpdateParameters()
    {
        if (initPoints.Count > 0) parameter.Value = initPoints.Dequeue();
        else parameter.Value = (float) optimizer.suggest();

    }

    public void SaveMax()
    {
        parameter.SaveValue((float)optimizer.space.max().param);
    }

    public float Max { get => (float)optimizer.space.max().param; }

    public void DrawGraph(float delay = 0)
    {
        SetupGraph();
        GraphDrawer.drawGraph();
# if UNITY_EDITOR
        if (delay == 0) EditorApplication.isPaused = true;
# endif
        this.In(delay).Call(() => GraphDrawer.hideGraph());
    }

    public void DrawGraphThen(Action action, float delay = 0)
    {
        SetupGraph();
        GraphDrawer.drawGraph();
        this.In(delay).Call(() => { GraphDrawer.hideGraph(); action(); });
    }

    void SetupGraph()
    {
        optimizer.suggest();

        float[] xs = optimizer.space.ParamSpace.Select(q => (float)q).ToArray();
        float[] mean = optimizer.space.Mean.Select(q => (float)q).ToArray();
        float[] std = optimizer.space.Covariance.Select(q => (float)q).ToArray();
        float[] xTrain = optimizer.space.Params.Select(q => (float)q).ToArray();
        float[] yTrain = optimizer.space.Target.Select(q => (float)q).ToArray();
        float[] upperConf = new float[xs.Length];
        float[] lowerConf = new float[xs.Length];
        upperConf.ForEach((i, _) => mean[i] + 1.96f*std[i]);
        lowerConf.ForEach((i, _) => mean[i] - 1.96f*std[i]);
        int maxAq = optimizer.space.ParamSpace.IndexOf(optimizer.space.NextBest);
        var acqValsDouble = optimizer.space.AcquisitionVals;
        if (!acqValsDouble.All(q => q == 0))
        {
            while (acqValsDouble.All(q => q < 1.5e-45))
            {
                acqValsDouble.ForEach(q => q * 1e+45);
            }
        }
        // (var s1d, var s2d, var s3d) = optimizer._gp.sampleY(optimizer.space.ParamSpace);
        // var s1 = s1d.Select(s => (float)s).ToArray();
        // var s2 = s2d.Select(s => (float)s).ToArray();
        // var s3 = s3d.Select(s => (float)s).ToArray();
        
        float[] aqVals = acqValsDouble.Select(q => (float)q).ToArray();

        var hyp = optimizer._gp.kernel.hyperparameters.Select(h => $"{h.value:0.00}").ToArray();
        var hypString = $"Optimised params -- lenScale: {hyp[0]},   kernvar: {hyp[1]},   noise: {hyp[2]}";

        Figure figure = GraphDrawer.figure(title: hypString);
        GridSpec gridSpec = new GridSpec(2, 1, heightRatios: new int[] {1, 2});
        Axes axesMain = figure.subplot(gridSpec, 1, 0);
        axesMain.setLimits(new float[] { minValue, maxValue },
                        new float[] { 0, maxValue });

        axesMain.plot(xs, mean, "--");
        // axesMain.plot(xs, s1, "-", Color.magenta);
        // axesMain.plot(xs, s2, "-", Color.yellow);
        // axesMain.plot(xs, s3, "-", Color.green);
        axesMain.fillBetween(xs, lowerConf, upperConf);
        axesMain.plotPoints(xTrain, yTrain, Color.yellow);

        Axes axesAq = figure.subplot(gridSpec, 0, 0);
        axesAq.setLimits(new float[] { minValue, maxValue },
                        new float[] { 0, aqVals[maxAq] * 1.05f });
        axesAq.fillBetween(xs, (new float[aqVals.Length]).ForEach(_ => 0), aqVals, Colors.orange);
        axesAq.plot(xs, aqVals, "--");

        axesMain.plotVline(parameter, Color.red);
        axesAq.plotVline(parameter, Color.red);
    }
}