﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagKit.Markup
{
    /// <summary>
    /// Specifies the state of the TagReader.
    /// </summary>
    public enum ReadState
    {
        // The Read method has not been called yet.
        Initial = 0,

        // Reading is in progress.
        Interactive = 1,

        // An error occurred that prevents the XmlReader from continuing.
        Error = 2,

        // The end of the stream has been reached successfully.
        EndOfFile = 3,

        // The Close method has been called and the XmlReader is closed.
        Closed = 4
    }
}
