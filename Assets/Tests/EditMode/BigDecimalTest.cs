#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
namespace SEF.Test {

    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using System.Numerics;
    using Data;

    public class BigDecimalTest
    {

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }


        [Test]
        public void BigDecimalTest_BigDecimal_Value_Positive()
        {
            var value = new BigDecimal(0.12345f);
            Debug.Log(value.GetDecimalValue());
        }

        [Test]
        public void BigDecimalTest_BigDecimal_Value_Negative()
        {
            var value = new BigDecimal(-0.12345f);
            Debug.Log(value);
            Debug.Log(value.GetDecimalValue());
        }

        //[Test]
        //public void BigDecimalTest_BigDecimal_Add()
        //{
        //    var value1 = new BigDecimal(0.12345f);
        //    var value2 = new BigDecimal(0.12345f);
        //    var result1 = value1 + value2;
        //    Debug.Log(result1.GetDecimalValue());
        //    Assert.IsTrue(result1.GetDecimalValue() == (decimal)0.24690);

        //    var value3 = new BigDecimal(0.12345f);
        //    var value4 = new BigDecimal(0.123f);
        //    var result2 = value3 + value4;
        //    Debug.Log(result2.GetDecimalValue());
        //    Assert.IsTrue(result2.GetDecimalValue() == (decimal)0.24645);

        //    var value5 = new BigDecimal(0.123f);
        //    var value6 = new BigDecimal(0.12345f);
        //    var result3 = value5 + value6;
        //    Debug.Log(result3.GetDecimalValue());
        //    Assert.IsTrue(result3.GetDecimalValue() == (decimal)0.24645);

        //    var value7 = new BigDecimal(1f);
        //    var value8 = new BigDecimal(0.1234567f);
        //    var result4 = value7 + value8;
        //    Debug.Log(result4.GetDecimalValue());
        //    Assert.IsTrue(result4.GetDecimalValue() == (decimal)1.12346);

        //    var value9 = new BigDecimal(0.0000001f);
        //    var value10 = new BigDecimal(0.9999999f);
        //    var result5 = value9 + value10;
        //    Debug.Log(result5.GetDecimalValue());
        //    Assert.IsTrue(result5.GetDecimalValue() == (decimal)1);

        //}

        //[Test]
        //public void BigDecimalTest_BigDecimal_Subject()
        //{
        //    var value1 = new BigDecimal(0.12345f);
        //    var value2 = new BigDecimal(0.12345f);
        //    var result1 = value1 - value2;
        //    Debug.Log(result1.GetDecimalValue());
        //    Assert.IsTrue(result1.GetDecimalValue() == (decimal)0);

        //    var value3 = new BigDecimal(0.12345f);
        //    var value4 = new BigDecimal(0.123f);
        //    var result2 = value3 - value4;
        //    Debug.Log(result2.GetDecimalValue());
        //    Assert.IsTrue(result2.GetDecimalValue() == (decimal)0.00045);

        //    var value5 = new BigDecimal(0.123f);
        //    var value6 = new BigDecimal(0.12345f);
        //    var result3 = value5 - value6;
        //    Debug.Log(result3.GetDecimalValue());
        //    Assert.IsTrue(result3.GetDecimalValue() == (decimal)-0.00045);

        //    var value7 = new BigDecimal(1f);
        //    var value8 = new BigDecimal(0.1234567f);
        //    var result4 = value7 - value8;
        //    Debug.Log(result4.GetDecimalValue());
        //    Assert.IsTrue(result4.GetDecimalValue() == (decimal)0.87654);

        //    var value9 = new BigDecimal(0.0000001f);
        //    var value10 = new BigDecimal(0.9999999f);
        //    var result5 = value9 - value10;
        //    Debug.Log(result5.GetDecimalValue());
        //    Assert.IsTrue(result5.GetDecimalValue() == (decimal)-1);

        //}

        //[Test]
        //public void BigDecimalTest_BigDecimal_Multiply()
        //{
        //    var value1 = new BigDecimal(0.12345f);
        //    var value2 = new BigDecimal(0.12345f);
        //    var result1 = value1 * value2;
        //    Debug.Log(result1.GetDecimalValue());
        //    Assert.IsTrue(result1.GetDecimalValue() == (decimal)0.01524);

        //    var value3 = new BigDecimal(0.12345f);
        //    var value4 = new BigDecimal(0.123f);
        //    var result2 = value3 * value4;
        //    Debug.Log(result2.GetDecimalValue());
        //    Assert.IsTrue(result2.GetDecimalValue() == (decimal)0.01518);

