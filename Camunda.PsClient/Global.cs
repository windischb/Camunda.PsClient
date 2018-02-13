using System;
using doob.Camunda.Client;

namespace Camunda.PsClient
{
    internal static class Global
    {
        public static CamundaClient CamundaClient { get; set; }

        
    }

    internal static class GlobalHelpers {

        public static CamundaClient GetCamundaClient(CamundaClient client) {
            var cl = client ?? Global.CamundaClient;
            if(cl == null)
                throw new Exception("Can't find CamundaClient...");

            return cl;
        }

    }
}
