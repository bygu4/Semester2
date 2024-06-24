// <copyright file="ErrorCodes.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Routers;

/// <summary>
/// Program exit codes.
/// </summary>
public enum ErrorCodes
{
    /// <summary>
    /// Program executed successfully.
    /// </summary>
    Success = 0,

    /// <summary>
    /// Missing or excess input arguments.
    /// </summary>
    IncorrectArguments = 1,

    /// <summary>
    /// File was not found at the given path.
    /// </summary>
    FileNotfound = 2,

    /// <summary>
    /// Input file contains invalid data. Unable to create network configuration.
    /// </summary>
    InvalidData = 3,

    /// <summary>
    /// Unable to create connected configuration.
    /// </summary>
    NetworkIsNotConnected = 4,
}