        //    var value5 = new BigDecimal(0.123f);
        //    var value6 = new BigDecimal(0.12345f);
        //    var result3 = value5 * value6;
        //    Debug.Log(result3.GetDecimalValue());
        //    Assert.IsTrue(result3.GetDecimalValue() == (decimal)0.01518);

        //    var value7 = new BigDecimal(1f);
        //    var value8 = new BigDecimal(0.1234567f);
        //    var result4 = value7 * value8;
        //    Debug.Log(result4.GetDecimalValue());
        //    Assert.IsTrue(result4.GetDecimalValue() == (decimal)0.12346f);

        //    var value9 = new BigDecimal(0.0000001f);
        //    var value10 = new BigDecimal(0.9999999f);
        //    var result5 = value9 * value10;
        //    Debug.Log(result5.GetDecimalValue());
        //    Assert.IsTrue(result5.GetDecimalValue() == (decimal)0);
        //}
        //[Test]
        //public void BigDecimalTest_BigDecimal_Divide()
        //{
        //    var value1 = new BigDecimal(0.12345f);
        //    var value2 = new BigDecimal(0.12345f);
        //    var result1 = value1 / value2;
        //    Debug.Log(result1.GetDecimalValue());
        //    Assert.IsTrue(result1.GetDecimalValue() == (decimal)1);

        //    var value3 = new BigDecimal(0.12345f);
        //    var value4 = new BigDecimal(0.123f);
        //    var result2 = value3 / value4;
        //    Debug.Log(result2.GetDecimalValue());
        //    Assert.IsTrue(result2.GetDecimalValue() == (decimal)1.00365);

        //    var value5 = new BigDecimal(0.123f);
        //    var value6 = new BigDecimal(0.12345f);
        //    var result3 = value5 / value6;
        //    Debug.Log(result3.GetDecimalValue());
        //    Assert.IsTrue(result3.GetDecimalValue() == (decimal)0.99635);

        //    var value7 = new BigDecimal(1f);
        //    var value8 = new BigDecimal(0.1234567f);
        //    var result4 = value7 / value8;
        //    Debug.Log(result4.GetDecimalValue());
        //    Assert.IsTrue(result4.GetDecimalValue() == (decimal)8.1);

        //    var value9 = new BigDecimal(0.0000001f);
        //    var value10 = new BigDecimal(0.9999999f);
        //    var result5 = value9 / value10;
        //    Debug.Log(result5.GetDecimalValue());
        //    Assert.IsTrue(result5.GetDecimalValue() == (decimal)0);
        //}

        //[Test]
        //public void BigDecimalTest_BigDecimal_Moduler()
        //{
        //    var value1 = new BigDecimal(0.12345f);
        //    var value2 = new BigDecimal(0.12345f);
        //    var result1 = value1 % value2;
        //    Debug.Log(result1.GetDecimalValue());
        //    Assert.IsTrue(result1.GetDecimalValue() == (decimal)0);

        //    var value3 = new BigDecimal(0.12345f);
        //    var value4 = new BigDecimal(0.123f);
        //    var result2 = value3 % value4;
        //    Debug.Log(result2.GetDecimalValue());
        //    Assert.IsTrue(result2.GetDecimalValue() == (decimal)0.00045);

        //    var value5 = new BigDecimal(0.123f);
        //    var value6 = new BigDecimal(0.12345f);
        //    var result3 = value5 % value6;
        //    Debug.Log(result3.GetDecimalValue());
        //    Assert.IsTrue(result3.GetDecimalValue() == (decimal)0.123);

        //    var value7 = new BigDecimal(1f);
        //    var value8 = new BigDecimal(0.1234567f);
        //    var result4 = value7 % value8;
        //    Debug.Log(result4.GetDecimalValue());
        //    Assert.IsTrue(result4.GetDecimalValue() == (decimal)0.01235);

        //    var value9 = new BigDecimal(0.0000001f);
        //    var value10 = new BigDecimal(0.9999999f);
        //    var result5 = value9 % value10;
        //    Debug.Log(result5.GetDecimalValue());
        //    Assert.IsTrue(result5.GetDecimalValue() == (decimal)0.00000);
        //}

