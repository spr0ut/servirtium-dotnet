﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Servirtium.Core
{
    public interface IInteractionTransforms
    {
        IInteraction TransformClientRequestForRealService(IInteraction clientRequest) => clientRequest;

        IInteraction TransformRealServiceResponseForClient(IInteraction serviceResponse) => serviceResponse;
    }
}