﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MBF.Matrix;
using MBF.TestAutomation.Util;
using MBF.Util;
using MBF.Util.Logging;
using NUnit.Framework;

namespace MBF.TestAutomation.Matrix
{
    /// <summary>
    /// Bvt test cases to confirm the features of Dense Matrix
    /// </summary>
    [TestFixture]
    public class DenseMatrixBvtTestCases
    {

        #region Constructor

        /// <summary>
        /// Static constructor to open log and make other settings needed for test
        /// </summary>
        static DenseMatrixBvtTestCases()
        {
            Trace.Set(Trace.SeqWarnings);
            if (!ApplicationLog.Ready)
            {
                ApplicationLog.Open("mbf.automation.log");
            }

            Utility._xmlUtil = new XmlUtility(@"TestUtils\MatrixTestsConfig.xml");
        }

        #endregion

        #region Test Cases

        /// <summary>
        /// Creates a Dense matrix instance with the help of default constructor
        /// Input : Valid values for DenseMatrix
        /// Validation : Proper instance of DenseMatrix
        /// </summary>
        [Test]
        public void ValidateDenseMatrixConstructor()
        {
            ValidateConstructorTestCases(false);
        }

        /// <summary>
        /// Creates a Dense matrix instance with the help of ref constructor
        /// Input : Valid values for DenseMatrix
        /// Validation : Proper instance of DenseMatrix
        /// </summary>
        [Test]
        public void ValidateDenseMatrixRefConstructor()
        {
            ValidateConstructorTestCases(true);
        }

