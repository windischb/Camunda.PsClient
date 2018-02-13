using System.Management.Automation;
using doob.Camunda.Client;

namespace Camunda.PsClient
{

    [Cmdlet(VerbsCommon.New, "CamundaClient")]
    public class NewCamundaClient : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string RestApiUrl { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Global { get; set; }

        protected override void ProcessRecord()
        {

            var client = new CamundaClient(opts =>
            {
                opts.WithCamundaRestApiUrl(RestApiUrl);
            });

            if (Global) {
                PsClient.Global.CamundaClient = client;
            } else {
                WriteObject(client);
            }

        }
    }
}
