using System;
using System.Management.Automation;
using doob.Camunda.Client;

namespace Camunda.PsClient {

    [Cmdlet(VerbsCommon.New, "CamundaClient")]
    public class NewCamundaClient : PSCmdlet {
        [Parameter(Mandatory = true, Position = 0)]
        public string RestApiUrl { get; set; }

        [Parameter(Mandatory = false)]
        public Uri Proxy { get; set; }

        [Parameter(Mandatory = false)]
        public PSCredential ProxyCredential { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Global { get; set; }

        protected override void ProcessRecord() {

            var client = new CamundaClient(opts => {
                opts.WithCamundaRestApiUrl(RestApiUrl);
                opts.WithProxy(Proxy, ProxyCredential?.GetNetworkCredential());
                
            });

            if (Global) {
                PsClient.Global.CamundaClient = client;
            } else {
                WriteObject(client);
            }

        }
    }
}
