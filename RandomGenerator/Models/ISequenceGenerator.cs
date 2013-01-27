using System.Collections.ObjectModel;

namespace RandomGenerator.Models
{
    interface ISequenceGenerator
    {
        void Generate(Sequence sequence);
        Sequence Generate(uint length);
        ObservableCollection<Sequence> GenerateMany(uint length, uint count);
    }
}
