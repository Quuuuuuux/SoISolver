using System.ComponentModel.DataAnnotations;
using SoISolver.Helpers;

namespace SoISolver.Multioperations;

public class Multioperation
{
    public readonly int Rank;
    public readonly int Dimension;
    
    private readonly byte[] _vectorForm;
    private readonly SpaceMatrix _spaceMatrix;

    public Multioperation(
        [Range(1, Constants.BaseInfo.MaxRank)] int rank, 
        [Range(1, Constants.BaseInfo.MaxDimension)] int dimension,
        byte[] vectorForm
    ) {
        if (vectorForm.Length != (int)Math.Pow(rank, dimension)) 
            throw new ArgumentException("Vector form length is invalid!", nameof(vectorForm));
        
        Rank = rank;
        Dimension = dimension;
        _vectorForm = vectorForm;
        _spaceMatrix = new SpaceMatrix(Rank, _vectorForm);
    }

    public bool this[int i, int j] => _spaceMatrix[i,j];

    public IEnumerable<bool[]> GetSpaceMatrixChunks()
    {
        return _spaceMatrix.GetChunks(Rank);
    }
}