        /// <summary>
        /// Creates a Dense Ansi
        /// Input : Valid values for DenseMatrix
        /// Validation : Dense Ansi Object
        /// </summary>
        [Test]
        public void ValidateDenseMatrixAsDenseAnsi()
        {
            DenseMatrix<string, string, double> denseMatObj = GetDenseMatrix();

            ParallelOptions parallelOptObj = new ParallelOptions();

            DenseAnsi denseAnsiObj = denseMatObj.AsDenseAnsi<double>(parallelOptObj);

            // Validate all properties of Ansi and DenseMatrix
            Assert.AreEqual(denseMatObj.ColCount, denseAnsiObj.ColCount);
            Assert.AreEqual(denseMatObj.ColKeys, denseAnsiObj.ColKeys);
            Assert.AreEqual(denseMatObj.IndexOfColKey, denseAnsiObj.IndexOfColKey);
            Assert.AreEqual(denseMatObj.IndexOfRowKey, denseAnsiObj.IndexOfRowKey);
            Assert.AreEqual("?", denseAnsiObj.MissingValue.ToString());
            Assert.AreEqual(denseMatObj.RowCount, denseAnsiObj.RowCount);
            Assert.AreEqual(denseMatObj.RowKeys, denseAnsiObj.RowKeys);
            Assert.AreEqual(Utility._xmlUtil.GetTextValue(
                Constants.SimpleMatrixNodeName,
                Constants.DenseAnsiStringNode),
                denseAnsiObj.ToString2D().Replace("\r", "").Replace("\n", "").Replace("\t", ""));

            Console.WriteLine(
                "DenseMatrix BVT : Validation of AsDenseAnsi() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of AsDenseAnsi() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix from DenseMatrix
        /// Input : Valid values for DenseMatrix
        /// Validation : Dense Matrix Object
        /// </summary>
        [Test]
        public void ValidateDenseMatrixAsDenseMatrix()
        {
            DenseMatrix<string, string, double> originalDenseMatObj = GetDenseMatrix();

            DenseMatrix<string, string, double> denseMatObj =
                originalDenseMatObj.AsDenseMatrix<string, string, double>();

            // Validate all properties of Ansi and DenseMatrix
            Assert.AreEqual(originalDenseMatObj.ColCount, denseMatObj.ColCount);
            Assert.AreEqual(originalDenseMatObj.ColKeys, denseMatObj.ColKeys);
            Assert.AreEqual(originalDenseMatObj.IndexOfColKey, denseMatObj.IndexOfColKey);
            Assert.AreEqual(originalDenseMatObj.IndexOfRowKey, denseMatObj.IndexOfRowKey);
            Assert.AreEqual(originalDenseMatObj.MissingValue, denseMatObj.MissingValue);
            Assert.AreEqual(originalDenseMatObj.RowCount, denseMatObj.RowCount);
            Assert.AreEqual(originalDenseMatObj.RowKeys, denseMatObj.RowKeys);
            Assert.AreEqual(Utility._xmlUtil.GetTextValue(
                Constants.SimpleMatrixNodeName,
                Constants.DenseMatrixStringNode),
                denseMatObj.ToString2D().Replace("\r", "").Replace("\n", "").Replace("\t", ""));

            Console.WriteLine(
                "DenseMatrix BVT : Validation of AsDenseMatrix() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of AsDenseMatrix() method successful");
        }

        /// <summary>
        /// Creates a Dense padded double object
        /// Input : Valid values for DenseMatrix
        /// Validation : Padded Double object
        /// </summary>
        [Test]
        public void ValidateDenseMatrixPaddedDouble()
        {
            DenseMatrix<string, string, double> denseMatObj = GetDenseMatrix();

            ParallelOptions parallelOptObj = new ParallelOptions();

            PaddedDouble padDoub = denseMatObj.AsPaddedDouble(parallelOptObj);

            // Validate all properties of Ansi and DenseMatrix
            Assert.AreEqual(denseMatObj.ColCount, padDoub.ColCount);
            Assert.AreEqual(denseMatObj.ColKeys, padDoub.ColKeys);
            Assert.AreEqual(denseMatObj.IndexOfColKey, padDoub.IndexOfColKey);
            Assert.AreEqual(denseMatObj.IndexOfRowKey, padDoub.IndexOfRowKey);
            Assert.AreEqual(denseMatObj.MissingValue, padDoub.MissingValue);
            Assert.AreEqual(denseMatObj.RowCount, padDoub.RowCount);
            Assert.AreEqual(denseMatObj.RowKeys, padDoub.RowKeys);
            Assert.AreEqual(Utility._xmlUtil.GetTextValue(
                Constants.SimpleMatrixNodeName,
                Constants.DenseMatrixStringNode),
                padDoub.ToString2D().Replace("\r", "").Replace("\n", "").Replace("\t", ""));

            Console.WriteLine(
                "DenseMatrix BVT : Validation of PaddedDouble() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of PaddedDouble() method successful");
        }

        /// <summary>
        /// Creates a Sparse matrix object
        /// Input : Valid values for DenseMatrix
        /// Validation : Sparse Matrix object
        /// </summary>
        [Test]
        public void ValidateDenseMatrixAsSparseMatrix()
        {
            DenseMatrix<string, string, double> denseMatObj = GetDenseMatrix();

            SparseMatrix<string, string, double> sparseMatrixObj = denseMatObj.AsSparseMatrix<string, string, double>();

            // Validate all properties of Ansi and DenseMatrix
            Assert.AreEqual(denseMatObj.ColCount, sparseMatrixObj.ColCount);
            Assert.AreEqual(denseMatObj.ColKeys, sparseMatrixObj.ColKeys);
            Assert.AreEqual(denseMatObj.IndexOfColKey, sparseMatrixObj.IndexOfColKey);
            Assert.AreEqual(denseMatObj.IndexOfRowKey, sparseMatrixObj.IndexOfRowKey);
            Assert.AreEqual(denseMatObj.MissingValue, sparseMatrixObj.MissingValue);
            Assert.AreEqual(denseMatObj.RowCount, sparseMatrixObj.RowCount);
            Assert.AreEqual(denseMatObj.RowKeys, sparseMatrixObj.RowKeys);
            Assert.AreEqual(Utility._xmlUtil.GetTextValue(
                Constants.SimpleMatrixNodeName,
                Constants.DenseMatrixStringNode),
                sparseMatrixObj.ToString2D().Replace("\r", "").Replace("\n", "").Replace("\t", ""));

            Console.WriteLine(
                "DenseMatrix BVT : Validation of AsSparseMatrix() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of AsSparseMatrix() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix with Col View Index
        /// Input : Valid values for DenseMatrix
        /// Validation : Dictionary of Col View
        /// </summary>
        [Test]
        public void ValidateDenseMatrixColViewIndex()
        {
            DenseMatrix<string, string, double> denseMatObj = GetDenseMatrix();

            IDictionary<string, double> colView = denseMatObj.ColView(0);

            for (int i = 0; i < denseMatObj.RowKeys.Count; i++)
            {
                Assert.AreEqual(colView["R" + i.ToString()], denseMatObj[i, 0]);
            }

            Console.WriteLine(
                "DenseMatrix BVT : Validation of ColView(index) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of ColView(index) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix with Col View Key
        /// Input : Valid values for DenseMatrix
        /// Validation : Dictionary of Col View
        /// </summary>
        [Test]
        public void ValidateDenseMatrixColViewKey()
        {
            DenseMatrix<string, string, double> denseMatObj = GetDenseMatrix();

            IDictionary<string, double> colView = denseMatObj.ColView("C0");

            for (int i = 0; i < denseMatObj.RowKeys.Count; i++)
            {
                Assert.AreEqual(colView["R" + i.ToString()], denseMatObj[i, 0]);
            }

            Console.WriteLine(
                "DenseMatrix BVT : Validation of ColView(key) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of ColView(key) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix with Row View Index
        /// Input : Valid values for DenseMatrix
        /// Validation : Dictionary of Row View
        /// </summary>
        [Test]
        public void ValidateDenseMatrixRowViewIndex()
        {
            DenseMatrix<string, string, double> denseMatObj = GetDenseMatrix();

            IDictionary<string, double> rowView = denseMatObj.RowView(0);

            for (int i = 0; i < denseMatObj.ColKeys.Count; i++)
            {
                Assert.AreEqual(rowView["C" + i.ToString()], denseMatObj[0, i]);
            }

            Console.WriteLine(
                "DenseMatrix BVT : Validation of RowView(index) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of RowView(index) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix with Row View Index
        /// Input : Valid values for DenseMatrix
        /// Validation : Dictionary of Row View
        /// </summary>
        [Test]
        public void ValidateDenseMatrixRowViewKey()
        {
            DenseMatrix<string, string, double> denseMatObj = GetDenseMatrix();

            IDictionary<string, double> rowView = denseMatObj.RowView("R0");

            for (int i = 0; i < denseMatObj.ColKeys.Count; i++)
            {
                Assert.AreEqual(rowView["C" + i.ToString()], denseMatObj[0, i]);
            }

            Console.WriteLine(
                "DenseMatrix BVT : Validation of RowView(key) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of RowView(key) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates ContainsColKey
        /// method
        /// Input : Valid values for DenseMatrix
        /// Validation : Contains Column Key
        /// </summary>
        [Test]
        public void ValidateDenseMatrixContainsColKey()
        {
            DenseMatrix<string, string, double> denseMatObj = GetDenseMatrix();
            Assert.IsTrue(denseMatObj.ContainsColKey("C0"));

            Console.WriteLine(
                "DenseMatrix BVT : Validation of ContainsColKey() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of ContainsColKey() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates ContainsRowAndColKeys
        /// method
        /// Input : Valid values for DenseMatrix
        /// Validation : Contains Row And Col Keys
        /// </summary>
        [Test]
        public void ValidateDenseMatrixContainsRowAndColKeys()
        {
            DenseMatrix<string, string, double> denseMatObj = GetDenseMatrix();
            Assert.IsTrue(denseMatObj.ContainsRowAndColKeys("R0", "C0"));

            Console.WriteLine(
                "DenseMatrix BVT : Validation of ContainsRowAndColKeys() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of ContainsRowAndColKeys() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates ContainsRowKey
        /// method
        /// Input : Valid values for DenseMatrix
        /// Validation : Contains Row Key
        /// </summary>
        [Test]
        public void ValidateDenseMatrixContainsRowKey()
        {
            DenseMatrix<string, string, double> denseMatObj = GetDenseMatrix();
            Assert.IsTrue(denseMatObj.ContainsRowKey("R0"));

            Console.WriteLine(
                "DenseMatrix BVT : Validation of ContainsRowKey() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of ContainsRowKey() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates ConvertValueView
        /// method
        /// Input : Valid values for DenseMatrix
        /// Validation : Convert Value View
        /// </summary>
        [Test]
        public void ValidateDenseMatrixConvertValueView()
        {
            DenseMatrix<string, string, double> denseMatObj = GetDenseMatrix();

            Matrix<string, string, int> matObj =
                denseMatObj.ConvertValueView<string, string, double, int>(
                ValueConverter.DoubleToInt,
                int.MaxValue);

            // Validate all properties of Ansi and DenseMatrix
            Assert.AreEqual(denseMatObj.ColCount, matObj.ColCount);
            Assert.AreEqual(denseMatObj.ColKeys, matObj.ColKeys);
            Assert.AreEqual(denseMatObj.IndexOfColKey, matObj.IndexOfColKey);
            Assert.AreEqual(denseMatObj.IndexOfRowKey, matObj.IndexOfRowKey);
            Assert.AreEqual("2147483647", matObj.MissingValue.ToString());
            Assert.AreEqual(denseMatObj.RowCount, matObj.RowCount);
            Assert.AreEqual(denseMatObj.RowKeys, matObj.RowKeys);
            Assert.AreEqual(Utility._xmlUtil.GetTextValue(
                Constants.SimpleMatrixNodeName,
                Constants.DenseConvertValueStringNode),
                matObj.ToString2D().Replace("\r", "").Replace("\n", "").Replace("\t", ""));

            Console.WriteLine(
                "DenseMatrix BVT : Validation of ConvertValueView() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of ConvertValueView() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates GetEnumerator
        /// method
        /// Input : Valid values for DenseMatrix
        /// Validation : Gets Enumerator
        /// </summary>
        [Test]
        public void ValidateDenseMatrixGetEnumerator()
        {
            DenseMatrix<string, string, double> denseMatObj = GetDenseMatrix();

            var enumObj = denseMatObj.RowKeyColKeyValues.GetEnumerator();
            enumObj.MoveNext();

            Assert.AreEqual(denseMatObj[0, 0].ToString(), enumObj.Current.Value.ToString());

            Console.WriteLine(
                "DenseMatrix BVT : Validation of GetEnumerator() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of GetEnumerator() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates GetValueOrMissing
        /// method
        /// Input : Valid values for DenseMatrix
        /// Validation : Get Value Or Missing
        /// </summary>
        [Test]
        public void ValidateDenseMatrixGetValueOrMissing()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Assert.AreEqual(denseMatObj[0, 1].ToString(),
                denseMatObj.GetValueOrMissing(0, 1).ToString());

            Console.WriteLine(
                "DenseMatrix BVT : Validation of GetValueOrMissing() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of GetValueOrMissing() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates GetValueOrMissing 
        /// with key
        /// Input : Valid values for DenseMatrix
        /// Validation : Get Value Or Missing (key)
        /// </summary>
        [Test]
        public void ValidateDenseMatrixGetValueOrMissingKey()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Assert.AreEqual(denseMatObj["R0", "C0"].ToString(),
                denseMatObj.GetValueOrMissing("R0", "C0").ToString());

            Console.WriteLine(
                "DenseMatrix BVT : Validation of GetValueOrMissing(key) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of GetValueOrMissing(key) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates HashableView 
        /// with key
        /// Input : Valid values for DenseMatrix
        /// Validation : Hashable View
        /// </summary>
        [Test]
        public void ValidateDenseMatrixHashableView()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Matrix<string, string, double> hasview = denseMatObj.HashableView<string, string, double>();

            // Validate the parent matrix.
            HashableView<string, string, double> parentHashableViewOrNull = hasview as HashableView<string, string, double>;
            Matrix<string, string, double> parentMatrixObj = parentHashableViewOrNull.ParentMatrix;

            // Validate all properties of Ansi and DenseMatrix
            Assert.AreEqual(denseMatObj.ColCount, parentMatrixObj.ColCount);
            Assert.AreEqual(denseMatObj.ColKeys, parentMatrixObj.ColKeys);
            Assert.AreEqual(denseMatObj.IndexOfColKey, parentMatrixObj.IndexOfColKey);
            Assert.AreEqual(denseMatObj.IndexOfRowKey, parentMatrixObj.IndexOfRowKey);
            Assert.AreEqual(denseMatObj.MissingValue, parentMatrixObj.MissingValue);
            Assert.AreEqual(denseMatObj.RowCount, parentMatrixObj.RowCount);
            Assert.AreEqual(denseMatObj.RowKeys, parentMatrixObj.RowKeys);
            Assert.AreEqual(Utility._xmlUtil.GetTextValue(
                Constants.SimpleMatrixNodeName,
                Constants.DenseMatrixStringNode),
                parentMatrixObj.ToString2D().Replace("\r", "").Replace("\n", "").Replace("\t", ""));

            Console.WriteLine(
                "DenseMatrix BVT : Validation of HashableView() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of HashableView() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates IsMissing 
        /// with key
        /// Input : Valid values for DenseMatrix
        /// Validation : Is Missing
        /// </summary>
        [Test]
        public void ValidateDenseMatrixIsMissing()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Assert.IsTrue(denseMatObj.IsMissing(double.NaN));
            Assert.IsFalse(denseMatObj.IsMissing(1.0));

            Console.WriteLine(
                "DenseMatrix BVT : Validation of IsMissing() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of IsMissing() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates IsMissing
        /// with Index
        /// with key
        /// Input : Valid values for DenseMatrix
        /// Validation : Is Missing(Index)
        /// </summary>
        [Test]
        public void ValidateDenseMatrixIsMissingIndex()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            for (int i = 0; i < denseMatObj.RowCount; i++)
            {
                for (int j = 0; j < denseMatObj.ColCount; j++)
                {
                    try
                    {
                        if (double.NaN.ToString() != denseMatObj[i, j].ToString())
                        {
                            Assert.IsFalse(denseMatObj.IsMissing(i, j));
                        }
                    }
                    catch (KeyNotFoundException)
                    {
                        break;
                    }
                }
            }

            Console.WriteLine(
                "DenseMatrix BVT : Validation of IsMissing(index) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of IsMissing(index) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates IsMissing
        /// with key
        /// Input : Valid values for DenseMatrix
        /// Validation : Is Missing(Key)
        /// </summary>
        [Test]
        public void ValidateDenseMatrixIsMissingKey()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            for (int i = 0; i < denseMatObj.RowCount; i++)
            {
                for (int j = 0; j < denseMatObj.ColCount; j++)
                {
                    try
                    {
                        if (double.NaN.ToString() != denseMatObj["R" + i.ToString(), "C" + j.ToString()].ToString())
                        {
                            Assert.IsFalse(denseMatObj.IsMissing("R" + i.ToString(), "C" + j.ToString()));
                        }
                    }
                    catch (KeyNotFoundException)
                    {
                        break;
                    }
                }
            }

            Console.WriteLine(
                "DenseMatrix BVT : Validation of IsMissing(key) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of IsMissing(key) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates IsMissingAll
        /// method
        /// Input : Valid values for DenseMatrix
        /// Validation : Is Missing All
        /// </summary>
        [Test]
        public void ValidateDenseMatrixIsMissingAll()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Assert.IsFalse(denseMatObj.IsMissingAll());

            Console.WriteLine(
                "DenseMatrix BVT : Validation of IsMissingAll() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of IsMissingAll() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates IsMissingAllInCol
        /// with index
        /// Input : Valid values for DenseMatrix
        /// Validation : Is Missing All In Col(index)
        /// </summary>
        [Test]
        public void ValidateDenseMatrixIsMissingAllInColIndex()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Assert.IsFalse(denseMatObj.IsMissingAllInCol(0));

            // Creates a temp dense matrix with all values set to Missing value
            DenseMatrix<string, string, double> tempDenseMatObj =
                new DenseMatrix<string, string, double>(new double[,] { { double.NaN }, { double.NaN }, { double.NaN } },
                    new string[] { "R0", "R1", "R2" }, new string[] { "C0" }, double.NaN);

            Assert.IsTrue(tempDenseMatObj.IsMissingAllInCol(0));

            Console.WriteLine(
                "DenseMatrix BVT : Validation of IsMissingAllInCol(index) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of IsMissingAllInCol(index) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates IsMissingAllInCol
        /// with key
        /// Input : Valid values for DenseMatrix
        /// Validation : Is Missing All In Col(key)
        /// </summary>
        [Test]
        public void ValidateDenseMatrixIsMissingAllInColKey()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Assert.IsFalse(denseMatObj.IsMissingAllInCol("C0"));

            // Creates a temp dense matrix with all values set to Missing value
            DenseMatrix<string, string, double> tempDenseMatObj =
                new DenseMatrix<string, string, double>(new double[,] { { double.NaN }, { double.NaN }, { double.NaN } },
                    new string[] { "R0", "R1", "R2" }, new string[] { "C0" }, double.NaN);

            Assert.IsTrue(tempDenseMatObj.IsMissingAllInCol("C0"));

            Console.WriteLine(
                "DenseMatrix BVT : Validation of IsMissingAllInCol(key) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of IsMissingAllInCol(key) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates IsMissingAllInRow
        /// with index
        /// Input : Valid values for DenseMatrix
        /// Validation : Is Missing All In Row(index)
        /// </summary>
        [Test]
        public void ValidateDenseMatrixIsMissingAllInRowIndex()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Assert.IsFalse(denseMatObj.IsMissingAllInRow(0));

            // Creates a temp dense matrix with all values set to Missing value
            DenseMatrix<string, string, double> tempDenseMatObj =
                new DenseMatrix<string, string, double>(new double[,] { { double.NaN }, { double.NaN }, { double.NaN } },
                    new string[] { "R0", "R1", "R2" }, new string[] { "C0" }, double.NaN);

            Assert.IsTrue(tempDenseMatObj.IsMissingAllInRow(0));

            Console.WriteLine(
                "DenseMatrix BVT : Validation of IsMissingAllInRow(index) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of IsMissingAllInRow(index) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates IsMissingAllInRow
        /// with key
        /// Input : Valid values for DenseMatrix
        /// Validation : Is Missing All In Row(key)
        /// </summary>
        [Test]
        public void ValidateDenseMatrixIsMissingAllInRowKey()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Assert.IsFalse(denseMatObj.IsMissingAllInRow("R0"));

            // Creates a temp dense matrix with all values set to Missing value
            DenseMatrix<string, string, double> tempDenseMatObj =
                new DenseMatrix<string, string, double>(new double[,] { { double.NaN }, { double.NaN }, { double.NaN } },
                    new string[] { "R0", "R1", "R2" }, new string[] { "C0" }, double.NaN);

            Assert.IsTrue(tempDenseMatObj.IsMissingAllInRow("R0"));

            Console.WriteLine(
                "DenseMatrix BVT : Validation of IsMissingAllInRow(key) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of IsMissingAllInRow(key) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates IsMissingAllInSomeCol
        /// Input : Valid values for DenseMatrix
        /// Validation : Is Missing All In Some Col
        /// </summary>
        [Test]
        public void ValidateDenseMatrixIsMissingAllInSomeCol()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Assert.IsFalse(denseMatObj.IsMissingAllInSomeCol());

            // Creates a temp dense matrix with all values set to no Missing value
            DenseMatrix<string, string, double> tempDenseMatObj =
                new DenseMatrix<string, string, double>(new double[,] { { double.NaN }, { double.NaN }, { double.NaN } },
                    new string[] { "R0", "R1", "R2" }, new string[] { "C0" }, double.NaN);

            Assert.IsTrue(tempDenseMatObj.IsMissingAllInSomeCol());

            Console.WriteLine(
                "DenseMatrix BVT : Validation of IsMissingAllInSomeCol() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of IsMissingAllInSomeCol() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates IsMissingAllInSomeRow
        /// Input : Valid values for DenseMatrix
        /// Validation : Is Missing All In Some Row
        /// </summary>
        [Test]
        public void ValidateDenseMatrixIsMissingAllInSomeRow()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Assert.IsFalse(denseMatObj.IsMissingAllInSomeRow());

            // Creates a temp dense matrix with all values set to no Missing value
            DenseMatrix<string, string, double> tempDenseMatObj =
                new DenseMatrix<string, string, double>(new double[,] { { double.NaN }, { double.NaN }, { double.NaN } },
                    new string[] { "R0", "R1", "R2" }, new string[] { "C0" }, double.NaN);

            Assert.IsTrue(tempDenseMatObj.IsMissingAllInSomeRow());

            Console.WriteLine(
                "DenseMatrix BVT : Validation of IsMissingAllInSomeRow() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of IsMissingAllInSomeRow() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates IsMissingSome
        /// Input : Valid values for DenseMatrix
        /// Validation : Is Missing Some
        /// </summary>
        [Test]
        public void ValidateDenseMatrixIsMissingSome()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Assert.IsTrue(denseMatObj.IsMissingSome());

            // Creates a temp dense matrix with all values set to no Missing value
            DenseMatrix<string, string, double> tempDenseMatObj =
                new DenseMatrix<string, string, double>(new double[,] { { 1 }, { 1 }, { 1 } },
                    new string[] { "R0", "R1", "R2" }, new string[] { "C0" }, double.NaN);

            Assert.IsFalse(tempDenseMatObj.IsMissingSome());

            Console.WriteLine(
                "DenseMatrix BVT : Validation of IsMissingSome() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of IsMissingSome() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates MatrixEquals
        /// Input : Valid values for DenseMatrix
        /// Validation : Matrix Equals
        /// </summary>
        [Test]
        public void ValidateDenseMatrixMatrixEquals()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Assert.IsTrue(denseMatObj.MatrixEquals((Matrix<string, string, double>)denseMatObj));

            // Creates a temp dense matrix with all values set to no Missing value
            DenseMatrix<string, string, double> tempDenseMatObj =
                new DenseMatrix<string, string, double>(new double[,] { { 1 }, { 1 }, { 1 } },
                    new string[] { "R0", "R1", "R2" }, new string[] { "C0" }, double.NaN);

            Assert.IsFalse(tempDenseMatObj.MatrixEquals(denseMatObj));

            Console.WriteLine(
                "DenseMatrix BVT : Validation of MatrixEquals() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of MatrixEquals() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates MergeColsView
        /// Input : Valid values for DenseMatrix
        /// Validation : Merge Cols View
        /// </summary>
        [Test]
        public void ValidateDenseMatrixMergeColsView()
        {
            DenseMatrix<string, string, double> denseMatObj =
                new DenseMatrix<string, string, double>(
                    new double[,] { { 7, 7, 7 }, { 7, 7, 7 }, { 7, 7, 7 } },
                    new string[] { "R0", "R1", "R2" }, new string[] { "C0", "C1", "C2" }, double.NaN);


            DenseMatrix<string, string, double> tempDenseMatObj =
                new DenseMatrix<string, string, double>(new double[,] { { 7 }, { 7 }, { 7 } },
                    new string[] { "M0", "M1", "M2" }, new string[] { "N0" }, double.NaN);

            Matrix<string, string, double> nonMergeMatrix =
                denseMatObj.MergeColsView<string, string, double>(false, tempDenseMatObj);

            Assert.AreEqual(denseMatObj.ColCount + 1, nonMergeMatrix.ColCount);
            Assert.AreEqual(denseMatObj.RowCount - 3, nonMergeMatrix.RowCount);

            tempDenseMatObj =
                new DenseMatrix<string, string, double>(new double[,] { { 7 }, { 7 }, { 7 } },
                    new string[] { "R0", "R1", "R2" }, new string[] { "N0" }, double.NaN);

            Matrix<string, string, double> mergeMatrix =
                denseMatObj.MergeColsView<string, string, double>(true, tempDenseMatObj);

            Assert.AreEqual(denseMatObj.RowCount, mergeMatrix.RowCount);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of MergeColsView() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of MergeColsView() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates MergeRowsView
        /// Input : Valid values for DenseMatrix
        /// Validation : Merge Rows View
        /// </summary>
        [Test]
        public void ValidateDenseMatrixMergeRowsView()
        {
            DenseMatrix<string, string, double> denseMatObj =
                new DenseMatrix<string, string, double>(new double[,] { { 7, 7, 7 } },
                    new string[] { "R0" }, new string[] { "C0", "C1", "C2" }, double.NaN);

            DenseMatrix<string, string, double> tempDenseMatObj =
                new DenseMatrix<string, string, double>(new double[,] { { 7, 7, 7 } },
                    new string[] { "M0" }, new string[] { "N0", "N1", "N2" }, double.NaN);

            Matrix<string, string, double> nonMergeMatrix =
                denseMatObj.MergeRowsView<string, string, double>(false, tempDenseMatObj);

            Assert.AreEqual(denseMatObj.ColCount - 3, nonMergeMatrix.ColCount);
            Assert.AreEqual(denseMatObj.RowCount + 1, nonMergeMatrix.RowCount);

            tempDenseMatObj =
                new DenseMatrix<string, string, double>(new double[,] { { 7, 7, 7 } },
                    new string[] { "M0" }, new string[] { "C0", "C1", "C2" }, double.NaN);

            Matrix<string, string, double> mergeMatrix =
                denseMatObj.MergeRowsView<string, string, double>(true, tempDenseMatObj);

            Assert.AreEqual(denseMatObj.ColCount, mergeMatrix.ColCount);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of MergeRowsView() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of MergeRowsView() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates PermuteColValuesForEachRowView
        /// with Random value
        /// Input : Valid values for DenseMatrix
        /// Validation : Permute Col Values For Each Row View
        /// </summary>
        [Test]
        public void ValidateDenseMatrixPermuteColValuesForEachRowViewRand()
        {
            DenseMatrix<string, string, double> denseMatObj = GetDenseMatrix();
            Random randomObj = new Random();
            Matrix<string, string, double> matObj =
                denseMatObj.PermuteColValuesForEachRowView(ref randomObj);

            Assert.AreEqual(denseMatObj.ColCount, matObj.ColCount);
            Assert.AreEqual(denseMatObj.RowCount, matObj.RowCount);
            Assert.AreEqual(denseMatObj.ColKeys, matObj.ColKeys);
            Assert.AreEqual(denseMatObj.RowKeys, matObj.RowKeys);
            Assert.AreEqual(denseMatObj.MissingValue, matObj.MissingValue);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of PermuteColValuesForEachRowView(random) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of PermuteColValuesForEachRowView(random) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates PermuteColValuesForEachRowView
        /// with Int value
        /// Input : Valid values for DenseMatrix
        /// Validation : Permute Col Values For Each Row View
        /// </summary>
        [Test]
        public void ValidateDenseMatrixPermuteColValuesForEachRowViewInt()
        {
            double[,] twoDArray = new double[,] { { 1 } };

            DenseMatrix<string, string, double> denseMatObj =
                new DenseMatrix<string, string, double>(twoDArray,
                    new string[] { "R0" },
                    new string[] { "C0" },
                    double.NaN);

            Matrix<string, string, double> matObj =
                denseMatObj.PermuteColValuesForEachRowView(new int[] { 0 });

            Assert.AreEqual(denseMatObj.ColCount, matObj.ColCount);
            Assert.AreEqual(denseMatObj.RowCount, matObj.RowCount);
            Assert.AreEqual(denseMatObj.ColKeys, matObj.ColKeys);
            Assert.AreEqual(denseMatObj.RowKeys, matObj.RowKeys);
            Assert.AreEqual(denseMatObj.MissingValue, matObj.MissingValue);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of PermuteColValuesForEachRowView(Int) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of PermuteColValuesForEachRowView(Int) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates PermuteColValuesForEachRowView
        /// with Key value
        /// Input : Valid values for DenseMatrix
        /// Validation : Permute Col Values For Each Row View
        /// </summary>
        [Test]
        public void ValidateDenseMatrixPermuteColValuesForEachRowViewKey()
        {
            double[,] twoDArray = new double[,] { { 1 } };

            DenseMatrix<string, string, double> denseMatObj =
                new DenseMatrix<string, string, double>(twoDArray,
                    new string[] { "R0" },
                    new string[] { "C0" },
                    double.NaN);

            Matrix<string, string, double> matObj =
                denseMatObj.PermuteColValuesForEachRowView(new string[] { "C0" });

            Assert.AreEqual(denseMatObj.ColCount, matObj.ColCount);
            Assert.AreEqual(denseMatObj.RowCount, matObj.RowCount);
            Assert.AreEqual(denseMatObj.ColKeys, matObj.ColKeys);
            Assert.AreEqual(denseMatObj.RowKeys, matObj.RowKeys);
            Assert.AreEqual(denseMatObj.MissingValue, matObj.MissingValue);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of PermuteColValuesForEachRowView(key) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of PermuteColValuesForEachRowView(key) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates PermuteRowValuesForEachColView
        /// with Random value
        /// Input : Valid values for DenseMatrix
        /// Validation : Permute Row Values For Each Col View
        /// </summary>
        [Test]
        public void ValidateDenseMatrixPermuteRowValuesForEachColViewRand()
        {
            DenseMatrix<string, string, double> denseMatObj = GetDenseMatrix();
            Random randomObj = new Random();
            Matrix<string, string, double> matObj =
                denseMatObj.PermuteRowValuesForEachColView(ref randomObj);

            Assert.AreEqual(denseMatObj.ColCount, matObj.ColCount);
            Assert.AreEqual(denseMatObj.RowCount, matObj.RowCount);
            Assert.AreEqual(denseMatObj.ColKeys, matObj.ColKeys);
            Assert.AreEqual(denseMatObj.RowKeys, matObj.RowKeys);
            Assert.AreEqual(denseMatObj.MissingValue, matObj.MissingValue);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of PermuteRowValuesForEachColView(random) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of PermuteRowValuesForEachColView(random) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates PermuteRowValuesForEachColView
        /// with Int value
        /// Input : Valid values for DenseMatrix
        /// Validation : Permute Row Values For Each Col View
        /// </summary>
        [Test]
        public void ValidateDenseMatrixPermuteRowValuesForEachColViewInt()
        {
            double[,] twoDArray = new double[,] { { 1 } };

            DenseMatrix<string, string, double> denseMatObj =
                new DenseMatrix<string, string, double>(twoDArray,
                    new string[] { "R0" },
                    new string[] { "C0" },
                    double.NaN);

            Matrix<string, string, double> matObj =
                denseMatObj.PermuteRowValuesForEachColView(new int[] { 0 });

            Assert.AreEqual(denseMatObj.ColCount, matObj.ColCount);
            Assert.AreEqual(denseMatObj.RowCount, matObj.RowCount);
            Assert.AreEqual(denseMatObj.ColKeys, matObj.ColKeys);
            Assert.AreEqual(denseMatObj.RowKeys, matObj.RowKeys);
            Assert.AreEqual(denseMatObj.MissingValue, matObj.MissingValue);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of PermuteRowValuesForEachColView(Int) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of PermuteRowValuesForEachColView(Int) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates Remove
        /// method
        /// Input : Valid values for DenseMatrix
        /// Validation : Remove method
        /// </summary>
        [Test]
        public void ValidateDenseMatrixRemove()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            for (int i = 0; i < denseMatObj.ColCount; i++)
            {
                Assert.IsTrue(denseMatObj.Remove(0, i));
            }

            try
            {
                double val = denseMatObj[0, 0];
                Assert.Fail(string.Format("Value {0} found instead of null", val));
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine(
                    "DenseMatrix BVT : Validation of Remove() method successful");
                ApplicationLog.WriteLine(
                    "DenseMatrix BVT : Validation of Remove() method successful");
            }
        }

        /// <summary>
        /// Creates a Dense Matrix and validates Remove
        /// method with Key
        /// Input : Valid values for DenseMatrix
        /// Validation : Remove(key)
        /// </summary>
        [Test]
        public void ValidateDenseMatrixRemoveKey()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            for (int i = 0; i < denseMatObj.ColCount; i++)
            {
                Assert.IsTrue(denseMatObj.Remove("R0", "C" + i.ToString()));
            }

            try
            {
                double val = denseMatObj["R0", "C0"];
                Assert.Fail(string.Format("Value {0} found instead of null", val));
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine(
                    "DenseMatrix BVT : Validation of Remove(key) method successful");
                ApplicationLog.WriteLine(
                    "DenseMatrix BVT : Validation of Remove(key) method successful");
            }
        }

        /// <summary>
        /// Creates a Dense Matrix and validates RenameColsView
        /// method
        /// Input : Valid values for DenseMatrix
        /// Validation : Rename Cols View
        /// </summary>
        [Test]
        public void ValidateDenseMatrixRenameColsView()
        {
            DenseMatrix<string, string, double> denseMatObj = CreateSimpleDenseMatrix();

            Matrix<string, string, double> newMatObj =
                denseMatObj.RenameColsView<string, string, double>(
                new Dictionary<string, string> { { "G0", "C0" }, 
                { "G1", "C1" }, { "G2", "C2" }, { "G3", "C3" } });

            Assert.AreEqual("1", newMatObj["R0", "G3"].ToString());
            Assert.AreEqual("3", newMatObj["R1", "G1"].ToString());
            Assert.AreEqual("3", newMatObj["R2", "G0"].ToString());

            Console.WriteLine(
                "DenseMatrix BVT : Validation of RenameColsView() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of RenameColsView() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates RenameRowsView
        /// method
        /// Input : Valid values for DenseMatrix
        /// Validation : Rename Rows View
        /// </summary>
        [Test]
        public void ValidateDenseMatrixRenameRowsView()
        {
            DenseMatrix<string, string, double> denseMatObj = CreateSimpleDenseMatrix();

            Matrix<string, string, double> newMatObj =
                denseMatObj.RenameRowsView<string, string, double>(
                new Dictionary<string, string> { { "G0", "R0" }, 
                { "G1", "R1" }, { "G2", "R2" }});

            Assert.AreEqual("1", newMatObj["G0", "C3"].ToString());
            Assert.AreEqual("3", newMatObj["G1", "C1"].ToString());
            Assert.AreEqual("3", newMatObj["G2", "C0"].ToString());

            Console.WriteLine(
                "DenseMatrix BVT : Validation of RenameRowsView() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of RenameRowsView() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates SelectColsView
        /// method
        /// Input : Valid values for DenseMatrix
        /// Validation : Select Cols View
        /// </summary>
        [Test]
        public void ValidateDenseMatrixSelectColsView()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Matrix<string, string, double> viewObj = null;

            if (denseMatObj.ColCount > 3)
            {
                viewObj =
                    denseMatObj.SelectColsView<string, string, double>(new int[] { 0, 1 });

                Assert.AreEqual("2", viewObj.ColCount.ToString());
                Assert.AreEqual(denseMatObj.RowCount, viewObj.RowCount);
                Assert.AreEqual(denseMatObj[0, 0], viewObj[0, 0]);
                Assert.AreEqual(denseMatObj[0, 1], viewObj[0, 1]);
            }
            else
            {
                viewObj =
                    denseMatObj.SelectColsView<string, string, double>(new int[] { 0 });

                Assert.AreEqual("1", viewObj.ColCount.ToString());
                Assert.AreEqual(denseMatObj.RowCount, viewObj.RowCount);
                Assert.AreEqual(denseMatObj[0, 0], viewObj[0, 0]);
            }

            Console.WriteLine(
                "DenseMatrix BVT : Validation of SelectColsView() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of SelectColsView() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates SelectColsView
        /// method with key
        /// Input : Valid values for DenseMatrix
        /// Validation : Select Cols View with key
        /// </summary>
        [Test]
        public void ValidateDenseMatrixSelectColsViewKey()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Matrix<string, string, double> viewObj = null;

            if (denseMatObj.ColCount > 3)
            {
                viewObj =
                    denseMatObj.SelectColsView<string, string, double>(
                    new string[] { "C0", "C1" });

                Assert.AreEqual("2", viewObj.ColCount.ToString());
                Assert.AreEqual(denseMatObj.RowCount, viewObj.RowCount);
                Assert.AreEqual(denseMatObj[0, 0], viewObj[0, 0]);
                Assert.AreEqual(denseMatObj[0, 1], viewObj[0, 1]);
            }
            else
            {
                viewObj =
                    denseMatObj.SelectColsView<string, string, double>(
                    new string[] { "C0" });

                Assert.AreEqual("1", viewObj.ColCount.ToString());
                Assert.AreEqual(denseMatObj.RowCount, viewObj.RowCount);
                Assert.AreEqual(denseMatObj[0, 0], viewObj[0, 0]);
            }

            Console.WriteLine(
                "DenseMatrix BVT : Validation of SelectColsView(key) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of SelectColsView(key) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates SelectColsView
        /// method with Index Sequence
        /// Input : Valid values for DenseMatrix
        /// Validation : Select Cols View with Index Sequence
        /// </summary>
        [Test]
        public void ValidateDenseMatrixSelectColsViewColIndexSeq()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Matrix<string, string, double> viewObj = null;

            if (denseMatObj.ColCount > 3)
            {
                int[] colIndexSeq = new int[] { 0, 1 };
                viewObj =
                    denseMatObj.SelectColsView<string, string, double>(
                    (IEnumerable<int>)colIndexSeq);

                Assert.AreEqual("2", viewObj.ColCount.ToString());
                Assert.AreEqual(denseMatObj.RowCount, viewObj.RowCount);
                Assert.AreEqual(denseMatObj[0, 0], viewObj[0, 0]);
                Assert.AreEqual(denseMatObj[0, 1], viewObj[0, 1]);
            }
            else
            {
                int[] colIndexSeq = new int[] { 0 };
                viewObj =
                    denseMatObj.SelectColsView<string, string, double>(
                    (IEnumerable<int>)colIndexSeq);

                Assert.AreEqual("1", viewObj.ColCount.ToString());
                Assert.AreEqual(denseMatObj.RowCount, viewObj.RowCount);
                Assert.AreEqual(denseMatObj[0, 0], viewObj[0, 0]);
            }

            Console.WriteLine(
                "DenseMatrix BVT : Validation of SelectColsView(ColIndexSeq) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of SelectColsView(ColIndexSeq) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates SelectColsView
        /// method with key sequence
        /// Input : Valid values for DenseMatrix
        /// Validation : Select Cols View with key
        /// </summary>
        [Test]
        public void ValidateDenseMatrixSelectColsViewKeySeq()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Matrix<string, string, double> viewObj = null;

            if (denseMatObj.ColCount > 3)
            {
                string[] colKeySeq = new string[] { "C0", "C1" };
                viewObj =
                    denseMatObj.SelectColsView<string, string, double>(
                    (IEnumerable<string>)colKeySeq);

                Assert.AreEqual("2", viewObj.ColCount.ToString());
                Assert.AreEqual(denseMatObj.RowCount, viewObj.RowCount);
                Assert.AreEqual(denseMatObj[0, 0], viewObj[0, 0]);
                Assert.AreEqual(denseMatObj[0, 1], viewObj[0, 1]);
            }
            else
            {
                string[] colKeySeq = new string[] { "C0" };
                viewObj =
                    denseMatObj.SelectColsView<string, string, double>(
                    (IEnumerable<string>)colKeySeq);

                Assert.AreEqual("1", viewObj.ColCount.ToString());
                Assert.AreEqual(denseMatObj.RowCount, viewObj.RowCount);
                Assert.AreEqual(denseMatObj[0, 0], viewObj[0, 0]);
            }

            Console.WriteLine(
                "DenseMatrix BVT : Validation of SelectColsView(keyseq) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of SelectColsView(keyseq) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates SelectRowsView
        /// method 
        /// Input : Valid values for DenseMatrix
        /// Validation : Select Rows View
        /// </summary>
        [Test]
        public void ValidateDenseMatrixSelectRowsView()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Matrix<string, string, double> viewObj = null;

            if (denseMatObj.RowCount > 2)
            {
                viewObj =
                    denseMatObj.SelectRowsView<string, string, double>(
                    new int[] { 0, 1 });

                Assert.AreEqual("2", viewObj.RowCount.ToString());
                Assert.AreEqual(denseMatObj.ColCount, viewObj.ColCount);
                Assert.AreEqual(denseMatObj[0, 0], viewObj[0, 0]);
                Assert.AreEqual(denseMatObj[1, 0], viewObj[1, 0]);
            }
            else
            {
                viewObj =
                    denseMatObj.SelectRowsView<string, string, double>(
                    new int[] { 0 });

                Assert.AreEqual("1", viewObj.RowCount.ToString());
                Assert.AreEqual(denseMatObj.ColCount, viewObj.ColCount);
                Assert.AreEqual(denseMatObj[0, 0], viewObj[0, 0]);
            }

            Console.WriteLine(
                "DenseMatrix BVT : Validation of SelectRowsView() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of SelectRowsView() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates SelectRowsView
        /// method with key
        /// Input : Valid values for DenseMatrix
        /// Validation : Select Rows View with key
        /// </summary>
        [Test]
        public void ValidateDenseMatrixSelectRowsViewKey()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Matrix<string, string, double> viewObj = null;

            if (denseMatObj.RowCount > 2)
            {
                viewObj =
                    denseMatObj.SelectRowsView<string, string, double>(
                    new string[] { "R0", "R1" });

                Assert.AreEqual("2", viewObj.RowCount.ToString());
                Assert.AreEqual(denseMatObj.ColCount, viewObj.ColCount);
                Assert.AreEqual(denseMatObj[0, 0], viewObj[0, 0]);
                Assert.AreEqual(denseMatObj[1, 0], viewObj[1, 0]);
            }
            else
            {
                viewObj =
                    denseMatObj.SelectRowsView<string, string, double>(
                    new string[] { "R0" });

                Assert.AreEqual("1", viewObj.RowCount.ToString());
                Assert.AreEqual(denseMatObj.ColCount, viewObj.ColCount);
                Assert.AreEqual(denseMatObj[0, 0], viewObj[0, 0]);
            }

            Console.WriteLine(
                "DenseMatrix BVT : Validation of SelectRowsView(key) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of SelectRowsView(key) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates SelectRowsView
        /// method 
        /// Input : Valid values for DenseMatrix
        /// Validation : Select Rows View
        /// </summary>
        [Test]
        public void ValidateDenseMatrixSelectRowsViewIndexSeq()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Matrix<string, string, double> viewObj = null;

            if (denseMatObj.RowCount > 2)
            {
                int[] rowIndexSeq = new int[] { 0, 1 };
                viewObj =
                    denseMatObj.SelectRowsView<string, string, double>(
                    (IEnumerable<int>)rowIndexSeq);

                Assert.AreEqual("2", viewObj.RowCount.ToString());
                Assert.AreEqual(denseMatObj.ColCount, viewObj.ColCount);
                Assert.AreEqual(denseMatObj[0, 0], viewObj[0, 0]);
                Assert.AreEqual(denseMatObj[1, 0], viewObj[1, 0]);
            }
            else
            {
                int[] rowIndexSeq = new int[] { 0 };
                viewObj =
                    denseMatObj.SelectRowsView<string, string, double>(
                    (IEnumerable<int>)rowIndexSeq);

                Assert.AreEqual("1", viewObj.RowCount.ToString());
                Assert.AreEqual(denseMatObj.ColCount, viewObj.ColCount);
                Assert.AreEqual(denseMatObj[0, 0], viewObj[0, 0]);
            }

            Console.WriteLine(
                "DenseMatrix BVT : Validation of SelectRowsView(indexseq) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of SelectRowsView(indexseq) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates SelectRowsView
        /// method with key sequence
        /// Input : Valid values for DenseMatrix
        /// Validation : Select Rows View with key sequence
        /// </summary>
        [Test]
        public void ValidateDenseMatrixSelectRowsViewKeyKeySeq()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Matrix<string, string, double> viewObj = null;

            if (denseMatObj.RowCount > 2)
            {
                string[] rowKeySeq = new string[] { "R0", "R1" };
                viewObj =
                    denseMatObj.SelectRowsView<string, string, double>(
                    (IEnumerable<string>)rowKeySeq);

                Assert.AreEqual("2", viewObj.RowCount.ToString());
                Assert.AreEqual(denseMatObj.ColCount, viewObj.ColCount);
                Assert.AreEqual(denseMatObj[0, 0], viewObj[0, 0]);
                Assert.AreEqual(denseMatObj[1, 0], viewObj[1, 0]);
            }
            else
            {
                string[] rowKeySeq = new string[] { "R0" };
                viewObj =
                    denseMatObj.SelectRowsView<string, string, double>(
                    (IEnumerable<string>)rowKeySeq);

                Assert.AreEqual("1", viewObj.RowCount.ToString());
                Assert.AreEqual(denseMatObj.ColCount, viewObj.ColCount);
                Assert.AreEqual(denseMatObj[0, 0], viewObj[0, 0]);
            }

            Console.WriteLine(
                "DenseMatrix BVT : Validation of SelectRowsView(keyseq) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of SelectRowsView(keyseq) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates SelectRowsAndColsView
        /// method 
        /// Input : Valid values for DenseMatrix
        /// Validation : Select Rows And Cols View
        /// </summary>
        [Test]
        public void ValidateDenseMatrixSelectColsRowsViewIndexSeq()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Matrix<string, string, double> viewObj = null;

            if (denseMatObj.RowCount > 2 &&
                denseMatObj.ColCount > 2)
            {
                int[] rowIndexSeq = new int[] { 0, 1 };
                int[] colIndexSeq = new int[] { 0, 1 };
                viewObj =
                    denseMatObj.SelectRowsAndColsView<string, string, double>(
                    (IEnumerable<int>)rowIndexSeq, (IEnumerable<int>)colIndexSeq);

                Assert.AreEqual("2", viewObj.RowCount.ToString());
                Assert.AreEqual("2", viewObj.ColCount.ToString());
                Assert.AreEqual(denseMatObj[0, 0], viewObj[0, 0]);
                Assert.AreEqual(denseMatObj[1, 0], viewObj[1, 0]);
            }
            else
            {
                int[] rowIndexSeq = new int[] { 0 };
                int[] colIndexSeq = new int[] { 0 };
                viewObj =
                    denseMatObj.SelectRowsAndColsView<string, string, double>(
                    (IEnumerable<int>)rowIndexSeq, (IEnumerable<int>)colIndexSeq);

                Assert.AreEqual("1", viewObj.RowCount.ToString());
                Assert.AreEqual("1", viewObj.ColCount.ToString());
                Assert.AreEqual(denseMatObj[0, 0], viewObj[0, 0]);
            }

            Console.WriteLine(
                "DenseMatrix BVT : Validation of SelectRowsAndColsView(indexseq) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of SelectRowsAndColsView(indexseq) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates SelectRowsAndColsView
        /// method with key sequence
        /// Input : Valid values for DenseMatrix
        /// Validation : Select Rows and Col View with key sequence
        /// </summary>
        [Test]
        public void ValidateDenseMatrixSelectColRowsViewKeySeq()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Matrix<string, string, double> viewObj = null;

            if (denseMatObj.RowCount > 2 &&
                denseMatObj.ColCount > 2)
            {
                string[] rowIndexSeq = new string[] { "R0", "R1" };
                string[] colIndexSeq = new string[] { "C0", "C1" };
                viewObj =
                    denseMatObj.SelectRowsAndColsView<string, string, double>(
                    (IEnumerable<string>)rowIndexSeq, (IEnumerable<string>)colIndexSeq);

                Assert.AreEqual("2", viewObj.RowCount.ToString());
                Assert.AreEqual("2", viewObj.ColCount.ToString());
                Assert.AreEqual(denseMatObj[0, 0], viewObj[0, 0]);
                Assert.AreEqual(denseMatObj[1, 0], viewObj[1, 0]);
            }
            else
            {
                string[] rowIndexSeq = new string[] { "R0" };
                string[] colIndexSeq = new string[] { "C0" };
                viewObj =
                    denseMatObj.SelectRowsAndColsView<string, string, double>(
                    (IEnumerable<string>)rowIndexSeq,
                    (IEnumerable<string>)colIndexSeq);

                Assert.AreEqual("1", viewObj.RowCount.ToString());
                Assert.AreEqual("1", viewObj.ColCount.ToString());
                Assert.AreEqual(denseMatObj[0, 0], viewObj[0, 0]);
            }

            Console.WriteLine(
                "DenseMatrix BVT : Validation of SelectRowsAndColsView(keyseq) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of SelectRowsAndColsView(keyseq) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates SetValueOrMissing
        /// method with index
        /// Input : Valid values for DenseMatrix
        /// Validation : Set Value Or Missing
        /// </summary>
        [Test]
        public void ValidateDenseMatrixSetValueOrMissingIndex()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            denseMatObj.SetValueOrMissing(0, 0, 10);
            denseMatObj.SetValueOrMissing(denseMatObj.RowCount - 1,
                denseMatObj.ColCount - 1, 10);

            Assert.AreEqual("10", denseMatObj[0, 0].ToString());
            Assert.AreEqual("10", denseMatObj[denseMatObj.RowCount - 1,
                denseMatObj.ColCount - 1].ToString());

            Console.WriteLine(
                "DenseMatrix BVT : Validation of SetValueOrMissing(index) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of SetValueOrMissing(index) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates SetValueOrMissing
        /// method with key
        /// Input : Valid values for DenseMatrix
        /// Validation : Set Value Or Missing
        /// </summary>
        [Test]
        public void ValidateDenseMatrixSetValueOrMissingKey()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            denseMatObj.SetValueOrMissing("R0", "C0", 10);
            denseMatObj.SetValueOrMissing(
                "R" + (denseMatObj.RowCount - 1).ToString(),
                "C" + (denseMatObj.ColCount - 1).ToString(), 10);

            Assert.AreEqual("10", denseMatObj[0, 0].ToString());
            Assert.AreEqual("10", denseMatObj[
                denseMatObj.RowCount - 1,
                denseMatObj.ColCount - 1].ToString());

            Console.WriteLine(
                "DenseMatrix BVT : Validation of SetValueOrMissing(key) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of SetValueOrMissing(key) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates Shuffle
        /// method
        /// Input : Valid values for DenseMatrix
        /// Validation : Shuffle
        /// </summary>
        [Test]
        public void ValidateDenseMatrixShuffle()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Random rdm = new Random(2);
            List<RowKeyColKeyValue<string, string, double>> rkValsObj =
                denseMatObj.RowKeyColKeyValues.Shuffle(rdm);

            // Validate if the shuffle doesnt return R0 or C0 at the first Row/col key
            Assert.AreNotEqual("R0", rkValsObj[0].RowKey);
            Assert.AreNotEqual("C0", rkValsObj[0].ColKey);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of Shuffle() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of Shuffle() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates StringJoin
        /// method
        /// Input : Valid values for DenseMatrix
        /// Validation : StringJoin
        /// </summary>
        [Test]
        public void ValidateDenseMatrixStringJoin()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            string strJoin = denseMatObj.RowKeyColKeyValues.StringJoin().Replace("<", "").Replace(">", "");
            string expStrJoin = Utility._xmlUtil.GetTextValue(Constants.SimpleMatrixNodeName,
                Constants.DenseStringJoinNode);

            // Validate if the shuffle doesnt return R0 or C0 at the first Row/col key
            Assert.AreEqual(expStrJoin, strJoin);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of StringJoin() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of StringJoin() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates StringJoin
        /// method with separator
        /// Input : Valid values for DenseMatrix
        /// Validation : String Join with separator
        /// </summary>
        [Test]
        public void ValidateDenseMatrixStringJoinSeparator()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            string strJoin = denseMatObj.RowKeyColKeyValues.StringJoin(".").Replace("<", "").Replace(">", "");
            string expStrJoin = Utility._xmlUtil.GetTextValue(Constants.SimpleMatrixNodeName,
                Constants.DenseStringJoinSeparatorNode);

            // Validate if the shuffle doesnt return R0 or C0 at the first Row/col key
            Assert.AreEqual(expStrJoin, strJoin);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of StringJoin(separator) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of StringJoin(separator) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates StringJoin
        /// method with separator, max, etc
        /// Input : Valid values for DenseMatrix
        /// Validation : String Join with separator, max, etc
        /// </summary>
        [Test]
        public void ValidateDenseMatrixStringJoinSeparatorEtc()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            string strJoin = denseMatObj.RowKeyColKeyValues.StringJoin(".", 2, "N").Replace("<", "").Replace(">", "");
            string expStrJoin = Utility._xmlUtil.GetTextValue(Constants.SimpleMatrixNodeName,
                Constants.DenseStringJoinSeparatorEtcNode);

            // Validate if the shuffle doesnt return R0 or C0 at the first Row/col key
            Assert.AreEqual(expStrJoin, strJoin);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of StringJoin(separator, max, etc) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of StringJoin(separator, max, etc) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates SubSequence
        /// Input : Valid values for DenseMatrix
        /// Validation : SubSequence
        /// </summary>
        [Test]
        public void ValidateDenseMatrixSubSequence()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            IEnumerable<RowKeyColKeyValue<string, string, double>> rowKeyColKeyObj =
                denseMatObj.RowKeyColKeyValues.SubSequence(0, 2);

            List<RowKeyColKeyValue<string, string, double>> rowKeyColKeyList =
                new List<RowKeyColKeyValue<string, string, double>>();

            foreach (RowKeyColKeyValue<string, string, double> keyVal in rowKeyColKeyObj)
            {
                rowKeyColKeyList.Add(keyVal);
            }

            Assert.AreEqual(2, rowKeyColKeyList.Count);
            Assert.AreEqual(denseMatObj[0, 0], rowKeyColKeyList[0].Value);
            Assert.AreEqual(denseMatObj[0, 1], rowKeyColKeyList[1].Value);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of SubSequence() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of SubSequence() method successful");
        }

        /// <summary>
        /// Creates a Dense Ansi object
        /// Input : Valid values for DenseMatrix
        /// Validation : Dense Ansi Object
        /// </summary>
        [Test]
        public void ValidateDenseMatrixToDenseAnsi()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            ParallelOptions pOptObj = new ParallelOptions();
            DenseAnsi denseAnsiObj =
                denseMatObj.ToDenseAnsi(pOptObj);

            // Validate all properties of Ansi and DenseMatrix
            Assert.AreEqual(denseMatObj.ColCount, denseAnsiObj.ColCount);
            Assert.AreEqual(denseMatObj.ColKeys, denseAnsiObj.ColKeys);
            Assert.AreEqual(denseMatObj.IndexOfColKey, denseAnsiObj.IndexOfColKey);
            Assert.AreEqual(denseMatObj.IndexOfRowKey, denseAnsiObj.IndexOfRowKey);
            Assert.AreEqual("?", denseAnsiObj.MissingValue.ToString());
            Assert.AreEqual(denseMatObj.RowCount, denseAnsiObj.RowCount);
            Assert.AreEqual(denseMatObj.RowKeys, denseAnsiObj.RowKeys);
            Assert.AreEqual(Utility._xmlUtil.GetTextValue(
                Constants.SimpleMatrixNodeName,
                Constants.DenseAnsiStringNode),
                denseAnsiObj.ToString2D().Replace("\r", "").Replace("\n", "").Replace("\t", ""));

            Console.WriteLine(
                "DenseMatrix BVT : Validation of ToDenseAnsi() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of ToDenseAnsi() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix object
        /// Input : Valid values for DenseMatrix
        /// Validation : Dense Matrix Object
        /// </summary>
        [Test]
        public void ValidateDenseMatrixToDenseMatrix()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            DenseMatrix<string, string, double> newDenseObj =
                denseMatObj.ToDenseMatrix();

            // Validate all properties of Ansi and DenseMatrix
            Assert.AreEqual(denseMatObj.ColCount, newDenseObj.ColCount);
            Assert.AreEqual(denseMatObj.ColKeys, newDenseObj.ColKeys);
            Assert.AreEqual(denseMatObj.IndexOfColKey, newDenseObj.IndexOfColKey);
            Assert.AreEqual(denseMatObj.IndexOfRowKey, newDenseObj.IndexOfRowKey);
            Assert.AreEqual(denseMatObj.MissingValue, newDenseObj.MissingValue);
            Assert.AreEqual(denseMatObj.RowCount, newDenseObj.RowCount);
            Assert.AreEqual(denseMatObj.RowKeyColKeyValues, newDenseObj.RowKeyColKeyValues);
            Assert.AreEqual(denseMatObj.RowKeys, newDenseObj.RowKeys);
            Assert.AreEqual(denseMatObj.Values, newDenseObj.Values);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of ToDenseMatrix() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of ToDenseMatrix() method successful");
        }

        /// <summary>
        /// Creates a HashSet from Dense Matrix
        /// Input : Valid values for DenseMatrix
        /// Validation : Hash Set
        /// </summary>
        [Test]
        public void ValidateDenseMatrixToHashSet()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            HashSet<RowKeyColKeyValue<string, string, double>> hashSetObj =
                denseMatObj.RowKeyColKeyValues.ToHashSet();

            foreach (RowKeyColKeyValue<string, string, double> keyVal
                in hashSetObj)
            {
                Assert.AreEqual(denseMatObj[keyVal.RowKey, keyVal.ColKey],
                    keyVal.Value);
            }

            Console.WriteLine(
                "DenseMatrix BVT : Validation of ToHashSet() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of ToHashSet() method successful");
        }

        /// <summary>
        /// Creates a HashSet from Dense Matrix with Comparer
        /// Input : Valid values for DenseMatrix
        /// Validation : Hash Set with comparer
        /// </summary>
        [Test]
        public void ValidateDenseMatrixToHashSetComparer()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            EqualityComparer<RowKeyColKeyValue<string, string, double>> compObj =
                new TempEqualityComparer();

            HashSet<RowKeyColKeyValue<string, string, double>> hashSetObj =
                denseMatObj.RowKeyColKeyValues.ToHashSet(compObj);

            foreach (RowKeyColKeyValue<string, string, double> keyVal
                in hashSetObj)
            {
                Assert.AreEqual(denseMatObj[keyVal.RowKey, keyVal.ColKey],
                    keyVal.Value);
            }

            Console.WriteLine(
                "DenseMatrix BVT : Validation of ToHashSet(comparer) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of ToHashSet(comparer) method successful");
        }

        /// <summary>
        /// Creates a Dense padded double object
        /// Input : Valid values for DenseMatrix
        /// Validation : Padded Double object
        /// </summary>
        [Test]
        public void ValidateDenseMatrixToPaddedDouble()
        {
            DenseMatrix<string, string, double> denseMatObj = GetDenseMatrix();

            ParallelOptions parallelOptObj = new ParallelOptions();

            PaddedDouble padDoub = denseMatObj.ToPaddedDouble(parallelOptObj);

            // Validate all properties of Ansi and DenseMatrix
            Assert.AreEqual(denseMatObj.ColCount, padDoub.ColCount);
            Assert.AreEqual(denseMatObj.ColKeys, padDoub.ColKeys);
            Assert.AreEqual(denseMatObj.IndexOfColKey, padDoub.IndexOfColKey);
            Assert.AreEqual(denseMatObj.IndexOfRowKey, padDoub.IndexOfRowKey);
            Assert.AreEqual(denseMatObj.MissingValue, padDoub.MissingValue);
            Assert.AreEqual(denseMatObj.RowCount, padDoub.RowCount);
            Assert.AreEqual(denseMatObj.RowKeys, padDoub.RowKeys);
            Assert.AreEqual(Utility._xmlUtil.GetTextValue(
                Constants.SimpleMatrixNodeName,
                Constants.DenseMatrixStringNode),
                padDoub.ToString2D().Replace("\r", "").Replace("\n", "").Replace("\t", ""));

            Console.WriteLine(
                "DenseMatrix BVT : Validation of ToPaddedDouble() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of ToPaddedDouble() method successful");
        }

        /// <summary>
        /// Creates a Sparse matrix object
        /// Input : Valid values for DenseMatrix
        /// Validation : Sparse Matrix object
        /// </summary>
        [Test]
        public void ValidateDenseMatrixToSparseMatrix()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            SparseMatrix<string, string, double> sparseMatrixObj =
                denseMatObj.ToSparseMatrix<string, string, double>();

            // Validate all properties of Ansi and DenseMatrix
            Assert.AreEqual(denseMatObj.ColCount, sparseMatrixObj.ColCount);
            Assert.AreEqual(denseMatObj.ColKeys, sparseMatrixObj.ColKeys);
            Assert.AreEqual(denseMatObj.IndexOfColKey, sparseMatrixObj.IndexOfColKey);
            Assert.AreEqual(denseMatObj.IndexOfRowKey, sparseMatrixObj.IndexOfRowKey);
            Assert.AreEqual(denseMatObj.MissingValue, sparseMatrixObj.MissingValue);
            Assert.AreEqual(denseMatObj.RowCount, sparseMatrixObj.RowCount);
            Assert.AreEqual(denseMatObj.RowKeys, sparseMatrixObj.RowKeys);
            Assert.AreEqual(Utility._xmlUtil.GetTextValue(
                Constants.SimpleMatrixNodeName,
                Constants.DenseMatrixStringNode),
                sparseMatrixObj.ToString2D().Replace("\r", "").Replace("\n", "").Replace("\t", ""));

            Console.WriteLine(
                "DenseMatrix BVT : Validation of ToSparseMatrix() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of ToSparseMatrix() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates ToString2D
        /// Input : Valid values for DenseMatrix
        /// Validation : 2 D string
        /// </summary>
        [Test]
        public void ValidateDenseMatrixToString2D()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Assert.AreEqual(Utility._xmlUtil.GetTextValue(
                Constants.SimpleMatrixNodeName,
                Constants.DenseMatrixStringNode),
                denseMatObj.ToString2D().Replace("\r", "").Replace("\n", "").Replace("\t", ""));

            Console.WriteLine(
                "DenseMatrix BVT : Validation of ToString2D() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of ToString2D() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates ToQueue
        /// method
        /// Input : Valid values for DenseMatrix
        /// Validation : To Queue
        /// </summary>
        [Test]
        public void ValidateDenseMatrixToQueue()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Queue<RowKeyColKeyValue<string, string, double>> rkValsObj =
                denseMatObj.RowKeyColKeyValues.ToQueue();

            List<RowKeyColKeyValue<string, string, double>> rowKeyColKeyList =
                new List<RowKeyColKeyValue<string, string, double>>();

            foreach (RowKeyColKeyValue<string, string, double> keyVal in rkValsObj)
            {
                rowKeyColKeyList.Add(keyVal);
            }

            // Validate if the queue does return R0 or C0 at the first Row/col key
            Assert.AreEqual("R0", rowKeyColKeyList[0].RowKey);
            Assert.AreEqual("C0", rowKeyColKeyList[0].ColKey);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of ToQueue() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of ToQueue() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates TransposeView 
        /// method
        /// Input : Valid values for DenseMatrix
        /// Validation : Transpose View
        /// </summary>
        [Test]
        public void ValidateDenseMatrixTransposeView()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            Matrix<string, string, double> transViewObj =
                denseMatObj.TransposeView();

            // Validate all properties of Ansi and DenseMatrix
            Assert.AreEqual(denseMatObj.ColCount, transViewObj.RowCount);
            Assert.AreEqual(denseMatObj.ColKeys, transViewObj.RowKeys);
            Assert.AreEqual(denseMatObj.IndexOfColKey, transViewObj.IndexOfRowKey);
            Assert.AreEqual(denseMatObj.IndexOfRowKey, transViewObj.IndexOfColKey);
            Assert.AreEqual(denseMatObj.MissingValue, transViewObj.MissingValue);
            Assert.AreEqual(denseMatObj.RowCount, transViewObj.ColCount);
            Assert.AreEqual(denseMatObj.RowKeys, transViewObj.ColKeys);
            Assert.AreEqual(Utility._xmlUtil.GetTextValue(
                Constants.SimpleMatrixNodeName,
                Constants.DenseTransposeStringNode),
                transViewObj.ToString2D().Replace("\r", "").Replace("\n", "").Replace("\t", ""));

            Console.WriteLine(
                "DenseMatrix BVT : Validation of TransposeView() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of TransposeView() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates TryGetValue 
        /// with index
        /// Input : Valid values for DenseMatrix
        /// Validation : Try Get Value with index
        /// </summary>
        [Test]
        public void ValidateDenseMatrixTryGetValueIndex()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            double tempVal = 0;
            Assert.IsTrue(denseMatObj.TryGetValue(0, 0, out tempVal));
            Assert.AreEqual(tempVal, denseMatObj[0, 0]);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of TryGetValue(index) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of TryGetValue(index) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates TryGetValue 
        /// with key
        /// Input : Valid values for DenseMatrix
        /// Validation : Try Get Value with key
        /// </summary>
        [Test]
        public void ValidateDenseMatrixTryGetValueKey()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            double tempVal = 0;
            Assert.IsTrue(denseMatObj.TryGetValue("R0", "C0", out tempVal));
            Assert.AreEqual(tempVal, denseMatObj["R0", "C0"]);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of TryGetValue(key) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of TryGetValue(key) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates Values 
        /// method
        /// Input : Valid values for DenseMatrix
        /// Validation : Values
        /// </summary>
        [Test]
        public void ValidateDenseMatrixValues()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            IEnumerable<double> enumList = denseMatObj.Values;
            StringBuilder strBuilder = new StringBuilder();
            foreach (double val in enumList)
            {
                strBuilder.Append(val.ToString());
            }
            string expectedValue =
                Utility._xmlUtil.GetTextValue(Constants.SimpleMatrixNodeName,
                Constants.ValueStringNode);
            Assert.AreEqual(expectedValue, strBuilder.ToString());

            Console.WriteLine(
                "DenseMatrix BVT : Validation of Values() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of Values() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates WriteDense 
        /// method
        /// Input : Valid values for DenseMatrix
        /// Validation : writes dense matrix
        /// </summary>
        [Test]
        public void ValidateDenseMatrixWriteDense()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            denseMatObj.WriteDense(Constants.FastQTempTxtFileName);

            string expectedFileValue =
                Utility._xmlUtil.GetTextValue(Constants.SimpleMatrixNodeName,
                Constants.DenseMatrixStringNode);

            ValidateWriteMethod(expectedFileValue);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of WriteDense() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of WriteDense() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates WriteDense 
        /// method
        /// Input : Valid values for DenseMatrix
        /// Validation : writes dense matrix
        /// </summary>
        [Test]
        public void ValidateDenseMatrixWriteDenseWriter()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            using (TextWriter writer = new StreamWriter(Constants.FastQTempTxtFileName))
            {
                denseMatObj.WriteDense(writer);
            }

            string expectedFileValue =
                Utility._xmlUtil.GetTextValue(Constants.SimpleMatrixNodeName,
                Constants.DenseMatrixStringNode);

            ValidateWriteMethod(expectedFileValue);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of WriteDense(Writer) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of WriteDense(Writer) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates WriteDense 
        /// method with T
        /// Input : Valid values for DenseMatrix
        /// Validation : writes dense matrix with T
        /// </summary>
        [Test]
        public void ValidateDenseMatrixWriteDenseT()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            denseMatObj.WriteDense<string, string, double>(Constants.FastQTempTxtFileName);

            string expectedFileValue =
                Utility._xmlUtil.GetTextValue(Constants.SimpleMatrixNodeName,
                Constants.DenseMatrixStringNode);

            ValidateWriteMethod(expectedFileValue);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of WriteDense<T>() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of WriteDense<T>() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates WriteDense Ansi
        /// method with filename
        /// Input : Valid values for DenseMatrix
        /// Validation : writes dense matrix ansi with filename
        /// </summary>
        [Test]
        public void ValidateDenseMatrixWriteDenseAnsi()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();
            ParallelOptions optObj = new ParallelOptions();

            denseMatObj.WriteDenseAnsi(
                Constants.FastQTempTxtFileName, optObj);

            string expectedFileValue =
                Utility._xmlUtil.GetTextValue(Constants.SimpleMatrixNodeName,
                Constants.DenseAnsiStringNode);

            ValidateWriteMethod(expectedFileValue);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of WriteDenseAnsi() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of WriteDenseAnsi() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates WriteDense Ansi
        /// method with writer
        /// Input : Valid values for DenseMatrix
        /// Validation : writes dense matrix ansi with writer
        /// </summary>
        [Test]
        public void ValidateDenseMatrixWriteDenseAnsiWriter()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();
            ParallelOptions optObj = new ParallelOptions();

            string expectedFileValue =
                Utility._xmlUtil.GetTextValue(Constants.SimpleMatrixNodeName,
                Constants.DenseAnsiStringNode);

            using (TextWriter writer = new StreamWriter(Constants.FastQTempTxtFileName))
            {
                denseMatObj.WriteDenseAnsi(writer, optObj);
            }

            ValidateWriteMethod(expectedFileValue);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of WriteDenseAnsi(writer) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of WriteDenseAnsi(writer) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates padded double
        /// method with filename
        /// Input : Valid values for DenseMatrix
        /// Validation : writes padded double with filename
        /// </summary>
        [Test]
        public void ValidateDenseMatrixWritePaddedDouble()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();
            ParallelOptions optObj = new ParallelOptions();

            denseMatObj.WritePaddedDouble(
                Constants.FastQTempTxtFileName, optObj);

            string expectedFileValue =
                Utility._xmlUtil.GetTextValue(Constants.SimpleMatrixNodeName,
                Constants.DenseMatrixStringNode);

            ValidateWriteMethod(expectedFileValue);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of WritePaddedDouble() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of WritePaddedDouble() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates Write padded double
        /// method with writer
        /// Input : Valid values for DenseMatrix
        /// Validation : writes padded double with writer
        /// </summary>
        [Test]
        public void ValidateDenseMatrixWritePaddedDoubleWriter()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();
            ParallelOptions optObj = new ParallelOptions();

            string expectedFileValue =
                Utility._xmlUtil.GetTextValue(Constants.SimpleMatrixNodeName,
                Constants.DenseMatrixStringNode);

            using (TextWriter writer = new StreamWriter(Constants.FastQTempTxtFileName))
            {
                denseMatObj.WritePaddedDouble(writer, optObj);
            }

            ValidateWriteMethod(expectedFileValue);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of WritePaddedDouble(writer) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of WritePaddedDouble(writer) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates write Sparse
        /// method with filename
        /// Input : Valid values for DenseMatrix
        /// Validation : write Sparse with filename
        /// </summary>
        [Test]
        public void ValidateDenseMatrixWriteSparse()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();
            denseMatObj.WriteSparse(
                Constants.FastQTempTxtFileName);

            string expectedFileValue =
                Utility._xmlUtil.GetTextValue(Constants.SimpleMatrixNodeName,
                Constants.SparseMatrixStringNode);

            ValidateWriteMethod(expectedFileValue);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of WriteDenseAnsi() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of WriteDenseAnsi() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates Write sparse
        /// method with writer
        /// Input : Valid values for DenseMatrix
        /// Validation : writes sparse with writer
        /// </summary>
        [Test]
        public void ValidateDenseMatrixWriteSparseWriter()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();
            string expectedFileValue =
                Utility._xmlUtil.GetTextValue(Constants.SimpleMatrixNodeName,
                Constants.SparseMatrixStringNode);

            using (TextWriter writer = new StreamWriter(Constants.FastQTempTxtFileName))
            {
                denseMatObj.WriteSparse(writer);
            }

            ValidateWriteMethod(expectedFileValue);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of WriteDense(writer) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of WriteDense(writer) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates write Sparse
        /// method with filename
        /// Input : Valid values for DenseMatrix
        /// Validation : write Sparse with filename
        /// </summary>
        [Test]
        public void ValidateDenseMatrixWriteSparseT()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();
            denseMatObj.WriteSparse<string, string, double>(
                Constants.FastQTempTxtFileName);

            string expectedFileValue =
                Utility._xmlUtil.GetTextValue(Constants.SimpleMatrixNodeName,
                Constants.SparseMatrixStringNode);

            ValidateWriteMethod(expectedFileValue);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of WriteSparse<T>() method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of WriteSparse<T>() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix and validates Write sparse
        /// method with writer
        /// Input : Valid values for DenseMatrix
        /// Validation : writes sparse with writer
        /// </summary>
        [Test]
        public void ValidateDenseMatrixWriteSparseWriterT()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();
            string expectedFileValue =
                Utility._xmlUtil.GetTextValue(Constants.SimpleMatrixNodeName,
                Constants.SparseMatrixStringNode);

            using (TextWriter writer =
                new StreamWriter(Constants.FastQTempTxtFileName))
            {
                denseMatObj.WriteSparse<string, string, double>(writer);
            }

            ValidateWriteMethod(expectedFileValue);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of WriteSparse<T>(writer) method successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of WriteSparse<T>(writer) method successful");
        }

        /// <summary>
        /// Validates all properties
        /// Input : Valid values for DenseMatrix
        /// Validation : All properties
        /// </summary>
        [Test]
        public void ValidateDenseMatrixAllProperties()
        {
            DenseMatrix<string, string, double> denseMatObj =
                CreateSimpleDenseMatrix();

            ReadOnlyCollection<string> colKeys = denseMatObj.ColKeys;
            ReadOnlyCollection<string> rowKeys = denseMatObj.RowKeys;
            IDictionary<string, int> indexCol = denseMatObj.IndexOfColKey;
            IDictionary<string, int> indexRow = denseMatObj.IndexOfRowKey;
            double[,] valArrayObj = denseMatObj.ValueArray;

            Assert.AreEqual("C0", denseMatObj.ColKeys[0].ToString());
            Assert.AreEqual("4", denseMatObj.ColCount.ToString());
            Assert.AreEqual("C0", colKeys[0].ToString());
            Assert.AreEqual("0", indexCol["C0"].ToString());
            Assert.AreEqual("0", indexRow["R0"].ToString());
            Assert.AreEqual("NaN", denseMatObj.MissingValue.ToString());
            Assert.AreEqual("3", denseMatObj.RowCount.ToString());
            Assert.AreEqual("R0", rowKeys[0].ToString());
            Assert.AreEqual("1", valArrayObj[0, 0].ToString());

            Console.WriteLine(
                "DenseMatrix BVT : Validation of all properties successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of all properties successful");
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gets the two D array from the xml
        /// </summary>
        /// <param name="nodeName">Node Name of the xml to be parsed</param>
        /// <param name="maxRows">Maximum rows</param>
        /// <param name="maxColumns">Maximum columns</param>
        /// <returns>2 D Array</returns>
        static double[,] GetTwoDArray(string nodeName, out int maxRows,
            out int maxColumns)
        {
            string[] rowArray = Utility._xmlUtil.GetTextValues(nodeName, Constants.RowsNode);

            // Gets the max number columns in the array
            maxColumns = 0;
            maxRows = rowArray.Length;
            for (int i = 0; i < maxRows; i++)
            {
                string[] colArray = rowArray[i].Split(',');
                if (maxColumns < colArray.Length)
                    maxColumns = colArray.Length;
            }

            // Creates a 2 D with max row and column length
            double[,] twoDArray = new double[maxRows, maxColumns];
            for (int i = 0; i < maxRows; i++)
            {
                string[] colArray = rowArray[i].Split(',');
                for (int j = 0; j < colArray.Length; j++)
                {
                    twoDArray[i, j] = double.Parse(colArray[j]);
                }
            }

            return twoDArray;
        }

        /// <summary>
        /// Gets the key sequence with the max length specified
        /// </summary>
        /// <param name="maxKey">Max length of the key sequence</param>
        /// <param name="isRow">If Row, append R else append C</param>
        /// <returns>Key Sequence Array</returns>
        static string[] GetKeySequence(int maxKey, bool isRow)
        {
            string[] keySeq = new string[maxKey];
            string tempSeq = string.Empty;

            if (isRow)
                tempSeq = "R";
            else
                tempSeq = "C";

            for (int i = 0; i < maxKey; i++)
            {
                keySeq[i] = tempSeq + i.ToString();
            }

            return keySeq;
        }

        /// <summary>
        /// Validates all the DenseMatrix constructor related test cases
        /// </summary>
        /// <param name="isRef">Should ref parameter be passed as part of constructor</param>
        static void ValidateConstructorTestCases(bool isRef)
        {
            int maxColumns = 0;
            int maxRows = 0;

            double[,] twoDArray = GetTwoDArray(Constants.SimpleMatrixNodeName,
                out maxRows, out maxColumns);

            string[] rowKeySeq = GetKeySequence(maxRows, true);
            string[] colKeySeq = GetKeySequence(maxColumns, false);

            DenseMatrix<string, string, double> denseMatrixObj = null;

            // Update Constructor with Ref parameter if required
            if (isRef)
            {
                denseMatrixObj =
                    new DenseMatrix<string, string, double>(ref twoDArray, rowKeySeq,
                        colKeySeq, double.NaN);
            }
            else
            {
                denseMatrixObj =
                    new DenseMatrix<string, string, double>(twoDArray, rowKeySeq,
                        colKeySeq, double.NaN);
            }

            // Validation of all properties set in the constructor
            Assert.IsNotNull(denseMatrixObj);
            Assert.AreEqual(maxRows, denseMatrixObj.RowCount);
            Assert.AreEqual(maxColumns, denseMatrixObj.ColCount);
            Assert.AreEqual(rowKeySeq, denseMatrixObj.RowKeys);
            Assert.AreEqual(colKeySeq, denseMatrixObj.ColKeys);

            Console.WriteLine(
                "DenseMatrix BVT : Validation of DenseMatrix Constructor successful");
            ApplicationLog.WriteLine(
                "DenseMatrix BVT : Validation of DenseMatrix Constructor successful");
        }

        /// <summary>
        /// Creates a DenseMatrix instance and returns the same.
        /// </summary>
        /// <returns>DenseMatrix Instance</returns>
        static DenseMatrix<string, string, double> GetDenseMatrix()
        {
            int maxRows = 0;
            int maxColumns = 0;
            double[,] twoDArray = GetTwoDArray(Constants.SimpleMatrixNodeName,
                out maxRows, out maxColumns);

            string[] rowKeySeq = GetKeySequence(maxRows, true);
            string[] colKeySeq = GetKeySequence(maxColumns, false);

            DenseMatrix<string, string, double> denseMatrixObj =
                new DenseMatrix<string, string, double>(twoDArray, rowKeySeq,
                    colKeySeq, double.NaN);

            return denseMatrixObj;
        }

        /// <summary>
        /// Creates a simple matrix for local validation
        /// </summary>
        /// <returns>Dense Matrix</returns>
        static DenseMatrix<string, string, double> CreateSimpleDenseMatrix()
        {
            double[,] twoDArray = new double[,] { { 1, 1, 1, 1 }, { 2, 3, 4, 5 }, { 3, 4, double.NaN, 5 } };

            DenseMatrix<string, string, double> denseMatObj =
                new DenseMatrix<string, string, double>(twoDArray,
                    new string[] { "R0", "R1", "R2" }, new string[] { "C0", "C1", "C2", "C3" }, double.NaN);

            return denseMatObj;
        }

        /// <summary>
        /// Validates the test cases which uses Write method
        /// </summary>
        /// <param name="expectedFileValue">Expected file contents</param>
        static void ValidateWriteMethod(string expectedFileValue)
        {
            // Reads the file and validates the results
            string actualFileValue = string.Empty;
            using (TextReader rdrObj =
                new StreamReader(Constants.FastQTempTxtFileName))
            {
                actualFileValue =
                    rdrObj.ReadToEnd().Replace("\t",
                    "").Replace("\n", "").Replace("\r", "").Replace(" ", "");
            }

            Assert.AreEqual(expectedFileValue, actualFileValue);

            if (File.Exists(Constants.FastQTempTxtFileName))
                File.Delete(Constants.FastQTempTxtFileName);
        }

        #endregion;
    }

    /// <summary>
    /// Class created for implementing the Equality comparer class
    /// </summary>
    internal class TempEqualityComparer :
        EqualityComparer<RowKeyColKeyValue<string, string, double>>
    {
        public override bool Equals(RowKeyColKeyValue<string, string, double> x,
            RowKeyColKeyValue<string, string, double> y)
        {
            if (x.ColKey == y.ColKey
                && x.RowKey == y.RowKey
                && x.Value == y.Value)
                return true;
            else
                return false;
        }

        public override int GetHashCode(RowKeyColKeyValue<string, string, double> obj)
        {
            string str = obj.RowKey + obj.ColKey + obj.Value.ToString();
            return str.GetHashCode();
        }
    }
}
