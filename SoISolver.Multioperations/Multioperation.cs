using SoISolver.Helpers;

namespace SoISolver.Multioperations;

public class Multioperation
{
    private readonly short _rank;
    private readonly short _dimension;
    private readonly byte[] _vectorForm;
    private readonly bool[] _spaceMatrix;

    public Multioperation(short rank, short dimension, byte[] vectorForm)
    {
        _rank = rank;
        _dimension = dimension;
        
        //TODO проверять значения векторной формы на 
        if (vectorForm.Length != GetVectorFormLengthByRankAndDimension()) 
            throw new ArgumentException("Vector form length is invalid!", nameof(vectorForm));
        
        _vectorForm = vectorForm;
        _spaceMatrix = ConstructSpaceMatrix();
    }

    public bool this[int i, int j]
    {
        get
        {
            var length = GetVectorFormLengthByRankAndDimension();
            return _spaceMatrix[i * length + j];
        }
    }

    public int GetVectorFormLengthByRankAndDimension() => ((int)Math.Pow(_rank, _dimension));
    public int GetSpaceMatrixLengthByRankAndDimension() => _rank * GetVectorFormLengthByRankAndDimension();
    
    private bool[] ConstructSpaceMatrix()
    {
        var matrix = new bool[GetSpaceMatrixLengthByRankAndDimension()];
        var index = 0;
        for (var i = 0; i < _rank; i++)
        {
            foreach (var element in _vectorForm)
            {
                var value = (element & Constants.BaseSet[i]) != 0;
                matrix[index] = value;
                index++;
            }
        }

        return matrix;
    } 
}