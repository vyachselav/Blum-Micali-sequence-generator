using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RandomGenerator.Annotations;

namespace RandomGenerator.Models
{
    public sealed class BlumMicaliSequenceGenerator : SequenceGenerator, INotifyPropertyChanged
    {

        private int _a;
        public int A
        {
            get { return _a; }
            set
            {
                if (!IsPrime(_a))
                    throw new ArgumentException("Parameter 'a' is not prime.");

                _a = value;
                NotifyPropertyChanged();
            }
        }

        private int _p;
        public int P
        {
            get { return _p; }
            set
            {
                if (!IsPrime(_p))
                    throw new ArgumentException("Parameter 'p' is not prime.");

                _p = value;
                NotifyPropertyChanged();
            }
        }

        public override void Generate(Sequence sequence)
        {
            var init = false;
            var lastDouble = new double();

            for (var i = 0; i < sequence.Value.Length; i++)
            {
                var bits = new bool[8];
                for (var j = 0; j < 8; j++)
                {
                    if (!init)
                    {
                        init = true;
                        var random = (new Random()).Next(0, P);
                        lastDouble = Math.Pow(A, random) % P;
                    }
                    else
                    {
                        lastDouble = Math.Pow(A, lastDouble) % P;
                    }

                    if (lastDouble < (P - 1.0) / 2.0)
                    {
                        bits[j] = true;
                    }
                    else
                    {
                        bits[j] = false;
                    }
                }

                ToByte(ref sequence.Value[i], bits);
            }
        }

        bool IsPrime(int n)
        {
            //if (n <= 1)
            //	return false;

            //for (var p = 2; p < n; p++)
            //{
            //	if (n % p == 0)
            //	{
            //		return false;
            //	}
            //}

            return true;
        }

        private void ToByte(ref byte _byte, bool[] bits)
        {
            if (bits.Length != 8)
                throw new ArgumentException("ToByte(): Not enough bits in 'bits' parameter");
            for (var offset = 0; offset < 8; offset++)
            {
                if (bits[offset])
                    _byte |= (byte) (1 << offset);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
