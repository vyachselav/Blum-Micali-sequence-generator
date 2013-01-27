using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RandomGenerator.Annotations;
using RandomGenerator.Models;

namespace RandomGenerator
{
    public sealed class ViewModel : INotifyPropertyChanged
    {
        private BlumMicaliSequenceGenerator _blumMicaliGenerator;
        public BlumMicaliSequenceGenerator BlumMicaliGenerator
        {
            get { return _blumMicaliGenerator; }
            set
            {
                _blumMicaliGenerator = value;
                NotifyPropertyChanged();
            }
        }

        private BuiltInSequenceGenerator _builtInGenerator;
        public BuiltInSequenceGenerator BuiltInGenerator
        {
            get { return _builtInGenerator; }
            set
            {
                _builtInGenerator = value;
                NotifyPropertyChanged();
            }
        }


        private ObservableCollection<Sequence> _sequences;
        public ObservableCollection<Sequence> Sequences
        {
            get { return _sequences; }
            set
            {
                _sequences = value;
                NotifyPropertyChanged();

            }
        }
        

        private uint _count;
        public uint Count
        {
            get { return _count; }
            set
            {
                _count = value;
                NotifyPropertyChanged();
            }
        }

        private uint _length;
        public uint Length
        {
            get { return _length; }
            set
            {
                _length = value;
                NotifyPropertyChanged();
            }
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
        
        

        private double _singleBitsTest;
        public double SingleBitsTest
        {
            get { return _singleBitsTest; }
            set
            {
                _singleBitsTest = value;
                NotifyPropertyChanged();
            }
        }

        private double _bitsPairsTest;
        public double BitsPairsTest
        {
            get { return _bitsPairsTest; }
            set
            {
                _bitsPairsTest = value;
                NotifyPropertyChanged();
            }
        }

        private double _pokerTest;
        public double PokerTest
        {
            get { return _pokerTest; }
            set
            {
                _pokerTest = value;
                NotifyPropertyChanged();
            }
        }

        private double _seriesTest;
        public double SeriesTest
        {
            get { return _seriesTest; }
            set
            {
                _seriesTest = value;
                NotifyPropertyChanged();
            }
        }

        private double _longSeriesTest;
        public double CorrelationTest
        {
            get { return _longSeriesTest; }
            set
            {
                _longSeriesTest = value;
                NotifyPropertyChanged();
            }
        }

        private string _plainText;
        public string PlainText
        {
            get { return _plainText; }
            set
            {
                _plainText = value;
                NotifyPropertyChanged();
            }
        }

        private string _cipherText;
        public string CipherText
        {
            get { return _cipherText; }
            set
            {
                _cipherText = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
