// Copyright (c) Alexander Bugaev 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

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