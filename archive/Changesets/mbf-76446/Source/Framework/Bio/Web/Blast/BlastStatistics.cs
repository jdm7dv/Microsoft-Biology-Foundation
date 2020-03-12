﻿// *********************************************************
// 
//     Copyright (c) Microsoft. All rights reserved.
//     This code is licensed under the Apache License, Version 2.0.
//     THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//     ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//     IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//     PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// 
// *********************************************************
namespace Bio.Web.Blast
{
    /// <summary>
    /// Container for the Statistics segment of the XML BLAST format.
    /// </summary>
    public class BlastStatistics
    {
        #region Constructors

        /// <summary>
        /// Default Constructor: Initializes an instance of class BlastStatistics
        /// </summary>
        public BlastStatistics()
        { }

        #endregion

        # region Properties

        /// <summary>
        /// The number of sequences in the iteration
        /// </summary>
        public int SequenceCount { get; set; }

        /// <summary>
        /// Database size, for correction
        /// </summary>
        public long DatabaseLength { get; set; }

        /// <summary>
        /// Effective HSP length
        /// </summary>
        public long HspLength { get; set; }

        /// <summary>
        /// Effective search space
        /// </summary>
        public double EffectiveSearchSpace { get; set; }

        /// <summary>
        /// Karlin-Altschul parameter K
        /// </summary>
        public double Kappa { get; set; }

        /// <summary>
        /// Karlin-Altschul parameter Lambda
        /// </summary>
        public double Lambda { get; set; }

        /// <summary>
        /// Karlin-Altschul parameter H
        /// </summary>
        public double Entropy { get; set; }

        #endregion
    }
}