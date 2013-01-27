using System.Security.Cryptography;

namespace RandomGenerator.Models
{
    public class BuiltInSequenceGenerator : SequenceGenerator
    {
        readonly RNGCryptoServiceProvider _rngCryptoServiceProvider = new RNGCryptoServiceProvider();

        public override void Generate(Sequence sequence)
        {
            _rngCryptoServiceProvider.GetBytes(sequence.Value);
        }
    }
}
