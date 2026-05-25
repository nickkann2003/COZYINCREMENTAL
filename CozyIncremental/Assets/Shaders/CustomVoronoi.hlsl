float2 hash22(float2 pp)
{
    pp = float2(
        dot(pp, float2(127.1, 311.7)),
        dot(pp, float2(269.5, 183.3))
    );

    return frac(sin(pp) * 43758.5453);
}

void CustomVoronoi_float(float2 UV, float AngleOffset, float CellDensity, out
float Out, out
float2 Center)
{

    float2 g = floor(UV * CellDensity);
    float2 f = frac(UV * CellDensity);
    float t = 8.0;
    float3 res = float3(8.0, 0.0, 0.0);

    for (
int y = -1; y <= 1; y++)
    {
        for (
int x = -1; x <= 1; x++)
        {
            float2 lattice = float2(x, y);

	//float2 sUV = lattice + g;
        //float2x2 m = float2x2(15.27, 47.63, 99.41, 89.98);
        //sUV = frac(sin(mul(sUV, m)) * 46839.32);

	//float2 offset = float2(sin(sUV.y*+AngleOffset)*0.5+0.5, cos(sUV.x*AngleOffset)*0.5+0.5);
            float offset = hash22(f + lattice);

        // Pseudo-random offset for the point in the cell
        //float2 offset = float2(
       //     frac(sin(dot(g + lattice, float2(127.1, 311.7))) * 43758.5453),
        //    frac(sin(dot(g + lattice, float2(269.5, 183.3))) * 43758.5453)
        //);
            float2 p = lattice + offset - f;
            float d = dot(p, p);

            if (d < res.x)
            {
                res.x =
d;
            // Store the absolute UV center of the closest cell
                Center = (g + lattice + offset) / CellDensity;
            }
        }
    }
    Out = res.
x;
}


