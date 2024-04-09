using SoISolver.Helpers;

namespace SoISolver.Multioperations;

public class SpaceMatrix
{
    private readonly bool[] _data;
    private readonly int _rawLength;
    private readonly int _columnLength;

    public SpaceMatrix(int rank, byte[] vectorForm)
    {
        _rawLength = vectorForm.Length;
        _columnLength = rank;
        _data = new bool[_rawLength * rank];
        
        var index = 0;
        for (var i = 0; i < rank; i++)
        {
            foreach (var element in vectorForm)
            {
                _data[index] = (element & Constants.BaseInfo.BaseSet[i]) != 0;
                index++;
            }
        }
    }
    
    public bool this[int i, int j] => _data[i * _rawLength + j];

    public int Length => _data.Length;

    public IEnumerable<bool[]> GetChunks(int chunkSize)
    {
        return _data.Chunk(chunkSize);
    }
}