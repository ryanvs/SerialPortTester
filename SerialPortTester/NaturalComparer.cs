// License - The Code Project Open License(CPOL)
// This article, along with any associated source code and files, is licensed under The Code Project Open License (CPOL)
// License: https://www.codeproject.com/info/cpol10.aspx
// Source: https://www.codeproject.com/Articles/22978/Implementing-the-NET-IComparer-interface-to-get-a

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace SerialPortTester
{
    /// <summary>
    /// Sorts strings containing mixed characters and numbers in a natural order.
    /// </summary>
    public class NaturalComparer : IComparer<string>, IComparer
    {
        private StringParser _parser1;
        private StringParser _parser2;
        private NaturalComparerOptions _options;

        private enum TokenType
        {
            Nothing,
            Numerical,
            String
        }

        private class StringParser
        {
            private TokenType _tokenType;
            private string _stringValue;
            private decimal _numericalValue;
            private int _index;
            private string _source;
            private int _length;
            private char _currentChar;
            private NaturalComparer _comparer;

            public StringParser(NaturalComparer comparer)
            {
                _comparer = comparer;
            }

            public void Init(string source)
            {
                if (source == null)
                    source = string.Empty;
                _source = source;
                _length = source.Length;
                _index = -1;
                _numericalValue = 0;
                NextChar();
                NextToken();
            }

            public TokenType TokenType
            {
                get { return _tokenType; }
            }

            public decimal NumericalValue
            {
                get
                {
                    if (_tokenType == TokenType.Numerical)
                    {
                        return _numericalValue;
                    }
                    else
                    {
                        throw new NaturalComparerException("Internal Error: NumericalValue called on a non numerical value.");
                    }
                }
            }

            public string StringValue
            {
                get { return _stringValue; }
            }

            public void NextToken()
            {
                do
                {
                    //CharUnicodeInfo.GetUnicodeCategory 
                    if (_currentChar == '\0')
                    {
                        _tokenType = TokenType.Nothing;
                        _stringValue = null;
                        return;
                    }
                    else if (char.IsDigit(_currentChar))
                    {
                        ParseNumericalValue();
                        return;
                    }
                    else if (char.IsLetter(_currentChar))
                    {
                        ParseString();
                        return;
                    }
                    else
                    {
                        //ignore this character and loop some more 
                        NextChar();
                    }
                }
                while (true);
            }

            private void NextChar()
            {
                _index += 1;
                if (_index >= _length)
                {
                    _currentChar = '\0';
                }
                else
                {
                    _currentChar = _source[_index];
                }
            }

            private void ParseNumericalValue()
            {
                int start = _index;
                char NumberDecimalSeparator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0];
                char NumberGroupSeparator = NumberFormatInfo.CurrentInfo.NumberGroupSeparator[0];
                do
                {
                    NextChar();
                    if (_currentChar == NumberDecimalSeparator)
                    {
                        // parse digits after the Decimal Separator 
                        do
                        {
                            NextChar();
                            if (!char.IsDigit(_currentChar) && _currentChar != NumberGroupSeparator)
                                break;

                        }
                        while (true);
                        break;
                    }
                    else
                    {
                        if (!char.IsDigit(_currentChar) && _currentChar != NumberGroupSeparator)
                            break;
                    }
                }
                while (true);

                _stringValue = _source.Substring(start, _index - start);
                if (decimal.TryParse(_stringValue, out _numericalValue))
                {
                    _tokenType = TokenType.Numerical;
                }
                else
                {
                    // We probably have a too long value 
                    _tokenType = TokenType.String;
                }
            }

            private void ParseString()
            {
                int start = _index;
                bool roman = (_comparer._options & NaturalComparerOptions.RomanNumbers) != 0;
                int romanValue = 0;
                int lastRoman = int.MaxValue;
                int cptLastRoman = 0;

                do
                {
                    if (roman)
                    {
                        int thisRomanValue = RomanLetterValue(_currentChar);
                        if (thisRomanValue > 0)
                        {
                            bool handled = false;

                            if ((thisRomanValue == 1 || thisRomanValue == 10 || thisRomanValue == 100))
                            {
                                NextChar();
                                int nextRomanValue = RomanLetterValue(_currentChar);
                                if (nextRomanValue == thisRomanValue * 10 | nextRomanValue == thisRomanValue * 5)
                                {
                                    handled = true;
                                    if (nextRomanValue <= lastRoman)
                                    {
                                        romanValue += nextRomanValue - thisRomanValue;
                                        NextChar();
                                        lastRoman = thisRomanValue / 10;
                                        cptLastRoman = 0;
                                    }
                                    else
                                    {
                                        roman = false;
                                    }
                                }
                            }
                            else
                            {
                                NextChar();
                            }
                            if (!handled)
                            {
                                if (thisRomanValue <= lastRoman)
                                {
                                    romanValue += thisRomanValue;
                                    if (lastRoman == thisRomanValue)
                                    {
                                        cptLastRoman += 1;
                                        switch (thisRomanValue)
                                        {
                                            case 1:
                                            case 10:
                                            case 100:
                                                if (cptLastRoman > 4)
                                                    roman = false;

                                                break;
                                            case 5:
                                            case 50:
                                            case 500:
                                                if (cptLastRoman > 1)
                                                    roman = false;

                                                break;
                                        }
                                    }
                                    else
                                    {
                                        lastRoman = thisRomanValue;
                                        cptLastRoman = 1;
                                    }
                                }
                                else
                                {
                                    roman = false;
                                }
                            }
                        }
                        else
                        {
                            roman = false;
                        }
                    }
                    else
                    {
                        NextChar();
                    }
                    if (!char.IsLetter(_currentChar)) break;
                }
                while (true);
                _stringValue = _source.Substring(start, _index - start);
                if (roman)
                {
                    _numericalValue = romanValue;
                    _tokenType = TokenType.Numerical;
                }
                else
                {
                    _tokenType = TokenType.String;
                }
            }

        }

        public NaturalComparer(NaturalComparerOptions options)
        {
            _options = options;
            _parser1 = new StringParser(this);
            _parser2 = new StringParser(this);
        }

        public NaturalComparer()
           : this(NaturalComparerOptions.Default)
        { }

        int System.Collections.Generic.IComparer<string>.Compare(string string1, string string2)
        {
            _parser1.Init(string1);
            _parser2.Init(string2);
            int result;

            do
            {
                if (_parser1.TokenType == TokenType.Numerical & _parser2.TokenType == TokenType.Numerical)
                {
                    // both string1 and string2 are numerical 
                    result = decimal.Compare(_parser1.NumericalValue, _parser2.NumericalValue);
                }
                else
                {
                    result = string.Compare(_parser1.StringValue, _parser2.StringValue);
                }
                if (result != 0)
                {
                    return result;
                }
                else
                {
                    _parser1.NextToken();
                    _parser2.NextToken();
                }
            }
            while (!(_parser1.TokenType == TokenType.Nothing & _parser2.TokenType == TokenType.Nothing));
            //identical 
            return 0;
        }

        private static int RomanLetterValue(char c)
        {
            switch (c)
            {
                case 'I':
                    return 1;
                case 'V':
                    return 5;
                case 'X':
                    return 10;
                case 'L':
                    return 50;
                case 'C':
                    return 100;
                case 'D':
                    return 500;
                case 'M':
                    return 1000;
                default:
                    return 0;
            }
        }

        public int RomanValue(string string1)
        {
            _parser1.Init(string1);

            if (_parser1.TokenType == TokenType.Numerical)
            {
                return (int)_parser1.NumericalValue;
            }
            else
            {
                return 0;
            }
        }

        int IComparer.Compare(object x, object y)
        {
            return ((System.Collections.Generic.IComparer<string>)this).Compare((string)x, (string)y);
        }
    }

    public class NaturalComparerException : Exception
    {
        public NaturalComparerException(string msg)
           : base(msg)
        { }
    }

    [Flags]
    public enum NaturalComparerOptions
    {
        None,
        RomanNumbers,
        //DecimalValues <- we could put this as an option 
        //IgnoreSpaces <- we could put this as an option 
        //IgnorePunctuation <- we could put this as an option 
        Default = None
    }
}
