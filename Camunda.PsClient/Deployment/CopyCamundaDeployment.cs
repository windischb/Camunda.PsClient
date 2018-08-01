using System.Management.Automation;
using doob.Camunda.Client;
using doob.Camunda.Client.Deployment;
using PSHelper.PowerShellStandard;

namespace Camunda.PsClient.Deployment {

    [Cmdlet(VerbsCommon.Copy, "CamundaDeployment")]
    public class CopyCamundaDeployment : PSCmdletAsync {

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public DeploymentObject[] Deployment { get; set; }

        [Parameter(Mandatory = false)]
        public string[] ResourceIds { get; set; } = new string[0];

        [Parameter(Mandatory = false)]
        public string[] ResourceNames { get; set; } = new string[0];

        [Parameter(Mandatory = false)]
        public string Source { get; set; }

        [Parameter(Mandatory = false)]
        public CamundaClient Client { get; set; }

        protected override void ProcessRecord() {

            foreach (var deploymentObject in Deployment) {
                var req = GlobalHelpers.GetCamundaClient(Client)
                    .Deployment.Redeploy(deploymentObject.Id,
                        builder => builder.WithResourceIds(ResourceIds)
                            .WithResourceNames(ResourceNames)
                            .WithSource(Source));

                var result = req.GetAwaiter().GetResult();
                WriteObject(result);
            }

        }
    }
}
