using System.Management.Automation;
using doob.Camunda.Client;

namespace Camunda.PsClient.ExternalTasks
{

    [Cmdlet(VerbsCommon.Unlock, "CamundaExternalTask")]
    public class UnlockCamundaExternalTask : PSCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string Id { get; set; }

        [Parameter(Mandatory = false)]
        public CamundaClient Client { get; set; }


        protected override void ProcessRecord()
        {
            var exTasks = GlobalHelpers.GetCamundaClient(Client).ExternalTask;
            var result = exTasks.Unlock(Id).GetAwaiter().GetResult();
        }

    }
}
