using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Axes
{
    private float[] xAxisCoords;
    private float[] xLimits;
    private float[] yAxisCoords;
    private float[] yLimits;
    private List<(Vector3[] coords, Color color)> pointsList;
    private List<(Vector3[] coords, Color color)> linesList;
    private List<(Vector3[][] coords, Color color)> fillBetweenList;
    private List<(Vector3[] coords, Color color)> vLineList;
    public Axes(float[] xAxisCoords, float[] yAxisCoords)
    {
        this.xAxisCoords = xAxisCoords;
        this.yAxisCoords = yAxisCoords;
        linesList = new List<(Vector3[], Color)>();
        fillBetweenList = new List<(Vector3[][], Color)>();
        pointsList = new List<(Vector3[], Color)>();
        vLineList = new List<(Vector3[], Color)>();
    }

    public void plot(float[] xs, float[] ys, string style, Color? color = null)
    {
        Vector3[] dataCoords = new Vector3[xs.Length];
        Color colorVal = color.HasValue ? color.Value : Color.black;

        if (xLimits == null)
        {
            float xMin = xs.Min();
            float xMax = xs.Max();
            float yMin = ys.Min();
            float yMax = ys.Max();
            setLimits(new float[] {xMin, xMax}, new float[] {yMin, yMax});
        }

        for (int i = 0; i < xs.Length; i++)
        {
            dataCoords[i] = getCoord(xs[i], ys[i]);
        }
        linesList.Add((dataCoords, colorVal));
    }

    public void plotPoints(float[] xs, float[] ys, Color? color = null)
    {
        Vector3[] dataCoords = new Vector3[xs.Length];
        Color colorVal = color.HasValue ? color.Value : Color.black;

        for (int i = 0; i < xs.Length; i++)
        {
            dataCoords[i] = getCoord(xs[i], ys[i]);
        }
        pointsList.Add((dataCoords, colorVal));
    }

    public void fillBetween(float[] xs, float[] y1s, float[] y2s, Color? color = null)
    {
        Vector3[][] dataCoords = new Vector3[2][];
        dataCoords[0] = new Vector3[xs.Length];
        dataCoords[1] = new Vector3[xs.Length];
        Color colorVal = color.HasValue ? color.Value : Colors.skyBlue;

        for (int i = 0; i < xs.Length; i++)
        {
            dataCoords[0][i] = getCoord(xs[i], y1s[i]);
            dataCoords[1][i] = getCoord(xs[i], y2s[i]);
        }
        fillBetweenList.Add((dataCoords, colorVal));
    }

    public void plotVline(float x, Color? color = null)
    {
        Vector3[] dataCoords = new Vector3[2];
        Color colorVal = color.HasValue ? color.Value : Color.black;

        dataCoords[0] = getCoord(x, yLimits[0]);
        dataCoords[1] = getCoord(x, yLimits[1]);
        vLineList.Add((dataCoords, colorVal));
    }

    public void setLimits(float[] xLimits, float[] yLimits)
    {
        this.xLimits = xLimits;
        this.yLimits = yLimits;
    }

    private Vector2 getCoord(float x, float y)
    {
        return new Vector2(getCoord(x, xAxisCoords, xLimits),
                            getCoord(y, yAxisCoords, yLimits));

    }

    private float getCoord(float val, float[] axisCoords, float[] limits)
    {
        float pos = (val - limits[0]) / (limits[1] - limits[0]);
        return axisCoords[0] + pos * (axisCoords[1] - axisCoords[0]);
    }

    public List<(Vector3[] coords, Color color)> getLinesList()
    {
        return linesList;
    }
    public List<(Vector3[][] coords, Color color)> getFillBetweenList()
    {
        return fillBetweenList;
    }

    public List<(Vector3[] coords, Color color)> getPointsList()
    {
        return pointsList;
    }

    public List<(Vector3[] coords, Color color)> getVlineList()
    {
        return vLineList;
    }

    public Vector3[] getXaxis()
    {
        return new Vector3[]
        {
            new Vector3(xAxisCoords[0], yAxisCoords[0], 0),
            new Vector3(xAxisCoords[1], yAxisCoords[0], 0)
        };
    }

    public Vector3[] getYaxis()
    {
        return new Vector3[]
        {
            new Vector3(xAxisCoords[0], yAxisCoords[0], 0),
            new Vector3(xAxisCoords[0], yAxisCoords[1], 0)
        };
    }
}