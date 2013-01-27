using System.Collections.ObjectModel;

namespace RandomGenerator.Models
{
    public abstract class SequenceGenerator : ISequenceGenerator
    {
        public abstract void Generate(Sequence sequence);

        public virtual Sequence Generate(uint length)
        {

            var sequence = new Sequence(length);
            Generate(sequence);

            return sequence;
        }
        
        public virtual ObservableCollection<Sequence> GenerateMany(uint length, uint count)
        {
            var list = new ObservableCollection<Sequence>();

			for (var i = 0; i < count; i++)
			{
				list.Add(Generate(length));
			}

			return list;
        }
    }
}