        //[Test]
        //public void BigDecimalTest_BigDecimal_Equal_NotEqual()
        //{
        //    var value1 = new BigDecimal(0.12345f);
        //    var value2 = new BigDecimal(0.12345f);
        //    var result1 = value1 == value2;
        //    Debug.Log(result1);
        //    Assert.IsTrue(result1);

        //    var value3 = new BigDecimal(0.12345f);
        //    var value4 = new BigDecimal(0.123f);
        //    var result2 = value3 == value4;
        //    Debug.Log(result2);
        //    Assert.IsFalse(result2);

        //    var value5 = new BigDecimal(0.12345f);
        //    var value6 = new BigDecimal(0.12345f);
        //    var result3 = value5 != value6;
        //    Debug.Log(result3);
        //    Assert.IsFalse(result3);

        //    var value7 = new BigDecimal(0.12345f);
        //    var value8 = new BigDecimal(0.123f);
        //    var result4 = value7 != value8;
        //    Debug.Log(result4);
        //    Assert.IsTrue(result4);
        //}

        //[Test]
        //public void BigDecimalTest_BigDecimal_Greater_GreaterThan()
        //{
        //    var value1 = new BigDecimal(0.12345f);
        //    var value2 = new BigDecimal(0.12345f);
        //    var result1 = value1 > value2;
        //    Debug.Log(result1);
        //    Assert.IsTrue(result1);

        //    var value3 = new BigDecimal(0.12345f);
        //    var value4 = new BigDecimal(0.123f);
        //    var result2 = value3 > value4;
        //    Debug.Log(result2);
        //    Assert.IsFalse(result2);

        //    var value5 = new BigDecimal(0.12345f);
        //    var value6 = new BigDecimal(0.12345f);
        //    var result3 = value5 >= value6;
        //    Debug.Log(result3);
        //    Assert.IsFalse(result3);

        //    var value7 = new BigDecimal(0.12345f);
        //    var value8 = new BigDecimal(0.123f);
        //    var result4 = value7 >= value8;
        //    Debug.Log(result4);
        //    Assert.IsTrue(result4);
        //}

        //[Test]
        //public void BigDecimalTest_BigDecimal_Less_LessThan()
        //{
        //    var value1 = new BigDecimal(0.12345f);
        //    var value2 = new BigDecimal(0.12345f);
        //    var result1 = value1 < value2;
        //    Debug.Log(result1);
        //    Assert.IsTrue(result1);

        //    var value3 = new BigDecimal(0.12345f);
        //    var value4 = new BigDecimal(0.123f);
        //    var result2 = value3 < value4;
        //    Debug.Log(result2);
        //    Assert.IsFalse(result2);

        //    var value5 = new BigDecimal(0.12345f);
        //    var value6 = new BigDecimal(0.12345f);
        //    var result3 = value5 <= value6;
        //    Debug.Log(result3);
        //    Assert.IsFalse(result3);

        //    var value7 = new BigDecimal(0.12345f);
        //    var value8 = new BigDecimal(0.123f);
        //    var result4 = value7 <= value8;
        //    Debug.Log(result4);
        //    Assert.IsTrue(result4);
        //}

        private struct BigDecimalTestCase
        {
            public BigDecimal a;
            public BigDecimal b;
            public BigDecimal? answer;

            public BigDecimalTestCase(BigDecimal a, BigDecimal b, BigDecimal? answer)
            {
                this.a = a;
                this.b = b;
                this.answer = answer;
            }
        }


        private struct BigDecimalBooleanTestCase
        {
            public BigDecimal a;
            public BigDecimal b;
            public bool answer;

            public BigDecimalBooleanTestCase(BigDecimal a, BigDecimal b, bool answer)
            {
                this.a = a;
                this.b = b;
                this.answer = answer;
            }
        }


        [Test]
        public void BigDecimalTest_BigDecimal_Add()
        {
            BigDecimalTestCase[] testList = new BigDecimalTestCase[]
            {
                new BigDecimalTestCase(new BigDecimal(98765), new BigDecimal(98765), new BigDecimal(197530)),
                new BigDecimalTestCase(new BigDecimal(987.65), new BigDecimal(987.65), new BigDecimal(1975.30)),
                new BigDecimalTestCase(new BigDecimal(987.65), new BigDecimal(0.98765), new BigDecimal(988.63765)),
                new BigDecimalTestCase(new BigDecimal(0.98765), new BigDecimal(987.65), new BigDecimal(988.63765)),
                new BigDecimalTestCase(new BigDecimal(0), new BigDecimal(0.98765), new BigDecimal(0.98765)),
                new BigDecimalTestCase(new BigDecimal(0.98765), new BigDecimal(0), new BigDecimal(0.98765)),
            };

            for(int i = 0; i < testList.Length; i++)
            {
                var tCase = testList[i];
                var answer = tCase.a + tCase.b;
                Debug.Log(tCase.answer.Value.GetDecimalValue() + " " + answer.GetDecimalValue());
                Assert.IsTrue(tCase.answer.Value == answer);
            }
        }


