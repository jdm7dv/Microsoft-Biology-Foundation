﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

using System.IO;

using MBF.Util.Logging;


using MBF.SimilarityMatrices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MBF.Tests
{
    /// <summary>
    /// Test the similarity matrices.
    /// </summary>
    [TestClass]
    public class SimilarityMatrixTests
    {

        /// <summary>
        /// Static constructor to open log and make other settings needed for test
        /// </summary>
        static SimilarityMatrixTests()
        {
            if (!ApplicationLog.Ready)
            {
                ApplicationLog.Open("MBF.Tests.log");
            }
        }

        /// <summary>
        /// Test the various similarity matrix constructors: standard, from file, from stream, custom and diagonal.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void SimilarityMatrixMultiTest()
        {
            string filename = @"TestUtils\SimilarityMatrices\SampleCustomMatrix.txt";
            Assert.IsTrue(File.Exists(filename));

            SimilarityMatrix blosum50 = new SimilarityMatrix(SimilarityMatrix.StandardSimilarityMatrix.Blosum50);
            SimilarityMatrix fromFile = new SimilarityMatrix(filename);
            SimilarityMatrix fromReader = null;
            using (TextReader reader = new StreamReader(filename))
            {
                fromReader = new SimilarityMatrix(reader);
            }
            // Custom matrix, clone BLOSUM50 and see if it works.
            int[][] customMatrixInput = (int[][])blosum50.Matrix.Clone();
            string symbolMap = "ARNDCQEGHILKMFPSTWYVBJZX*";
            SimilarityMatrix custom = new SimilarityMatrix(customMatrixInput, symbolMap, "custom matrix, should be BLOSUM50", MoleculeType.Protein);


            int[][] matrix = blosum50.Matrix;
            int[][] matrixFile = fromFile.Matrix;
            int[][] matrixReader = fromReader.Matrix;
            int[][] matrixCustom = custom.Matrix;

            int i, j;
            int iLength = matrix.GetLength(0);
            int jLength = matrix[0].GetLength(0);
            Assert.AreEqual(iLength, matrixFile.GetLength(0));
            Assert.AreEqual(jLength, matrixFile[0].GetLength(0));
            Assert.AreEqual(iLength, matrixReader.GetLength(0));
            Assert.AreEqual(jLength, matrixReader[0].GetLength(0));
            Assert.AreEqual(iLength, matrixCustom.GetLength(0));
            Assert.AreEqual(jLength, matrixCustom[0].GetLength(0));

            for (i = 0; i < iLength; i++)
            {
                for (j = 0; j < jLength; j++)
                {
                    int blosum50Value = blosum50.Matrix[i][j];
                    Assert.AreEqual(blosum50Value, matrixFile[i][j]);
                    Assert.AreEqual(blosum50Value, matrixReader[i][j]);
                    Assert.AreEqual(blosum50Value, matrixCustom[i][j]);
                }
            }

            // Diagonal
            int matchValue = 10;
            int mismatchValue = -5;
            DiagonalSimilarityMatrix dsm = new DiagonalSimilarityMatrix(matchValue, mismatchValue, MoleculeType.Protein);
            // There is no real matrix here, just a function that returns matchValue for i==j and otherwise mismatchValue.
            // Run it 25x25
            for (i = 0; i < 25; i++)
            {
                for (j = 0; j < 25; j++)
                {
                    if (i == j)
                    {
                        Assert.AreEqual(matchValue, dsm[i, j]);
                    }
                    else
                    {
                        Assert.AreEqual(mismatchValue, dsm[i, j]);
                    }
                }
            }
        }

        /// <summary>
        /// Read standard similarity matrices, confirm that they exist and can be read.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void StandardSimilarityMatrices()
        {
            SimilarityMatrix sm;

            sm = null;
            sm = new SimilarityMatrix(SimilarityMatrix.StandardSimilarityMatrix.Blosum45);
            Assert.IsNotNull(sm);

            sm = null;
            sm = new SimilarityMatrix(SimilarityMatrix.StandardSimilarityMatrix.Blosum50);
            Assert.IsNotNull(sm);

            sm = null;
            sm = new SimilarityMatrix(SimilarityMatrix.StandardSimilarityMatrix.Blosum62);
            Assert.IsNotNull(sm);

            sm = null;
            sm = new SimilarityMatrix(SimilarityMatrix.StandardSimilarityMatrix.Blosum80);
            Assert.IsNotNull(sm);

            sm = null;
            sm = new SimilarityMatrix(SimilarityMatrix.StandardSimilarityMatrix.Blosum90);
            Assert.IsNotNull(sm);

            sm = null;
            sm = new SimilarityMatrix(SimilarityMatrix.StandardSimilarityMatrix.Pam250);
            Assert.IsNotNull(sm);

            sm = null;
            sm = new SimilarityMatrix(SimilarityMatrix.StandardSimilarityMatrix.Pam30);
            Assert.IsNotNull(sm);

            sm = null;
            sm = new SimilarityMatrix(SimilarityMatrix.StandardSimilarityMatrix.Pam70);
            Assert.IsNotNull(sm);
        }

        /// <summary>
        /// Read a test matrix using encoding NcbiStdAA
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void NcbiStdAASimilarityMatrices()
        {
            string filename = @"TestUtils\SimilarityMatrices\TestNcbiStdAA.txt";

            SimilarityMatrix sm = new SimilarityMatrix(filename);
            Assert.IsNotNull(sm.Matrix);
        }

        /// <summary>
        /// Read a test matrix using encoding IupacNA
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void IupacNASimilarityMatrices()
        {
            string filename = @"TestUtils\SimilarityMatrices\TestIupacNA.txt";

            SimilarityMatrix sm = new SimilarityMatrix(filename);
            Assert.IsNotNull(sm.Matrix);
        }
    }
}