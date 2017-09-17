using System.Numerics;

namespace MineCase.Algorithm.Noise
{
    public interface INoise
    {
        float Noise(float x, float y, float z);

        void Noise(float[,,] noise, Vector3 offset, Vector3 scale);

        void AddNoise(float[,,] noise, Vector3 offset, Vector3 scale, float noiseScale);
    }
}