        [Test]
        public void BigDecimalTest_BigDecimal_Subject()
        {
            BigDecimalTestCase[] testList = new BigDecimalTestCase[]
            {
                new BigDecimalTestCase(new BigDecimal(98765), new BigDecimal(88888), new BigDecimal(9877)),
                new BigDecimalTestCase(new BigDecimal(987.65), new BigDecimal(888.88), new BigDecimal(98.77)),
                new BigDecimalTestCase(new BigDecimal(987.65), new BigDecimal(0.88888), new BigDecimal(986.76112)),
                new BigDecimalTestCase(new BigDecimal(0.98765), new BigDecimal(888.88), new BigDecimal(-887.89235)),
                new BigDecimalTestCase(new BigDecimal(0), new BigDecimal(0.88888), new BigDecimal(-0.88888)),
                new BigDecimalTestCase(new BigDecimal(0.88888), new BigDecimal(0), new BigDecimal(0.88888)),
            };

            for (int i = 0; i < testList.Length; i++)
            {
                var tCase = testList[i];
                var answer = tCase.a - tCase.b;
                Debug.Log(tCase.answer.Value.GetDecimalValue() + " " + answer.GetDecimalValue());
                Assert.IsTrue(tCase.answer.Value == answer);
            }
        }


        [Test]
        public void BigDecimalTest_BigDecimal_Multifly()
        {
            BigDecimalTestCase[] testList = new BigDecimalTestCase[]
            {
                new BigDecimalTestCase(new BigDecimal(98765), new BigDecimal(88888), new BigDecimal(8779023320)),
                new BigDecimalTestCase(new BigDecimal(987.65), new BigDecimal(888.88), new BigDecimal(877902.332)),
                new BigDecimalTestCase(new BigDecimal(987.65), new BigDecimal(0.88888), new BigDecimal(877.902332)),
                new BigDecimalTestCase(new BigDecimal(0.98765), new BigDecimal(888.88), new BigDecimal(877.902332)),
                new BigDecimalTestCase(new BigDecimal(0), new BigDecimal(0.88888), new BigDecimal(0)),
                new BigDecimalTestCase(new BigDecimal(0.88888), new BigDecimal(0), new BigDecimal(0)),
            };

            for (int i = 0; i < testList.Length; i++)
            {
                var tCase = testList[i];
                var answer = tCase.a * tCase.b;
                Debug.Log(tCase.answer.Value.GetDecimalValue() + " " + answer.GetDecimalValue());
                Assert.IsTrue(tCase.answer.Value == answer);
            }
        }


        [Test]
        public void BigDecimalTest_BigDecimal_Division()
        {
            BigDecimalTestCase[] testList = new BigDecimalTestCase[]
            {
                new BigDecimalTestCase(new BigDecimal(98765), new BigDecimal(88888), new BigDecimal(1.1111173611)),
                new BigDecimalTestCase(new BigDecimal(987.65), new BigDecimal(888.88), new BigDecimal(1.1111173611)),
                new BigDecimalTestCase(new BigDecimal(987.65), new BigDecimal(0.88888), new BigDecimal(1111.1173611736)),
                new BigDecimalTestCase(new BigDecimal(0.98765), new BigDecimal(888.88), new BigDecimal(0.0011111173)),
                new BigDecimalTestCase(new BigDecimal(0), new BigDecimal(0.88888), new BigDecimal(0)),
                new BigDecimalTestCase(new BigDecimal(0.88888), new BigDecimal(0), null),
            };

            for (int i = 0; i < testList.Length; i++)
            {
                try
                {
                    var tCase = testList[i];
                    var answer = tCase.a / tCase.b;
                    Debug.Log(tCase.answer.Value.GetDecimalValue() + " " + answer.GetDecimalValue());
                    Assert.IsTrue(tCase.answer.Value == answer);
                }
                catch(System.DivideByZeroException e)
                {
                    Debug.Log(e.Message);
                }
            }
        }



