using System;
using System.Collections.Generic;
using System.Management.Automation;
using doob.Camunda.Client;
using doob.Camunda.Client.Deployment;

namespace Camunda.PsClient.Deployment {

    [Cmdlet(VerbsCommon.Get, "CamundaResource")]
    public class GetCamundaResource : PSCmdlet {

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public DeploymentObject[] Deployment { get; set; }

        [Parameter(Mandatory = false)]
        public string ResourceId { get; set; }

        [Parameter(Mandatory = false)]
        public CamundaClient Client { get; set; }

        protected override void ProcessRecord() {

            foreach (var deploymentObject in Deployment) {
                if (String.IsNullOrWhiteSpace(ResourceId)) {
                    WriteObject(GetResource(deploymentObject.Id), true);
                } else {
                    WriteObject(GetResource(deploymentObject.Id), false);
                }
            }

        }

        private List<ResourceObject> GetResource(string deploymentId) {

            var req = GlobalHelpers.GetCamundaClient(Client)
                .Deployment.GetResources(deploymentId);

            return req.GetAwaiter().GetResult();

        }

        private ResourceObject GetResource(string deploymentId, string resourceId) {

            var req = GlobalHelpers.GetCamundaClient(Client)
                .Deployment.GetResource(deploymentId, resourceId);

            return req.GetAwaiter().GetResult();

        }
    }
}
