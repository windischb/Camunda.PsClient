using System;
using System.Management.Automation;
using doob.Camunda.Client;
using doob.Camunda.Client.Deployment;

namespace Camunda.PsClient.Deployment
{

    [Cmdlet(VerbsCommon.Remove, "CamundaDeployment")]
    public class RemoveCamundaDeployment : PSCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public DeploymentObject[] Deployment { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Cascade { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter SkipCustomListeners { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter SkipIoMappings { get; set; }

       
        [Parameter(Mandatory = false)]
        public CamundaClient Client { get; set; }

        
        protected override void ProcessRecord() {
            foreach (var deploymentObject in Deployment) {
                var req = GlobalHelpers.GetCamundaClient(Client).Deployment.Delete(deploymentObject.Id, options => {
                    options.Cascade(Cascade)
                            .SkipCustomListeners(SkipCustomListeners)
                            .SkipIoMappings(SkipIoMappings);
                });

                var result = req.GetAwaiter().GetResult();

                if (!result) {
                    WriteError(new ErrorRecord(new Exception($"Error on deleting Deployment with Id '{deploymentObject.Id}'"), "0", ErrorCategory.ObjectNotFound, null));
                }
                    
            }

        }
    }
}