        [Test]
        public void BigDecimalTest_BigDecimal_Modular()
        {
            BigDecimalTestCase[] testList = new BigDecimalTestCase[]
            {
                new BigDecimalTestCase(new BigDecimal(98765), new BigDecimal(88888), new BigDecimal(9877)),
                new BigDecimalTestCase(new BigDecimal(987.65), new BigDecimal(888.88), new BigDecimal(98.77)),
                new BigDecimalTestCase(new BigDecimal(987.65), new BigDecimal(0.88888), new BigDecimal(0.10432)),
                new BigDecimalTestCase(new BigDecimal(0.98765), new BigDecimal(888.88), new BigDecimal(0.98765)),
                new BigDecimalTestCase(new BigDecimal(0), new BigDecimal(0.88888), new BigDecimal(0)),
                new BigDecimalTestCase(new BigDecimal(0.88888), new BigDecimal(0), null),
            };

            for (int i = 0; i < testList.Length; i++)
            {
                try
                {
                    var tCase = testList[i];
                    var answer = tCase.a % tCase.b;
                    Debug.Log(tCase.answer.Value.GetDecimalValue() + " " + answer.GetDecimalValue());
                    Assert.IsTrue(tCase.answer.Value == answer);
                }
                catch (System.DivideByZeroException e)
                {
                    Debug.Log(e.Message);
                }
            }
        }


        [Test]
        public void BigDecimalTest_BigDecimal_Greater()
        {
            BigDecimalBooleanTestCase[] testList = new BigDecimalBooleanTestCase[]
            {
                new BigDecimalBooleanTestCase(new BigDecimal(98765), new BigDecimal(88888), true),
                new BigDecimalBooleanTestCase(new BigDecimal(88888), new BigDecimal(98765), false),
                new BigDecimalBooleanTestCase(new BigDecimal(98765), new BigDecimal(98765), false),
                new BigDecimalBooleanTestCase(new BigDecimal(987.65), new BigDecimal(888.88), true),
                new BigDecimalBooleanTestCase(new BigDecimal(987.65), new BigDecimal(0.88888), true),
                new BigDecimalBooleanTestCase(new BigDecimal(0.98765), new BigDecimal(888.88), false),
                new BigDecimalBooleanTestCase(new BigDecimal(0), new BigDecimal(0.88888), false),
                new BigDecimalBooleanTestCase(new BigDecimal(0.88888), new BigDecimal(0), true),
            };

            for (int i = 0; i < testList.Length; i++)
            {
                var tCase = testList[i];
                var answer = tCase.a > tCase.b;
                Debug.Log(tCase.answer + " " + answer);
                Assert.IsTrue(tCase.answer == answer);
            }
        }


        [Test]
        public void BigDecimalTest_BigDecimal_Less()
        {
            BigDecimalBooleanTestCase[] testList = new BigDecimalBooleanTestCase[]
            {
                new BigDecimalBooleanTestCase(new BigDecimal(98765), new BigDecimal(88888), false),
                new BigDecimalBooleanTestCase(new BigDecimal(88888), new BigDecimal(98765), true),
                new BigDecimalBooleanTestCase(new BigDecimal(98765), new BigDecimal(98765), false),
                new BigDecimalBooleanTestCase(new BigDecimal(987.65), new BigDecimal(888.88), false),
                new BigDecimalBooleanTestCase(new BigDecimal(987.65), new BigDecimal(0.88888), false),
                new BigDecimalBooleanTestCase(new BigDecimal(0.98765), new BigDecimal(888.88), true),
                new BigDecimalBooleanTestCase(new BigDecimal(0), new BigDecimal(0.88888), true),
                new BigDecimalBooleanTestCase(new BigDecimal(0.88888), new BigDecimal(0), false),
            };

            for (int i = 0; i < testList.Length; i++)
            {
                var tCase = testList[i];
                var answer = tCase.a < tCase.b;
                Debug.Log(tCase.answer + " " + answer);
                Assert.IsTrue(tCase.answer == answer);
            }
        }


        [Test]
        public void BigDecimalTest_BigDecimal_GreaterThan()
        {
            BigDecimalBooleanTestCase[] testList = new BigDecimalBooleanTestCase[]
            {
                new BigDecimalBooleanTestCase(new BigDecimal(98765), new BigDecimal(98765), true),
                new BigDecimalBooleanTestCase(new BigDecimal(987.65), new BigDecimal(987.65), true),
            };

            for (int i = 0; i < testList.Length; i++)
            {
                var tCase = testList[i];
                var answer = tCase.a >= tCase.b;
                Debug.Log(tCase.answer + " " + answer);
                Assert.IsTrue(tCase.answer == answer);
            }
        }



