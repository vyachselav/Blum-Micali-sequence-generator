using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using RandomGenerator.Annotations;

namespace RandomGenerator.Models
{
    public sealed class Sequence : INotifyPropertyChanged
    {
        public Sequence(uint length)
        {
            Value = new byte[length];
        }

        private byte[] _value;
        public byte[] Value
        {
            get { return _value; }
            set
            {
                _value = value;

                NotifyPropertyChanged();
                NotifyPropertyChanged("StringValue");
            }
        }

        public string StringValue
        {
            get { return ToString(); }
            set { throw new InvalidOperationException(); }
        }

        private uint _zeroes;
        public uint Zeroes
        {
            get { return _zeroes; }
            set
            { 
                _zeroes = value;
                NotifyPropertyChanged();
            }
        }

        private uint _ones;

        public uint Ones
        {
            get { return _ones; }
            set
            {
                _ones = value;
                NotifyPropertyChanged();
            }
        }
        
        public void UpdateZeroesAndOnes()
        {
            Zeroes = 0;
            Ones = 0;

            foreach (var @byte in Value)
            {
                var _byte = (int) @byte;
                for (var i = 0; i < 8; i++)
                {
                    var temp = (1 << i);
                    var bit = _byte & temp;

                    if (bit != 0)
                    {
                        Ones++;
                    }
                    else
                    {
                        Zeroes++;
                    }
                }
            }
        }

        public double SingleBitsTest()
        {
            return ((double) Ones) / ((double) (Zeroes + Ones));
        }

        public double BitsPairsTest(out double n00, out double n11)
        {
            n00 = 0.0;
            double n01 = 0.0;
            double n10 = 0.0;
            n11 = 0.0;

            for (var i = 0; i < (Value.Length * 8) - 1; i++)
            {
                if (this[i] == 0 && this[i + 1] == 0)
                    n00++;
                if (this[i] == 0 && this[i + 1] == 1)
                    n01++;
                if (this[i] == 1 && this[i + 1] == 0)
                    n10++;
                if (this[i] == 1 && this[i + 1] == 1)
                    n11++;
            }

            var n = (double) Value.Length;
            var liczba_zer = (double) Zeroes;
            var liczba_jedynek = (double) Ones;

            double x2 = 4.0 / (n - 1.0);
            x2 *= (Math.Pow(n00, 2) + Math.Pow(n01, 2) + Math.Pow(n10, 2) + Math.Pow(n11, 2));
            x2 -= (2.0 / n) * (Math.Pow(liczba_jedynek, 2) + Math.Pow(liczba_zer, 2));
            x2++;

            return x2 / 100.0;
        }

        public double PokerTest()
        {
            var n = (double) Value.Length * 8;
            var m = 3;
            var k = n / m;

            var wymiar_tabeli = (int) k;

            int licz000 = 0;
            int licz001 = 0;
            int licz010 = 0;
            int licz011 = 0;
            int licz100 = 0;
            int licz101 = 0;
            int licz110 = 0;
            int licz111 = 0;

            for (var i = 0; i < wymiar_tabeli * 3; i = i + m)
            {
                if (this[i] == 0 && this[i + 1] == 0 && this[i + 2] == 0)
                    licz000++;
                if (this[i] == 0 && this[i + 1] == 0 && this[i + 2] == 1)
                    licz001++;
                if (this[i] == 0 && this[i + 1] == 1 && this[i + 2] == 0)
                    licz010++;
                if (this[i] == 0 && this[i + 1] == 1 && this[i + 2] == 1)
                    licz011++;
                if (this[i] == 1 && this[i + 1] == 0 && this[i + 2] == 0)
                    licz100++;
                if (this[i] == 1 && this[i + 1] == 0 && this[i + 2] == 1)
                    licz101++;
                if (this[i] == 1 && this[i + 1] == 1 && this[i + 2] == 0)
                    licz110++;
                if (this[i] == 1 && this[i + 1] == 1 && this[i + 2] == 1)
                    licz111++;
            }

            double x3 = Math.Pow(2, m) / wymiar_tabeli;
            x3 = x3 * (Math.Pow(licz000, 2) + Math.Pow(licz001, 2) + Math.Pow(licz010, 2) + Math.Pow(licz011, 2) + Math.Pow(licz100, 2) + Math.Pow(licz101, 2) + Math.Pow(licz110, 2) + Math.Pow(licz111, 2));
            x3 = x3 - wymiar_tabeli;
            
            return x3;
        }

        public double SeriesTest(double n00, double n11)
        {
            var liczba_zer = (double) Zeroes;
            var liczba_jedynek = (double) Ones;
            double x4 = 0;
            double e1, e2, e3;

            e1 = (160 - 1 + 3) / Math.Pow(2, 1 + 2);
            e2 = (160 - 2 + 3) / Math.Pow(2, 2 + 2);
            e3 = (160 - 2 + 3) / Math.Pow(2, 3 + 2);

            // i = 1

            x4 = x4 + Math.Pow(liczba_jedynek - e1, 2) / e1;
            x4 = x4 + Math.Pow(liczba_zer - e1, 2) / e1;

            // i = 2

            x4 = x4 + Math.Pow(n00 - e2, 2) / e2;
            x4 = x4 + Math.Pow(n11 - e2, 2) / e2;

            // i = 3

            int n000 = 0;
            int n111 = 0;

            for (int i = 0; i < (Value.Length * 8) - 2; i++)
            {
                if (this[i] == 0 && this[i + 1] == 0 && this[i + 2] == 0)
                    n000++;
                if (this[i] == 1 && this[i + 1] == 1 && this[i + 2] == 1)
                    n111++;
            }

            x4 = x4 + Math.Pow(n000 - e3, 2) / e3;
            x4 = x4 + Math.Pow(n111 - e3, 2) / e3;

            return x4;
        }

        public double CorrelationTest()
        {
            int d = 5;
            int ad = 0;

            for (int i = 0; i < (Value.Length * 8) - d - 1; i++)
            {
                if (this[i] == this[i + d])
                { }
                else
                {
                    ad++;
                }
            }

            return 2 * (ad - ((Value.Length * 8) - d) / 2) / Math.Sqrt((Value.Length * 8) - d);
        }

        public string EncryptText(string text)
        {
            if (text.Length > Value.Length)
            {
                throw new ArgumentException("Sequence length must be greater or equal to text length!");
            }

            var stringBuilder = new StringBuilder();
            for (var i = 0; i < text.Length; i++)
            {
                var encryptedByte = EncryptChar(text[i], Value[i]);
                BuildString(stringBuilder, encryptedByte);
            }

            return stringBuilder.ToString();
        }

        private static byte EncryptChar(char c, byte @byte)
        {
            return (byte) (c & @byte);
        }

        private static void BuildString(StringBuilder stringBuilder, byte @byte)
        {
            for (var i = 0; i < 8; i++)
            {
                var _byte = (int) @byte;
                var bit = _byte & (1 << i);
                stringBuilder.Append(bit != 0 ? '1' : '0');
            }
        }

        public int this[int key]
        {
            get
            {
                var bytePosition = key / 8;
                var bitPosition = key % 8;

                var bit = (Value[bytePosition] >> bitPosition) & 1;
                return bit;
            }
        }

        public override string ToString()
        {
            var str = new StringBuilder();

            foreach (var @byte in Value)
            {
                BuildString(str, @byte);
                str.Append(' '); 
            }

            return str.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
