using System;
using System.Text;

namespace Interview
{
    public class BigNumber
    {
        private LinkedList<int> digits;
        bool isNegative;

        private BigNumber(LinkedList<int> digits, bool isNegative)
        {
            this.digits = digits;
            this.isNegative = isNegative;
        }

        private BigNumber(string str)
        {
            digits = ConvertToList(str);
        }

        static LinkedList<int> ConvertToList(string number)
        {
            var num = new LinkedList<int>();

            if (number[0] == '0') throw new InvalidOperationException();

            while (number.Count() > 0)
            {
                if (!Char.IsNumber(number[0])) throw new InvalidOperationException();
                num.AddLast(int.Parse(number[0].ToString()));
                number = number.Remove(0, 1);
            }

            if (num.First() == '-') throw new InvalidOperationException();

            return num;
        }

        public static BigNumber operator +(BigNumber num1, BigNumber num2)
        {
            var maxNum = new LinkedList<int>();
            var minNum = new LinkedList<int>();
            var sumNum = new LinkedList<int>();
            string sum = null;
            int remains = 0;
            int last;
            if (num1.digits.Count() >= num2.digits.Count())
            {
                maxNum = num1.digits;
                minNum = num2.digits;
            }
            else
            {
                maxNum = num2.digits;
                minNum = num1.digits;
            }

            while (minNum.Count() > 0)
            {
                last = maxNum.Last.Value + minNum.Last.Value + remains;
                remains = 0;
                maxNum.RemoveLast();
                minNum.RemoveLast();
                if (last > 9)
                {
                    remains = last / 10;
                    last %= 10;
                }

                sumNum.AddFirst(last);
            }

            while (maxNum.Count() > 0)
            {
                if (remains != 0)
                {
                    if (maxNum.Count() == 1)
                    {
                        sumNum.AddFirst(0);
                        sumNum.AddFirst(remains);
                        maxNum.RemoveLast();
                        break;
                    }
                    last = remains + maxNum.Last.Value;
                    if (last > 9)
                    {
                        remains = last / 10;
                        last %= 10;
                    }
                    sumNum.AddFirst(last);
                    maxNum.RemoveLast();
                }
                else
                {
                    sumNum.AddFirst(maxNum.Last.Value);
                    maxNum.RemoveLast();
                }
            }

            foreach (var e in sumNum)
                sum += e.ToString();

            return new BigNumber(sumNum);
        }
    }
}