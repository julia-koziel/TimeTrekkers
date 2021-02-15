using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEditor;

public class GraphDrawer : MonoBehaviour
{
    private Camera cam;
    static Material lineMaterial;
    static void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;

            // lineMaterial = Resources.Load<Material>("Line Material");
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }
    private float pixelConversion;
    private Figure mFigure;
    private bool graphOn = false;
    private Rect rect;
    private GUIStyle style;

    // Start is called before the first frame update
    void Awake()
    {
        cam = gameObject.GetComponent<Camera>();
        pixelConversion = cam.pixelWidth / 1920.0f;

        style = new GUIStyle();
        style.normal.textColor = Color.black;
        style.alignment = TextAnchor.UpperCenter;
        style.fontSize = (int) Mathf.Ceil(18 * cam.pixelHeight / 1200.0f) + 6;
        
        GUI.contentColor = Color.black;

        rect = new Rect(
            cam.pixelRect.x + 0.1f * cam.pixelWidth,
            Screen.height - cam.pixelRect.yMax + 0.05f * cam.pixelHeight,
            cam.pixelWidth - 0.1f * cam.pixelWidth,
            Screen.height - cam.pixelRect.yMax + 0.1f * cam.pixelHeight
        );
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPostRender()
    {
   
        if (graphOn)
        {
            CreateLineMaterial();
            Vector3[] corners = mFigure.corners;

            GL.PushMatrix();
            lineMaterial.SetPass(0);
            GL.LoadPixelMatrix();
            GL.Viewport(cam.pixelRect);

            // White background
            drawSquare(Vector3.zero, new Vector3(Screen.width, Screen.height, 0), Color.white);

            foreach (Axes axes in mFigure.getAxesList())
            {
                foreach (var boundaries in axes.getFillBetweenList())
                {
                    fillBetween(boundaries.coords[0], boundaries.coords[1], boundaries.color);
                }

                foreach (var line in axes.getLinesList())
                {
                    drawLine(line.coords, 2, line.color);
                }

                // X axis
                drawLine(axes.getXaxis(), 4, Color.black);

                // Y axis
                drawLine(axes.getYaxis(), 4, Color.black);
            }

            foreach (Axes axes in mFigure.getAxesList())
            {

                foreach (var points in axes.getPointsList())
                {
                    drawDiamonds(points.coords, 15, points.color);
                }

                foreach (var vline in axes.getVlineList())
                {
                    drawLine(vline.coords, 2, vline.color);
                }
            }

            GL.PopMatrix(); // Pop changes.
        }
    }

    void OnGUI()
    {
        if (graphOn)
        {
            GUI.Label(rect, mFigure.getTitle(), style);
        }
    }

    private void drawLine(Vector3[] vertices, int lineWidth, Color color)
    {
        lineWidth = (int) Mathf.Ceil(lineWidth * pixelConversion);
        
        GL.Begin(GL.QUADS);
        GL.Color(color);
        for (int i = 0; i < vertices.Length - 1; i++)
        {
            if (vertices[i+1].x == vertices[i].x)
            {
                GL.Vertex(vertices[i] + lineWidth * Vector3.right);
                GL.Vertex(vertices[i]);
                GL.Vertex(vertices[i+1]);
                GL.Vertex(vertices[i+1] + lineWidth * Vector3.right);
            }
            else
            {
                GL.Vertex(vertices[i] + lineWidth * Vector3.up);
                GL.Vertex(vertices[i]);
                GL.Vertex(vertices[i+1]);
                GL.Vertex(vertices[i+1] + lineWidth * Vector3.up);
            }
        }
        GL.End();
    }

    private void drawQuad(Vector3[] vertices, Color color)
    {
        if (vertices.Length % 4 != 0)
        {
            // TODO throw error
            return;
        }
        GL.Begin(GL.QUADS);
        GL.Color(color);
        vertices.ForEach(vertex => GL.Vertex(vertex));
        GL.End();
    }

    private void drawSquare(Vector3 bottomLeft, Vector3 topRight, Color color)
    {
        Vector3[] vertices = new Vector3[]
        {
            bottomLeft,
            new Vector3(bottomLeft.x, topRight.y, 0),
            topRight,
            new Vector3(topRight.x, bottomLeft.y, 0)
        };
        drawQuad(vertices, color);
    }

    private void drawDiamond(Vector3 point, int size, Color color)
    {
        size = (int) Mathf.Ceil(size * pixelConversion);
        Vector3[] vertices = new Vector3[]
        {
            point + size * Vector3.left,
            point + size * Vector3.up,
            point + size * Vector3.right,
            point + size * Vector3.down
        };
        drawQuad(vertices, color);
    }

    private void drawDiamonds(Vector3[] points, int size, Color color)
    {
        foreach (Vector3 point in points)
        {
            drawDiamond(point, size, color);
        }
    }

    private void fillBetween(Vector3[] line1, Vector3[] line2, Color color)
    {
        if (line1.Length != line2.Length)
        {
            // TODO throw exception
            return;
        }
        Vector3[] vertices = new Vector3[4];
        for (int i = 0; i < line1.Length - 1; i++)
        {
            vertices[0] = line1[i];
            vertices[1] = line2[i];
            vertices[2] = line2[i+1];
            vertices[3] = line1[i+1];
            drawQuad(vertices, color);
        }
    }

    public Figure figure(float panelWidth = 1, float panelHeight = 1,
                        float graphWidth = 0.8f, float graphHeight = 0.8f,
                        string title = "")
    {
        // TODO new panel & graph?

        float xOffset = (1 - panelWidth * graphWidth) * cam.pixelWidth / 2;
        float yOffset = (1 - panelHeight * graphHeight) * cam.pixelHeight / 2;
        Vector3[] corners = new Vector3[]
        {
            new Vector3(cam.pixelRect.xMin + xOffset, cam.pixelRect.yMin + yOffset, 0),
            new Vector3(cam.pixelRect.xMin + xOffset, cam.pixelRect.yMax - yOffset, 0),
            new Vector3(cam.pixelRect.xMax - xOffset, cam.pixelRect.yMax - yOffset, 0),
            new Vector3(cam.pixelRect.xMax - xOffset, cam.pixelRect.yMin + yOffset, 0),
        };

        mFigure = new Figure(this, corners, title);
        return mFigure;
    }

    public void drawGraph()
    {
        graphOn = true;
    }

    public void hideGraph()
    {
        graphOn = false;
    }
    
}
