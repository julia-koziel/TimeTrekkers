using UnityEngine;
using System.Collections.Generic;

public class Figure
{
    private string title;
    private GraphDrawer drawer;
    public Vector3[] corners;
    private List<Axes> axesList;
    private Vector3 origin;
    private float width;
    private float height;
    public Figure(GraphDrawer drawer, Vector3[] corners, string title = "")
    {
        this.drawer = drawer;
        this.corners = corners;
        origin = corners[0];
        width = corners[3].x - corners[0].x;
        height = corners[1].y - corners[0].y;
        this.axesList = new List<Axes>();
        this.title = title;
    }

    private float getCoord(float val, float[] axis)
    {
        return 0;
    }

    public Axes subplot()
    {
        GridSpec gridSpec = new GridSpec(1, 1);
        return subplot(gridSpec, 0, 0);
    }

    public Axes subplot(GridSpec gridSpec, int row, int col)
    {
        (float left, float right, float bottom, float top) = gridSpec.getSubplot(row, col);
        float[] xs = new float[] { origin.x + left*width, origin.x + right*width};
        float[] ys = new float[] { origin.y + bottom*height, origin.y + top*height };
        Axes axes = new Axes(xs, ys);
        axesList.Add(axes);
        return axes;
    }

    public List<Axes> getAxesList()
    {
        return axesList;
    }

    public string getTitle()
    {
        return title;
    }
}