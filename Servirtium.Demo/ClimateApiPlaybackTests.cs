﻿using Servirtium.AspNetCore;
using Servirtium.Core;
using Servirtium.Core.Replay;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servirtium.Demo
{
    [Xunit.Collection("Servirtium Demo")]
    public class ClimateApiPlaybackTests : ClimateApiTests
    {
        internal override IEnumerable<(IServirtiumServer, ClimateApi)> GenerateTestServerClientPairs(string script)
        {
            var replayer = new MarkdownReplayer();
            replayer.LoadScriptFile($@"..\..\..\test_playbacks\{script}");
            yield return
            (
                AspNetCoreServirtiumServer.Default(replayer, ClimateApi.DEFAULT_SITE),
                new ClimateApi(new Uri("http://localhost:5000"))
            );
        }
    }
}