        [Test]
        public void BigDecimalTest_BigDecimal_LessThan()
        {
            BigDecimalBooleanTestCase[] testList = new BigDecimalBooleanTestCase[]
            {
                new BigDecimalBooleanTestCase(new BigDecimal(98765), new BigDecimal(98765), true),
                new BigDecimalBooleanTestCase(new BigDecimal(987.65), new BigDecimal(987.65), true),
            };

            for (int i = 0; i < testList.Length; i++)
            {
                var tCase = testList[i];
                var answer = tCase.a <= tCase.b;
                Debug.Log(tCase.answer + " " + answer);
                Assert.IsTrue(tCase.answer == answer);
            }
        }



        [Test]
        public void BigDecimalTest_BigDecimal_Equal()
        {
            BigDecimalBooleanTestCase[] testList = new BigDecimalBooleanTestCase[]
            {
                new BigDecimalBooleanTestCase(new BigDecimal(98765), new BigDecimal(98765), true),
                new BigDecimalBooleanTestCase(new BigDecimal(9876.5), new BigDecimal(987.65), false),
                new BigDecimalBooleanTestCase(new BigDecimal(987.65), new BigDecimal(987.65), true),
                new BigDecimalBooleanTestCase(new BigDecimal(98765), new BigDecimal(88888), false),
                new BigDecimalBooleanTestCase(new BigDecimal(987.65), new BigDecimal(888.88), false),
            };

            for (int i = 0; i < testList.Length; i++)
            {
                var tCase = testList[i];
                var answer = tCase.a == tCase.b;
                Debug.Log(tCase.answer + " " + answer);
                Assert.IsTrue(tCase.answer == answer);
            }
        }



        [Test]
        public void BigDecimalTest_BigDecimal_NotEqual()
        {
            BigDecimalBooleanTestCase[] testList = new BigDecimalBooleanTestCase[]
            {
                new BigDecimalBooleanTestCase(new BigDecimal(98765), new BigDecimal(98765), false),
                new BigDecimalBooleanTestCase(new BigDecimal(9876.5), new BigDecimal(987.65), true),
                new BigDecimalBooleanTestCase(new BigDecimal(987.65), new BigDecimal(987.65), false),
                new BigDecimalBooleanTestCase(new BigDecimal(98765), new BigDecimal(88888), true),
                new BigDecimalBooleanTestCase(new BigDecimal(987.65), new BigDecimal(888.88), true),
            };

            for (int i = 0; i < testList.Length; i++)
            {
                var tCase = testList[i];
                var answer = tCase.a != tCase.b;
                Debug.Log(tCase.answer + " " + answer);
                Assert.IsTrue(tCase.answer == answer);
            }
        }



        [Test]
        public void BigDecimalTest_BigDecimal_Increase()
        {
            BigDecimalBooleanTestCase[] testList = new BigDecimalBooleanTestCase[]
            {
                new BigDecimalBooleanTestCase(new BigDecimal(98765), new BigDecimal(98766), true),
                new BigDecimalBooleanTestCase(new BigDecimal(987.65), new BigDecimal(988.65), true),
            };

            for (int i = 0; i < testList.Length; i++)
            {
                var tCase = testList[i];
                tCase.a++;
                Debug.Log(tCase.a.GetDecimalValue() + " " + tCase.b.GetDecimalValue());
                Assert.IsTrue(tCase.a.GetDecimalValue() == tCase.b.GetDecimalValue());
            }
        }


        [Test]
        public void BigDecimalTest_BigDecimal_Decrease()
        {
            BigDecimalBooleanTestCase[] testList = new BigDecimalBooleanTestCase[]
            {
                new BigDecimalBooleanTestCase(new BigDecimal(98765), new BigDecimal(98764), true),
                new BigDecimalBooleanTestCase(new BigDecimal(987.65), new BigDecimal(986.65), true),
            };

            for (int i = 0; i < testList.Length; i++)
            {
                var tCase = testList[i];
                tCase.a--;
                Debug.Log(tCase.a.GetDecimalValue() + " " + tCase.b.GetDecimalValue());
                Assert.IsTrue(tCase.a.GetDecimalValue() == tCase.b.GetDecimalValue());
            }
        }
    }
}
#endif